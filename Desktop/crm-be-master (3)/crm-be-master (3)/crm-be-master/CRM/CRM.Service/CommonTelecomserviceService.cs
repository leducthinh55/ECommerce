using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface ICommonTelecomserviceService
    {
        IEnumerable<CommonTelecomservice> GetCommonTelecomservices();
        IEnumerable<CommonTelecomservice> GetCommonTelecomservices(Expression<Func<CommonTelecomservice, bool>> where);
        CommonTelecomservice GetCommonTelecomservice(Guid id);
        void CreateCommonTelecomservice(CommonTelecomservice CommonTelecomservice);
        void EditCommonTelecomservice(CommonTelecomservice CommonTelecomservice);
        void RemoveCommonTelecomservice(Guid id);
        void RemoveCommonTelecomservice(CommonTelecomservice CommonTelecomservice);
        void SaveCommonTelecomservice();
    }

    public class CommonTelecomserviceService : ICommonTelecomserviceService
    {
        private readonly ICommonTelecomserviceRepository _CommonTelecomserviceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommonTelecomserviceService(ICommonTelecomserviceRepository CommonTelecomserviceRepository, IUnitOfWork unitOfWork)
        {
            this._CommonTelecomserviceRepository = CommonTelecomserviceRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateCommonTelecomservice(CommonTelecomservice CommonTelecomservice)
        {
            _CommonTelecomserviceRepository.Add(CommonTelecomservice);
        }

        public void EditCommonTelecomservice(CommonTelecomservice CommonTelecomservice)
        {
            var entity = _CommonTelecomserviceRepository.GetById(CommonTelecomservice.Id);
            entity = CommonTelecomservice;
            _CommonTelecomserviceRepository.Update(entity);
        }

        public CommonTelecomservice GetCommonTelecomservice(Guid id)
        {
            return _CommonTelecomserviceRepository.GetById(id);
        }

        public IEnumerable<CommonTelecomservice> GetCommonTelecomservices()
        {
            return _CommonTelecomserviceRepository.GetAll();
        }

        public void RemoveCommonTelecomservice(Guid id)
        {
            var entity = _CommonTelecomserviceRepository.GetById(id);
            _CommonTelecomserviceRepository.Delete(entity);
        }

        public void SaveCommonTelecomservice()
        {
            _unitOfWork.Commit();
        }

        public void RemoveCommonTelecomservice(CommonTelecomservice CommonTelecomservice)
        {
            _CommonTelecomserviceRepository.Delete(CommonTelecomservice);
        }

        public IEnumerable<CommonTelecomservice> GetCommonTelecomservices(Expression<Func<CommonTelecomservice, bool>> where)
        {
            return _CommonTelecomserviceRepository.GetMany(where);
        }
    }
}
