using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> where);
        Product GetProduct(Guid id);
        void CreateProduct(Product product);
        void EditProduct(Product product);
        void RemoveProduct(Guid id);
        void RemoveProduct(Product product);
        void SaveProduct();
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateProduct(Product product)
        {
            _productRepository.Add(product);
        }

        public void EditProduct(Product product)
        {
            var entity = _productRepository.GetById(product.Id);
            entity = product;
            _productRepository.Update(entity);
        }

        public Product GetProduct(Guid id)
        {
            return _productRepository.GetById(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetProducts(Expression<Func<Product, bool>> where)
        {
            return _productRepository.GetMany(where);
        }

        public void RemoveProduct(Guid id)
        {
            var entity = _productRepository.GetById(id);
            _productRepository.Delete(entity);
        }

        public void RemoveProduct(Product product)
        {
            product.IsDeleted = true;
            _productRepository.Update(product);
        }

        public void SaveProduct()
        {
            _unitOfWork.Commit();
        }
    }
}
