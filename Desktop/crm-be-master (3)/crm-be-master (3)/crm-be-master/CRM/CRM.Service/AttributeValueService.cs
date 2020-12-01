using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IAttributeValueService
    {
        IEnumerable<AttributeValue> GetAttributeValues();
        IEnumerable<AttributeValue> GetAttributeValues(Expression<Func<AttributeValue,bool>> where);
        AttributeValue GetAttributeValue(Expression<Func<AttributeValue, bool>> where);
        AttributeValue GetAttributeValue(Guid id);
        void CreateAttributeValue(AttributeValue attributeValue);
        void EditAttributeValue(AttributeValue attributeValue);
        void RemoveAttributeValue(Guid id);
        void RemoveAttributeValue(AttributeValue attributeValue);
        void SaveAttributeValue();
    }
    public class AttributeValueService : IAttributeValueService
    {
        private readonly IAttributeValueRepository _attributeValueRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AttributeValueService(IAttributeValueRepository attributeValueRepository, IUnitOfWork unitOfWork)
        {
            _attributeValueRepository = attributeValueRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AttributeValue> GetAttributeValues()
        {
            return _attributeValueRepository.GetAll();
        }

        public AttributeValue GetAttributeValue(Guid id)
        {
            return _attributeValueRepository.GetById(id);
        }

        public void CreateAttributeValue(AttributeValue attributeValue)
        {
            _attributeValueRepository.Add(attributeValue);
            _unitOfWork.Commit();
        }

        public void EditAttributeValue(AttributeValue attributeValue)
        {
            _attributeValueRepository.Update(attributeValue);
        }

        public void RemoveAttributeValue(Guid id)
        {
            var entity = _attributeValueRepository.GetById(id);
            _attributeValueRepository.Delete(entity);
        }

        public void SaveAttributeValue()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<AttributeValue> GetAttributeValues(Expression<Func<AttributeValue, bool>> where)
        {
            return _attributeValueRepository.GetMany(where);
        }

        public AttributeValue GetAttributeValue(Expression<Func<AttributeValue, bool>> where)
        {
            return _attributeValueRepository.Get(where);
        }

        public void RemoveAttributeValue(AttributeValue attributeValue)
        {
            _attributeValueRepository.Delete(attributeValue);
        }
    }
}
