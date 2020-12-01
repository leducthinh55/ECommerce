using System;
using System.Collections.Generic;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;


namespace CRM.Service
{
    public interface IHsWorkFlowService
    {
        IEnumerable<HsWorkFlow> GetHsWorkFlows();
        HsWorkFlow GetHsWorkFlow(Guid id);
        void CreateHsWorkFlow(HsWorkFlow workFlow);
        void EditHsWorkFlow(HsWorkFlow workFlow);
        void RemoveHsWorkFlow(HsWorkFlow workFlow);
        void SaveHsWorkFlow();
    }
    public class HsWorkFlowService : IHsWorkFlowService
    {
        private readonly IHsWorkFlowRepository _workFlowRepository;
        private readonly IHsWorkFlowInstanceRepository _instanceRepository;
        private readonly IHsWorkFlowConnectionRepository _connectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionRepository _permissionRepository;

        public HsWorkFlowService(IHsWorkFlowRepository workFlowRepository, IHsWorkFlowInstanceRepository instanceRepository, IHsWorkFlowConnectionRepository connectionRepository, IUnitOfWork unitOfWork, IPermissionRepository permissionRepository)
        {
            _workFlowRepository = workFlowRepository;
            _instanceRepository = instanceRepository;
            _connectionRepository = connectionRepository;
            _unitOfWork = unitOfWork;
            _permissionRepository = permissionRepository;
        }

        public void CreateHsWorkFlow(HsWorkFlow workFlow)
        {
            _workFlowRepository.Add(workFlow);
        }

        public void EditHsWorkFlow(HsWorkFlow workFlow)
        {
            _workFlowRepository.Update(workFlow);
        }

        public HsWorkFlow GetHsWorkFlow(Guid id)
        {
            return _workFlowRepository.GetById(id);
        }

        public IEnumerable<HsWorkFlow> GetHsWorkFlows()
        {
            return _workFlowRepository.GetMany(_ => _.IsDeleted == false);
        }

        public void RemoveHsWorkFlow(HsWorkFlow workFlow)
        {
            workFlow.IsDeleted = true;
            _permissionRepository.GetById(workFlow.PermissionIdR.Value).IsDeleted = true;
            _permissionRepository.GetById(workFlow.PermissionIdW.Value).IsDeleted = true;
            _workFlowRepository.Update(workFlow);
            //Delete Instance
            var instances = workFlow.Instances;
            foreach(var instance in instances)
            {
                instance.IsDeleted = true;
                _permissionRepository.GetById(instance.PermissionIdR.Value).IsDeleted = true;
                _permissionRepository.GetById(instance.PermissionIdW.Value).IsDeleted = true;
                _permissionRepository.GetById(instance.PermissionIdNoti.Value).IsDeleted = true;
                _instanceRepository.Update(instance);
                //Delete Connection
                var connections = instance.ToInstances;
                foreach (var conn in connections)
                {
                    conn.IsDeleted = true;
                    _connectionRepository.Update(conn);
                }
            }
        }

        public void SaveHsWorkFlow()
        {
            _unitOfWork.Commit();
        }
    }
}
