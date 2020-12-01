
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;

namespace CRM.Service
{
    public interface IFormService
    {
        IEnumerable<Form> GetForms();
        IEnumerable<Form> GetForms(Expression<Func<Form, bool>> where);
        Form GetForm(Guid id);
        void CreateForm(Form form);
        void UpdateForm(Form form);
        void DeleteForm(Guid id);
        void SaveChange();
    }
    public class FormService : IFormService
    {
        private readonly IFormRepository _formRepository;
        private readonly IFormGroupRepository _formGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FormService(IFormRepository formRepository, IUnitOfWork unitOfWork, IFormGroupRepository formGroupRepository)
        {
            _formRepository = formRepository ;
            _formGroupRepository = formGroupRepository;
            _unitOfWork = unitOfWork ;
        }

        public void CreateForm(Form form)
        {
            _formRepository.Add(form);
        }

        public void UpdateForm(Form form)
        {
            _formRepository.Update(form);
        }

        public IEnumerable<Form> GetForms(Expression<Func<Form, bool>> where)
        {
            return _formRepository.GetMany(where);
        }

        public Form GetForm(Guid id)
        {
            return _formRepository.GetById(id);
        }

        public IEnumerable<Form> GetForms()
        {
            return _formRepository.GetAll();
        }

        public void DeleteForm(Guid id)
        {
            var form = _formRepository.GetById(id);
            form.IsDeleted = true;
            _formRepository.Update(form);
            foreach(var item in form.FormGroups)
            {
                item.IsDeleted = true;
                _formGroupRepository.Update(item);
            }
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}
