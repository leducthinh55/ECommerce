using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using CRM.ViewModels;
using CRM.Model;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using CRM.Utils;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerWorkFlowsController : ControllerBase
    {
        private readonly ICustomerWorkFlowService _customerWorkFlowService;
        private readonly IHsWorkFlowService _workFlowService;
        private readonly IHsWorkFlowInstanceService _workflowInstanceService;
        private readonly IWorkFlowHistoryService _workFlowHistoryService;
        private readonly ICustomerService _customerService;
        private readonly IGlobalVariableValueService _globalValueService;
        private readonly IGlobalVariableService _globalVariableService;
        private readonly IWorkFlowHistoryFileService _workFlowHistoryFileService;
        private readonly IPermissionService _permissionService;
        private readonly UserManager<HsUser> _userManager;

        public CustomerWorkFlowsController(ICustomerWorkFlowService customerWorkFlowService, IHsWorkFlowService workFlowService, IHsWorkFlowInstanceService workflowInstanceService, IWorkFlowHistoryService workFlowHistoryService, ICustomerService customerService, IGlobalVariableValueService globalValueService, IGlobalVariableService globalVariableService, IWorkFlowHistoryFileService workFlowHistoryFileService, IPermissionService permissionService, UserManager<HsUser> userManager)
        {
            _customerWorkFlowService = customerWorkFlowService;
            _workFlowService = workFlowService;
            _workflowInstanceService = workflowInstanceService;
            _workFlowHistoryService = workFlowHistoryService;
            _customerService = customerService;
            _globalValueService = globalValueService;
            _globalVariableService = globalVariableService;
            _workFlowHistoryFileService = workFlowHistoryFileService;
            _permissionService = permissionService;
            _userManager = userManager;
        }

        [HttpGet("IsInWorkflow")]
        public ActionResult IsInWorkflow(Guid CustomerId, Guid WorkFlowId)
        {
            try
            {
                var workflowCustomer = _customerWorkFlowService.GetCustomerWorkFlows(_ =>
                                    _.CustomerId.Equals(CustomerId)
                                    && _.WorkFlowId.Equals(WorkFlowId)).FirstOrDefault();
                return Ok(new { result = workflowCustomer != null });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public ActionResult Get()
        {
            var rs = _customerWorkFlowService.GetCustomerWorkFlows()
                .Where(c => c.EndDate == null)
                .Select(c => new
                {
                    c.Id,
                    c.Code,
                    WorkFlow = new
                    {
                        c.WorkFlow.Id,
                        c.WorkFlow.Name,
                        c.WorkFlow.Code
                    },
                    Customer = new
                    {
                        c.Customer.Id,
                        c.Customer.Name,
                    },
                    c.StartDate,
                    CurrentProcesses = _workFlowHistoryService.GetInProcess(c.Id).Select(h => new
                    {
                        h.InstanceId,
                        h.InstanceName,
                    }).ToList(),
                })
                .ToList();
            return Ok(rs);
        }

        [HttpGet("{id}", Name = "GetCustomerWorkFlow")]
        public ActionResult Get(Guid id)
        {
            var r = _customerWorkFlowService.GetCustomerWorkFlow(id);
            if (r == null)
            {
                return NotFound();
            }
            return Ok(r.Adapt<CustomerWorkFlowViewModel>());
        }

        [HttpGet("GetByCustomer/{id}")]
        public ActionResult GetByCustomer(Guid id)
        {
            var rs = _customerWorkFlowService.GetCustomerWorkFlows()
                .Where(c => c.CustomerId.Equals(id))
                .Select(c => new
                {
                    c.Id,
                    c.Code,
                    c.WorkFlowId,
                    c.WorkFlow.Name,
                    c.StartDate,
                    c.EndDate,
                    CurrentProcesses = _workFlowHistoryService.GetInProcess(c.Id).Select(h => new
                    {
                        //h.InstanceId,
                        h.InstanceName,
                    })
                    .ToList(),
                })
                 .OrderByDescending(_ => _.StartDate)
                .ToList();
            return Ok(rs);
        }

        [HttpGet("GetByWorkFlow/{id}")]
        public ActionResult GetByWorkFlow(Guid id)
        {
            var rs = _customerWorkFlowService.GetCustomerWorkFlows()
                .Where(c => c.WorkFlowId.Equals(id))
                .Select(c => new
                {
                    c.Id,
                    c.Code,
                    CustomerId = c.Customer != null ? c.Customer.Id : new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0),
                    CustomerName = c.Customer != null ? c.Customer.Name : "",
                    c.StartDate,
                    c.EndDate,
                    CurrentProcesses = _workFlowHistoryService.GetInProcess(c.Id).Select(h => new
                    {
                        h.InstanceId,
                        h.InstanceName,
                    }).ToList(),
                })
                .OrderByDescending(_ => _.StartDate)
                .ToList();
            return Ok(rs);
        }

        [HttpPost]
        public ActionResult Post([FromBody]CustomerWorkFlowCreateModel model)
        {
            var _user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var permissions = JsonConvert.DeserializeObject<List<Guid>>(_user.Permissions);


            var customerWorkFlow = model.Adapt<CustomerWorkFlow>();
            customerWorkFlow.StartDate = DateTime.Now;
            var workflow = _workFlowService.GetHsWorkFlow(model.WorkFlowId);
            if (workflow == null || !permissions.Contains(workflow.PermissionIdW.Value))
            {
                return BadRequest();
            }
            var oldWorkFlows = _customerWorkFlowService
                                .GetCustomerWorkFlows()
                                .Where(w => w.CustomerId == model.CustomerId && w.WorkFlowId == model.WorkFlowId);
            Customer customer = null;
            if (model.CustomerId != null)
            {
                customer = _customerService.GetCustomer(model.CustomerId.Value);
                customerWorkFlow.Code = customer.Code + "-" + workflow.Code + "-" + (oldWorkFlows.Count() + 1);
            }
            else
            {
                customerWorkFlow.Code = "00000000-"+ workflow.Code + "-" + (oldWorkFlows.Count() + 1);
            }
            _customerWorkFlowService.CreateCustomerWorkFlow(customerWorkFlow);
            _customerWorkFlowService.SaveCustomerWorkFlow();
            var startEvent = _workflowInstanceService.GetStartEvent(workflow.Id);
            var nextSteps = _workflowInstanceService.GetNextSteps(startEvent.Id);

            #region fix 15/5
            nextSteps = nextSteps.Where(_ => permissions.Contains(_.PermissionIdR.Value)).ToList();
            #endregion

            var firstStep = new WorkFlowHistory()
            {
                CustomerWorkFlowId = customerWorkFlow.Id,
                DateCreated = DateTime.Now.AddSeconds(-10),
                InstanceId = startEvent.Id,
                InstanceName = startEvent.Name,
                Status = 2,
            };
            _workFlowHistoryService.CreateWorkFlowHistory(firstStep);
            _workFlowHistoryService.SaveWorkFlowHistory();
            nextSteps.ToList().ForEach(e => _workFlowHistoryService.CreateWorkFlowHistory(new WorkFlowHistory
            {
                CustomerWorkFlowId = customerWorkFlow.Id,
                DateCreated = DateTime.Now,
                InstanceName = e.Name,
                InstanceId = e.Id,
                Status = 1,
                PreviousStep = firstStep.Id
            }));
            _workFlowHistoryService.SaveWorkFlowHistory();

            // lay gv 
            if (workflow.Type == (int)WorkflowType.selectCustomer && customer != null)
            {
                var gvvalue = new List<Guid>();
                gvvalue.Add(customer.Id);
                var gvs = _globalVariableService
                                        .GetGlobalVariables(_ => _.WorkflowId == workflow.Id && _.Type == nameof(GVType.selectCustomer));
                foreach (var gv in gvs)
                {
                    _globalValueService.CreateGlobalVariableValue(new GlobalVariableValue
                    {
                        CustomerWorkflowId = customerWorkFlow.Id,
                        GlobalVariableId = gv.Id,
                        IsObject = true,
                        Value = JsonConvert.SerializeObject(gvvalue)
                    });
                }
                _globalVariableService.SaveChanges();
            }
            return CreatedAtRoute("GetCustomerWorkFlow", new
            {
                id = customerWorkFlow.Id
            }, customerWorkFlow.Adapt<CustomerWorkFlowViewModel>());
        }

        [HttpGet("Files")]
        public ActionResult GetAllFileOfWorkFlow(Guid customerWorkFlowId)
        {
            List<FileCommonVM> result = _globalValueService.GetGlobalVariableValues(_ => _.CustomerWorkflowId == customerWorkFlowId
                            && _.DateCreated.Year > 1)  // Chỉ lấy file.  === Bùa === 
                        .Select(_ => _.Adapt<FileCommonVM>())
                        .ToList();
            result.AddRange(_workFlowHistoryFileService.GetWorkFlowHistoryFiles().Where(_ => _.WorkFlowHistory.CustomerWorkFlowId == customerWorkFlowId)
                        .Select(_ => _.Adapt<FileCommonVM>())
                        .ToList());

            result = result.OrderByDescending(_ => _.DateCreated).ToList();
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var r = _customerWorkFlowService.GetCustomerWorkFlow(id);
            if (r == null)
            {
                return NotFound();
            }
            _customerWorkFlowService.RemoveCustomerWorkFlow(id);
            _customerWorkFlowService.SaveCustomerWorkFlow();
            return Ok();
        }
    }
}