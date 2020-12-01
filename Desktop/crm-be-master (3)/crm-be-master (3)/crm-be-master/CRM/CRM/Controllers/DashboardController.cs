using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CRM.HangfireJob;
using CRM.Helpers;
using CRM.Model;
using CRM.Service;
using CRM.Service.Utils;
using CRM.Utils;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Newtonsoft.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ITransactionLogService _transactionLogService;
        private readonly IBuildingService _buildingService;
        private readonly IContractService _contractService;
        private readonly IContractTelecomAppendixService _contractTelecomAppendixService;
        private readonly ITelecomserviceService _telecomserviceService;
        private readonly UserManager<HsUser> _userManager;
        private readonly ICommonTelecomserviceService _commonTelecomserviceService;
        private readonly ILogService _logService;
        private readonly IContractAppendixService _appendixService;
        private readonly ISubCoContractService _subCoContractService;
        private readonly ICoContractTelServiceService _coContractTelServiceService;
        private readonly IDashBoardChartService _dashBoardChartService;
        public const string NĐT = "nhà đầu tư";
        public const string DNPM_1 = "doanh nghiệp phần mềm";
        public const string DNPM_2 = "ươm tạo";
        public const string DNPM_3 = "r&d";
        public const string DNPM_4 = "cntt";
        public const string DOMESTIC = "việt nam";
        public const string INSIDE = "trong công viên phần mềm quang trung";

        public DashboardController(ICustomerService customerService, ITransactionLogService transactionLogService, IBuildingService buildingService, IContractService contractService,
            IContractTelecomAppendixService contractTelecomAppendixService, ITelecomserviceService telecomserviceService, UserManager<HsUser> userManager,
            ICommonTelecomserviceService commonTelecomserviceService, ILogService logService, IContractAppendixService appendixService, ISubCoContractService subCoContractService
            , ICoContractTelServiceService coContractTelServiceService, IDashBoardChartService dashBoardChartService)
        {
            //log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
            _customerService = customerService;
            _transactionLogService = transactionLogService;
            _buildingService = buildingService;
            _contractService = contractService;
            _contractTelecomAppendixService = contractTelecomAppendixService;
            _telecomserviceService = telecomserviceService;
            _userManager = userManager;
            _commonTelecomserviceService = commonTelecomserviceService;
            _logService = logService;
            _appendixService = appendixService;
            _subCoContractService = subCoContractService;
            _coContractTelServiceService = coContractTelServiceService;
            _dashBoardChartService = dashBoardChartService;
            GetListCommonTelecomservice();
        }



        [Authorize]
        [HttpGet("CheckAccess")]
        public ActionResult CheckAccessAsync()
        {
            try
            {
                var result = new AccessDashboardVM
                {
                    BusinessAccess = false,
                    MarketAccess = false,
                    TelecomAccess = false
                };
                var username = User.Identity.Name;
                //var user = _userManager.Users.FirstOrDefault(u => u.UserName.Equals(username));
                var user = _userManager.FindByNameAsync(username).Result;
                var roles = _userManager.GetRolesAsync(user).Result.ToList();

                //if (roles.Contains(""))
                if (user == null)
                {
                    return NotFound();
                }
                if (roles.Contains("P.KD"))
                {
                    result.BusinessAccess = true;
                }
                if (roles.Contains("P.CLTT"))
                {
                    result.MarketAccess = true;
                }
                if (roles.Contains("TTVT"))
                {
                    result.TelecomAccess = true;
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet("CompanyStatisticsInQTSC")]
        public ActionResult CompanyStatisticsInQTSC()
        {
            List<CompanyStatistics> resultList = new List<CompanyStatistics>();

            //List<Customer> totalCompanyNumber = _customerService.GetCustomers().Where(_ => _.MemberOfQuangTrungSoftware.Contains("Trong") && _.Country != null &&
            //                                                                  _.Country != "").ToList();
            List<Customer> totalCompanyNumber = _customerService.GetCustomers().Where(_ => _.MemberOfQuangTrungSoftware == "Trong công viên phần mềm Quang Trung" &&
                                                                                _.Country != null && _.Country != "").ToList();


            int numberOfTotal = totalCompanyNumber.Count();
            var companyGroupByCountry = from company in totalCompanyNumber
                                        group company by company.Country.ToUpper() into companyGroup
                                        select new
                                        {
                                            Country = companyGroup.Key,
                                            Total = companyGroup.Count(),
                                        };


            foreach (var company in companyGroupByCountry)
            {
                decimal rate = ((decimal)company.Total / numberOfTotal) * 100;
                CompanyStatistics companyStatistics = new CompanyStatistics(company.Country, rate);
                resultList.Add(companyStatistics);

            }
            return Ok(resultList);


        }

        #region private method
        /// <summary>
        /// Default is Domestic if null
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsDomestic(Customer c)
        {
            return c.Country != null ? c.Country.ToLower().Contains(DOMESTIC) : true;
        }

        private bool IsSoftwareBusiness(Customer c)
        {
            //TODO : Need to modify
            return c.CompanyType != null ? (c.CompanyType.ToLower().Contains(DNPM_1) || c.CompanyType.ToLower().Contains(DNPM_2)
                || c.CompanyType.ToLower().Contains(DNPM_3) || c.CompanyType.ToLower().Contains(DNPM_4)) : false;
        }

        private bool IsInsideCustomer(Customer c)
        {
            return c.MemberOfQuangTrungSoftware != null ? c.MemberOfQuangTrungSoftware.ToLower().Equals(INSIDE) : false;
        }
        private bool InMonthCustomer(Customer c)
        {
            var contract = _contractService.GetContracts(_ => _.CustomerId.Equals(c.Id)).OrderBy(_ => _.StartDate).FirstOrDefault();
            var result = true;
            result = result && contract != null && contract.StartDate != null ? (contract.StartDate.Value.Year == DateTime.Now.Year &&
            contract.StartDate.Value.Month == DateTime.Now.Month) : false;
            //result = result && c.StartYear != null ? (c.StartYear.Value.Year == DateTime.Now.Year &&
            //c.StartYear.Value.Month == DateTime.Now.Month) : false;
            return result;
        }

        private bool IsRunningContract(Contract c)
        {
            var result = true;
            result = result && c.StartDate != null ? c.StartDate.Value < DateTime.Now.Date : false;
            result = result && c.EndDate != null ? c.EndDate.Value > DateTime.Now.Date : false;
            result = result && c.Status == 0;
            return result;
        }
        private bool IsLiquidatedContract(Contract c)
        {
            var result = true;
            result = result && c.EndDate != null ? c.EndDate.Value < DateTime.Now.Date : false;
            result = result && c.Status == 1;
            return result;
        }

        private bool IsAround30Days(DateTime date, bool isAfter)
        {
            var result = true;
            result = date.Year == DateTime.Now.Year;
            if (isAfter)
            {
                result = 0 < DateTime.Now.Date.Subtract(date).TotalDays && DateTime.Now.Date.Subtract(date).TotalDays <= 30;
            }
            else
            {
                result = 0 < date.Subtract(DateTime.Now.Date).TotalDays && date.Subtract(DateTime.Now.Date).TotalDays <= 30;
            }

            return result;
        }
        #endregion

        #region P.CLTT
        [HttpGet("TotalInvestors")]
        public ActionResult TotalInvestors(int year = 2020)
        {
            try
            {
                var customers = _customerService.GetCustomers(c => c.CompanyType != null ? c.CompanyType.ToLower().Contains(NĐT) : false);
                var result = new List<TotalInvestorsCustomerVM>();
                for (int currentYear = year; currentYear <= DateTime.Now.Year; currentYear++)
                {
                    var customersByYear = customers.Where(c => StaticMethod.IsAvailableCustomer(c, currentYear));
                    result.Add(new TotalInvestorsCustomerVM
                    {
                        Year = currentYear.ToString(),
                        Total = customersByYear.Count(),
                        TotalDomestic = customersByYear.Count(c => IsDomestic(c)),
                        TotalInternational = customersByYear.Count(c => !IsDomestic(c))
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //[HttpGet("VondautuInvestors")]

        //public ActionResult VondautuInvestors(int year = 2016)
        //{
        //    try
        //    {
        //        var customers = _customerService.GetCustomers(c => c.CompanyType != null ? c.CompanyType.ToLower().Contains(NĐT) : false);
        //        var result = new List<object>();
        //        for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
        //        {
        //            var customersByYear = customers.Where(c => StaticMethod.IsAvailableCustomer(c, currentYear));
        //            foreach (var customerByYear in customersByYear)
        //            {
        //                _transactionLogService.AdaptChange<Customer>(currentYear == DateTime.Now.Year ? new DateTime(currentYear, 12, 30) : DateTime.Now, customerByYear, customerByYear.Id);
        //            }
        //            result.Add(new
        //            {
        //                Year = currentYear.ToString(),
        //                VondautuRegister = customersByYear.Sum(c => c.VondautuRegister)
        //            });
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpGet("VondautuInvestors")]
        //public ActionResult VondautuInvestors(int year = 2016)
        //{
        //    try
        //    {
        //        var customers = _customerService.GetCustomers(c => c.CompanyType != null ? c.CompanyType.ToLower().Contains(NĐT) : false);
        //        var result = new List<object>();
        //        for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
        //        {
        //            decimal VondautuRegiter = 0;
        //            decimal LastDashBoard = 0;
        //            for (int month = 1; month <= 12; month++)
        //            {
        //                var dashBoardChart = _dashBoardChartService.GetDashBoardCharts(_ => _.Time.Year == currentYear && _.Time.Month == month).FirstOrDefault();
        //                if (dashBoardChart != null)
        //                {
        //                    VondautuRegiter += dashBoardChart.VondautuInvestors;
        //                    LastDashBoard = dashBoardChart.VondautuInvestors;
        //                }
        //                else
        //                {
        //                    VondautuRegiter += LastDashBoard;
        //                }
        //            }
        //            if (VondautuRegiter > 0)
        //            {
        //                result.Add(new
        //                {
        //                    Year = currentYear.ToString(),
        //                    VondautuRegister = VondautuRegiter / 12
        //                });
        //            }
        //            else if(VondautuRegiter == 0 && currentYear > DateTime.Now.Year)
        //            {
        //                result.Add(new
        //                {
        //                    Year = currentYear.ToString()
        //                });
        //            }
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        // lấy tháng cuối đại diện cả năm
        [HttpGet("VondautuInvestors")]
        public ActionResult VondautuInvestors(int year = 2016)
        {
            try
            {
                var customers = _customerService.GetCustomers(c => c.CompanyType != null ? c.CompanyType.ToLower().Contains(NĐT) : false);
                var result = new List<object>();
                for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
                {
                    decimal VondautuRegiter = 0;
                    for (int month = 1; month <= 12; month++)
                    {
                        var dashBoardChart = _dashBoardChartService.GetDashBoardCharts(_ => _.Time.Year == currentYear && _.Time.Month == month).FirstOrDefault();
                        if (dashBoardChart != null)
                        {
                            VondautuRegiter = dashBoardChart.VondautuInvestors;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (VondautuRegiter > 0)
                    {
                        result.Add(new
                        {
                            Year = currentYear.ToString(),
                            VondautuRegister = VondautuRegiter
                        });
                    }
                    else if (VondautuRegiter == 0 && currentYear > DateTime.Now.Year)
                    {
                        result.Add(new
                        {
                            Year = currentYear.ToString()
                        });
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("VondautuSoftwares")]
        //public ActionResult VondautuSoftwares(int year = 2016)
        //{
        //    try
        //    {
        //        var customers = _customerService.GetCustomers(c => IsSoftwareBusiness(c));
        //        var result = new List<object>();
        //        for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
        //        {
        //            var customersByYear = customers.Where(c => StaticMethod.IsAvailableCustomer(c, currentYear));
        //            foreach (var customerByYear in customersByYear)
        //            {
        //                _transactionLogService.AdaptChange<Customer>(currentYear == DateTime.Now.Year ? new DateTime(currentYear, 12, 30) : DateTime.Now, customerByYear, customerByYear.Id);
        //            }
        //            result.Add(new
        //            {
        //                Year = currentYear.ToString(),
        //                VondautuRegister = customersByYear.Sum(c => c.VondautuRegister)
        //            });
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        // lấy trung bình 12 tháng
        //[HttpGet("VondautuSoftwares")]
        //public ActionResult VondautuSoftwares(int year = 2016)
        //{
        //    try
        //    {
        //        var customers = _customerService.GetCustomers(c => IsSoftwareBusiness(c));
        //        var result = new List<object>();
        //        for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
        //        {
        //            decimal VondautuSoftWare = 0;
        //            decimal LastDashBoard = 0;
        //            for (int month = 1; month <= 12; month++)
        //            {
        //                var dashBoardChart = _dashBoardChartService.GetDashBoardCharts(_ => _.Time.Year == currentYear && _.Time.Month == month).FirstOrDefault();
        //                if (dashBoardChart != null)
        //                {
        //                    VondautuSoftWare += dashBoardChart.VondautuSoftwares;
        //                    LastDashBoard = dashBoardChart.VondautuSoftwares;
        //                }
        //                else
        //                {
        //                    VondautuSoftWare += LastDashBoard;
        //                }
        //            }
        //            if (VondautuSoftWare > 0)
        //            {
        //                result.Add(new
        //                {
        //                    Year = currentYear.ToString(),
        //                    VondautuRegister = VondautuSoftWare / 12
        //                });
        //            }
        //            else if (VondautuSoftWare == 0 && currentYear > DateTime.Now.Year)
        //            {
        //                result.Add(new
        //                {
        //                    Year = currentYear.ToString()
        //                });
        //            }
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        // lấy tháng cuối cùng đại diện cho cả năm
        [HttpGet("VondautuSoftwares")]
        public ActionResult VondautuSoftwares(int year = 2016)
        {
            try
            {
                var customers = _customerService.GetCustomers(c => IsSoftwareBusiness(c));
                var result = new List<object>();
                for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
                {
                    decimal VondautuSoftWare = 0;
                    for (int month = 1; month <= 12; month++)
                    {
                        var dashBoardChart = _dashBoardChartService.GetDashBoardCharts(_ => _.Time.Year == currentYear && _.Time.Month == month).FirstOrDefault();
                        if (dashBoardChart != null)
                        {
                            VondautuSoftWare = dashBoardChart.VondautuSoftwares;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (VondautuSoftWare > 0)
                    {
                        result.Add(new
                        {
                            Year = currentYear.ToString(),
                            VondautuRegister = VondautuSoftWare
                        });
                    }
                    else if (VondautuSoftWare == 0 && currentYear > DateTime.Now.Year)
                    {
                        result.Add(new
                        {
                            Year = currentYear.ToString()
                        });
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("TotalSoftwares")]
        //public ActionResult TotalSoftwares(int year = 2016)
        //{
        //    try
        //    {
        //        var customers = _customerService.GetCustomers(c => IsSoftwareBusiness(c));

        //        var result = new List<TotalSoftwareCustomerVM>();
        //        for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
        //        {
        //            var customersByYear = customers.Where(c => StaticMethod.IsAvailableCustomer(c, currentYear));
        //            result.Add(new TotalSoftwareCustomerVM
        //            {
        //                Year = currentYear.ToString(),
        //                Total = customersByYear.Count(),
        //                TotalDomestic = customersByYear.Count(c => IsDomestic(c)),
        //                TotalInternational = customersByYear.Count(c => !IsDomestic(c))
        //            });
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        // Lấy trung bình 12 tháng
        //[HttpGet("TotalSoftwares")]
        //public ActionResult TotalSoftwares(int year = 2016)
        //{
        //    try
        //    {
        //        var result = new List<TotalSoftwareCustomerVM>();
        //        for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
        //        {
        //            long TotalDomestic = 0;
        //            long TotalIntenational = 0;
        //            long LastTotalDomestic = 0;
        //            long LastTotalIntenational = 0;
        //            for (int month = 1; month <= 12; month++)
        //            {
        //                var dashBoardChart = _dashBoardChartService.GetDashBoardCharts(_ => _.Time.Year == currentYear && _.Time.Month == month).FirstOrDefault();
        //                if (dashBoardChart != null)
        //                {
        //                    TotalDomestic += dashBoardChart.TotalDomestic;
        //                    TotalIntenational += dashBoardChart.TotalInternational;
        //                    LastTotalDomestic = dashBoardChart.TotalDomestic;
        //                    LastTotalIntenational = dashBoardChart.TotalInternational;
        //                }
        //                else
        //                {
        //                    TotalDomestic += LastTotalDomestic;
        //                    TotalIntenational += LastTotalIntenational;
        //                }
        //            }
        //            if ((TotalDomestic + TotalIntenational) > 0)
        //            {
        //                result.Add(new TotalSoftwareCustomerVM
        //                {
        //                    Year = currentYear.ToString(),
        //                    Total = TotalDomestic / 12 + TotalIntenational / 12,
        //                    TotalDomestic = TotalDomestic / 12,
        //                    TotalInternational = TotalIntenational / 12
        //                });
        //            }
        //            else if((TotalDomestic + TotalIntenational) == 0 && currentYear > DateTime.Now.Year) 
        //            {
        //                result.Add(new TotalSoftwareCustomerVM
        //                {
        //                    Year = currentYear.ToString(),
        //                });
        //            }
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        // lấy tháng cuối cùng làm đại diện cho cả năm
        [HttpGet("TotalSoftwares")]
        public ActionResult TotalSoftwares(int year = 2016)
        {
            try
            {
                var result = new List<TotalSoftwareCustomerVM>();
                for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
                {
                    long TotalDomestic = 0;
                    long TotalIntenational = 0;
                    for (int month = 1; month <= 12; month++)
                    {
                        var dashBoardChart = _dashBoardChartService.GetDashBoardCharts(_ => _.Time.Year == currentYear && _.Time.Month == month).FirstOrDefault();
                        if (dashBoardChart != null)
                        {
                            TotalDomestic = dashBoardChart.TotalDomestic;
                            TotalIntenational = dashBoardChart.TotalInternational;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if ((TotalDomestic + TotalIntenational) > 0)
                    {
                        result.Add(new TotalSoftwareCustomerVM
                        {
                            Year = currentYear.ToString(),
                            Total = TotalDomestic + TotalIntenational,
                            TotalDomestic = TotalDomestic,
                            TotalInternational = TotalIntenational
                        });
                    }
                    else if ((TotalDomestic + TotalIntenational) == 0 && currentYear > DateTime.Now.Year)
                    {
                        result.Add(new TotalSoftwareCustomerVM
                        {
                            Year = currentYear.ToString(),
                        });
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("TotalInsideOutsideSoftwares")]
        public ActionResult TotalInsideOutsideSoftwares(int year = 2020)
        {
            try
            {
                var customers = _customerService.GetCustomers(c => IsSoftwareBusiness(c));

                var result = new List<TotalInsideOutsideSoftwareVM>();
                for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
                {
                    var customersByYear = customers.Where(c => StaticMethod.IsAvailableCustomer(c, currentYear));
                    result.Add(new TotalInsideOutsideSoftwareVM
                    {
                        Year = currentYear.ToString(),
                        Total = customersByYear.Count(),
                        TotalInside = customersByYear.Count(c => IsInsideCustomer(c)),
                        TotalOutside = customersByYear.Count(c => !IsInsideCustomer(c))
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("TotalPeoples")]
        //public ActionResult TotalPeoples(int year = 2016)
        //{
        //    //TODO : Fix lại khi có dữ liệu hoàn chỉnh từ KH
        //    //try
        //    //{
        //    //    var customers = _customerService.GetCustomers();
        //    //    var customersByYear = customers.ToList();
        //    //    var result = new List<TotalPeopleVM>();
        //    //    for (int currentYear = year; currentYear <= DateTime.Now.Year; currentYear++)
        //    //    {
        //    //        //var customersByYear = customers.Where(c => StaticMethod.IsAvailableCustomer(c, currentYear));
        //    //        foreach (var customerByYear in customersByYear)
        //    //        {
        //    //            _transactionLogService.AdaptChange<Personnel>(currentYear == DateTime.Now.Year ? new DateTime(currentYear, 12, 30) : DateTime.Now, customerByYear.Personnel, customerByYear.Personnel.Id);
        //    //        }
        //    //        var totalEmployee = customersByYear.Sum(c => c.Personnel.TotalEmployee);
        //    //        var totalStudent = customersByYear.Sum(c => c.Personnel.TotalStudent);
        //    //        result.Add(new TotalPeopleVM
        //    //        {
        //    //            Year = currentYear.ToString(),
        //    //            TotalEmployee = totalEmployee,
        //    //            TotalStudent = totalStudent,
        //    //            Total = totalEmployee + totalStudent
        //    //        });
        //    //    }
        //    //    return Ok(result);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return BadRequest(ex.Message);
        //    //}
        //    try
        //    {
        //        var personels = _customerService.GetCustomers().Select(c => c.Personnel);
        //        var personelsByYear = personels.ToList();
        //        var result = new List<TotalPeopleVM>();
        //        for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
        //        {
        //            //var customersByYear = customers.Where(c => StaticMethod.IsAvailableCustomer(c, currentYear));
        //            //foreach (var customerByYear in personelsByYear)
        //            //{
        //            //    _transactionLogService.AdaptChange<Personnel>(currentYear == DateTime.Now.Year ? new DateTime(currentYear, 12, 30) : DateTime.Now, customerByYear.Personnel, customerByYear.Personnel.Id);
        //            //}
        //            var totalEmployee = personelsByYear.Sum(c => c.TotalEmployee);
        //            var totalAlumnus = personelsByYear.Sum(c => c.TotalAlumnus);
        //            result.Add(new TotalPeopleVM
        //            {
        //                Year = currentYear.ToString(),
        //                TotalEmployee = totalEmployee,
        //                TotalAlumnus = totalAlumnus,
        //                Total = totalEmployee + totalAlumnus
        //            });
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        // lấy trung bình
        //[HttpGet("TotalPeoples")]
        //public ActionResult TotalPeoples(int year = 2016)
        //{
        //    try
        //    {
        //        var result = new List<TotalPeopleVM>();
        //        for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
        //        {
        //            long totalEmployee = 0;
        //            long totalAlumnus = 0;
        //            long LastTotalEmployee = 0;
        //            long LastTotalAlumnus = 0;
        //            for (int month = 1; month <= 12; month++)
        //            {
        //                var dashBoardChart = _dashBoardChartService.GetDashBoardCharts(_ => _.Time.Year == currentYear && _.Time.Month == month).FirstOrDefault();
        //                if (dashBoardChart != null)
        //                {
        //                    totalEmployee += dashBoardChart.TotalEmployee;
        //                    totalAlumnus += dashBoardChart.TotalAlumnus;
        //                    LastTotalEmployee = dashBoardChart.TotalEmployee;
        //                    LastTotalAlumnus = dashBoardChart.TotalAlumnus;
        //                }
        //                else
        //                {
        //                    totalEmployee += LastTotalEmployee;
        //                    totalAlumnus += LastTotalAlumnus;
        //                }
        //            }
        //            if ((totalEmployee + totalAlumnus) > 0)
        //            {
        //                result.Add(new TotalPeopleVM
        //                {
        //                    Year = currentYear.ToString(),
        //                    TotalEmployee = totalEmployee / 12,
        //                    TotalAlumnus = totalAlumnus / 12,
        //                    Total = totalEmployee / 12 + totalAlumnus / 12
        //                });
        //            }
        //            else if ((totalEmployee + totalAlumnus) == 0 && currentYear > DateTime.Now.Year)
        //            {
        //                result.Add(new TotalPeopleVM
        //                {
        //                    Year = currentYear.ToString(),
        //                });
        //            }
        //        }
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}


         // lấy tháng cuối trong năm
        [HttpGet("TotalPeoples")]
        public ActionResult TotalPeoples(int year = 2016)
        {
            try
            {
                var result = new List<TotalPeopleVM>();
                for (int currentYear = year; currentYear <= DateTime.Now.Year + 3; currentYear++)
                {
                    long totalEmployee = 0;
                    long totalAlumnus = 0;
                    for (int month = 1; month <= 12; month++)
                    {
                        var dashBoardChart = _dashBoardChartService.GetDashBoardCharts(_ => _.Time.Year == currentYear && _.Time.Month == month).FirstOrDefault();
                        if (dashBoardChart != null)
                        {
                            totalEmployee = dashBoardChart.TotalEmployee;
                            totalAlumnus = dashBoardChart.TotalAlumnus;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if ((totalEmployee + totalAlumnus) > 0)
                    {
                        result.Add(new TotalPeopleVM
                        {
                            Year = currentYear.ToString(),
                            TotalEmployee = totalEmployee,
                            TotalAlumnus = totalAlumnus,
                            Total = totalEmployee + totalAlumnus
                        });
                    }
                    else if ((totalEmployee + totalAlumnus) == 0 && currentYear > DateTime.Now.Year)
                    {
                        result.Add(new TotalPeopleVM
                        {
                            Year = currentYear.ToString(),
                        });
                    }
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ObjectType")]
        public ActionResult CountObjectType()
        {
            var mapping = new Dictionary<string, int>();
            var objectTypes = _customerService.GetCustomers(c => StaticMethod.IsAvailableCustomer(c, DateTime.Now.Year))
                .Select(s => s.ObjectType).ToList();
            string error = "";
            int count = 0;
            foreach (var item in objectTypes)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    try
                    {
                        var list = JsonConvert.DeserializeObject<List<string>>(item);
                        foreach (var o in list)
                        {
                            int val = 0;
                            if (mapping.TryGetValue(o.ToLower(), out val))
                            {
                                mapping[o.ToLower()]++;
                            }
                            else
                            {
                                mapping.TryAdd(o.ToLower(), 1);
                            };
                        }
                    }
                    catch (Exception ex)
                    {
                        error += item + " ;";
                    }
                }
                count++;
            }
            if (String.IsNullOrEmpty(error))
            {
                var rs = mapping.Select(m => new
                {
                    MarketType = m.Key,
                    Total = m.Value,
                }).ToList();
                return Ok(rs);
            }
            else
            {
                return BadRequest(new { Error = "Can't DeserializeObject : " + error });
            }
        }

        [HttpGet("MarketType")]
        public ActionResult CountMarketType()
        {
            var mapping = new Dictionary<string, int>();
            var marketTypes = _customerService.GetCustomers(c => StaticMethod.IsAvailableCustomer(c, DateTime.Now.Year))
                .Select(s => s.MarketType).ToList();
            string error = "";
            foreach (var item in marketTypes)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    try
                    {
                        var list = JsonConvert.DeserializeObject<List<string>>(item);
                        foreach (var o in list)
                        {
                            int val = 0;
                            if (mapping.TryGetValue(o.ToLower(), out val))
                            {
                                mapping[o.ToLower()]++;
                            }
                            else
                            {
                                mapping.TryAdd(o.ToLower(), 1);
                            };
                        }
                    }
                    catch (Exception e)
                    {
                        error += item + " ;";
                    }
                }
            }
            if (String.IsNullOrEmpty(error))
            {
                var rs = mapping.Select(m => new
                {
                    MarketType = m.Key,
                    Total = m.Value,
                }).ToList();
                return Ok(rs);
            }
            else
            {
                return BadRequest(new { Error = "Can't DeserializeObject : " + error });
            }
        }
        #endregion

        #region Phòng KD
        [HttpGet("TopRevenue")]
        public ActionResult TopRevenue(int top = 5)
        {
            var result = new TopTotalVndRevenueVM();
            var customers = _customerService.GetCustomers(c => StaticMethod.IsAvailableCustomer(c, DateTime.Now.Year)).OrderByDescending(c => c.Amount.TotalVndRevenue);
            //result = customers.Adapt(result);
            foreach (var customer in customers.Take(top))
            {
                result.TotalVndRevenueVMs.Add(new TotalVndRevenueVM
                {
                    Name = customer.Name,
                    TotalVndRevenue = customer.Amount.TotalVndRevenue
                });
            }
            result.NumberOfCustomer = customers.Count();
            return Ok(result);
        }

        [HttpGet("ParticipantsInMonth")]
        public ActionResult ParticipantsInMonth()
        {
            var customers = _customerService.GetCustomers(c => StaticMethod.IsAvailableCustomer(c, DateTime.Now.Year));
            customers = customers.Where(c => InMonthCustomer(c));
            var customerInMonths = new List<CustomerContractDashboardVM>();
            foreach (var customer in customers)
            {
                var contract = customer.Contracts.OrderBy(c => c.StartDate);
                var contractTelecom = customer.ContractTelecoms.OrderBy(c => c.DateStart);
                customerInMonths.Add(new CustomerContractDashboardVM
                {
                    ContractNo = contract.Count() != 0 ? contract.Last().ContractNo : null,
                    ContractTelecomNo = contractTelecom.Count() != 0 ? contractTelecom.Last().ContractNo : null,
                    Name = customer.Name,
                    //StartYear = customer.StartYear.Value
                });
            }
            return Ok(new
            {
                Customers = customerInMonths,
                NumberOfCustomer = customers.Count()
            });
        }

        [HttpGet("ContractExpire")]
        public ActionResult ContractExpire()
        {
            try
            {
                var currentYear = DateTime.Now;
                var contracts = _contractService.GetContracts(c => StaticMethod.IsAvailableCustomer(c.Customer, currentYear.Year)
                && StaticMethod.IsRunningContract(c)
                && IsAround30Days(c.EndDate.Value, false));
                var contractDashboardVMs = new List<ContractDashboardVM>();
                foreach (var contract in contracts)
                {
                    contractDashboardVMs.Add(new ContractDashboardVM
                    {
                        Name = contract.Customer.Name,
                        ContractNo = contract.ContractNo,
                        EndDate = contract.EndDate.Value
                    });
                }
                return Ok(contractDashboardVMs);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("ContractAdjustment")]
        public ActionResult ContractAdjustment()
        {
            try
            {
                var currentYear = DateTime.Now;
                var contracts = _contractService.GetContracts(c => StaticMethod.IsAvailableCustomer(c.Customer, currentYear.Year)
                && IsRunningContract(c));
                //&& c.DateEnd.Month == currentYear.Month
                //&& c.DateEnd.Year == currentYear.Year);
                var telecomContractDashboardVMs = new List<ContractDashboardVM>();
                //foreach (var contract in contracts)
                //{
                //    if (contract.ContractAppendices.Where(c => IsAround30Days(c.DateStart, false)).Count() > 0)
                //    {
                //        telecomContractDashboardVMs.Add(new ContractDashboardVM
                //        {
                //            Name = contract.Customer.Name,
                //            ContractNo = contract.ContractNo,
                //            EndDate = contract.EndDate.Value
                //        });
                //    }
                //}

                // điều chỉnh ngày tăng giá
                var dateFuture = DateTime.Now.AddYears(1000);
                foreach (var contract in contracts)
                {
                    var UpPriceDate1 = contract.UpPriceDate == null ? dateFuture : contract.UpPriceDate;
                    var UpPriceDate2 = contract.UpPriceDate_2 == null ? dateFuture : contract.UpPriceDate_2;
                    var UpPriceDate3 = contract.UpPriceDate_3 == null ? dateFuture : contract.UpPriceDate_3;
                    var UpPriceDate4 = contract.UpPriceDate_4 == null ? dateFuture : contract.UpPriceDate_4;

                    var UpPriceDate = UpPriceDate1 < UpPriceDate2 ? UpPriceDate1 : UpPriceDate2;
                    UpPriceDate = UpPriceDate < UpPriceDate3 ? UpPriceDate : UpPriceDate3;
                    UpPriceDate = UpPriceDate < UpPriceDate4 ? UpPriceDate : UpPriceDate4;


                    //if (IsAround30Days(UpPriceDate.Value, false))
                    //{
                    //    telecomContractDashboardVMs.Add(new ContractDashboardVM
                    //    {
                    //        Name = contract.Customer.Name,
                    //        ContractNo = contract.ContractNo,
                    //        EndDate = contract.EndDate.Value,
                    //    });
                    //}

                    if (IsAround30Days(UpPriceDate.Value, false))
                    {
                        telecomContractDashboardVMs.Add(new ContractDashboardVM
                        {
                            Name = contract.Customer.Name,
                            ContractNo = contract.ContractNo,
                            EndDate = UpPriceDate.Value,
                        });
                    }
                }
                return Ok(telecomContractDashboardVMs);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("ContractLiquidated")]
        public ActionResult ContractLiquidated(DateTime date)
        {
            try
            {
                var currentYear = DateTime.Now;
                var contracts = _contractService.GetContracts(c => StaticMethod.IsAvailableCustomer(c.Customer, currentYear.Year)
                && IsLiquidatedContract(c));
                var telecomContractDashboardVMs = new List<ContractDashboardVM>();
                foreach (var contract in contracts)
                {
                    telecomContractDashboardVMs.Add(new ContractDashboardVM
                    {
                        Name = contract.Customer.Name,
                        ContractNo = contract.ContractNo,
                        EndDate = contract.EndDate.Value
                    });
                }
                return Ok(telecomContractDashboardVMs);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("BuildingInventory")]
        public ActionResult GetBuildingInventory()
        {
            try
            {
                var result = new List<BuildingInventoryVM>();
                var builds = _buildingService.GetBuildings(b => !b.IsDeteled);
                var appendices = _appendixService.GetContractAppendixs(a => _appendixService.IsRunningAppendice(a, DateTime.Now)
                    && a.Type == (int)ContractAppendixType.AreaRent);
                builds.ToList().ForEach(b =>
                {
                    var appendicesByBuilding = appendices.Where(a => a.Building.ToLower().Contains(b.Name.ToLower()));
                    double usedSquare = appendicesByBuilding.Sum(a => a.Square.Value);
                    result.Add(new BuildingInventoryVM
                    {
                        Name = b.Name,
                        UnusedSquare = b.Square - usedSquare,
                        UsedSquare = usedSquare
                    });
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion


        #region P.VT
        #region Will delete
        public const string NSDV = "Ngân sách đơn vị";
        public const string NSTT = "Ngân sách tập trung";
        public const string DSDN = "Doanh số doanh nghiệp";
        //public const string DTDN = "Doanh thu dự án";
        public const string DSIS = "Doanh số bên trong công viên";
        public const string DSOS = "Doanh số bên ngoài công viên";
        public List<CommonTelecomVM> COMMONTELECOMSERVICEVMS_KHONG;
        public List<CommonTelecomVM> COMMONTELECOMSERVICEVMS_ALL;
        public CommonTelecomVM COMMONTELECOMSERVICEVMS_TAPTRUNG;
        public CommonTelecomVM COMMONTELECOMSERVICEVMS_DONVI;
        //public List<CommonTelecomVM> BRANCH_COMMONTELECOMSERVICE; //branchCommonTelecomservice
        public Dictionary<Guid, decimal> TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG;
        public Dictionary<Guid, decimal> TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL;
        //public class CommonTelecomVM
        //{
        //    public CommonTelecomVM()
        //    {
        //        ChildCommons = new List<CommonTelecomVM>();
        //    }
        //    public CommonTelecomViewModel commonTelecomservice { get; set; }
        //    public decimal ImplementRevenue { get; set; }
        //    public decimal RegisterRevenue { get; set; }
        //    public List<CommonTelecomVM> ChildCommons { get; set; }
        //}
        //public class CommonTelecomViewModel
        //{
        //    public Guid Id { get; set; }
        //    public string Name { get; set; }
        //}
        #endregion

        public class ContractTelecomAppendixRevenueVM
        {
            public ContractTelecomAppendix ContractTelecomAppendix { get; set; }
            public decimal ImplementRevenue { get; set; }
        }
        public decimal GetDictionaryValue<T>(Dictionary<T, decimal> dict)
        {
            decimal result = 0;
            foreach (KeyValuePair<T, decimal> t in dict)
            {
                result += t.Value;
            }
            return result;
        }


        /// <summary>
        /// Get total month of contractTelecomAppendix
        /// </summary>
        /// <param name="appendix"></param>
        /// <returns></returns>
        private double GetTotalMonthOfContractTelecomAppendix(ContractTelecomAppendix appendix, int? currentYear)
        {

            var datetime = DateTime.Now;
            var dateStartContract = appendix.DateAccept.Value;
            if (currentYear != null) //current != null
            {
                if (currentYear > datetime.Year) // current > 2020
                {
                    throw new Exception("Maximum of year is : " + datetime.Year);
                }
                if (currentYear.Value < datetime.Year) // current < now (2TH)
                {
                    return currentYear.Value > dateStartContract.Year ?
                        DateTime.IsLeapYear(currentYear.Value) ? 366 : 365 : //start < current
                        new DateTime(dateStartContract.Year, 12, 31).Date.Subtract(dateStartContract).TotalDays / 30; //start = current
                }
                if (currentYear.Value == datetime.Year) //current = now (6TH)
                {
                    if (appendix.DateEnd != null)
                    {
                        if (datetime.Date > appendix.DateEnd) // now > end =====> end == current
                        {
                            return currentYear.Value > dateStartContract.Year ?
                                appendix.DateEnd.Value.Date.Subtract(new DateTime(currentYear.Value, 1, 1)).TotalDays / 30 : //now > end > start
                                appendix.DateEnd.Value.Date.Subtract(dateStartContract.Date).TotalDays / 30; //now > end == start
                        }
                        // end > now =======> chung CT vs now == current
                    }
                    return currentYear.Value > dateStartContract.Year ?
                            datetime.Date.Subtract(new DateTime(currentYear.Value, 1, 1)).TotalDays / 30 : //current == now > start // end(date) > now(date) > start(year) 
                            datetime.Date.Subtract(appendix.DateStart).TotalDays / 30; //current == now == start // end(date) > now(date) == start(year) 
                }
                throw new Exception("GetTotalMonthOfContractTelecomAppendix ERROR!!!");
            }
            else
            {
                //var currentDate = DateTime.Now.Date;
                //var startYearDate = new DateTime(currentDate.Year, 01, 01).Date;

                ////Find DateStartReal and DateEndReal
                //var DateStartReal = dateStartContract < startYearDate ? startYearDate : dateStartContract;
                //var DateEndReal = contract.DateEnd != null ? (contract.DateEnd < currentDate ? contract.DateEnd.Value : currentDate) : currentDate;

                //double month = 0;
                ////DateEnd DateStart in 1 month
                //if (DateStartReal.Month == DateEndReal.Month)
                //{
                //    return (double)(DateEndReal.Day - DateStartReal.Day) / 30;
                //}
                //if (DateStartReal != startYearDate)
                //{
                //    var DateStartOfMonth = DateTime.DaysInMonth(currentDate.Year, DateStartReal.Month) - DateStartReal.Day + 1;
                //    month += (double)DateStartOfMonth / 30;
                //}
                //month += (double)DateEndReal.Day / 30;
                //month += (double)DateEndReal.Month - DateStartReal.Month - 1;
                //return month;


                var contractVT = appendix.ContractTelecom;
                var current = datetime;
                var endYear = new DateTime(current.Year, 12, 30);
                var firstYear = new DateTime(current.Year, 1, 1);
                if (appendix.DateAccept == null ||
                        appendix.DateStart > endYear ||
                        appendix.DateEnd < firstYear) return 0;
                DateTime dateEnd = endYear;
                double result = 0;

                // sửa đổi doanh thu tính theo ngày kết thúc dịch vụ 
                if (contractVT == null)//TH cua HD HT
                {
                    dateEnd = appendix.DateEnd != null ? appendix.DateEnd.Value : endYear;
                }
                else
                {
                    if (contractVT.DateEnd == null && appendix.DateEnd == null)
                    {
                        dateEnd = endYear;
                    }

                    if (contractVT.DateEnd != null)
                    {
                        dateEnd = contractVT.DateEnd;
                    }
                    if (appendix.DateEnd != null)
                    {
                        dateEnd = appendix.DateEnd.Value;
                    }
                }
                // hết sửa đổi 

                if (dateEnd > endYear)
                {
                    dateEnd = endYear;
                }
                var dateStart = appendix.DateAccept.Value > firstYear ? appendix.DateAccept.Value : firstYear;

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
                    result += (double)period;
                }
                return (double)result / 30;
            }
            //current == null
        }


        #region GetImplementRevenueOfContractTelecomAppendix
        /// <summary>
        /// Get Implement Revenue of contractAppendix with totalmonth Condition
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        private ContractTelecomAppendixRevenueVM GetImplementRevenueOfContractTelecomAppendix(ContractTelecomAppendix contract, int? currentYear)
        {
            decimal implemenprice = 0;
            var registerRevenue = new Dictionary<Guid, decimal>();
            var totalMonth = GetTotalMonthOfContractTelecomAppendix(contract, currentYear);
            contract.TelecomserviceContractAppendices.ToList().ForEach(c =>
            {
                var totalMonthService = totalMonth;
                if (c.DateEnd != null)
                {
                    totalMonthService = GetTotalMonthOfContractTelecomAppendix(new ContractTelecomAppendix() { DateStart = contract.DateStart, DateAccept = contract.DateAccept, DateEnd = c.DateEnd }, null);
                }
                implemenprice += c.UnitAmount * decimal.Parse(totalMonthService.ToString());
            });
            return new ContractTelecomAppendixRevenueVM
            {
                ContractTelecomAppendix = contract,
                ImplementRevenue = implemenprice
            };
        }
        #endregion

        private void GetListCommonTelecomservice()
        {
            #region TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL
            #region PRICE of serrvice Type 0 (HD VT)
            var telecomAppendices_all = _contractTelecomAppendixService.GetContractTelecomAppendixs(_ => _contractTelecomAppendixService.IsRunningAppendice(_, DateTime.Now.Year));
            TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL = new Dictionary<Guid, decimal>();
            telecomAppendices_all.ToList().ForEach(c =>
            {
                //appendixMonthData[c.Id] = c.DateEnd != null ? c.DateEnd.Value.Subtract(c.DateStart).TotalDays / 30 : DateTime.Now.Date.Subtract(c.DateStart).TotalDays / 30;
                var totalMonthOfAppendix = GetTotalMonthOfContractTelecomAppendix(c, null);

                //Run List PLTelecom
                c.TelecomserviceContractAppendices.ToList().ForEach(subTable =>
                {
                    double totalMonthImplementService = totalMonthOfAppendix;
                    if (subTable.DateEnd != null)
                    {
                        var PL = new ContractTelecomAppendix { DateAccept = c.DateAccept, DateEnd = subTable.DateEnd };
                        totalMonthImplementService = GetTotalMonthOfContractTelecomAppendix(PL, null);
                    }
                    decimal val = 0;
                    if (TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL.TryGetValue(subTable.TelecomserviceId, out val))
                    {
                        TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL[subTable.TelecomserviceId] += subTable.UnitAmount * decimal.Parse(totalMonthImplementService.ToString());
                    }
                    else
                    {
                        TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL.TryAdd(subTable.TelecomserviceId, subTable.UnitAmount * decimal.Parse(totalMonthImplementService.ToString()));
                    }
                });
            });
            //foreach(var c in telecomAppendices_all.ToList()) { 
            //    //appendixMonthData[c.Id] = c.DateEnd != null ? c.DateEnd.Value.Subtract(c.DateStart).TotalDays / 30 : DateTime.Now.Date.Subtract(c.DateStart).TotalDays / 30;
            //    var totalMonthOfAppendix = GetTotalMonthOfContractTelecomAppendix(c, null);

            //    //Run List PLTelecom
            //    foreach(var subTable in c.TelecomserviceContractAppendices.ToList())
            //    {
            //        double totalMonthImplementService = totalMonthOfAppendix;
            //        if (subTable.DateEnd != null)
            //        {
            //            var PL = new ContractTelecomAppendix { DateAccept = c.DateAccept, DateEnd = subTable.DateEnd };
            //            totalMonthImplementService = GetTotalMonthOfContractTelecomAppendix(PL, null);
            //        }
            //        decimal val = 0;
            //        if (TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL.TryGetValue(subTable.TelecomserviceId, out val))
            //        {
            //            TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL[subTable.TelecomserviceId] += subTable.UnitAmount * decimal.Parse(totalMonthImplementService.ToString());
            //        }
            //        else
            //        {
            //            TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL.TryAdd(subTable.TelecomserviceId, subTable.UnitAmount * decimal.Parse(totalMonthImplementService.ToString()));
            //        }
            //    };
            //};
            #endregion

            #region PRICE off service Type 1 (HD HT)
            var subCoContracts = _subCoContractService.GetSubCoContracts(_ => _subCoContractService.IsContractOfYear(_, DateTime.Now.Year));
            subCoContracts.ToList().ForEach(subCoContract =>
            {
                var totalMonth = GetTotalMonthOfContractTelecomAppendix(new ContractTelecomAppendix { DateAccept = subCoContract.DateStart, DateEnd = subCoContract.DateEnd }, null);
                subCoContract.SubServices.ToList().ForEach(subServiceItem =>
                {
                    decimal val = 0;
                    var persent = (double)_coContractTelServiceService.GetCoContractTelService(subServiceItem.CoContractTelServiceId).Percentage / 100;
                    var price = subServiceItem.Amount * decimal.Parse(totalMonth.ToString()) * (decimal)persent;
                    if (TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL.TryGetValue(subServiceItem.ServiceId, out val))
                    {
                        TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL[subServiceItem.ServiceId] += price;
                    }
                    else
                    {
                        TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL.TryAdd(subServiceItem.ServiceId, price);
                    }
                });
            });
            #endregion
            #endregion

            #region COMMONTELECOMSERVICEVMS_ALL
            COMMONTELECOMSERVICEVMS_ALL = new List<CommonTelecomVM>();
            var commons = _commonTelecomserviceService.GetCommonTelecomservices().OrderBy(c => c.Type);
            foreach (var common in commons)
            {
                var commonVM = common.Adapt<CommonTelecomVM>();
                decimal implement = 0;
                foreach (var service in common.Telecomservices)
                {
                    var serviceVM = service.Adapt<TelecomserviceRevenueVM>();
                    decimal val = 0;
                    if (TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL.TryGetValue(serviceVM.Id, out val))
                    {
                        serviceVM.ImplementRevenue = TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL[serviceVM.Id];
                        implement += TELECOMSERVICE_PRICES_INVESMENTTYPE_ALL[serviceVM.Id];
                    }

                    commonVM.Services.Add(serviceVM);
                }
                commonVM.RevenueRegister = common.Telecomservices.Sum(c => c.RevenueRegister);
                commonVM.ImplementRevenue = implement;
                COMMONTELECOMSERVICEVMS_ALL.Add(commonVM);
            }
            #endregion

            #region TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG
            var telecomAppendices = _contractTelecomAppendixService.GetContractTelecomAppendixs(_ => _.ContractTelecom.TypeInvestment == (int)TypeInvestment.KHONG
            && _contractTelecomAppendixService.IsRunningAppendice(_, DateTime.Now.Year));
            TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG = new Dictionary<Guid, decimal>();
            telecomAppendices.ToList().ForEach(c =>
            {
                //appendixMonthData[c.Id] = c.DateEnd != null ? c.DateEnd.Value.Subtract(c.DateStart).TotalDays / 30 : DateTime.Now.Date.Subtract(c.DateStart).TotalDays / 30;
                var totalMonthOfAppendix = GetTotalMonthOfContractTelecomAppendix(c, null);

                //Run List PLTelecom
                c.TelecomserviceContractAppendices.ToList().ForEach(subTable =>
                {
                    double totalMonthImplementService = totalMonthOfAppendix;
                    if (subTable.DateEnd != null)
                    {
                        var PL = new ContractTelecomAppendix { DateAccept = c.DateAccept, DateEnd = subTable.DateEnd };
                        totalMonthImplementService = GetTotalMonthOfContractTelecomAppendix(PL, null);
                    }
                    decimal val = 0;
                    if (TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG.TryGetValue(subTable.TelecomserviceId, out val))
                    {
                        TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG[subTable.TelecomserviceId] += subTable.UnitAmount * decimal.Parse(totalMonthImplementService.ToString());
                    }
                    else
                    {
                        TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG.TryAdd(subTable.TelecomserviceId, subTable.UnitAmount * decimal.Parse(totalMonthImplementService.ToString()));
                    }
                });
            });
            #endregion

            #region COMMONTELECOMSERVICEVMS_KHONG
            COMMONTELECOMSERVICEVMS_KHONG = new List<CommonTelecomVM>();
            var commons_of_investmentContract_khong = _commonTelecomserviceService.GetCommonTelecomservices().OrderBy(c => c.Type);
            foreach (var common in commons_of_investmentContract_khong)
            {
                var commonVM = common.Adapt<CommonTelecomVM>();
                decimal implement = 0;
                foreach (var service in common.Telecomservices)
                {
                    var serviceVM = service.Adapt<TelecomserviceRevenueVM>();
                    decimal val = 0;
                    if (TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG.TryGetValue(serviceVM.Id, out val))
                    {
                        serviceVM.ImplementRevenue = TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG[serviceVM.Id];
                        implement += TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG[serviceVM.Id];
                    }

                    commonVM.Services.Add(serviceVM);
                }
                commonVM.RevenueRegister = common.Telecomservices.Sum(c => c.RevenueRegister);
                commonVM.ImplementRevenue = implement;
                COMMONTELECOMSERVICEVMS_KHONG.Add(commonVM);
            }
            #endregion

            #region BRANCH_COMMONTELECOMSERVICE and COMMONTELECOMSERVICEVMS
            #region Will delete
            //    #region BRANCH_COMMONTELECOMSERVICE
            //    BRANCH_COMMONTELECOMSERVICE = new List<CommonTelecomVM>();
            //    var commonTelecomservices = _commonTelecomserviceService.GetCommonTelecomservices();
            //    var telecomservices = _telecomserviceService.GetTelecomservices();
            //    var commons = commonTelecomservices.ToList();
            //    commons.ForEach(c =>
            //    {
            //        if (commonTelecomservices.Where(a => a.ParentId == c.Id).Count() == 0)
            //        {
            //            var commonTelecomVM = new CommonTelecomVM
            //            {
            //                commonTelecomservice = c.Adapt<CommonTelecomViewModel>()
            //            };
            //            var telecoms = telecomservices.Where(t => t.CommonTelecomserviceId != null ? t.CommonTelecomserviceId == commonTelecomVM.commonTelecomservice.Id : false);
            //            foreach (var t in telecoms)
            //            {
            //                decimal val = 0;
            //                if (TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG.TryGetValue(t.Id, out val))
            //                {
            //                    commonTelecomVM.ImplementRevenue += TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG[t.Id];
            //                }
            //            }
            //            BRANCH_COMMONTELECOMSERVICE.Add(commonTelecomVM);
            //        }
            //    });
            //    #endregion

            //    #region COMMONTELECOMSERVICEVMS
            //    var telecomservices = _telecomserviceService.GetTelecomservices();
            //    commonTelecomservices = _commonTelecomserviceService.GetCommonTelecomservices(c => c.ParentId == null
            //    && !c.Name.ToLower().Contains(NSDV.ToLower())
            //    && !c.Name.ToLower().Contains(NSTT.ToLower()));
            //    if (COMMONTELECOMSERVICEVMS == null)
            //    {
            //        #region TypeInvesment == KHONG
            //        COMMONTELECOMSERVICEVMS = new List<CommonTelecomVM>();
            //        foreach (var commonTelecom in commonTelecomservices.ToList())
            //        {
            //            var commonVM = new CommonTelecomVM
            //            {
            //                commonTelecomservice = commonTelecom.Adapt<CommonTelecomViewModel>()
            //            };
            //            commonVM.ChildCommons = GetChildCommon(commonVM, commonTelecomservices, telecomservices, telecomservicePrices);
            //            commonVM.ChildCommons = GetChildCommon(commonVM, telecomservices, TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG);
            //            commonVM.Price = ChildPrice(commonTelecom, telecomservices, telecomservicePrices);
            //            COMMONTELECOMSERVICEVMS.Add(commonVM);
            //        }
            //        #endregion
            #endregion
            #region TypeInvesment = TAP TRUNG
            var contractAppendix_taptrung = _contractTelecomAppendixService.GetContractTelecomAppendixs(_ => _.ContractTelecom.TypeInvestment == (int)TypeInvestment.TAPTRUNG
            && _contractTelecomAppendixService.IsRunningAppendice(_, DateTime.Now.Year));
            decimal implemenprice = 0;
            //var registerRevenue = new Dictionary<Guid, decimal>();
            contractAppendix_taptrung.ToList().ForEach(_c =>
            {
                implemenprice += GetImplementRevenueOfContractTelecomAppendix(_c, null).ImplementRevenue;
                //_c.TelecomserviceContractAppendices.ToList().ForEach(c =>
                //{
                //     decimal val = 0;
                //    if (!registerRevenue.TryGetValue(c.TelecomserviceId, out val))
                //    {
                //        registerRevenue[c.TelecomserviceId] = c.Telecomservice.RevenueRegister;
                //    }
                //});
            });
            //var common_taptrung = _commonTelecomserviceService.GetCommonTelecomservices(_ => _.Name.ToLower().Contains(NSTT.ToLower())).FirstOrDefault();
            COMMONTELECOMSERVICEVMS_TAPTRUNG = new CommonTelecomVM
            {
                Name = NSTT,
                ImplementRevenue = implemenprice,
                //RegisterRevenue = GetDictionaryValue<Guid>(registerRevenue)
                RevenueRegister = 0
            };
            #endregion
            #region TypeInvesment = DON VI
            var contractAppendix_donvi = _contractTelecomAppendixService.GetContractTelecomAppendixs(_ => _.ContractTelecom.TypeInvestment == (int)TypeInvestment.DONVI
            && _contractTelecomAppendixService.IsRunningAppendice(_, DateTime.Now.Year));
            //registerRevenue = new Dictionary<Guid, decimal>();
            implemenprice = 0;
            contractAppendix_donvi.ToList().ForEach(_c =>
            {
                implemenprice += GetImplementRevenueOfContractTelecomAppendix(_c, null).ImplementRevenue;
                //_c.TelecomserviceContractAppendices.ToList().ForEach(c =>
                //{
                //    decimal val = 0;
                //    if (!registerRevenue.TryGetValue(c.TelecomserviceId, out val))
                //    {
                //        registerRevenue[c.TelecomserviceId] = c.Telecomservice.RevenueRegister;
                //    }
                //});
            });
            var commonTelecom_donvi = _commonTelecomserviceService.GetCommonTelecomservices(_ => _.Name.ToLower().Contains(NSDV.ToLower())).FirstOrDefault();
            COMMONTELECOMSERVICEVMS_DONVI = new CommonTelecomVM
            {
                Name = NSDV,
                ImplementRevenue = implemenprice,
                //RegisterRevenue = GetDictionaryValue<Guid>(registerRevenue)
                RevenueRegister = 0
            };
            #endregion
            #endregion
        }






















        //private List<CommonTelecomVM> GetChildCommon(CommonTelecomVM commonTelecomVM,
        //    //IEnumerable<CommonTelecomservice> commonTelecomservices,
        //    IEnumerable<Telecomservice> telecomservices, Dictionary<Guid, decimal> telecomservicePrices)
        //{
        //    var childs = _commonTelecomserviceService.GetCommonTelecomservices(c => c.ParentId != null ? c.ParentId == commonTelecomVM.commonTelecomservice.Id : false);
        //    decimal implementRevenue = 0;
        //    decimal registerRevenue = 0;
        //    if (childs.Count() > 0)
        //    {
        //        var result = new List<CommonTelecomVM>();
        //        foreach (var c in childs) //3 thang cua so 2
        //        {
        //            var comonVM = new CommonTelecomVM
        //            {
        //                commonTelecomservice = c.Adapt<CommonTelecomViewModel>()

        //            };
        //            //comonVM.ChildCommons = GetChildCommon(comonVM, commonTelecomservices, telecomservices,telecomPrice);
        //            comonVM.ChildCommons = GetChildCommon(comonVM, telecomservices, telecomservicePrices);
        //            result.Add(comonVM);
        //            implementRevenue += comonVM.ImplementRevenue;
        //            registerRevenue += comonVM.RegisterRevenue;
        //            //commonTelecomVM.ChildCommons = GetChildCommon(c, commonTelecomservices);
        //        }
        //        commonTelecomVM.ImplementRevenue = implementRevenue;
        //        commonTelecomVM.RegisterRevenue = registerRevenue;
        //        return result;
        //    }
        //    else
        //    {
        //        var telecom = telecomservices.Where(c => c.CommonTelecomserviceId != null ? c.CommonTelecomserviceId == commonTelecomVM.commonTelecomservice.Id : false);
        //        foreach (var t in telecom)
        //        {
        //            decimal val = 0;
        //            if (telecomservicePrices.TryGetValue(t.Id, out val))
        //            {
        //                commonTelecomVM.ImplementRevenue += telecomservicePrices[t.Id];
        //            }
        //            commonTelecomVM.RegisterRevenue = t.RevenueRegister;
        //        }
        //        return null;
        //    }


        private bool IsAvailableContractTelecomAppendix(ContractTelecomAppendix contractTelecomAppendix, int currentYear)
        {
            var result = contractTelecomAppendix.DateStart.Year <= currentYear;
            result = result && (contractTelecomAppendix.DateEnd != null ? contractTelecomAppendix.DateEnd.Value.Year >= currentYear : true);
            return result;
        }

        [HttpGet("GetCompleteRate")]
        public ActionResult GetCompleteRate()
        {
            return Ok(COMMONTELECOMSERVICEVMS_ALL);
        }


        [HttpGet("ProportionOfTelecommunicationRevenue")]
        public ActionResult ProportionOfTelecommunicationRevenue()
        {
            //Lay cac branchCommonTelecomservice
            var mapping = new Dictionary<string, decimal>(); //CommonName, ImplementRevenue
            string error = "";
            foreach (var common in COMMONTELECOMSERVICEVMS_KHONG)
            {
                foreach (var service in common.Services)
                {
                    mapping[service.Name] = service.ImplementRevenue;
                }
            }

            mapping[NSTT] = COMMONTELECOMSERVICEVMS_TAPTRUNG.ImplementRevenue;
            mapping[NSDV] = COMMONTELECOMSERVICEVMS_DONVI.ImplementRevenue;

            //Reponse Data
            if (String.IsNullOrEmpty(error))
            {
                var rs = mapping.Select(m => new
                {
                    CommonTelecomservice = m.Key,
                    ImplementRevenue = m.Value,
                }).ToList();
                return Ok(rs);
            }
            else
            {
                return BadRequest(new { Error = "Can't DeserializeObject : " + error });
            }
        }

        [HttpGet("ProportionOfParentTelecommunicationRevenue")]
        public ActionResult ProportionOfParentTelecommunicationRevenue()
        {
            try
            {
                var mapping = new Dictionary<string, decimal>(); //CommonName, ImplementRevenue
                string error = "";

                decimal implementPrice = 0;
                foreach (KeyValuePair<Guid, decimal> t in TELECOMSERVICE_PRICES_INVESMENTTYPE_KHONG)
                {
                    implementPrice += t.Value;
                }
                mapping[DSDN] = implementPrice;
                mapping[NSTT] = COMMONTELECOMSERVICEVMS_TAPTRUNG.ImplementRevenue;
                mapping[NSDV] = COMMONTELECOMSERVICEVMS_DONVI.ImplementRevenue;

                var rs = mapping.Select(m => new
                {
                    CommonTelecomservice = m.Key,
                    ImplementRevenue = m.Value,
                }).ToList();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return Ok(ex);
            }

        }

        [HttpGet("ProportionOfTelecommunicationRevenueInsideOutside")]
        public ActionResult GetProportionOfTelecommunicationRevenueInsideOutside()
        {
            var mapping = new Dictionary<string, decimal>();
            var telecomAppendices_inside = _contractTelecomAppendixService.GetContractTelecomAppendixs(_ => IsInsideCustomer(_.ContractTelecom.Customer));
            var telecomAppendices_outside = _contractTelecomAppendixService.GetContractTelecomAppendixs(_ => !IsInsideCustomer(_.ContractTelecom.Customer));
            decimal implementRevenueInside = 0;
            decimal implementRevenueOutside = 0;
            telecomAppendices_inside.ToList().ForEach(t =>
            {
                implementRevenueInside += t.TelecomserviceContractAppendices.Sum(s => s.UnitAmount);
            });
            mapping[DSIS] = implementRevenueInside;

            telecomAppendices_outside.ToList().ForEach(t =>
            {
                implementRevenueOutside += t.TelecomserviceContractAppendices.Sum(s => s.UnitAmount);
            });
            mapping[DSOS] = implementRevenueOutside;
            var rs = mapping.Select(m => new
            {
                CommonTelecomservice = m.Key,
                ImplementRevenue = m.Value,
            }).ToList();
            return Ok(rs);
        }

        [HttpGet("IncreaseRate")]
        public ActionResult GetIncreaseRate(int year = 2019)
        {
            var result = new List<TotalContractTelecomRevenueVM>();
            var contractTelecomAppendices = _contractTelecomAppendixService.GetContractTelecomAppendixs();
            for (int currentYear = year; currentYear <= DateTime.Now.Year; currentYear++)
            {
                var contractTelecomAppendicesByYear = contractTelecomAppendices.Where(c => IsAvailableContractTelecomAppendix(c, currentYear));
                decimal total = 0;

                foreach (var currentContractAppendix in contractTelecomAppendicesByYear)
                {
                    total += GetImplementRevenueOfContractTelecomAppendix(currentContractAppendix, currentYear).ImplementRevenue;
                }


                result.Add(new TotalContractTelecomRevenueVM
                {
                    Year = currentYear.ToString(),
                    Total = total
                });
            }
            return Ok(result);
        }

        [HttpGet("IncreaseOutsideRate")]
        public ActionResult GetIncreaseOutsideRate(int year = 2019)
        {
            var result = new List<TotalContractTelecomRevenueVM>();
            var contractTelecomAppendices = _contractTelecomAppendixService.GetContractTelecomAppendixs();
            for (int currentYear = year; currentYear <= DateTime.Now.Year; currentYear++)
            {
                var contractTelecomAppendicesByYear = contractTelecomAppendices.Where(c => IsAvailableContractTelecomAppendix(c, currentYear) && !IsInsideCustomer(c.ContractTelecom.Customer));
                decimal total = 0;

                foreach (var currentContractAppendix in contractTelecomAppendicesByYear)
                {
                    total += GetImplementRevenueOfContractTelecomAppendix(currentContractAppendix, currentYear).ImplementRevenue;
                }

                result.Add(new TotalContractTelecomRevenueVM
                {
                    Year = currentYear.ToString(),
                    Total = total
                });
            }
            return Ok(result);
        }
        #endregion


    }
}
