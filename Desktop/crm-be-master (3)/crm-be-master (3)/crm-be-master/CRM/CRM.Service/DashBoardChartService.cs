using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Service
{
    public interface IDashBoardChartService
    {
        IEnumerable<DashBoardChart> GetDashBoardCharts();
        IEnumerable<DashBoardChart> GetDashBoardCharts(Expression<Func<DashBoardChart, bool>> where);
        DashBoardChart GetDashBoardChart(Guid id);
        void CreateDashBoardChart(DashBoardChart dashBoardChart);
        void UpdateDashBoardChart(DashBoardChart dashBoardChart);
        void DeleteDashBoardChart(Guid id);
        void SaveChange();
    }
    public class DashBoardChartService : IDashBoardChartService
    {
        private readonly IDashBoardChartRepository _DashBoardChartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DashBoardChartService(IDashBoardChartRepository DashBoardChartRepository, IUnitOfWork unitOfWork)
        {
            _DashBoardChartRepository = DashBoardChartRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateDashBoardChart(DashBoardChart DashBoardChart)
        {
            _DashBoardChartRepository.Add(DashBoardChart);
        }

        public void UpdateDashBoardChart(DashBoardChart DashBoardChart)
        {
            _DashBoardChartRepository.Update(DashBoardChart);
        }

        public IEnumerable<DashBoardChart> GetDashBoardCharts(Expression<Func<DashBoardChart, bool>> where)
        {
            return _DashBoardChartRepository.GetMany(where);
        }

        public DashBoardChart GetDashBoardChart(Guid id)
        {
            return _DashBoardChartRepository.GetById(id);
        }

        public IEnumerable<DashBoardChart> GetDashBoardCharts()
        {
            return _DashBoardChartRepository.GetAll();
        }

        public void DeleteDashBoardChart(Guid id)
        {
            var DashBoardChart = _DashBoardChartRepository.GetById(id);
            _DashBoardChartRepository.Delete(DashBoardChart);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}
