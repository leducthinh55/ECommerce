
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IProductCategoryService
    {
        IEnumerable<ProductCategory> GetProductCategories();
        IEnumerable<ProductCategory> GetProductCategories(Expression<Func<ProductCategory,bool>> where);
        ProductCategory GetProductCategory(Expression<Func<ProductCategory, bool>> where);
        ProductCategory GetProductCategory(Guid id);
        void CreateProductCategory(ProductCategory productCategory);
        void UpdateProductCategory(ProductCategory productCategory);
        void DeleteProductCategory(Guid id);
        void DeleteProductCategory(ProductCategory productCategory);
        void SaveChange();
    }
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateProductCategory(ProductCategory productCategory)
        {
            _repository.Add(productCategory);
        }

        public void DeleteProductCategory(Guid id)
        {
            var ProductCategory = _repository.GetById(id);
            _repository.Delete(ProductCategory);
        }

        public void DeleteProductCategory(ProductCategory productCategory)
        {
            _repository.Delete(productCategory);
        }

        public ProductCategory GetProductCategory(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return _repository.GetAll();
        }

        public IEnumerable<ProductCategory> GetProductCategories(Expression<Func<ProductCategory, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void UpdateProductCategory(ProductCategory productCategory)
        {
            _repository.Update(productCategory);
        }

        public ProductCategory GetProductCategory(Expression<Func<ProductCategory, bool>> where)
        {
            return _repository.Get(where);
        }
    }
}
