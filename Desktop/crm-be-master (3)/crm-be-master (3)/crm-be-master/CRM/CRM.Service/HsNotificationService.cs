using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface IHsNotificationService
    {
        IEnumerable<HsNotification> GetHsNotifications();
        IEnumerable<HsNotification> GetHsNotifications(Expression<Func<HsNotification, bool>> where);
        HsNotification GetHsNotification(Guid id);
        void CreateHsNotification(HsNotification HsNotification);
        void EditHsNotification(HsNotification HsNotification);
        void RemoveHsNotification(Guid id);
        void RemoveHsNotification(HsNotification HsNotification);
        void SaveHsNotification();
    }

    public class HsNotificationService : IHsNotificationService
    {
        private readonly IHsNotificationRepository _HsNotificationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HsNotificationService(IHsNotificationRepository HsNotificationRepository, IUnitOfWork unitOfWork)
        {
            this._HsNotificationRepository = HsNotificationRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateHsNotification(HsNotification HsNotification)
        {
            _HsNotificationRepository.Add(HsNotification);
        }

        public void EditHsNotification(HsNotification HsNotification)
        {
            var entity = _HsNotificationRepository.GetById(HsNotification.Id);
            entity = HsNotification;
            _HsNotificationRepository.Update(entity);
        }

        public HsNotification GetHsNotification(Guid id)
        {
            return _HsNotificationRepository.GetById(id);
        }

        public IEnumerable<HsNotification> GetHsNotifications()
        {
            return _HsNotificationRepository.GetAll();
        }

        public IEnumerable<HsNotification> GetHsNotifications(Expression<Func<HsNotification, bool>> where)
        {
            return _HsNotificationRepository.GetMany(where);
        }

        public void RemoveHsNotification(Guid id)
        {
            var entity = _HsNotificationRepository.GetById(id);
            _HsNotificationRepository.Delete(entity);
        }

        public void RemoveHsNotification(HsNotification HsNotification)
        {
            _HsNotificationRepository.Delete(HsNotification);
        }

        public void SaveHsNotification()
        {
            _unitOfWork.Commit();
        }
    }
}
