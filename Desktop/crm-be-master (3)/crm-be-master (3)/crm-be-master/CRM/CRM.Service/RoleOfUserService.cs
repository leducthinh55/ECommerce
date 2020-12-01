using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IRoleOfUserService
    {
        IEnumerable<HsRoleOfUser> GetRoleOfUsers();
        IEnumerable<HsRoleOfUser> GetRoleOfUsers(Expression<Func<HsRoleOfUser, bool>> where);
        HsRoleOfUser GetRoleOfUser(Guid id);
        void CreateRoleOfUser(HsRoleOfUser hsRoleOfUser);
        void EditRoleOfUser(HsRoleOfUser hsRoleOfUser);
        void RemoveRoleOfUser(Guid id);
        void SaveRoleOfUser();
    }
    public class RoleOfUserService : IRoleOfUserService
    {
        private readonly IRoleOfUserRepository _roleOfUserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleOfUserService(IRoleOfUserRepository roleOfUserRepository, IUnitOfWork unitOfWork)
        {
            _roleOfUserRepository = roleOfUserRepository;
            _unitOfWork = unitOfWork;
        }

        public RoleOfUserService()
        {
        }

        public IEnumerable<HsRoleOfUser> GetRoleOfUsers()
        {
            return _roleOfUserRepository.GetAll();
        }

        public HsRoleOfUser GetRoleOfUser(Guid id)
        {
            return _roleOfUserRepository.GetById(id);
        }

        public void CreateRoleOfUser(HsRoleOfUser hsRoleOfUser)
        {
            _roleOfUserRepository.Add(hsRoleOfUser);
        }

        public void EditRoleOfUser(HsRoleOfUser hsRoleOfUser)
        {
            _roleOfUserRepository.Update(hsRoleOfUser);
        }

        public void RemoveRoleOfUser(Guid id)
        {
            var roleOfUser = _roleOfUserRepository.GetById(id);
            _roleOfUserRepository.Delete(roleOfUser);
        }

        public void SaveRoleOfUser()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<HsRoleOfUser> GetRoleOfUsers(Expression<Func<HsRoleOfUser, bool>> where)
        {
            return _roleOfUserRepository.GetMany(where);
        }
    }
}
