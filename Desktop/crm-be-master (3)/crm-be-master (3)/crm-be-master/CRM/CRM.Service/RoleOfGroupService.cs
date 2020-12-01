using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{

    public interface IRoleOfGroupService
    {
        IEnumerable<HsRoleOfGroup> GetRoleOfGroups();
        IEnumerable<HsRoleOfGroup> GetRoleOfGroups(Expression<Func<HsRoleOfGroup, bool>> where);
        HsRoleOfGroup GetRoleOfGroup(Guid id);
        void CreateRoleOfGroup(HsRoleOfGroup hsRoleOfGroup);
        void EditRoleOfGroup(HsRoleOfGroup hsRoleOfGroup);
        void RemoveRoleOfGroup(Guid id);
        void SaveRoleOfGroup();
    }
    public class RoleOfGroupService : IRoleOfGroupService
    {
        private readonly IRoleOfGroupRepository _roleOfGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleOfGroupService(IRoleOfGroupRepository roleOfGroupRepository, IUnitOfWork unitOfWork)
        {
            _roleOfGroupRepository = roleOfGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<HsRoleOfGroup> GetRoleOfGroups()
        {
            return _roleOfGroupRepository.GetAll();
        }

        public HsRoleOfGroup GetRoleOfGroup(Guid id)
        {
            return _roleOfGroupRepository.GetById(id);
        }

        public void CreateRoleOfGroup(HsRoleOfGroup hsRoleOfGroup)
        {
            _roleOfGroupRepository.Add(hsRoleOfGroup);
            _unitOfWork.Commit();
        }

        public void EditRoleOfGroup(HsRoleOfGroup hsRoleOfGroup)
        {
            _roleOfGroupRepository.Update(hsRoleOfGroup);
        }

        public void RemoveRoleOfGroup(Guid id)
        {
            var entity = _roleOfGroupRepository.GetById(id);
            _roleOfGroupRepository.Delete(entity);
        }

        public void SaveRoleOfGroup()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<HsRoleOfGroup> GetRoleOfGroups(Expression<Func<HsRoleOfGroup, bool>> where)
        {
            return _roleOfGroupRepository.GetMany(where);
        }
    }
}
