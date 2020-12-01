using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Category> GetCategories(Expression<Func<Category, bool>> where);
        Category GetCategory(Guid id);
        void CreateCategory(Category category);
        void EditCategory(Category category);
        void RemoveCategory(Guid id);
        void RemoveCategory(Category category);
        void SaveCategory();
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this._categoryRepository = categoryRepository;  
            this._unitOfWork = unitOfWork;
        }

        public void CreateCategory(Category category)
        {
            _categoryRepository.Add(category);
        }

        public void EditCategory(Category category)
        {
            var entity = _categoryRepository.GetById(category.Id);
            entity = category;
            _categoryRepository.Update(entity);
        }

        public Category GetCategory(Guid id)
        {
            return _categoryRepository.GetById(id);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetAll();
        }

        public void RemoveCategory(Guid id)
        {
            var entity = _categoryRepository.GetById(id);
            _categoryRepository.Delete(entity);
        }

        public void SaveCategory()
        {
            _unitOfWork.Commit();
        }

        public void RemoveCategory(Category category)
        {
            category.IsDeleted = true;
            _categoryRepository.Update(category);
        }

        public IEnumerable<Category> GetCategories(Expression<Func<Category, bool>> where)
        {
            return _categoryRepository.GetMany(where);
        }
    }
}
