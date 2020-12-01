using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CRM.Service
{
    public interface ITransactionLogService
    {
        IEnumerable<TransactionLog> GetTransactionLogs();
        TransactionLog GetTransactionLog(Guid id);
        IEnumerable<TransactionLog> GetTransactionLogs(Guid entityId, string className);
        void CreateTransactionLog(TransactionLog hsTransactionLog);
        void EditTransactionLog(TransactionLog hsTransactionLog);
        void RemoveTransactionLog(Guid id);
        void RemoveTransactionLog(Expression<Func<TransactionLog, bool>> where);
        void SaveTransactionLog();
        void UpdateTransaction<T>(T oldObject, T newObject, string username);
        //void AdaptChange<T>(List<ChangeLog> changeLogs, T obj);
        void AdaptChange<T>(DateTime selectedDate, T obj, Guid entityId);
        bool IsTheSame<T>(T oldObj, T newObj);
    }
    public class TransactionLogService : ITransactionLogService
    {
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChangeLogService _changeLogService;
        public const string UPDATE = "update";
        public const string DELETE = "delete";
        public const string CREATE = "create";

        public TransactionLogService(ITransactionLogRepository transactionLogRepository,
            IUnitOfWork unitOfWork,
            IChangeLogService changeLogService
        )
        {
            _transactionLogRepository = transactionLogRepository;
            _unitOfWork = unitOfWork;
            _changeLogService = changeLogService;
        }

        public IEnumerable<TransactionLog> GetTransactionLogs()
        {
            return _transactionLogRepository.GetAll();
        }

        public TransactionLog GetTransactionLog(Guid id)
        {
            return _transactionLogRepository.GetById(id);
        }

        public IEnumerable<TransactionLog> GetTransactionLogs(Guid entityId, string className)
        {
            return _transactionLogRepository.GetMany(n => n.EntityId.Equals(entityId) && n.EntityName.Equals(className));
        }

        public void CreateTransactionLog(TransactionLog hsTransactionLog)
        {
            _transactionLogRepository.Add(hsTransactionLog);
            _unitOfWork.Commit();
        }

        public void EditTransactionLog(TransactionLog hsTransactionLog)
        {
            _transactionLogRepository.Update(hsTransactionLog);
        }

        public void RemoveTransactionLog(Guid id)
        {
            var entity = _transactionLogRepository.GetById(id);
            _transactionLogRepository.Delete(entity);
        }

        public void RemoveTransactionLog(Expression<Func<TransactionLog, bool>> @where)
        {
            _transactionLogRepository.Delete(where);
        }

        public void SaveTransactionLog()
        {
            _unitOfWork.Commit();
        }

        public void UpdateTransaction<T>(T oldObj, T newObj, string username)
        {
            // Get list of fields of Object
            var properties = oldObj != null ? GetProperties(oldObj) : GetProperties(newObj);

            // Create transaction log
            var entityId = GetObjectId(oldObj != null ? oldObj : newObj, properties);
            var transLog = new TransactionLog
            {
                EntityId = Guid.Parse(entityId),
                EntityName = typeof(T).ToString(),
                DateChanged = DateTime.Now,
                FunctionType = oldObj == null ? CREATE : (newObj == null ? DELETE : UPDATE),
                ByUser = username
            };
            CreateTransactionLog(transLog);

            // if DELETE then no need to create change logs
            if (transLog.FunctionType.Equals(DELETE)) return;

            // Get list of change logs
            var changeLogs = transLog.FunctionType.Equals(UPDATE)
                ? UpdateType(oldObj, newObj, properties, transLog.Id)
                : CreateType(newObj, properties, transLog.Id);

            AddChangeLog(changeLogs);
            SaveTransactionLog();
        }

        public void AdaptChange<T>(DateTime selectedDate, T obj, Guid entityId)
        {
            if (obj == null) return;
            var transactionLogs = GetTransactionLogs(entityId, typeof(T).ToString());
            transactionLogs = transactionLogs.Where(t => t.DateChanged > selectedDate && !t.FunctionType.Equals(TransactionLogService.CREATE)).OrderByDescending(t => t.DateChanged);
            foreach (var transactionLog in transactionLogs)
            {
                var properties = GetProperties(obj);
                var oldCustomer = new Customer();
                Type myType = typeof(T);
                foreach (var changeLog in transactionLog.ChangeLogs.ToList())
                {
                    PropertyInfo myPropInfo = myType.GetProperty(changeLog.PropertyName);
                    if (myPropInfo.PropertyType == typeof(int))
                    {
                        myPropInfo.SetValue(obj, Convert.ToInt32(changeLog.OldValue), null);
                    }
                    else if (myPropInfo.PropertyType == typeof(decimal))
                    {
                        myPropInfo.SetValue(obj, Convert.ToDecimal(changeLog.OldValue), null);
                    }
                    else if (myPropInfo.PropertyType == typeof(DateTime?) || (myPropInfo.PropertyType == typeof(DateTime)))
                    {
                        myPropInfo.SetValue(obj, Convert.ToDateTime(changeLog.OldValue), null);
                    }
                    else
                    {
                        myPropInfo.SetValue(obj, changeLog.OldValue, null);
                    }
                }
            }
        }

        public bool IsTheSame<T>(T oldObj, T newObj)
        {
            var rs = true;
            var properties = GetProperties(oldObj);
            foreach (var p in properties)
            {
                if (!isPrimitive(p)) continue;
                var oldValue = p.GetValue(oldObj);
                var newValue = p.GetValue(newObj);
                var checkType = oldValue != null ? oldValue : newValue;
                if(checkType != null)
                {
                    if ((newValue == null && oldValue != null)
                        || (newValue != null && oldValue == null)
                        || (!oldValue.Equals(newValue)))
                    {
                        //Khac
                        rs = false;
                        break;
                    }
                }
            }
            return rs;
        }

        #region UpdateTransLogUltilities    
        private List<ChangeLog> UpdateType<T>(T oldObj, T newObj, PropertyInfo[] properties, Guid transLogId)
        {
            var changeLogs = new List<ChangeLog>();

            foreach (var p in properties)
            {
                // Get type of Object and check primitive
                if (!isPrimitive(p)) continue;

                // Get old value and new value
                var oldValue = p.GetValue(oldObj);
                var newValue = p.GetValue(newObj);

                // If 2 value are equal => continue
                if (oldValue == null && newValue == null) continue;
                if (oldValue != null && oldValue.Equals(newValue)) continue;

                // Else, add to changeLogs
                var changeLog = new ChangeLog
                {
                    TransactionLogId = transLogId,
                    PropertyName = p.Name,
                    NewValue = newValue == null ? null : newValue.ToString(), //TODO : Tai sao a.Long de la "NULL" ma khong phai la null
                    OldValue = oldValue == null ? null : oldValue.ToString()
                };
                changeLogs.Add(changeLog);
            }
            
            return changeLogs;
        }
        private List<ChangeLog> CreateType<T>(T newObj, PropertyInfo[] properties, Guid transLogId)
        {
            var changeLogs = new List<ChangeLog>();

            foreach (var p in properties)
            {
                // Get type of Object and check primitive
                if (!isPrimitive(p)) continue;

                // Get new value
                var newValue = p.GetValue(newObj);

                // Else, add to changeLogs
                var changeLog = new ChangeLog
                {
                    TransactionLogId = transLogId,
                    PropertyName = p.Name,
                    NewValue = newValue == null ? "NULL" : newValue.ToString(),
                    OldValue = null
                };
                changeLogs.Add(changeLog);
            }
            return changeLogs;
        }

        private string GetObjectValue<T>(PropertyInfo propertyInfo, T obj)
        {
            string value;
            try
            {
                value = propertyInfo.GetValue(obj).ToString();
            }
            catch (Exception e)
            {
                value = e.Message;
            }
            return value;
        }

        private void AddChangeLog(List<ChangeLog> changeLogs)
        {
            foreach (var changeLog in changeLogs)
            {
                _changeLogService.CreateChangeLog(changeLog);
            }
            _changeLogService.SaveChangeLog();
        }
        private string GetObjectId<T>(T obj, PropertyInfo[] properties)
        {
            foreach (var p in properties)
            {
                if (p.Name.ToLower().Equals("id"))
                {
                    return p.GetValue(obj).ToString();
                }
            }

            return "";
        }
        private bool isPrimitive(PropertyInfo p)
        {
            var type = p.PropertyType;
            var typeName = type.Name;
            if (typeName.Equals("String"))
            {
                return true;
            }
            if (type.IsValueType)
            {
                return true;
            }

            return false;
        }
        private PropertyInfo[] GetProperties<T>(T obj)
        {
            return obj.GetType().GetProperties();
        }

        
        #endregion

    }
}

