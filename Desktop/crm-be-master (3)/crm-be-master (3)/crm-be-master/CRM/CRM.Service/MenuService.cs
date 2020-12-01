using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface IMenuService
    {
        IEnumerable<Menu> GetMenus();
        IEnumerable<Menu> GetMenus(Expression<Func<Menu, bool>> where);
        Menu GetMenu(Guid id);
        void CreateMenu(Menu menu);
        void UpdateMenu(Menu menu);
        void DeleteMenu(Guid id);
        void SaveChanges();
    }
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public MenuService(IMenuRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateMenu(Menu menu)
        {
            _repository.Add(menu);
        }

        public void DeleteMenu(Guid id)
        {
            var menu = _repository.GetById(id);
            _repository.Delete(menu);
        }

        public Menu GetMenu(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Menu> GetMenus()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Menu> GetMenus(Expression<Func<Menu, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateMenu(Menu menu)
        {
            _repository.Update(menu);
        }
    }
}
