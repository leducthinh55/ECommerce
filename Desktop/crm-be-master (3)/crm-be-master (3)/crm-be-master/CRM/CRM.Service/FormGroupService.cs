using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface IFormGroupService
    {
        IEnumerable<FormGroup> GetFormGroups();
        IEnumerable<FormGroup> GetFormGroups(Expression<Func<FormGroup, bool>> where);
        FormGroup GetFormGroup(Guid id);
        void CreateFormGroup(FormGroup formGroup);
        void UpdateFormGroup(FormGroup formGroup);
        void DeleteFormGroup(Guid id);
        void DeleteFormGroup(Expression<Func<FormGroup, bool>> where);
        void SaveChange();
    }
    public class FormGroupService  : IFormGroupService
    {
        private readonly IFormGroupRepository _formGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FormGroupService(IFormGroupRepository formGroupRepository, IUnitOfWork unitOfWork)
        {
            _formGroupRepository = formGroupRepository ;
            _unitOfWork = unitOfWork;
        }

        public void CreateFormGroup(FormGroup formGroup)
        {
            _formGroupRepository.Add(formGroup);
        }

        public void DeleteFormGroup(Guid id)
        {
            var formGroup = _formGroupRepository.GetById(id);
            _formGroupRepository.Delete(formGroup);
        }

        public void DeleteFormGroup(Expression<Func<FormGroup, bool>> where)
        {
            _formGroupRepository.Delete(where);
        }

        public FormGroup GetFormGroup(Guid id)
        {
            return _formGroupRepository.GetById(id);
        }

        public IEnumerable<FormGroup> GetFormGroups()
        {
            return _formGroupRepository.GetAll();
        }

        public IEnumerable<FormGroup> GetFormGroups(Expression<Func<FormGroup, bool>> where)
        {
            return _formGroupRepository.GetMany(where);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void UpdateFormGroup(FormGroup formGroup)
        {
            _formGroupRepository.Update(formGroup);
        }
    }
}
