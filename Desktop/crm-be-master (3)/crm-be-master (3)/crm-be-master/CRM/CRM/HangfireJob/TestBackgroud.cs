
using CRM.Hubs;
using CRM.Model;
using CRM.Service;
using CRM.Utils;
using CRM.ViewModels;
using Hangfire;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CRM.HangfireJob
{
    public interface ITestBackgroud
    {
        void ExportCustomerHangfire(string name, string downloadURL, HsUser user);
        void NotiExpireContract();
        void InsertCustomers(List<Object> customers, string username);
        void CheckRequest(Guid customerWorkFlowId, string fullname);
        void CheckRequestVT(Guid customerWorkFlowId, string fullname);
        void ExtensionTelecomContract();
        void AddDashBoardChart();
    }
    public class TestBackgroud : ITestBackgroud
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerTypeService _customerTypeService;
        private readonly IErrorLogService _errorLogService;
        private readonly IContractService _contractService;
        private readonly IContractTelecomService _contractTelecomService;
        private readonly IContractTelecomAppendixService _contractTelecomAppendixService;
        private readonly UserManager<HsUser> _userManager;
        private readonly IEmailService _mailService;
        private readonly IWorkFlowHistoryService _workFlowHistoryService;
        private readonly IConfiguration _configuration;

        private readonly IHsNotificationService _notiService;
        private readonly IHubContext<CenterHub> _hubContext;
        private readonly IDashBoardChartService _dashBoardChartService;
        private readonly IHubUserConnectionService _hubService;
        private readonly ITransactionLogService _transactionLogService;
        

        public const string NĐT = "nhà đầu tư";
        public const string DNPM_1 = "doanh nghiệp phần mềm";
        public const string DNPM_2 = "ươm tạo";
        public const string DNPM_3 = "r&d";
        public const string DNPM_4 = "cntt";
        public const string DOMESTIC = "việt nam";
        public const string INSIDE = "trong công viên phần mềm quang trung";

        public TestBackgroud(IWorkFlowHistoryService workFlowHistoryService, ICustomerService customerService, ICustomerTypeService customerTypeService, IErrorLogService errorLogService, IContractService contractService, UserManager<HsUser> userManager, IEmailService mailService, IHsNotificationService notiService, IHubContext<CenterHub> hubContext, IHubUserConnectionService hubService, IContractTelecomService contractTelecomService, IContractTelecomAppendixService contractTelecomAppendixService, IDashBoardChartService dashBoardChartService, ITransactionLogService transactionLogService, IConfiguration configuration)
        {
            _customerService = customerService;
            _customerTypeService = customerTypeService;
            _errorLogService = errorLogService;
            _contractService = contractService;
            _contractTelecomService = contractTelecomService;
            _contractTelecomAppendixService = contractTelecomAppendixService;
            _userManager = userManager;
            _mailService = mailService;
            _notiService = notiService;
            _hubContext = hubContext;
            _hubService = hubService;
            _workFlowHistoryService = workFlowHistoryService;
            _dashBoardChartService = dashBoardChartService;
            _transactionLogService = transactionLogService;
            _configuration = configuration;
        }

        public void SendMail()
        {
            //_mailService.SendEmail("xhunter1412@gmail.com", "Test HangFire", "Confirm to test hangfire success !!!");
        }

        private String generateMessageForSendMail(String PhongBan, String fullName)
        {
            String dateCreate = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            string message = "Thông báo. \n" +
                        "Có một công việc thông báo đến quý vị để biết hoặc phối hợp thực hiện. \n" +
                        $"Tiêu đề: {PhongBan} nhận yêu cầu phối hợp\n" +
                        $"Người tạo: {fullName} \n" +
                        $"Ngày tạo: {dateCreate}.";
            return message;
        }

        //public void CheckRequest(Guid customerWorkFlowId, string fullname)
        //{
        //    var steps = _workFlowHistoryService.GetWorkFlowHistories().Where(_ => _.CustomerWorkFlowId.Equals(customerWorkFlowId)
        //                                                                    && _.PreviousStep.Equals(Guid.Parse("A29B4740-E9A2-48C2-A73F-08D6A4384626"))
        //                                                                    && _.Status == 1).ToList();
        //    if (steps.Any())
        //    {
        //        foreach (var step in steps)
        //        {
        //            string instanceIdString = step.InstanceId.ToString();
        //            String message = "";
        //            switch (instanceIdString.ToUpper())
        //            {
        //                case "D6566FEF-A111-4ADF-A742-08D6A4384626":
        //                    message = generateMessageForSendMail("P.HT&CSKH", fullname);
        //                    _mailService.SendEmail("phtcskh.test@qtsc.com.vn", "[P.HT&CSKH] Cooperation confirmation", message, null, null);
        //                    break;
        //                case "61F11388-555F-4C53-A743-08D6A4384626":
        //                    message = generateMessageForSendMail("P.KTDN", fullname);
        //                    _mailService.SendEmail("pktdn.test@qtsc.com.vn", "[P.KTDN] Cooperation confirmation", message, null, null);
        //                    break;
        //                case "2B2C9208-600D-4EB3-A744-08D6A4384626":
        //                    message = generateMessageForSendMail("TTVT", fullname);
        //                    _mailService.SendEmail("ttvt.test@qtsc.com.vn", "[TTVT] Cooperation confirmation", message, null, null);
        //                    break;
        //                case "15090338-03E0-4DCD-A745-08D6A4384626":
        //                    message = generateMessageForSendMail("P.KTTC", fullname);
        //                    _mailService.SendEmail("pkttc.test@qtsc.com.vn", "[P.KTTC] Cooperation confirmation", message, null, null);
        //                    break;
        //                case "919B760B-63BD-4A46-30EE-08D6D2B460DA":
        //                    message = generateMessageForSendMail("P.QLTN", fullname);
        //                    _mailService.SendEmail("pqltn.test@qtsc.com.vn", "[P.QLTN] Cooperation confirmation", message, null, null);
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //    }
        //}

        public void CheckRequest(Guid customerWorkFlowId, string fullname)
        {
            var steps = _workFlowHistoryService.GetWorkFlowHistories().Where(_ => _.CustomerWorkFlowId.Equals(customerWorkFlowId)
                                                                            && _.PreviousStep.Equals(Guid.Parse("A29B4740-E9A2-48C2-A73F-08D6A4384626"))
                                                                            && _.Status == 1).ToList();
            

            if (steps.Any())
            {
                foreach (var step in steps)
                {
                    string instanceIdString = step.InstanceId.ToString();
                    String message = "";
                    switch (instanceIdString.ToUpper())
                    {
                        case "D6566FEF-A111-4ADF-A742-08D6A4384626":
                            message = generateMessageForSendMail("P.HT&CSKH", fullname);
                            String emailPHT_CSKH = _configuration.GetValue<String>("Email:PHT&CSKH");
                            String[] emailListPHT_CSKH = emailPHT_CSKH.Split(new char[] { ',' });
                            _mailService.SendListEmail(emailListPHT_CSKH, "[P.HT&CSKH] Cooperation confirmation", message, null, null);
                            break;
                        case "61F11388-555F-4C53-A743-08D6A4384626":
                            message = generateMessageForSendMail("P.KTDN", fullname);
                            String emailPKTDN = _configuration.GetValue<String>("Email:PKTDN");
                            String[] emailListPKTDN = emailPKTDN.Split(new char[] { ',' });
                            _mailService.SendListEmail(emailListPKTDN, "[P.KTDN] Cooperation confirmation", message, null, null);
                            break;
                        case "2B2C9208-600D-4EB3-A744-08D6A4384626":
                            message = generateMessageForSendMail("TTVT", fullname);
                            String emailTTVT = _configuration.GetValue<String>("Email:TTVT");
                            String[] emailListTTVT = emailTTVT.Split(new char[] { ',' });
                            _mailService.SendListEmail(emailListTTVT, "[TTVT] Cooperation confirmation", message, null, null);
                            break;
                        case "15090338-03E0-4DCD-A745-08D6A4384626":
                            message = generateMessageForSendMail("P.KTTC", fullname);
                            String emailPKTTC = _configuration.GetValue<String>("Email:PKTTC");
                            String[] emailListKTTC = emailPKTTC.Split(new char[] { ',' });
                            _mailService.SendListEmail(emailListKTTC, "[P.KTTC] Cooperation confirmation", message, null, null);
                            break;
                        case "919B760B-63BD-4A46-30EE-08D6D2B460DA":
                            message = generateMessageForSendMail("P.QLTN", fullname);
                            String emailPQLTN = _configuration.GetValue<String>("Email:PQLTN");
                            String[] emailListPQLTN = emailPQLTN.Split(new char[] { ',' });
                            _mailService.SendListEmail(emailListPQLTN, "[P.QLTN] Cooperation confirmation", message, null, null);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void CheckRequestVT(Guid customerWorkFlowId, string fullname)
        {
            var steps = _workFlowHistoryService.GetWorkFlowHistories().Where(_ => _.CustomerWorkFlowId.Equals(customerWorkFlowId)
                                                                            && _.PreviousStep.Equals(Guid.Parse("BCB67654-1C3F-4003-8379-08D765BEB257"))
                                                                            && _.Status == 1).ToList();
            if (steps.Any())
            {
                foreach (var step in steps)
                {
                    string instanceIdString = step.InstanceId.ToString();
                    String message = "";
                    switch (instanceIdString.ToUpper())
                    {
                        case "36772E82-01A8-4B58-1706-08D71BC2FA47":
                            message = generateMessageForSendMail("PM", fullname);
                            String emailPM = _configuration.GetValue<String>("Email:TTVT-QT20:PM");
                            String[] emailListPM = emailPM.Split(new char[] { ',' });
                            _mailService.SendListEmail(emailListPM, "PM Thực hiện hợp đồng", message, null, null);
                            break;
                        case "BB873E9A-1A34-4F4E-1707-08D71BC2FA47":
                            message = generateMessageForSendMail("BP. KTVT", fullname);
                            String emailKTVT = _configuration.GetValue<String>("Email:TTVT-QT20:KyThuat");
                            String[] emailListKTVT = emailKTVT.Split(new char[] { ',' });
                            _mailService.SendListEmail(emailListKTVT, "BP. KTVT Thực hiện hợp đồng", message, null, null);
                            break;
                        case "7E279DFE-BAEF-4EC6-1708-08D71BC2FA47":
                            message = generateMessageForSendMail("BP. QLHT", fullname);
                            String emailQLHT = _configuration.GetValue<String>("Email:TTVT-QT20:QLHT");
                            String[] emailListQLHT = emailQLHT.Split(new char[] { ',' });
                            _mailService.SendListEmail(emailListQLHT, "BP. QLHT Thực hiện hợp đồng", message, null, null);
                            break;
                        case "0A394EB3-2204-4758-1709-08D71BC2FA47":
                            message = generateMessageForSendMail("NOC", fullname);
                            String emailNOC = _configuration.GetValue<String>("Email:TTVT-QT20:NOC");
                            String[] emailListNOC = emailNOC.Split(new char[] { ',' });
                            _mailService.SendListEmail(emailListNOC, "NOC Thực hiện hợp đồng", message, null, null);
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        public void InsertCustomers(List<Object> customers, string username)
        {
            //var result = new List<ErrorList>();
            bool first = true;
            string jobId = null;
            jobId = BackgroundJob.Enqueue(() => BackgroundInsertCustomers(customers, username));
            while (customers.Count > 20)
            {
                var cs = customers.Take(20).ToList();
                if (first)
                {
                    jobId = BackgroundJob.Enqueue(() => BackgroundInsertCustomers(cs, username));
                }
                else
                {
                    jobId = BackgroundJob.ContinueWith(
                        jobId,
                        () => BackgroundInsertCustomers(cs.ToList(), username));
                }
                customers.RemoveRange(0, 20);
                first = false;
            }
            if (jobId == null)
            {
                jobId = BackgroundJob.Enqueue(() => BackgroundInsertCustomers(customers.ToList(), username));
            }
            else
            {
                BackgroundJob.ContinueWith(
                        jobId,
                        () => BackgroundInsertCustomers(customers.ToList(), username));
            }
        }
        public void BackgroundInsertCustomers(List<Object> customers, string username)
        {
            foreach (var _item in customers)
            {
                var item = new CustomerCML();
                try
                {
                    item = JsonConvert.DeserializeObject<CustomerCML>(_item.ToString());
                    var customer = item.Adapt<Customer>();
                    customer.CustomerTypeId = _customerTypeService.GetCustomerTypes().FirstOrDefault().Id;
                    customer.IsReal = true;
                    _customerService.CreateCustomer(customer, username);
                }
                catch (Exception e)
                {
                    CreateErrorBackground(e, _item);
                }
            }
        }

        public void UpdateCustomers(Expression<Func<Customer, bool>> where, string username)
        {
            var cusIds = _customerService.GetCustomers(where).Select(c => c.Id).ToList();
            bool first = true;
            string jobId = null;
            while (cusIds.Count > 20)
            {
                var ids = cusIds.Take(20).ToList();
                if (first)
                {
                    jobId = BackgroundJob.Enqueue(() => BackgroundUpdateCustomers(ids, username));
                }
                else
                {
                    jobId = BackgroundJob.ContinueWith(
                        jobId,
                        () => BackgroundUpdateCustomers(ids, username));
                }
                cusIds.RemoveRange(0, 20);
                first = false;
            }
            if (jobId == null)
            {
                jobId = BackgroundJob.Enqueue(() => BackgroundUpdateCustomers(cusIds.ToList(), username));
            }
            else
            {
                BackgroundJob.ContinueWith(
                        jobId,
                        () => BackgroundUpdateCustomers(cusIds.ToList(), username));
            }
        }

        /// <summary>
        /// Recommed : 20 elements in list
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="username"></param>
        public void BackgroundUpdateCustomers(List<Guid> ids, string username)
        {
            foreach (var id in ids)
            {
                var item = new CustomerDetailVM();
                try
                {
                    var customer = _customerService.GetCustomer(id);
                    var oldCustomer = customer.Adapt<CustomerDetailVM>().Adapt<Customer>();

                    var personnel = customer.Personnel;
                    personnel.TotalEmployeeInSide = personnel.TotalEmployee;

                    _customerService.EditCustomer(customer, oldCustomer, username);
                    _customerService.SaveCustomer();
                }
                catch (Exception e)
                {
                    CreateErrorBackground(e, id);
                }
            }
        }

        /// <summary>
        /// Create errorLog when run background tasks
        /// </summary>
        /// <param name="e"></param>
        /// <param name="item"></param>
        public void CreateErrorBackground(Exception e, object item)
        {
            var cusError = JsonConvert.DeserializeObject<CustomerError>(item.ToString());
            _errorLogService.CreateErrorLog(new ErrorLog
            {
                Error = e.Message + String.Format("\t Name : {0}, ShortName : {1}, Country: {2}", cusError.Name, cusError.ShortName, cusError.Country),
                Location = this.ToString(),
                When = DateTime.Now
            });
            _errorLogService.SaveChange();
        }

        public void NotiExpireContract()
        {
            _mailService.SendEmail("huynguyen0257@gmail.com", "Run NotiExpireContract", " have been execured on " + DateTime.Now, null, null);
            var currentYear = DateTime.Now;
            var contracts = _contractService.GetContracts(c => StaticMethod.IsAvailableCustomer(c.Customer, currentYear.Year)
            && StaticMethod.IsRunningContract(c)
            && StaticMethod.IsExpireContract(c.EndDate.Value));
            //_errorLogService.CreateErrorLog(new ErrorLog
            //{
            //    When = DateTime.Now,
            //    Location = "TestBackground.NotiExpireContract",
            //    Error = "Run check ExpireContract!!!"
            //});
            //_errorLogService.SaveChange();
            foreach (var contract in contracts)
            {
                var title = "Contract Code " + contract.ContractNo + ": Have 30days left to expire";
                NotificationCM noti = new NotificationCM
                {
                    Title = title,
                    Type = "info",
                    Body = "Nhap vao de xem chi tiet hop dong",
                    NData = new
                    {
                        url = "/contract/" + contract.Id,
                        type = 0,
                    }
                };
                var users = _userManager.GetUsersInRoleAsync("P.KD").Result.ToList();
                SendNotiAsync(users, noti);
            }
        }
        public void SendNotiAsync(List<HsUser> users, NotificationCM model)
        {
            List<string> connections = new List<string>();
            try
            {

                foreach (var userId in users.Select(_ => _.Id))
                {
                    try
                    {
                        var notification = StaticMethod.CreateHsNotification(userId, model);
                        _notiService.CreateHsNotification(notification);
                        _notiService.SaveHsNotification();

                        connections = connections.Union(_hubService.GetHubUserConnections(_ => _.UserId.Equals(userId))
                                        .Select(_ => _.Connection).ToList()).ToList();
                    }
                    catch { continue; }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            //Send notification
            var _notification = new NotificationVM
            {
                Title = model.Title,
                Type = model.Type,
                Body = model.Body,
                NData = model.NData == null ? null : JsonConvert.SerializeObject(model.NData),
                IsSeen = false,
                DateCreated = DateTime.Now
            };

            _hubContext.Clients.Clients(connections)
                            .SendAsync("Notify", JsonConvert.SerializeObject(_notification.Adapt<NotificationVM>()));
        }

        public void ExportCustomerHangfire(string name, string downloadURL, HsUser user)
        {
            _customerService.ExportCustomerToExcel(downloadURL, name);
            NotificationCM noti = new NotificationCM
            {
                Title = "Xuất dữ liệu đơn vị",
                Type = "info",
                Body = "Xuất dữ liệu thành công... Ấn vào để download !",
                NData = new
                {
                    url = downloadURL,
                    type = 0,
                }
            };
            var usersPKD = _userManager.GetUsersInRoleAsync("P.KD").Result.ToList();
            var usersPCLTT = _userManager.GetUsersInRoleAsync("P.CLTT").Result.ToList(); // mới chỉnh
            var usersGD = _userManager.GetUsersInRoleAsync("QT.GĐ").Result.ToList(); // mới chỉnh
            var users = usersPKD.Concat(usersPCLTT).Concat(usersGD).ToList();
            if (user != null) users = new List<HsUser> { user };
            SendNotiAsync(users, noti);
        }

        private bool IsRunningContractTelecom(ContractTelecom c)
        {
            var appendixs = _contractTelecomAppendixService.GetContractTelecomAppendixs(_ => _.ContractTelecomId == c.Id).ToList();
            foreach (var appendix in appendixs)
            {
                if (appendix.Status != (int)ContractStatus.PAYOFF)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsExtensionContractTelecom(ContractTelecom c)
        {
            var workFlowHistories = _workFlowHistoryService.GetWorkFlowHistories().ToList();
            workFlowHistories = workFlowHistories.Where(_ => _.CustomerWorkFlowId == c.CustomerWorkflowId).ToList();
            var FormData = workFlowHistories.Where(_ => _.FormData.Contains("\"autoReneval\":\"Có\"") && _.InstanceName.Equals("Ký hợp đồng"));
            if (FormData != null)
            {
                return true;
            }
            return false;
        }

        public void ExtensionTelecomContract()
        {
            var TelecomContracts = _contractTelecomService.GetContractTelecoms().ToList();
            TelecomContracts = TelecomContracts.Where(_ => IsRunningContractTelecom(_)).ToList();
            TelecomContracts = TelecomContracts.Where(_ => IsExtensionContractTelecom(_)).ToList();

            foreach (var telecomContract in TelecomContracts)
            {
                var DateEnd = telecomContract.DateEnd;
                if (DateEnd != null && DateEnd == new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
                {
                    DateEnd = DateEnd.AddYears(1);
                    telecomContract.DateEnd = DateEnd;
                    _contractTelecomService.EditContractTelecom(telecomContract);
                    _contractTelecomService.SaveContractTelecom();
                }
            }

        }

        private bool IsSoftwareBusiness(Customer c)
        {
            //TODO : Need to modify
            return c.CompanyType != null ? (c.CompanyType.ToLower().Contains(DNPM_1) || c.CompanyType.ToLower().Contains(DNPM_2)
                || c.CompanyType.ToLower().Contains(DNPM_3) || c.CompanyType.ToLower().Contains(DNPM_4)) : false;
        }

        private bool IsDomestic(Customer c)
        {
            return c.Country != null ? c.Country.ToLower().Contains(DOMESTIC) : true;
        }

        public void AddDashBoardChart()
        {
            #region VondautuSoftWare
            var customersVondautuSoftWare = _customerService.GetCustomers(c => IsSoftwareBusiness(c));
            int currentYear = DateTime.Now.Year;
            var customersVondautuSoftWareByYear = customersVondautuSoftWare.Where(c => StaticMethod.IsAvailableCustomer(c, currentYear));
            foreach (var customerByYear in customersVondautuSoftWareByYear)
            {
                _transactionLogService.AdaptChange<Customer>(currentYear == DateTime.Now.Year ? new DateTime(currentYear, 12, 30) : DateTime.Now, customerByYear, customerByYear.Id);
            }

            decimal VondautuSoftWare = customersVondautuSoftWareByYear.Sum(c => c.VondautuRegister);
            #endregion

            #region VondautuInvestors
            var customersVondautuInvestors = _customerService.GetCustomers(c => c.CompanyType != null ? c.CompanyType.ToLower().Contains(NĐT) : false);
            var customersVondautuInvestorsByYear = customersVondautuInvestors.Where(c => StaticMethod.IsAvailableCustomer(c, currentYear));
            foreach (var customerByYear in customersVondautuInvestorsByYear)
            {
                _transactionLogService.AdaptChange<Customer>(currentYear == DateTime.Now.Year ? new DateTime(currentYear, 12, 30) : DateTime.Now, customerByYear, customerByYear.Id);
            }
            decimal VondautuInvestors = customersVondautuInvestorsByYear.Sum(c => c.VondautuRegister);
            #endregion

            #region TotalSoftWare
            var customersTotalSoftWare = _customerService.GetCustomers(c => IsSoftwareBusiness(c));
            var customersByYear = customersTotalSoftWare.Where(c => StaticMethod.IsAvailableCustomer(c, currentYear));
            long TotalSoftWare = customersByYear.Count();
            long TotalDomestic = customersByYear.Count(c => IsDomestic(c));
            long TotalInternational = customersByYear.Count(c => !IsDomestic(c));
            #endregion

            #region Total Peoples
            var personels = _customerService.GetCustomers().Select(c => c.Personnel);
            var personelsByYear = personels.ToList();
            var totalEmployee = personelsByYear.Sum(c => c.TotalEmployee);
            var totalAlumnus = personelsByYear.Sum(c => c.TotalAlumnus);
            long TotalEmployee = totalEmployee;
            long TotalAlumnus = totalAlumnus;
            long TotalPeople = totalEmployee + totalAlumnus;
            #endregion

            var DashBoardChart = new DashBoardChart
            {
                Time = DateTime.Now,
                VondautuSoftwares = VondautuSoftWare,
                VondautuInvestors = VondautuInvestors,
                TotalSoftWare = TotalSoftWare,
                TotalDomestic = TotalDomestic,
                TotalInternational = TotalInternational,
                TotalEmployee = TotalEmployee,
                TotalAlumnus = TotalAlumnus,
                TotalPeoples = TotalPeople,
            };
            _dashBoardChartService.CreateDashBoardChart(DashBoardChart);
            _dashBoardChartService.SaveChange();
        }
    }

    public static class StaticMethod
    {



        public static bool IsAvailableCustomer(Customer c, int currentYear)
        {
            //TODO : Chek lai xem co can chinh sua theo ben zalo khong
            var realYear = DateTime.Now;
            var currentDate = currentYear == realYear.Year ? realYear : new DateTime(currentYear, 12, 30);
            var result = true;
            result = result && (c.StartYear != null ? c.StartYear.Value <= currentDate : true);
            result = result && (c.EndYear != null ? DateTime.Parse(c.EndYear) > currentDate : true);
            //result = result && (c.StartYear != null ? Convert.ToInt32(c.StartYear.Value.Year) <= currentYear : false);
            //result = result && (c.EndYear != null ? Convert.ToInt32(DateTime.Parse(c.EndYear).Year) > currentYear : true);
            return result;
        }
        public static bool IsRunningContract(Contract c)
        {
            var result = true;
            result = result && c.StartDate != null ? c.StartDate.Value < DateTime.Now.Date : false;
            result = result && c.EndDate != null ? c.EndDate.Value > DateTime.Now.Date : false;
            result = result && c.Status == 0;
            return result;
        }
        public static bool IsExpireContract(DateTime date)
        {
            //return true;
            return 0 < date.Subtract(DateTime.Now.Date).TotalDays && date.Subtract(DateTime.Now.Date).TotalDays <= 30;
        }



        public static HsNotification CreateHsNotification(string userId, NotificationCM model)
        {
            return new HsNotification
            {
                Title = model.Title,
                Type = model.Type,
                Body = model.Body,
                UserId = userId,
                NData = model.NData == null ? null : JsonConvert.SerializeObject(model.NData),
                IsSeen = false,
                DateCreated = DateTime.Now
            };
        }
    }
}
