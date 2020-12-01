using System;
using System.Collections.Generic;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IRoleService
    {
        IEnumerable<HsRole> GetRoles();
        HsRole GetRole(Guid id);
        void CreateRole(HsRole hsRole);
        void EditRole(HsRole hsRole);
        void RemoveRole(Guid id);
        void SaveRole();
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<HsRole> GetRoles()
        {
            return _roleRepository.GetAll();
        }

        public HsRole GetRole(Guid id)
        {
            return _roleRepository.GetById(id);
        }

        public void CreateRole(HsRole hsRole)
        {
            _roleRepository.Add(hsRole);
            _unitOfWork.Commit();
        }

        public void EditRole(HsRole hsRole)
        {
            _roleRepository.Update(hsRole);
        }

        public void RemoveRole(Guid id)
        {
            var entity = _roleRepository.GetById(id);
            _roleRepository.Delete(entity);
        }

        public void SaveRole()
        {
            _unitOfWork.Commit();
        }
    }
}

