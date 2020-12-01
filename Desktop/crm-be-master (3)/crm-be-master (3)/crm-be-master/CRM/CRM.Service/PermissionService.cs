using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IPermissionService
    {
        IEnumerable<HsPermission> GetPermissionsByUser(string userId);
        IEnumerable<string> GetUsersByPermission(Guid id);
        IEnumerable<HsPermission> GetPermissions();
        IEnumerable<HsPermission> GetPermissions(Expression<Func<HsPermission,bool>> where);
        HsPermission GetPermission(Guid id);
        void CreatePermission(HsPermission hsPermission);
        void EditPermission(HsPermission hsPermission);
        void RemovePermission(Guid id);
        void SavePermission();
    }
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRoleOfUserRepository _roleOfUserRepository;
        private readonly IGroupUserRepository _groupUserRepository;
        private readonly IRoleOfGroupRepository _roleOfGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPermissionOfRoleRepository _permissionOfRoleRepository;

        public PermissionService(IPermissionRepository permissionRepository, 
            IRoleOfUserRepository roleOfUserRepository, IGroupUserRepository groupUserRepository, 
            IRoleOfGroupRepository roleOfGroupRepository, IUnitOfWork unitOfWork,
            IPermissionOfRoleRepository permissionOfRoleRepository)
        {
            _permissionRepository = permissionRepository;
            _roleOfUserRepository = roleOfUserRepository;
            _groupUserRepository = groupUserRepository;
            _roleOfGroupRepository = roleOfGroupRepository;
            _permissionOfRoleRepository = permissionOfRoleRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<HsPermission> GetPermissions()
        {
            return _permissionRepository.GetAll();
        }

        public HsPermission GetPermission(Guid id)
        {
            return _permissionRepository.GetById(id);
        }

        public void CreatePermission(HsPermission hsPermission)
        {
            _permissionRepository.Add(hsPermission);
            _unitOfWork.Commit();
        }

        public void EditPermission(HsPermission hsPermission)
        {
            _permissionRepository.Update(hsPermission);
        }

        public void RemovePermission(Guid id)
        {
            var entity = _permissionRepository.GetById(id);
            _permissionRepository.Delete(entity);
        }

        public void SavePermission()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<HsPermission> GetPermissionsByUser(string userId)
        {
            List<HsPermission> permissions = new List<HsPermission>();
            var roles = _roleOfUserRepository.GetAll().Where(u => u.UserId.Equals(userId)).Select(u => u.Role).ToList();
            var groups = _groupUserRepository.GetAll().Where(u => u.UserId.Equals(userId)).Select(u => u.Group).ToList();
            foreach (var group in groups)
            {
                roles = roles.Union(group.Roles.Select(g => g.Role).ToList()).ToList();
            }
            foreach (var role in roles)
            {
                permissions = permissions.Union(role.Permissions.Select(r => r.Permission).ToList()).ToList();
            }
            return permissions;
        }
        public IEnumerable<String> GetUsersByPermission(Guid id)
        {
            List<string> result = new List<string>();
            var roles = _permissionOfRoleRepository.GetMany(_ => _.PermissionId.Equals(id)).Select(_ => _.RoleId).ToList();
            foreach (var roleId in roles)
            {
                var users = _roleOfUserRepository.GetMany(_ => _.RoleId.Equals(roleId)).Select(_ => _.UserId).ToList();
                result = result.Union(users).ToList();

                var groups = _roleOfGroupRepository.GetMany(_ => _.RoleId.Equals(roleId)).Select(_ => _.GroupId).ToList();
                foreach (var groupId in groups)
                {
                    var usersInGroup = _groupUserRepository.GetMany(_ => _.GroupId.Equals(groupId)).Select(_ => _.UserId).ToList();
                    result = result.Union(usersInGroup).ToList();
                }
            }
            return result;
        }

        public IEnumerable<HsPermission> GetPermissions(Expression<Func<HsPermission, bool>> where)
        {
            return _permissionRepository.GetMany(where);
        }
    }
}
