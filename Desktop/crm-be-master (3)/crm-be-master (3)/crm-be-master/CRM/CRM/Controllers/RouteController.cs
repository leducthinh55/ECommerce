using CRM.HangfireJob;
using CRM.Hubs;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Hangfire;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    public class RouteModel
    {
        public Guid CurrentInstanceId { get; set; }
        public Guid NextInstanceId { get; set; }
        public Guid CustomerWorkFlowId { get; set; }
        public string Comment { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IHsWorkFlowInstanceService _workflowInstanceService;
        private readonly ICustomerWorkFlowService _customerWorkFlowService;
        private readonly IWorkFlowHistoryService _workFlowHistoryService;
        private readonly UserManager<HsUser> _userManager;
        private readonly IPermissionService _permissionService;
        private readonly IEmailService _emailService;
        private readonly ITestBackgroud _testBackgroud;
        private readonly IConfiguration _configuration;

        private readonly IHsNotificationService _notiService;
        private readonly IHubContext<CenterHub> _hubContext;
        private readonly IHubUserConnectionService _hubService;
        private static String ConfigWorkFlowEr = "Config workflow is wrong!!! Please check again. Make sure next step is existed";


        public RouteController(ITestBackgroud testBackgroud, IEmailService emailService, IHsWorkFlowInstanceService workflowInstanceService, ICustomerWorkFlowService customerWorkFlowService, IWorkFlowHistoryService workFlowHistoryService, UserManager<HsUser> userManager, IPermissionService permissionService, IHsNotificationService notiService, IHubContext<CenterHub> hubContext, IHubUserConnectionService hubService, IConfiguration configuration)
        {
            _workflowInstanceService = workflowInstanceService;
            _customerWorkFlowService = customerWorkFlowService;
            _workFlowHistoryService = workFlowHistoryService;
            _userManager = userManager;
            _permissionService = permissionService;
            _notiService = notiService;
            _hubContext = hubContext;
            _hubService = hubService;
            _emailService = emailService;
            _testBackgroud = testBackgroud;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        /***
         * Input:   instanceId current instance
         *          customerWorkFlowId: current customerWorkflow
         * Output: next step we can go after this instance.
         */
        public ActionResult NextSteps(Guid instanceId, Guid customerWorkFlowId)
        {
            var username = User.Identity.Name;
            var _user = _userManager.FindByNameAsync(username).Result;
            var permissions = JsonConvert.DeserializeObject<List<Guid>>(_user.Permissions);

            var currentInstance = _workflowInstanceService.GetHsWorkFlowInstance(instanceId);
            if (currentInstance == null)
            {
                return NotFound();
            }
            if (currentInstance.SubType.Equals("Task"))
            {
                var _c = _workFlowHistoryService.GetWorkFlowHistories()
                .LastOrDefault(c => c.CustomerWorkFlowId.Equals(customerWorkFlowId) && c.InstanceId.Equals(instanceId));

                if (
                    _c == null ||  // current step not existed
                    _c.Status == 2  // current step is done !
                    ) // user have not permission in this step.
                {
                    return BadRequest();
                }
            } // check opened task
            if (!permissions.Contains(currentInstance.PermissionIdR.Value))
            {
                return BadRequest();
            } // check permission
            var nextStep = currentInstance.ToInstances.FirstOrDefault(_ => !_.IsDeleted)?.ToInstance;
            if (nextStep == null)
                return Ok();
            switch (currentInstance.SubType)
            {
                case "Task":
                    switch (nextStep.SubType)
                    {
                        case "Task":
                            {
                                return Ok(new
                                {
                                    CurrentSubType = currentInstance.SubType,
                                    nextStep.SubType,
                                    nextStep.Id,
                                    nextStep.Name,
                                });
                            }
                        case "Inclusive":
                            {
                                while (nextStep.ToInstances.Count(u => !u.IsDeleted) == 1 && nextStep.SubType.Equals("Inclusive"))
                                {
                                    nextStep = nextStep.ToInstances.First(_ => !_.IsDeleted).ToInstance;
                                }
                                if (!nextStep.SubType.Equals("Inclusive"))
                                {
                                    switch (nextStep.SubType)
                                    {
                                        case "Parallel":
                                            {

                                                return Ok(new
                                                {
                                                    CurrentSubType = currentInstance.SubType,
                                                    nextStep.SubType,
                                                    nextStep.Id,
                                                    NextSteps = _workflowInstanceService.GetNextSteps(nextStep.Id)
                                                            .Select(e => new
                                                            {
                                                                e.Id,
                                                                e.Name,
                                                            }).ToList(),
                                                });
                                            }
                                        //return BadRequest(ConfigWorkFlowEr);
                                        case "Task":
                                            return Ok(new
                                            {
                                                CurrentSubType = currentInstance.SubType,
                                                nextStep.SubType,
                                                nextStep.Id,
                                                nextStep.Name,
                                            });
                                        case "Exclusive":
                                            return Ok(new
                                            {
                                                CurrentSubType = currentInstance.SubType,
                                                nextStep.SubType,
                                                nextStep.Id,
                                                nextStep.Name,
                                            });
                                        //return BadRequest(ConfigWorkFlowEr);
                                        case "End event":
                                            return Ok(new
                                            {
                                                CurrentSubType = currentInstance.SubType,
                                                nextStep.SubType,
                                                nextStep.Id,
                                                nextStep.Name,
                                            });
                                        default: return BadRequest(ConfigWorkFlowEr);
                                    }
                                }// is not inclusive
                                else
                                {
                                    return BadRequest(ConfigWorkFlowEr);
                                }
                            }
                        case "Exclusive": // like task
                            {
                                return Ok(new
                                {
                                    CurrentSubType = currentInstance.SubType,
                                    nextStep.SubType,
                                    nextStep.Id,
                                    nextStep.Name,
                                });
                            }
                        case "End event": // like task
                            {
                                return Ok(new
                                {
                                    CurrentSubType = currentInstance.SubType,
                                    nextStep.SubType,
                                    nextStep.Id,
                                    nextStep.Name,
                                });
                            }
                        case "Parallel":
                            {

                                return Ok(new
                                {
                                    CurrentSubType = currentInstance.SubType,
                                    nextStep.SubType,
                                    nextStep.Id,
                                    NextSteps = _workflowInstanceService.GetNextSteps(currentInstance.Id)
                                        .Select(e => new
                                        {
                                            e.Id,
                                            e.Name,
                                        }).ToList(),
                                });
                                //if (nextStep.ToInstances.Count(_ => !_.IsDeleted) > 1)
                                //{

                                //}
                                //else
                                //{

                                //}
                            }
                    }
                    return Ok();
                case "Exclusive":
                    if (currentInstance.ToInstances.Count(_ => !_.IsDeleted) >= 2)
                    {
                        return Ok(new
                        {
                            CurrentSubType = currentInstance.SubType,
                            currentInstance.SubType,
                            currentInstance.Id,
                            currentInstance.Name,
                            Commands = currentInstance.ToInstances.Where(_ => !_.IsDeleted).Select(u => new
                            {
                                u.Command,
                                u.ToInstance.Id,
                                u.ToInstance.Name,
                            }).ToList(),
                        });
                    }// decision making
                    return Ok();
                case "End event":
                    return Ok();
                default:
                    return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult ToStep([FromBody]RouteModel model)
        {
            var customerWorkFlow = _customerWorkFlowService.GetCustomerWorkFlow(model.CustomerWorkFlowId);
            var nextInstance = _workflowInstanceService.GetHsWorkFlowInstance(model.NextInstanceId);
            var currentInstance = _workflowInstanceService.GetHsWorkFlowInstance(model.CurrentInstanceId);
            if (customerWorkFlow == null || nextInstance == null || currentInstance == null)
            {
                return NotFound();
            }

            //if (currentInstance.ToInstances.Count(_ => _.ToInstanceId.Equals(nextInstance.Id)) == 0)
            //{
            //    return BadRequest();
            //};
            //var _currentInstance = _workFlowHistoryService.GetWorkFlowHistories().FirstOrDefault(
            //        _=>_.InstanceId.Equals(model.CurrentInstanceId)
            //        && _.CustomerWorkFlowId.Equals(model.CustomerWorkFlowId)
            //    );
            //if (_currentInstance == null || _currentInstance.Status == 2) return BadRequest();

            if (!_workflowInstanceService.CheckRoute(currentInstance, nextInstance))
            {
                return BadRequest();
            }
            if (nextInstance.SubType.Equals("End event"))
            {
                customerWorkFlow.EndDate = DateTime.Now;
            }//check end event
            List<HsWorkFlowInstance> nextInstances = new List<HsWorkFlowInstance>();
            switch (nextInstance.SubType)
            {
                case "Inclusive":
                    {
                        while (nextInstance.ToInstances.Count(u => !u.IsDeleted) == 1 && nextInstance.SubType.Equals("Inclusive"))
                        {
                            nextInstance = nextInstance.ToInstances.First().ToInstance;
                        }
                        if (!nextInstance.SubType.Equals("Inclusive"))
                        {
                            switch (nextInstance.SubType)
                            {
                                case "Parallel":
                                    // Begin parallel
                                    if (nextInstance.ToInstances.Where(_ => !_.IsDeleted).Count(c => !c.IsDeleted) > 1)
                                    {
                                        _workflowInstanceService.GetNextSteps(nextInstance.Id).ToList().ForEach(e =>
                                        {
                                            nextInstances.Add(e);
                                        });
                                    }
                                    break;
                                case "Task":
                                    nextInstances.Add(nextInstance);
                                    break;
                                case "Exclusive":
                                    nextInstances.Add(nextInstance);
                                    break;
                                case "End event":
                                    nextInstances.Add(nextInstance);
                                    break;
                            }
                        }// is not inclusive
                        else
                        {

                        }
                        break;
                    }
                case "Task":
                    nextInstances.Add(nextInstance);
                    break;
                case "Exclusive":
                    nextInstances.Add(nextInstance);
                    break;
                case "End event":
                    nextInstances.Add(nextInstance);
                    break;
                case "Parallel":
                    {
                        // Begin parallel
                        if (nextInstance.ToInstances.Where(_ => _.IsDeleted == false).Count(c => !c.IsDeleted) > 1)
                        {
                            _workflowInstanceService.GetNextSteps(nextInstance.Id).ToList().ForEach(e =>
                            {
                                nextInstances.Add(e);
                            });
                        }
                        // End parallel
                        else
                        {
                            bool isDone = true;
                            int loop = _workFlowHistoryService.GetWorkFlowHistories().Count(_ =>
                                       _.CustomerWorkFlowId.Equals(customerWorkFlow.Id)
                                       && _.InstanceId.Equals(currentInstance.Id)
                                );

                            foreach (var item in _workflowInstanceService.GetPreviousSteps(nextInstance))
                            {
                                if (!isDone) break;
                                if (item.Id.Equals(currentInstance.Id)) continue;

                                var i = _workFlowHistoryService.GetWorkFlowHistories()
                                    .Where(u => u.CustomerWorkFlowId.Equals(customerWorkFlow.Id)
                                                            //   && !u.InstanceId.Equals(currentInstance.Id)
                                                            && u.InstanceId.Equals(item.Id));
                                if (i == null || i.Count() < loop)
                                {
                                    isDone = false;
                                    break;
                                }
                                else
                                {
                                    isDone = i.Count(_ => _.Status == 2) == loop && isDone;
                                }
                            }
                            if (isDone)
                            {
                                _workflowInstanceService.GetNextSteps(nextInstance.Id).ToList().ForEach(e =>
                                {
                                    nextInstances.Add(e);
                                });
                            }
                        }//merge
                        break;
                    }
            }

            List<WorkFlowHistory> workFlowHistories = new List<WorkFlowHistory>();
            nextInstances.ForEach(e =>
            {
                workFlowHistories.Add(new WorkFlowHistory()
                {
                    CustomerWorkFlowId = model.CustomerWorkFlowId,
                    DateCreated = DateTime.Now,
                    InstanceId = e.Id,
                    InstanceName = e.Name,
                    PreviousStep = currentInstance.Id,
                    Status = e.SubType.Equals("End event") ? 2 : 1,

                });
                _workFlowHistoryService.CreateWorkFlowHistory(workFlowHistories.Last());
            });
            var doneStep = _workFlowHistoryService.GetWorkFlowHistories()
                            .FirstOrDefault(d => d.CustomerWorkFlowId.Equals(customerWorkFlow.Id)
                                && d.InstanceId.Equals(currentInstance.Id) && d.Status == 1);
            doneStep.Status = 2;
            doneStep.Comment = model.Comment;
            _customerWorkFlowService.EditCustomerWorkFlow(customerWorkFlow);
            _customerWorkFlowService.SaveCustomerWorkFlow();

            _workFlowHistoryService.EditWorkFlowHistory(doneStep);
            _workFlowHistoryService.SaveWorkFlowHistory();
            //notification

            for (int i = 0; i < nextInstances.Count(); i++)
            {
                var instance = nextInstances[i];
                var title = customerWorkFlow.Code + ": " +
                       ((customerWorkFlow.Customer == null) ? "" : customerWorkFlow.Customer.Name);
                NotificationCM noti = new NotificationCM
                {
                    Title = title,
                    Type = "info",
                    Body = instance.Name,
                    NData = new
                    {
                        //workFlowHistories[i].Id,
                        url = "/process/" + workFlowHistories[i].CustomerWorkFlowId,
                        type = 0,
                        //workFlowHistories[i].Status,
                        //workFlowHistories[i].DateCreated,
                    }
                };
                if (instance.PermissionIdNoti != null)
                {
                    var jobId = BackgroundJob.Enqueue(
                    () => SendNotiAsync(instance.PermissionIdNoti.Value, noti));
                }
            }

            if (model.CurrentInstanceId.Equals(Guid.Parse("a29b4740-e9a2-48c2-a73f-08d6a4384626"))) // gửi yêu cầu phối hợp
            {

                var username = User.Identity.Name;
                String fullName = "";
                if(username != null)
                {
                    Task<HsUser> user = _userManager.FindByNameAsync(username);
                    fullName = user.Result.FullName;
                }
                var jobSendEmailId = BackgroundJob.Schedule(
                () => _testBackgroud.CheckRequest(model.CustomerWorkFlowId, fullName),
                TimeSpan.FromMinutes(0));

                var jobSendEmailRemind = BackgroundJob.Schedule(
                () => _testBackgroud.CheckRequest(model.CustomerWorkFlowId, fullName),
                TimeSpan.FromHours(24));
            }

            if (model.CurrentInstanceId.Equals(Guid.Parse("BCB67654-1C3F-4003-8379-08D765BEB257"))) // dùng cho VT mới tạo 
            {
                var username = User.Identity.Name;
                String fullName = "";
                if (username != null)
                {
                    Task<HsUser> user = _userManager.FindByNameAsync(username);
                    fullName = user.Result.FullName;
                }
                var jobSendEmailId = BackgroundJob.Schedule(
                () => _testBackgroud.CheckRequestVT(model.CustomerWorkFlowId, fullName),
                TimeSpan.FromMinutes(0));

                var jobSendEmailRemind = BackgroundJob.Schedule(
                () => _testBackgroud.CheckRequestVT(model.CustomerWorkFlowId, fullName),
                TimeSpan.FromHours(24));
            }

            return Ok();
        }



        public async Task SendNotiAsync(Guid permissionId, NotificationCM model)
        {
            List<string> connections = new List<string>();
            try
            {
                var permission = _permissionService.GetPermission(permissionId);
                if (permission == null)
                {
                    return;
                }

                var userIds = _permissionService.GetUsersByPermission(permissionId);

                foreach (var userId in userIds)
                {
                    try
                    {
                        var notification = CreateHsNotification(userId, model);
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
        private HsNotification CreateHsNotification(string userId, NotificationCM model)
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