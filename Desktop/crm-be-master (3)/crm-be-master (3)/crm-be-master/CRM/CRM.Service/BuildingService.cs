using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface IBuildingService
    {
        IEnumerable<Building> GetBuildings();
        IEnumerable<Building> GetBuildings(Expression<Func<Building, bool>> where);
        Building GetBuilding(Guid id);
        void CreateBuilding(Building Building, string username);
        void EditBuilding(Building Building, string username);
        void RemoveBuilding(Guid id, string username);
        void RemoveBuilding(Building Building, string username);
        void SaveBuilding();
    }

    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _BuildingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BuildingService(IBuildingRepository BuildingRepository, IUnitOfWork unitOfWork)
        {
            this._BuildingRepository = BuildingRepository;
            this._unitOfWork = unitOfWork;
        }

        public void CreateBuilding(Building Building, string username)
        {
            Building.UserCreated = username;
            Building.DateCreated = DateTime.Now;
            _BuildingRepository.Add(Building);
        }

        public void EditBuilding(Building Building, string username)
        {
            var entity = _BuildingRepository.GetById(Building.Id);
            entity = Building;
            entity.UserUpdated = username;
            entity.DateUpdated = DateTime.Now;
            _BuildingRepository.Update(entity);
        }

        public Building GetBuilding(Guid id)
        {
            return _BuildingRepository.GetById(id);
        }

        public IEnumerable<Building> GetBuildings()
        {
            return _BuildingRepository.GetAll();
        }

        public IEnumerable<Building> GetBuildings(Expression<Func<Building, bool>> where)
        {
            return _BuildingRepository.GetMany(where);
        }

        public void RemoveBuilding(Guid id, string username)
        {
            var entity = _BuildingRepository.GetById(id);
            entity.IsDeteled = true;
            EditBuilding(entity, username);
        }

        public void RemoveBuilding(Building Building, string username)
        {
            Building.IsDeteled = true;
            EditBuilding(Building, username);
        }

        public void SaveBuilding()
        {
            _unitOfWork.Commit();
        }
    }
}
