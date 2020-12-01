using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IProductAttributeService
    {
        IEnumerable<ProductAttribute> GetAttributeServices();
        IEnumerable<ProductAttribute> GetAttributeServices(Expression<Func<ProductAttribute, bool>> where);
        ProductAttribute GetAttributeService(Guid id);
        void CreateAttributeService(ProductAttribute productAttribute);
        void UpdateAttributeService(ProductAttribute productAttribute);
        void DeleteAttributeService(ProductAttribute productAttribute);
        void SaveChange();
    }
    public class ProductAttributeService : IProductAttributeService
    {
        private readonly IProductAttributeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductAttributeService(IProductAttributeRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        

        public void CreateAttributeService(ProductAttribute productAttribute)
        {
            _repository.Add(productAttribute);
        }

        public void DeleteAttributeService(ProductAttribute productAttribute)
        {
            productAttribute.IsDeleted = true;
            _repository.Update(productAttribute);
        }

        public ProductAttribute GetAttributeService(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<ProductAttribute> GetAttributeServices()
        {
            return _repository.GetAll();
        }

        public IEnumerable<ProductAttribute> GetAttributeServices(Expression<Func<ProductAttribute, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void UpdateAttributeService(ProductAttribute productAttribute)
        {
            _repository.Update(productAttribute);
        }
    }
}
