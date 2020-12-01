using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace CRM.Service
{
    public interface IHsWorkFlowInstanceService
    {
        IEnumerable<HsWorkFlowInstance> GetHsWorkFlowInstances();
        IEnumerable<HsWorkFlowInstance> GetHsWorkFlowInstances(Expression<Func<HsWorkFlowInstance, bool>> where);
        HsWorkFlowInstance GetHsWorkFlowInstance(Guid Id);
        HsWorkFlowInstance GetStartEvent(Guid workFlowId);
        ICollection<HsWorkFlowInstance> GetNextSteps(Guid id);
        ICollection<HsWorkFlowInstance> GetPreviousSteps(HsWorkFlowInstance parallelInstance);
        ICollection<HsTemplate> GetTemplates(Guid id);
        bool CheckRoute(HsWorkFlowInstance currentInstance, HsWorkFlowInstance nextInstance);

        void CreateHsWorkFlowInstance(HsWorkFlowInstance hsWorkFlowInstance);
        void UpdateHsWorkFlowInstance(HsWorkFlowInstance hsWorkFlowInstance);
        void DeleteHsWorkFlowInstance(HsWorkFlowInstance hsWorkFlowInstance);
        void SaveChange();
    }
    public class HsWorkFlowInstanceService : IHsWorkFlowInstanceService
    {
        private readonly IHsWorkFlowInstanceRepository _ihsWorkFlowInstanceRepository;
        private readonly IHsWorkFlowConnectionRepository _connectionRepository;
        private readonly IUnitOfWork _iunitOfWork;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionOfRoleRepository _permissionOfRoleRepository;

        public HsWorkFlowInstanceService(IHsWorkFlowInstanceRepository ihsWorkFlowInstanceRepository, IHsWorkFlowConnectionRepository connectionRepository, IUnitOfWork iunitOfWork, IPermissionRepository permissionRepository, IPermissionOfRoleRepository permissionOfRoleRepository)
        {
            _ihsWorkFlowInstanceRepository = ihsWorkFlowInstanceRepository;
            _connectionRepository = connectionRepository;
            _iunitOfWork = iunitOfWork;
            _permissionRepository = permissionRepository;
            _permissionOfRoleRepository = permissionOfRoleRepository;
        }

        public void CreateHsWorkFlowInstance(HsWorkFlowInstance hsWorkFlowInstance)
        {
            _ihsWorkFlowInstanceRepository.Add(hsWorkFlowInstance);
        }

        public void DeleteHsWorkFlowInstance(HsWorkFlowInstance hsWorkFlowInstance)
        {
            hsWorkFlowInstance.IsDeleted = true;
            var R = _permissionRepository.GetById(hsWorkFlowInstance.PermissionIdR.Value);
            var W = _permissionRepository.GetById(hsWorkFlowInstance.PermissionIdW.Value);
            var N = _permissionRepository.GetById(hsWorkFlowInstance.PermissionIdNoti.Value);
            R.IsDeleted = true;
            W.IsDeleted = true;
            N.IsDeleted = true;

            _permissionOfRoleRepository.Delete(_=>_.PermissionId.Equals(R.Id));
            _permissionOfRoleRepository.Delete(_ => _.PermissionId.Equals(W.Id));
            _permissionOfRoleRepository.Delete(_ => _.PermissionId.Equals(N.Id));

            _ihsWorkFlowInstanceRepository.Update(hsWorkFlowInstance);
            //Delete Connection
            var connections = _connectionRepository.GetMany(_ => _.ToInstanceId.Equals(hsWorkFlowInstance.Id) ||
                                                                  _.FromInstanceId.Equals(hsWorkFlowInstance.Id));
            foreach (var conn in connections)
            {
                conn.IsDeleted = true;
                _connectionRepository.Update(conn);
            }
        }

        public HsWorkFlowInstance GetHsWorkFlowInstance(Guid Id)
        {
            return _ihsWorkFlowInstanceRepository.GetById(Id);
        }

        public IEnumerable<HsWorkFlowInstance> GetHsWorkFlowInstances()
        {
            return _ihsWorkFlowInstanceRepository.GetMany(_ => !_.IsDeleted);
        }

        public IEnumerable<HsWorkFlowInstance> GetHsWorkFlowInstances(Expression<Func<HsWorkFlowInstance, bool>> where)
        {
            return _ihsWorkFlowInstanceRepository.GetMany(where);
        }


        public void UpdateHsWorkFlowInstance(HsWorkFlowInstance hsWorkFlowInstance)
        {
            _ihsWorkFlowInstanceRepository.Update(hsWorkFlowInstance);
        }

        public void SaveChange()
        {
            _iunitOfWork.Commit();
        }

        public HsWorkFlowInstance GetStartEvent(Guid workFlowId)
            => _ihsWorkFlowInstanceRepository.Get(e => !e.IsDeleted
                                                    && e.SubType.Equals("Start event")
                                                    && e.WorkFlowId.Equals(workFlowId));

        public ICollection<HsWorkFlowInstance> GetNextSteps(Guid id)
        {
            List<HsWorkFlowInstance> nextSteps = new List<HsWorkFlowInstance>();
            var instance = _ihsWorkFlowInstanceRepository.GetById(id);
            var _queue = instance.ToInstances.Where(i => !i.IsDeleted ).ToList();
            int begin = 0, end = _queue.Count - 1;
            while (begin <= end)
            {
                if (_queue[begin].ToInstance.Type.Equals("Activity"))
                {
                    nextSteps.Add(_queue[begin].ToInstance);
                }
                else if (_queue[begin].ToInstance.Type.Equals("Gateway"))
                {
                    if (_queue[begin].ToInstance.SubType.Equals("Exclusive"))
                    {
                        if (_queue[begin].ToInstance.ToInstances.Count(i => !i.IsDeleted) > 1)
                        {
                            nextSteps.Add(_queue[begin].ToInstance);
                        }//decision gateway
                    }
                    else if (_queue[begin].ToInstance.SubType.Equals("Exclusive Event Based"))
                    {

                    }
                    else if (_queue[begin].ToInstance.SubType.Equals("Event Based"))
                    {

                    }
                    else if (_queue[begin].ToInstance.SubType.Equals("Parallel"))
                    {
                        //code here
                        foreach (var item in _queue[begin].ToInstance.ToInstances.Where(i => i.IsDeleted == false))
                        {
                            _queue.Add(item);
                            end++;
                        }
                    }
                    else if (_queue[begin].ToInstance.SubType.Equals("Parallel Event Based"))
                    {

                    }
                    else if (_queue[begin].ToInstance.SubType.Equals("Inclusive"))
                    {
                        if (_queue[begin].ToInstance.ToInstances.Where(i => i.IsDeleted == false).Count() == 1)
                        {
                            _queue.Add(_queue[begin].ToInstance.ToInstances.First());
                            end++;
                        }//merge gateway
                    }
                    else if (_queue[begin].ToInstance.SubType.Equals("Complex"))
                    {

                    }
                }
                else if (_queue[begin].ToInstance.Type.Equals("Event"))
                {
                    if (_queue[begin].ToInstance.SubType.Equals("End event"))
                    {
                        nextSteps = new List<HsWorkFlowInstance>()
                        {
                            _queue[begin].ToInstance
                        };
                        return nextSteps;
                    }
                    else if (_queue[begin].ToInstance.SubType.Equals("Intermediate event"))
                    {

                    }
                }
                begin++;
            };

            return nextSteps;
        }

        public bool CheckRoute(HsWorkFlowInstance currentInstance, HsWorkFlowInstance nextInstance)
        {

            // return GetNextSteps(currentInstance.Id).Count(_ => _.Id.Equals(nextInstance.Id)) > 0;
            return true;
        }

        public ICollection<HsWorkFlowInstance> GetPreviousSteps(HsWorkFlowInstance parallelInstance)
        {
            List<HsWorkFlowConnection> _queue = parallelInstance.FromInstances.Where(i => i.IsDeleted == false).ToList();
            List<HsWorkFlowInstance> instances = new List<HsWorkFlowInstance>();
            int begin = 0, end = _queue.Count - 1;
            while (begin <= end)
            {
                if (_queue[begin].FromInstance.SubType.Equals("Task") || _queue[begin].FromInstance.SubType.Equals("Exclusive"))
                {
                    instances.Add(_queue[begin].FromInstance);
                }
                else
                {

                }
                begin++;
            }
            return instances;
        }

        public ICollection<HsTemplate> GetTemplates(Guid id) => _ihsWorkFlowInstanceRepository.GetById(id).Templates;
    }
}
