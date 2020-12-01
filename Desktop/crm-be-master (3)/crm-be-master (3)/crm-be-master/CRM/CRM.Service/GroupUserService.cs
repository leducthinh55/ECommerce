using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{

    public interface IGroupUserService
    {
        IEnumerable<HsGroupUser> GetGroupUsers();
        IEnumerable<HsGroupUser> GetGroupUsers(Expression<Func<HsGroupUser, bool>> where);
        HsGroupUser GetGroupUser(Guid id);
        void CreateGroupUser(HsGroupUser hsGroupUser);
        void EditGroupUser(HsGroupUser hsGroupUser);
        void RemoveGroupUser(Guid id);
        void SaveGroupUser();
    }
    public class GroupUserService : IGroupUserService
    {
        private readonly IGroupUserRepository _groupUserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GroupUserService(IGroupUserRepository groupUserRepository, IUnitOfWork unitOfWork)
        {
            _groupUserRepository = groupUserRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<HsGroupUser> GetGroupUsers()
        {
            return _groupUserRepository.GetAll();
        }

        public HsGroupUser GetGroupUser(Guid id)
        {
            return _groupUserRepository.GetById(id);
        }

        public void CreateGroupUser(HsGroupUser hsGroupUser)
        {
            _groupUserRepository.Add(hsGroupUser);
            _unitOfWork.Commit();
        }

        public void EditGroupUser(HsGroupUser hsGroupUser)
        {
            _groupUserRepository.Update(hsGroupUser);
        }

        public void RemoveGroupUser(Guid id)
        {
            var entity = _groupUserRepository.GetById(id);
            _groupUserRepository.Delete(entity);
        }

        public void SaveGroupUser()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<HsGroupUser> GetGroupUsers(Expression<Func<HsGroupUser, bool>> where)
        {
            return _groupUserRepository.GetMany(where);
        }
    }
}
