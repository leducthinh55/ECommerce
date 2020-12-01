using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IGroupService
    {
        IEnumerable<HsGroup> GetGroups();
        HsGroup GetGroup(Guid id);
        void CreateGroup(HsGroup hsGroup);
        void EditGroup(HsGroup hsGroup);
        void RemoveGroup(Guid id);
        void SaveGroup();
    }
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GroupService(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }

        public GroupService()
        {
        }

        public IEnumerable<HsGroup> GetGroups()
        {
            return _groupRepository.GetAll();
        }

        public HsGroup GetGroup(Guid id)
        {
            return _groupRepository.GetById(id);
        }

        public void CreateGroup(HsGroup hsGroup)
        {
            _groupRepository.Add(hsGroup);
        }

        public void EditGroup(HsGroup hsGroup)
        {
            _groupRepository.Update(hsGroup);
        }

        public void RemoveGroup(Guid id)
        {
            var group = _groupRepository.GetById(id);
            _groupRepository.Delete(group);
        }

        public void SaveGroup()
        {
            _unitOfWork.Commit();
        }
    }
}
