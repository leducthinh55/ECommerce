using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IPermissionOfRoleService
    {
        IEnumerable<HsPermissionOfRole> GetPermissionOfRoles();
        IEnumerable<HsPermissionOfRole> GetPermissionOfRoles(Expression<Func<HsPermissionOfRole, bool>> where);
        HsPermissionOfRole GetPermissionOfRole(Guid id);
        void CreatePermissionOfRole(HsPermissionOfRole hsPermissionOfRole);
        void EditPermissionOfRole(HsPermissionOfRole hsPermissionOfRole);
        void RemovePermissionOfRole(Guid id);
        void SavePermissionOfRole();
    }
    public class PermissionOfRoleService : IPermissionOfRoleService
    {
        private readonly IPermissionOfRoleRepository _permissionOfRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PermissionOfRoleService(IPermissionOfRoleRepository permissionOfRoleRepository, IUnitOfWork unitOfWork)
        {
            _permissionOfRoleRepository = permissionOfRoleRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<HsPermissionOfRole> GetPermissionOfRoles()
        {
            return _permissionOfRoleRepository.GetAll();
        }

        public HsPermissionOfRole GetPermissionOfRole(Guid id)
        {
            return _permissionOfRoleRepository.GetById(id);
        }

        public void CreatePermissionOfRole(HsPermissionOfRole hsPermissionOfRole)
        {
            _permissionOfRoleRepository.Add(hsPermissionOfRole);
        }

        public void EditPermissionOfRole(HsPermissionOfRole hsPermissionOfRole)
        {
            _permissionOfRoleRepository.Update(hsPermissionOfRole);
        }

        public void RemovePermissionOfRole(Guid id)
        {
            var entity = _permissionOfRoleRepository.GetById(id);
            _permissionOfRoleRepository.Delete(entity);
        }

        public void SavePermissionOfRole()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<HsPermissionOfRole> GetPermissionOfRoles(Expression<Func<HsPermissionOfRole, bool>> where)
        {
            return _permissionOfRoleRepository.GetMany(where);
        }
    }
}
