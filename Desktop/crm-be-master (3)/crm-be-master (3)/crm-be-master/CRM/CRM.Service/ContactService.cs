using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface IContactService
    {
        IEnumerable<Contact> GetContacts();
        IEnumerable<Contact> GetContacts(Expression<Func<Contact, bool>> where);
        Contact GetContact(Guid id);
        void CreateContact(Contact Contact);
        void EditContact(Contact Contact);
        void RemoveContact(Guid id);
        void RemoveContact(Contact Contact);
        void SaveContact();
    }

    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IContactRepository ContactRepository, IUnitOfWork unitOfWork)
        {
            this._contactRepository = ContactRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateContact(Contact Contact)
        {
            _contactRepository.Add(Contact);
        }

        public void EditContact(Contact Contact)
        {
            var entity = _contactRepository.GetById(Contact.Id);
            entity = Contact;
            _contactRepository.Update(entity);
        }

        public Contact GetContact(Guid id)
        {
            return _contactRepository.GetById(id);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return _contactRepository.GetAll();
        }

        public void RemoveContact(Guid id)
        {
            var entity = _contactRepository.GetById(id);
            _contactRepository.Delete(entity);
        }

        public void SaveContact()
        {
            _unitOfWork.Commit();
        }

        public void RemoveContact(Contact Contact)
        {
            _contactRepository.Delete(Contact);
        }

        public IEnumerable<Contact> GetContacts(Expression<Func<Contact, bool>> where)
        {
            return _contactRepository.GetMany(where);
        }
    }
}
