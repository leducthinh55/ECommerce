using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IHsWorkFlowConnectionService
    {
        IEnumerable<HsWorkFlowConnection> GetHsWorkFlowConnections();
        IEnumerable<HsWorkFlowConnection> GetHsWorkFlowConnections(Expression<Func<HsWorkFlowConnection, bool>> where);
        HsWorkFlowConnection GetHsWorkFlowConnection(Guid id);
        void CreateHsWorkFlowConnection(HsWorkFlowConnection hsWorkFlowConnection);
        void UpdateHsWorkFlowConnection(HsWorkFlowConnection hsWorkFlowConnection);
        void DeleteHsWorkFlowConnection(HsWorkFlowConnection hsWorkFlowConnection);
        void SaveChange();

    }
    public class HsWorkFlowConnectionService : IHsWorkFlowConnectionService
    {
        private readonly IHsWorkFlowConnectionRepository _hsWorkFlowConnectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HsWorkFlowConnectionService(IHsWorkFlowConnectionRepository hsWorkFlowConnectionRepository, IUnitOfWork unitOfWork)
        {
            _hsWorkFlowConnectionRepository = hsWorkFlowConnectionRepository ;
            _unitOfWork = unitOfWork ;
        }

        public void CreateHsWorkFlowConnection(HsWorkFlowConnection hsWorkFlowConnection)
        {
            _hsWorkFlowConnectionRepository.Add(hsWorkFlowConnection);
        }

        public void DeleteHsWorkFlowConnection(HsWorkFlowConnection hsWorkFlowConnection)
        {
            hsWorkFlowConnection.IsDeleted = true;
            _hsWorkFlowConnectionRepository.Update(hsWorkFlowConnection);

        }

        public HsWorkFlowConnection GetHsWorkFlowConnection(Guid id)
        {
            return _hsWorkFlowConnectionRepository.GetById(id);
        }

        public IEnumerable<HsWorkFlowConnection> GetHsWorkFlowConnections()
        {
            return _hsWorkFlowConnectionRepository.GetMany(_=>_.IsDeleted == false);
        }

        public void UpdateHsWorkFlowConnection(HsWorkFlowConnection hsWorkFlowConnection)
        {
            _hsWorkFlowConnectionRepository.Update(hsWorkFlowConnection);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<HsWorkFlowConnection> GetHsWorkFlowConnections(Expression<Func<HsWorkFlowConnection, bool>> where)
        {
            return _hsWorkFlowConnectionRepository.GetMany(where);
        }
    }
}
