using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IIdentityCardService
    {
        IEnumerable<IdentityCard> GetIdentityCards();
        IdentityCard GetIdentityCard(Guid id);
        void CreateIdentityCard(IdentityCard hsIdentityCard);
        void EditIdentityCard(IdentityCard hsIdentityCard);
        void RemoveIdentityCard(Guid id);
        void RemoveIdentityCard(Expression<Func<IdentityCard, bool>> where);
        void SaveIdentityCard();
    }
    public class IdentityCardService : IIdentityCardService
    {
        private readonly IIdentityCardRepository _identityCardRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IdentityCardService(IIdentityCardRepository identityCardRepository, IUnitOfWork unitOfWork)
        {
            _identityCardRepository = identityCardRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<IdentityCard> GetIdentityCards()
        {
            return _identityCardRepository.GetAll();
        }

        public IdentityCard GetIdentityCard(Guid id)
        {
            return _identityCardRepository.GetById(id);
        }

        public void CreateIdentityCard(IdentityCard hsIdentityCard)
        {
            _identityCardRepository.Add(hsIdentityCard);
            _unitOfWork.Commit();
        }

        public void EditIdentityCard(IdentityCard hsIdentityCard)
        {
            _identityCardRepository.Update(hsIdentityCard);
        }

        public void RemoveIdentityCard(Guid id)
        {
            var entity = _identityCardRepository.GetById(id);
            _identityCardRepository.Delete(entity);
        }

        public void RemoveIdentityCard(Expression<Func<IdentityCard, bool>> @where)
        {
            _identityCardRepository.Delete(where);
        }

        public void SaveIdentityCard()
        {
            _unitOfWork.Commit();
        }
    }
}

