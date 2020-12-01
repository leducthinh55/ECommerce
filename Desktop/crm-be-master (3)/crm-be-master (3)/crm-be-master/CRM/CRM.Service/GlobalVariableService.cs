using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface IGlobalVariableService
    {
        IEnumerable<GlobalVariable> GetGlobalVariables(Expression<Func<GlobalVariable,bool>> where);
        GlobalVariable GetGlobalVariable(Guid id);
        void CreateGlobalVariable(GlobalVariable globalVariable);
        void UpdateGlobalVariable(GlobalVariable globalVariable);
        void DeleteGlobalVariable(GlobalVariable globalVariable);
        void SaveChanges();
    }
    public class GlobalVariableService : IGlobalVariableService
    {
        private readonly IGlobalVariableRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public GlobalVariableService(IGlobalVariableRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void CreateGlobalVariable(GlobalVariable globalVariable)
        {
            _repository.Add(globalVariable);
        }

        public void DeleteGlobalVariable(GlobalVariable globalVariable)
        {
            _repository.Delete(globalVariable);
        }

        public GlobalVariable GetGlobalVariable(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<GlobalVariable> GetGlobalVariables(Expression<Func<GlobalVariable, bool>> where)
        {
            return _repository.GetMany(where);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateGlobalVariable(GlobalVariable globalVariable)
        {
            _repository.Update(globalVariable);
        }
    }
}
