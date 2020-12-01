using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace CRM.Service
{
    public interface IHubUserConnectionService
    {
        IEnumerable<HubUserConnection> GetHubUserConnections();
        IEnumerable<HubUserConnection> GetHubUserConnections(Expression<Func<HubUserConnection, bool>> where);
        HubUserConnection GetHubUserConnection(Guid id);
        void CreateHubUserConnection(HubUserConnection HubUserConnection);
        void EditHubUserConnection(HubUserConnection HubUserConnection);
        void RemoveHubUserConnection(Guid id);
        void RemoveHubUserConnection(Expression<Func<HubUserConnection, bool>> where);
        void SaveHubUserConnection();
    }

    public class HubUserConnectionService : IHubUserConnectionService
    {
        private readonly IHubUserConnectionRepository _HubUserConnectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HubUserConnectionService(IHubUserConnectionRepository HubUserConnectionRepository, IUnitOfWork unitOfWork)
        {
            this._HubUserConnectionRepository = HubUserConnectionRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateHubUserConnection(HubUserConnection HubUserConnection)
        {
            _HubUserConnectionRepository.Add(HubUserConnection);
        }

        public void EditHubUserConnection(HubUserConnection HubUserConnection)
        {
            var entity = _HubUserConnectionRepository.GetById(HubUserConnection.Id);
            entity = HubUserConnection;
            _HubUserConnectionRepository.Update(entity);
        }

        public HubUserConnection GetHubUserConnection(Guid id)
        {
            return _HubUserConnectionRepository.GetById(id);
        }

        public IEnumerable<HubUserConnection> GetHubUserConnections()
        {
            return _HubUserConnectionRepository.GetAll();
        }

        public IEnumerable<HubUserConnection> GetHubUserConnections(Expression<Func<HubUserConnection, bool>> where)
        {
            return _HubUserConnectionRepository.GetMany(where);
        }

        public void RemoveHubUserConnection(Guid id)
        {
            var entity = _HubUserConnectionRepository.GetById(id);
            _HubUserConnectionRepository.Delete(entity);
        }

        public void RemoveHubUserConnection(Expression<Func<HubUserConnection, bool>> where)
        {
            _HubUserConnectionRepository.Delete(where);
        }

        public void SaveHubUserConnection()
        {
            _unitOfWork.Commit();
        }
    }
}
