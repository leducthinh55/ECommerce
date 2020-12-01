using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.Service;
using CRM.Service.Utils;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly IContractService _contractService;
        private readonly IContractTelecomService _contractTelecomService;
        private static readonly int maxDayInMonth = 30;

        public RevenueController(IContractService _contractService, IContractTelecomService _contractTelecomService)
        {
            this._contractService = _contractService;
            this._contractTelecomService = _contractTelecomService;
        }

        //private double CalculatingPriceByMonth(Contract contract, int month)
        //{
        //    double result = 0;

        //    return result;
        //}
        public enum ChangeType
        {
            StartDateRentChange,
            StartDateServiceChange,
            ContractAppendicesChange,
        }
        private List<int> ListChange(DateTime currentMonth, DateTime? startDateRent, DateTime? startDateService, List<ContractAppendix> contractAppendicesByMonth)
        {
            var result = new List<int>();
            if (startDateRent != null && currentMonth.Month == startDateRent.Value.Month && currentMonth.Year == startDateRent.Value.Year) result.Add((int)ChangeType.StartDateRentChange);
            if (startDateService != null && currentMonth.Month == startDateService.Value.Month && currentMonth.Year == startDateService.Value.Year) result.Add((int)ChangeType.StartDateServiceChange);
            if (contractAppendicesByMonth.Count != 0) result.Add((int)ChangeType.ContractAppendicesChange);
            return result;
        }
        private decimal CalculateRevenue(int dayOfRent, int dayOfService, decimal currentUnitPrice, decimal currentUnitServicePrice, double currentSquare)
        {
            var result = ((currentUnitPrice / 30 * dayOfRent) + (currentUnitServicePrice / 30 * dayOfService)) * (decimal)currentSquare;
            return result;
        }

        //[HttpGet]
        //public ActionResult Get(int year = 0, Guid? contractId = null)
        //{
        //    List<RevenueVM> result = new List<RevenueVM>();
        //    var current = DateTime.Now;
        //    current.AddYears(year - current.Year );

        //    var contracts = _contractService.GetContracts();

        //    if (contractId != null) contracts = contracts.Where(_ => _.Id == contractId).ToList();
        //    foreach (var contract in contracts)
        //    {
        //        var revenueVM = contract.Adapt<RevenueVM>();
        //        decimal currentUnitPrice = _contractService.GetPriceByDate(contract,new DateTime(current.Year,01,01));
        //        decimal currentUnitServicePrice = _contractService.GetServicePriceByDate(contract, new DateTime(current.Year, 01, 01));
        //        double currentSquare = _contractService.GetSquareByDate(contract, new DateTime(current.Year, 01, 01));
        //        var contractAppendices = contract.ContractAppendices.AsQueryable();
        //        revenueVM.CurrentSquare = _contractService.GetSquareByDate(contract, current);

        //        //calculating Current Square depend on appendix
        //        for (var i = 1; i <= 12; i++)
        //        {
        //            var monthData = new MonthData();
        //            var _contractAppendicesByMonth = contractAppendices.Where(appendix => appendix.DateStart.Month == i && appendix.DateStart.Year == current.Year);
        //            int dayOfRent = 30;
        //            int dayOfService = 30;
        //            decimal revenue = 0;
        //            var listChange = ListChange(new DateTime(current.Year, i, 01), contract.StartDateRent, contract.StartDateService, _contractAppendicesByMonth.ToList());
        //            if (listChange.Count != 0)
        //            {
        //                foreach (var change in listChange)
        //                {
        //                    switch (change)
        //                    {
        //                        case (int)ChangeType.StartDateRentChange:
        //                            dayOfRent = 30 - contract.StartDateRent.Day;
        //                            currentUnitPrice = _contractService.GetPriceByDate(contract, contract.StartDateRent);
        //                            currentSquare = _contractService.GetSquareByDate(contract, contract.StartDateRent);
        //                            break;
        //                        case (int)ChangeType.StartDateServiceChange:
        //                            dayOfService = 30 - contract.StartDateService.Value.Day;
        //                            currentUnitServicePrice = _contractService.GetServicePriceByDate(contract, contract.StartDateService.Value);
        //                            currentSquare = _contractService.GetSquareByDate(contract, contract.StartDateService.Value);
        //                            break;
        //                        case (int)ChangeType.ContractAppendicesChange:
        //                            var a = _contractService.GetRevenueByFollowingMonth(contract, _contractAppendicesByMonth, i, (decimal)currentSquare, currentUnitPrice);
        //                            revenue = a["revenue"];
        //                            currentUnitPrice = a["currentPrice"];
        //                            currentSquare = (double)a["currentSquare"];
        //                            break;
        //                    }
        //                }
        //                if (!listChange.Contains((int)ChangeType.ContractAppendicesChange))
        //                    revenue = CalculateRevenue(dayOfRent, dayOfService, currentUnitPrice, currentUnitServicePrice, currentSquare);
        //            }
        //            else
        //            {
        //                revenue = CalculateRevenue(dayOfRent,dayOfService,currentUnitPrice,currentUnitServicePrice,currentSquare);
        //            }

        //            monthData.TotalRevenue = revenue;
        //            monthData.Increase = current.Year > contract.StartDate.Value.Year ? 0 : revenue;
        //            revenueVM.MonthDatas.Add(monthData);
        //        }
        //        revenueVM.RevenueSeason1 = revenueVM.MonthDatas[0].TotalRevenue
        //                                    + revenueVM.MonthDatas[1].TotalRevenue
        //                                    + revenueVM.MonthDatas[2].TotalRevenue;
        //        revenueVM.RevenueSeason2 = revenueVM.MonthDatas[3].TotalRevenue
        //                                    + revenueVM.MonthDatas[4].TotalRevenue
        //                                    + revenueVM.MonthDatas[5].TotalRevenue;
        //        revenueVM.RevenueSeason3 = revenueVM.MonthDatas[6].TotalRevenue
        //                                    + revenueVM.MonthDatas[7].TotalRevenue
        //                                    + revenueVM.MonthDatas[8].TotalRevenue;
        //        revenueVM.RevenueSeason4 = revenueVM.MonthDatas[9].TotalRevenue
        //                                    + revenueVM.MonthDatas[10].TotalRevenue
        //                                    + revenueVM.MonthDatas[11].TotalRevenue;
        //        revenueVM.RevenueYear = revenueVM.RevenueSeason1 + revenueVM.RevenueSeason2 + revenueVM.RevenueSeason3 + revenueVM.RevenueSeason4;
        //        result.Add(revenueVM);
        //    }
        //    return Ok(result);
        //}
        private decimal Cal(decimal SR, decimal SS, decimal PR, decimal PS, int period)
        {
            if (period <= 0) return 0;
            return SR * PR / 30 * period + SS * PS / 30 * period;
            // gia thue*dien tich + gia dich vu* dien tich
            // period la so ngay khi thue ko tron thang
        }

        //[HttpGet("Recode")]
        //public ActionResult GetReCode(int year = 2020, Guid? contractId = null)
        //{
        //    List<RevenueVM> result = new List<RevenueVM>();
        //    var contracts = _contractService.GetContracts();
        //    if (contractId != null) contracts = contracts.Where(_ => _.Id == contractId).ToList();
        //    var yearCal = DateTime.Now;
        //    yearCal = yearCal.AddYears(year - yearCal.Year);
        //    var yearBegin = new DateTime(yearCal.Year, 1, 1);
        //    var yearEnd = new DateTime(yearCal.Year, 12, 30);

        //    foreach (var contract in contracts)
        //    {
        //        var revenueVM = contract.Adapt<RevenueVM>();
        //        List<ChangePoint> changePoints = new List<ChangePoint>();
        //        for (int i = 0; i < 4; i++)
        //        {
        //            var appendixs = contract.ContractAppendices
        //                .Where(_ => _.DateStart.Date <= yearEnd.Date
        //                && (_.DateEnd == null || _.DateEnd.Value.Date >= yearBegin.Date)
        //                && (_.DateEnd == null || _.DateEnd.Value.Date >= _.DateStart.Date)
        //                && _.Key == i.ToString()
        //                ).ToList();
        //            if (appendixs.Count == 0) break;

        //            foreach (var appendix in appendixs)
        //            {
        //                DateTime dateStart = appendix.DateStart.Date >= yearBegin.Date ? appendix.DateStart.Date : yearBegin.Date;
        //                DateTime dateEnd = (appendix.DateEnd != null && appendix.DateEnd.Value.Date <= yearEnd.Date)
        //                                    ? appendix.DateEnd.Value.Date : yearEnd;
        //                decimal value = 0;
        //                switch (appendix.Type)
        //                {
        //                    case (int)ContractAppendixType.PriceRent:
        //                        value = appendix.UnitPrice.Value;
        //                        break;
        //                    case (int)ContractAppendixType.PriceService:
        //                        value = appendix.UnitServicePrice.Value;
        //                        break;
        //                    case (int)ContractAppendixType.AreaRent:
        //                    case (int)ContractAppendixType.AreaService:
        //                        value = (decimal)appendix.Square.Value;
        //                        break;
        //                    default:
        //                        return BadRequest(
        //                   appendix.Adapt<ContractAppendixVM>());
        //                }
        //                ChangePoint openPoint = new ChangePoint
        //                {
        //                    DateChange = dateStart,
        //                    IsOpen = true,
        //                    Type = appendix.Type,
        //                    Value = value,
        //                    Ground = i
        //                };
        //                ChangePoint closePoint = new ChangePoint
        //                {
        //                    DateChange = dateEnd.AddDays(1),
        //                    IsOpen = false,
        //                    Type = appendix.Type,
        //                    Value = value,
        //                    Ground = i
        //                };
        //                changePoints.Add(openPoint);
        //                if (closePoint.DateChange.Date < yearEnd.Date)
        //                {
        //                    changePoints.Add(closePoint);
        //                }


        //            }


        //        }

        //        changePoints = changePoints.OrderBy(_ => _.Ground).OrderBy(_ => _.DateChange).ThenBy(_ => _.IsOpen).ToList();

        //        int changePointCurrent = 0;
        //        decimal SR = 0, SS = 0;
        //        decimal PR = 0, PS = 0;
        //        decimal PreSR = 0, PreSS = 0;
        //        decimal PrePR = 0, PrePS = 0;
        //        for (var month = 1; month <= 12; month++)
        //        {
        //            decimal RevenueInMonth = 0;
        //            for (int i = 0; i < 4; i++)
        //            {
        //                if (i > changePoints.LastOrDefault().Ground) break;
        //                int currentDay = 1;
        //                int preDay = 1;
        //                while (currentDay <= 30)
        //                {
        //                    var point = changePointCurrent >= changePoints.Count ? null : changePoints[changePointCurrent];
        //                    if (point != null && point.DateChange.Month == month && point.Ground == i)
        //                    {
        //                        update - currentday

        //                        while (point != null && point.DateChange.Month == month && point.DateChange.Day == currentDay && point.Ground == i)
        //                        {
        //                            switch (point.Type)
        //                            {
        //                                case (int)ContractAppendixType.PriceRent:
        //                                    PR = point.IsOpen ? point.Value : 0;
        //                                    break;
        //                                case (int)ContractAppendixType.PriceService:
        //                                    PS = point.IsOpen ? point.Value : 0;
        //                                    break;
        //                                case (int)ContractAppendixType.AreaRent:
        //                                    SR = point.IsOpen ? point.Value : 0;
        //                                    break;
        //                                case (int)ContractAppendixType.AreaService:
        //                                    SS = point.IsOpen ? point.Value : 0;
        //                                    break;
        //                                default:
        //                                    return BadRequest(
        //                               contract.Adapt<ContractVM>());
        //                            }
        //                            point = ++changePointCurrent >= changePoints.Count ? null : changePoints[changePointCurrent];
        //                        }
        //                        if (point != null && point.DateChange.Month == month && point.Ground == i)
        //                        {
        //                            RevenueInMonth += Cal(PreSR, PreSS, PrePR, PrePS, currentDay - 1 - preDay + 1);
        //                            preDay = currentDay;
        //                            currentDay = point.DateChange.Day;
        //                        }
        //                        else if (point != null && point.Ground == i)
        //                        {
        //                            RevenueInMonth += Cal(PreSR, PreSS, PrePR, PrePS, currentDay - 1 - preDay + 1);
        //                            RevenueInMonth += Cal(SR, SS, PR, PS, 30 - currentDay + 1);
        //                            currentDay = 31;
        //                        }
        //                        PreSR = SR;
        //                        PreSS = SS;
        //                        PrePR = PR;
        //                        PrePS = PS;
        //                    }
        //                    else
        //                    {
        //                        RevenueInMonth += Cal(SR, SS, PR, PS, 30 - currentDay + 1);
        //                        currentDay = 31;
        //                    }
        //                }
        //            }
        //            revenueVM.MonthDatas[month - 1] = new MonthData
        //            {
        //                TotalRevenue = RevenueInMonth
        //            };
        //        }
        //        for (int month = 1; month <= 12; month++)
        //        {
        //            if (revenueVM.MonthDatas[month - 1] == null)
        //            {
        //                revenueVM.MonthDatas[month - 1] = new MonthData
        //                {
        //                    TotalRevenue = 0
        //                };
        //            }
        //        }

        //        revenueVM.RevenueSeason1 = revenueVM.MonthDatas[0].TotalRevenue
        //                                       + revenueVM.MonthDatas[1].TotalRevenue
        //                                       + revenueVM.MonthDatas[2].TotalRevenue;
        //        revenueVM.RevenueSeason2 = revenueVM.MonthDatas[3].TotalRevenue
        //                                    + revenueVM.MonthDatas[4].TotalRevenue
        //                                    + revenueVM.MonthDatas[5].TotalRevenue;
        //        revenueVM.RevenueSeason3 = revenueVM.MonthDatas[6].TotalRevenue
        //                                    + revenueVM.MonthDatas[7].TotalRevenue
        //                                    + revenueVM.MonthDatas[8].TotalRevenue;
        //        revenueVM.RevenueSeason4 = revenueVM.MonthDatas[9].TotalRevenue
        //                                    + revenueVM.MonthDatas[10].TotalRevenue
        //                                    + revenueVM.MonthDatas[11].TotalRevenue;
        //        revenueVM.RevenueYear = revenueVM.RevenueSeason1 + revenueVM.RevenueSeason2 + revenueVM.RevenueSeason3 + revenueVM.RevenueSeason4;

        //        if (revenueVM.RevenueYear != 0)
        //        {
        //            result.Add(revenueVM);
        //        }
        //    }
        //    return Ok(result);
        //}
        [HttpGet("Recode")]
        public ActionResult GetReCode(int year = 2020, Guid? contractId = null)
        {
            List<RevenueVM> result = new List<RevenueVM>();
            var contracts = _contractService.GetContracts();
            if (contractId != null) contracts = contracts.Where(_ => _.Id == contractId).ToList();
            var yearCal = DateTime.Now;
            yearCal = yearCal.AddYears(year - yearCal.Year);
            var yearBegin = new DateTime(yearCal.Year, 1, 1);
            var yearEnd = new DateTime(yearCal.Year, 12, 30);

            foreach (var contract in contracts)
            {
                var revenueVM = contract.Adapt<RevenueVM>();
                Dictionary<int, decimal> revenueDictionary = new Dictionary<int, decimal>();
                for (int i = 0; i < 4; i++)
                {

                    var appendixs = contract.ContractAppendices
                        .Where(_ => _.DateStart.Date <= yearEnd.Date
                        && (_.DateEnd == null || _.DateEnd.Value.Date >= yearBegin.Date)
                        && (_.DateEnd == null || _.DateEnd.Value.Date >= _.DateStart.Date)
                        && _.Key == i.ToString()
                        ).ToList();
                    if (appendixs.Count == 0) break;
                    if (i > 0)
                    {
                        revenueVM.Square = 0;
                    }
                    List<ChangePoint> changePoints = new List<ChangePoint>();
                    foreach (var appendix in appendixs)
                    {
                        if (appendix.DateEnd == null) appendix.DateEnd = contract.EndDate;

                        DateTime dateStart = appendix.DateStart.Date >= (yearBegin.Date) ? appendix.DateStart.Date : yearBegin.Date;

                        DateTime dateEnd = (appendix.DateEnd != null && appendix.DateEnd.Value.Date <= yearEnd.Date)
                                            ? appendix.DateEnd.Value.Date : yearEnd;
                        decimal value = 0;
                        switch (appendix.Type)
                        {
                            case (int)ContractAppendixType.PriceRent:
                                value = appendix.UnitPrice.Value;
                                break;
                            case (int)ContractAppendixType.PriceService:
                                value = appendix.UnitServicePrice.Value;
                                break;
                            case (int)ContractAppendixType.AreaRent:
                            case (int)ContractAppendixType.AreaService:
                                value = (decimal)appendix.Square.Value;
                                break;
                            default:
                                return BadRequest(
                           appendix.Adapt<ContractAppendixVM>());
                        }
                        ChangePoint openPoint = new ChangePoint
                        {
                            DateChange = dateStart,
                            IsOpen = true,
                            Type = appendix.Type,
                            Value = value
                        };
                        ChangePoint closePoint = new ChangePoint
                        {
                            DateChange = dateEnd.AddDays(1),
                            IsOpen = false,
                            Type = appendix.Type,
                            Value = value
                        };
                        changePoints.Add(openPoint);
                        if (closePoint.DateChange.Date < yearEnd.Date)
                        {
                            changePoints.Add(closePoint);
                        }


                    }

                    changePoints = changePoints.OrderBy(_ => _.DateChange).ThenBy(_ => _.IsOpen).ToList();

                    int changePointCurrent = 0;
                    decimal SR = 0, SS = 0;
                    decimal PR = 0, PS = 0;
                    decimal PreSR = 0, PreSS = 0;
                    decimal PrePR = 0, PrePS = 0;
                    for (var month = 1; month <= 12; month++)
                    {
                        decimal RevenueInMonth = 0;
                        int currentDay = 1;
                        int preDay = 1;
                        while (currentDay <= 30)
                        {
                            var point = changePointCurrent >= changePoints.Count ? null : changePoints[changePointCurrent];
                            if (point != null && point.DateChange.Month == month)
                            {
                                //update- currentday 
                                while (point != null && point.DateChange.Month == month && point.DateChange.Day == currentDay)
                                {
                                    switch (point.Type)
                                    {
                                        case (int)ContractAppendixType.PriceRent:
                                            PR = point.IsOpen ? point.Value : 0;
                                            break;
                                        case (int)ContractAppendixType.PriceService:
                                            PS = point.IsOpen ? point.Value : 0;
                                            break;
                                        case (int)ContractAppendixType.AreaRent:
                                            SR = point.IsOpen ? point.Value : 0;
                                            break;
                                        case (int)ContractAppendixType.AreaService:
                                            SS = point.IsOpen ? point.Value : 0;
                                            break;
                                        default:
                                            return BadRequest(
                                       contract.Adapt<ContractVM>());
                                    }
                                    point = ++changePointCurrent >= changePoints.Count ? null : changePoints[changePointCurrent];
                                }
                                if (point != null && point.DateChange.Month == month)
                                {
                                    RevenueInMonth += Cal(PreSR, PreSS, PrePR, PrePS, currentDay - 1 - preDay + 1);
                                    preDay = currentDay;
                                    currentDay = point.DateChange.Day;
                                }
                                else
                                {
                                    RevenueInMonth += Cal(PreSR, PreSS, PrePR, PrePS, currentDay - 1 - preDay + 1);
                                    RevenueInMonth += Cal(SR, SS, PR, PS, 30 - currentDay + 1);
                                    currentDay = 31;
                                }
                                PreSR = SR;
                                PreSS = SS;
                                PrePR = PR;
                                PrePS = PS;
                            }
                            else
                            {
                                RevenueInMonth += Cal(SR, SS, PR, PS, 30 - currentDay + 1);
                                currentDay = 31;
                            }
                        }
                        if (!revenueDictionary.ContainsKey(month))
                        {
                            revenueDictionary.Add(month, RevenueInMonth);
                        }
                        else
                        {
                            decimal revenueInMonth = revenueDictionary[month];
                            revenueDictionary[month] = revenueInMonth + RevenueInMonth;
                        }
                    }
                }

                for (int month = 1; month <= 12; month++)
                {
                    if (!revenueDictionary.ContainsKey(month))
                    {
                        revenueVM.MonthDatas[month - 1] = new MonthData
                        {
                            TotalRevenue = 0
                        };
                    }
                    else
                    {
                        revenueVM.MonthDatas[month - 1] = new MonthData
                        {
                            TotalRevenue = revenueDictionary[month]
                        };
                    }
                }
                revenueVM.RevenueSeason1 = revenueVM.MonthDatas[0].TotalRevenue
                                               + revenueVM.MonthDatas[1].TotalRevenue
                                               + revenueVM.MonthDatas[2].TotalRevenue;
                revenueVM.RevenueSeason2 = revenueVM.MonthDatas[3].TotalRevenue
                                            + revenueVM.MonthDatas[4].TotalRevenue
                                            + revenueVM.MonthDatas[5].TotalRevenue;
                revenueVM.RevenueSeason3 = revenueVM.MonthDatas[6].TotalRevenue
                                            + revenueVM.MonthDatas[7].TotalRevenue
                                            + revenueVM.MonthDatas[8].TotalRevenue;
                revenueVM.RevenueSeason4 = revenueVM.MonthDatas[9].TotalRevenue
                                            + revenueVM.MonthDatas[10].TotalRevenue
                                            + revenueVM.MonthDatas[11].TotalRevenue;
                revenueVM.RevenueYear = revenueVM.RevenueSeason1 + revenueVM.RevenueSeason2 + revenueVM.RevenueSeason3 + revenueVM.RevenueSeason4;

                if (revenueVM.RevenueYear != 0)
                {
                    result.Add(revenueVM);
                }

            }
            return Ok(result);
        }

        [HttpGet("CalculateRevenueStatistics")]
        public ActionResult CalculateRevenueStatistics(int year = 2020)
        {
            decimal[] revenue = new decimal[12];
            decimal[] squareUtilities = new decimal[12];
            decimal[] squareInfrastructure = new decimal[12];
            decimal[] electricityWater = new decimal[12];

            var contracts = _contractService.GetContracts();

            var yearCal = DateTime.Now;
            yearCal = yearCal.AddYears(year - yearCal.Year);
            var yearBegin = new DateTime(yearCal.Year, 1, 1);
            var yearEnd = new DateTime(yearCal.Year, 12, 30);

            foreach (var contract in contracts)
            {
                var revenueVM = contract.Adapt<RevenueVM>();
                for (int i = 0; i < 4; i++)
                {

                    var appendixs = contract.ContractAppendices
                        .Where(_ => _.DateStart.Date <= yearEnd.Date
                        && (_.DateEnd == null || _.DateEnd.Value.Date >= yearBegin.Date)
                        && (_.DateEnd == null || _.DateEnd.Value.Date >= _.DateStart.Date)
                        && _.Key == i.ToString()
                        ).ToList();
                    if (appendixs.Count == 0) break;
                    List<ChangePoint> changePoints = new List<ChangePoint>();
                    foreach (var appendix in appendixs)
                    {
                        DateTime dateStart = appendix.DateStart.Date >= yearBegin.Date ? appendix.DateStart.Date : yearBegin.Date;
                        DateTime dateEnd = (appendix.DateEnd != null && appendix.DateEnd.Value.Date <= yearEnd.Date)
                                            ? appendix.DateEnd.Value.Date : yearEnd;
                        decimal value = 0;
                        switch (appendix.Type)
                        {
                            case (int)ContractAppendixType.PriceRent:
                                value = appendix.UnitPrice.Value;
                                break;
                            case (int)ContractAppendixType.PriceService:
                                value = appendix.UnitServicePrice.Value;
                                break;
                            case (int)ContractAppendixType.AreaRent:
                            case (int)ContractAppendixType.AreaService:
                                value = (decimal)appendix.Square.Value;
                                break;
                            default:
                                return BadRequest(
                           appendix.Adapt<ContractAppendixVM>());
                        }
                        ChangePoint openPoint = new ChangePoint
                        {
                            DateChange = dateStart,
                            IsOpen = true,
                            Type = appendix.Type,
                            Value = value
                        };
                        ChangePoint closePoint = new ChangePoint
                        {
                            DateChange = dateEnd.AddDays(1),
                            IsOpen = false,
                            Type = appendix.Type,
                            Value = value
                        };
                        changePoints.Add(openPoint);
                        if (closePoint.DateChange.Date < yearEnd.Date)
                        {
                            changePoints.Add(closePoint);
                        }


                    }

                    changePoints = changePoints.OrderBy(_ => _.DateChange).ThenBy(_ => _.IsOpen).ToList();

                    int changePointCurrent = 0;
                    decimal SR = 0, SS = 0;
                    decimal PR = 0, PS = 0;
                    decimal PreSR = 0, PreSS = 0;
                    decimal PrePR = 0, PrePS = 0;
                    for (var month = 1; month <= 12; month++)
                    {
                        decimal RevenueInMonth = 0;
                        int currentDay = 1;
                        int preDay = 1;
                        while (currentDay <= 30)
                        {
                            var point = changePointCurrent >= changePoints.Count ? null : changePoints[changePointCurrent];
                            if (point != null && point.DateChange.Month == month)
                            {
                                //update- currentday 
                                while (point != null && point.DateChange.Month == month && point.DateChange.Day == currentDay)
                                {
                                    switch (point.Type)
                                    {
                                        case (int)ContractAppendixType.PriceRent:
                                            PR = point.IsOpen ? point.Value : 0;
                                            break;
                                        case (int)ContractAppendixType.PriceService:
                                            PS = point.IsOpen ? point.Value : 0;
                                            break;
                                        case (int)ContractAppendixType.AreaRent:
                                            SR = point.IsOpen ? point.Value : 0;
                                            break;
                                        case (int)ContractAppendixType.AreaService:
                                            SS = point.IsOpen ? point.Value : 0;
                                            break;
                                        default:
                                            return BadRequest(
                                       contract.Adapt<ContractVM>());
                                    }
                                    point = ++changePointCurrent >= changePoints.Count ? null : changePoints[changePointCurrent];
                                }
                                if (point != null && point.DateChange.Month == month)
                                {
                                    RevenueInMonth += Cal(PreSR, PreSS, PrePR, PrePS, currentDay - 1 - preDay + 1);
                                    preDay = currentDay;
                                    currentDay = point.DateChange.Day;
                                }
                                else
                                {
                                    RevenueInMonth += Cal(PreSR, PreSS, PrePR, PrePS, currentDay - 1 - preDay + 1);
                                    RevenueInMonth += Cal(SR, SS, PR, PS, 30 - currentDay + 1);
                                    currentDay = 31;
                                }
                                PreSR = SR;
                                PreSS = SS;
                                PrePR = PR;
                                PrePS = PS;
                            }
                            else
                            {
                                RevenueInMonth += Cal(SR, SS, PR, PS, 30 - currentDay + 1);
                                currentDay = 31;
                            }
                        }
                        revenueVM.MonthDatas[month - 1] = new MonthData
                        {
                            TotalRevenue = RevenueInMonth
                        };
                    }

                    //revenue[] = {revenueVM.MonthDatas[0].TotalRevenue , revenueVM.MonthDatas[1].TotalRevenue, revenueVM.MonthDatas[2].TotalRevenue, revenueVM.MonthDatas[3].TotalRevenue,
                    //                        revenueVM.MonthDatas[4].TotalRevenue, revenueVM.MonthDatas[5].TotalRevenue, revenueVM.MonthDatas[6].TotalRevenue , revenueVM.MonthDatas[7].TotalRevenue,
                    //                        revenueVM.MonthDatas[8].TotalRevenue, revenueVM.MonthDatas[9].TotalRevenue, revenueVM.MonthDatas[10].TotalRevenue, revenueVM.MonthDatas[11].TotalRevenue };
                    //squareUtilities = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    //squareInfrastructure = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                    for (int j = 0; j < 12; j++)
                    {
                        revenue[j] += revenueVM.MonthDatas[j].TotalRevenue;
                        squareUtilities[j] = 0;
                        squareInfrastructure[j] = 0;
                        electricityWater[j] = 0;
                    }

                }


            }


            return Ok(new { revenue, squareInfrastructure, squareUtilities, electricityWater });
        }
        [HttpGet("VT")]
        public ActionResult getRevenueVT(int year = 2020)
        {
            var current = DateTime.Now;
            current = current.AddYears(year - current.Year);
            var endYear = new DateTime(current.Year, 12, 30);
            var firstYear = new DateTime(current.Year, 1, 1);

            List<RevenueVTVM> result = new List<RevenueVTVM>();
            var contractVTs = _contractTelecomService.GetContractTelecoms().ToList();
            foreach (var contractVT in contractVTs)
            {
                RevenueVTVM itemData = contractVT.Adapt<RevenueVTVM>();
                itemData.CompanyName = contractVT.Customer.Name;
                itemData.CustomerCode = contractVT.Customer.Code;
                itemData.ContractId = contractVT.Id;
                itemData.MonthDatas = new decimal[12];
                foreach (var appendix in contractVT.ContractTelecomAppendices.Where(_ => _.Type == 0).ToList())
                {
                    if (appendix.DateAccept == null ||
                        appendix.DateStart > endYear ||
                        appendix.DateEnd < firstYear) continue;
                    DateTime dateEnd;

                    dateEnd = endYear;

                    if (contractVT.DateEnd != null)
                    {
                        dateEnd = contractVT.DateEnd;
                    }

                    if (appendix.DateEnd != null)
                    {
                        dateEnd = appendix.DateEnd.Value;
                    }

                    if (dateEnd > endYear)
                    {
                        dateEnd = endYear;
                    }
                    var dateStart = appendix.DateAccept.Value > firstYear ? appendix.DateAccept.Value : firstYear;
                    foreach (var service in appendix.TelecomserviceContractAppendices)
                    {
                        if (service.DateEnd != null && dateEnd <= endYear && service.DateEnd.Value.Year <= dateEnd.Year)
                        {
                            dateEnd = service.DateEnd.Value;
                        }
                        if (dateStart > dateEnd) continue;
                        for (var i = dateStart.Month; i <= dateEnd.Month; i++)
                        {
                            var period = 30;
                            if (i == dateStart.Month && dateStart.Day > 1)
                            {
                                period = 30 - dateStart.Day + 1;
                            }

                            if (i == dateEnd.Month && dateEnd.Day < 30)
                            {
                                period = dateEnd.Day;
                            }

                            var money = service.UnitAmount * (decimal)(1.0 * period / 30);
                            itemData.MonthDatas[i - 1] += money;
                        }
                    }
                }

                itemData.RevenueSeason1 = itemData.MonthDatas[0]
                                            + itemData.MonthDatas[1]
                                            + itemData.MonthDatas[2];
                itemData.RevenueSeason2 = itemData.MonthDatas[3]
                                            + itemData.MonthDatas[4]
                                            + itemData.MonthDatas[5];
                itemData.RevenueSeason3 = itemData.MonthDatas[6]
                                            + itemData.MonthDatas[7]
                                            + itemData.MonthDatas[8];
                itemData.RevenueSeason4 = itemData.MonthDatas[9]
                                            + itemData.MonthDatas[10]
                                            + itemData.MonthDatas[11];
                itemData.RevenueYear = itemData.RevenueSeason1 +
                                itemData.RevenueSeason2 +
                                itemData.RevenueSeason3 +
                                itemData.RevenueSeason4;
                if (itemData.RevenueYear > 0)
                {
                    result.Add(itemData);
                }
            }
            return Ok(result);
        }

        [HttpGet("VT/{id}/Detail")]
        public ActionResult getRevenueVTDetail(Guid id)
        {
            var current = DateTime.Now;
            var endYear = new DateTime(current.Year, 12, 30);
            var firstYear = new DateTime(current.Year, 1, 1);

            RevenueVTVMDetail result = new RevenueVTVMDetail();
            var contract = _contractTelecomService.GetContractTelecom(id);
            result = contract.Adapt(result);
            result.CompanyName = contract.Customer.Name;
            result.CustomerCode = contract.Customer.Code;
            foreach (var appendix in contract.ContractTelecomAppendices)
            {
                foreach (var service in appendix.TelecomserviceContractAppendices)
                {
                    var serviceRevenue = new ServiceRevenueVM();
                    serviceRevenue.MonthDatas = new decimal[12];
                    serviceRevenue.AppendixCode = appendix.Code;
                    serviceRevenue.DateAccept = appendix.DateAccept;
                    serviceRevenue.DateEnd = appendix.DateEnd;


                    serviceRevenue.ServiceId = service.TelecomserviceId;
                    serviceRevenue.ServiceName = service.Telecomservice.Name;
                    serviceRevenue.Data = service.Data;
                    serviceRevenue.UnitAmount = service.UnitAmount;

                    if (appendix.DateAccept == null ||
                        appendix.DateStart > endYear ||
                        appendix.DateEnd < firstYear) continue;
                    var dateEnd = appendix.DateEnd == null ? endYear : appendix.DateEnd.Value;
                    dateEnd = service.DateEnd != null ? service.DateEnd.Value : dateEnd;
                    if (dateEnd > endYear)
                    {
                        dateEnd = endYear;
                    }
                    var dateStart = appendix.DateAccept.Value > firstYear ? appendix.DateAccept.Value : firstYear;

                    if (dateStart > dateEnd) continue;

                    for (var i = dateStart.Month; i <= dateEnd.Month; i++)
                    {
                        var period = 30;
                        if (i == dateStart.Month && dateStart.Day > 1)
                        {
                            period = 30 - dateStart.Day + 1;
                        }

                        if (i == dateEnd.Month && dateEnd.Day < 30)
                        {
                            period = dateEnd.Day;
                        }

                        var money = service.UnitAmount * (decimal)(1.0 * period / 30);
                        serviceRevenue.MonthDatas[i - 1] += money;
                    }
                    serviceRevenue.Data = JsonConvert.DeserializeObject(service.Data);

                    serviceRevenue.RevenueSeason1 = serviceRevenue.MonthDatas[0]
                                            + serviceRevenue.MonthDatas[1]
                                            + serviceRevenue.MonthDatas[2];
                    serviceRevenue.RevenueSeason2 = serviceRevenue.MonthDatas[3]
                                                + serviceRevenue.MonthDatas[4]
                                                + serviceRevenue.MonthDatas[5];
                    serviceRevenue.RevenueSeason3 = serviceRevenue.MonthDatas[6]
                                                + serviceRevenue.MonthDatas[7]
                                                + serviceRevenue.MonthDatas[8];
                    serviceRevenue.RevenueSeason4 = serviceRevenue.MonthDatas[9]
                                                + serviceRevenue.MonthDatas[10]
                                                + serviceRevenue.MonthDatas[11];
                    serviceRevenue.RevenueYear = serviceRevenue.RevenueSeason1 +
                                    serviceRevenue.RevenueSeason2 +
                                    serviceRevenue.RevenueSeason3 +
                                    serviceRevenue.RevenueSeason4;

                    result.Services.Add(serviceRevenue);

                }
            }
            return Ok(result);
        }
    }



    class ChangePoint
    {
        public DateTime DateChange { get; set; }
        public int Type { get; set; }
        public bool IsOpen { get; set; }
        public decimal Value { get; set; }
        public int Ground { get; set; } // Mặt bằng 1,2,3,4
    }
}
