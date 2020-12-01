using CRM.Helpers;
using CRM.Model;
using CRM.Service;
using CRM.Service.Utils;
using CRM.Utils;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkFlowHistoriesController : ControllerBase
    {
        private static string COTRACT_TELECOM_SIGNAL = "CONTRACT_TELECOM_SIGNAL";
        private static string CONTRACT_TELECOM_PAYOFF_SIGNAL = "CONTRACT_TELECOM_PAYOFF_SIGNAL";

        private static string COTRACT_AREA_SIGNAL = "COTRACT_AREA_SIGNAL";
        private static string CONTRACT_AREA_PAYOFF_SIGNAL = "CONTRACT_AREA_PAYOFF_SIGNAL";
        private static string GV = "GV";
        private static string DIVIDE_CHAR = "_";
        private readonly IWorkFlowHistoryService _workFlowHistoryService;
        private readonly IHsWorkFlowInstanceService _workFlowInstanceService;
        private readonly IFormService _formService;
        private readonly IWorkFlowHistoryFileService _workFlowHistoryFileService;
        private readonly IHsTemplateService _templateService;
        private readonly IGlobalVariableValueService _globalVariableValueService;
        private readonly IGlobalVariableService _globalVariableService;
        private readonly IPermissionService _permissionService;
        private readonly IContractTelecomService _contractTelecomService;
        private readonly IContractService _contractService;
        private readonly IContractAppendixService _contractAppendixService;

        private readonly UserManager<HsUser> _userManager;

        public WorkFlowHistoriesController(IContractAppendixService contractAppendixService,IContractService _contractService,IWorkFlowHistoryService workFlowHistoryService, IHsWorkFlowInstanceService workFlowInstanceService, IFormService formService, IWorkFlowHistoryFileService workFlowHistoryFileService, IHsTemplateService templateService, IGlobalVariableValueService globalVariableValueService, IGlobalVariableService globalVariableService, IPermissionService permissionService, IContractTelecomService contractTelecomService, UserManager<HsUser> userManager)
        {
            _workFlowHistoryService = workFlowHistoryService;
            _workFlowInstanceService = workFlowInstanceService;
            _formService = formService;
            _workFlowHistoryFileService = workFlowHistoryFileService;
            _templateService = templateService;
            _globalVariableValueService = globalVariableValueService;
            _globalVariableService = globalVariableService;
            _permissionService = permissionService;
            _contractTelecomService = contractTelecomService;
            _userManager = userManager;
            this._contractService = _contractService;
            _contractAppendixService = contractAppendixService;
        }

        [HttpGet]
        public ActionResult Get(Guid customerWorkFlowId)
        {
            var rs = _workFlowHistoryService.GetWorkFlowHistories()
                .Where(h => h.CustomerWorkFlowId.Equals(customerWorkFlowId))
                .Select(h => new
                {
                    h.Id,
                    h.InstanceId,
                    h.InstanceName,
                    h.Status,
                    h.DateCreated,
                    _workFlowInstanceService.GetHsWorkFlowInstance(h.InstanceId).Icon
                })
                .OrderByDescending(h => h.DateCreated)
                .ToList();
            return Ok(rs);
        }

        [HttpGet("form")]
        public async Task<ActionResult> GetForm(Guid id)
        {
            try
            {

                var _instance = _workFlowHistoryService.GetWorkFlowHistory(id);
                var instance = _workFlowInstanceService.GetHsWorkFlowInstance(_instance.InstanceId);
                if (instance.FormId == null)
                {
                    return Ok();
                }
                var _form = _formService.GetForm(instance.FormId.Value);
                var _user = await _userManager.FindByNameAsync(User.Identity.Name);
                var permissions = JsonConvert.DeserializeObject<List<Guid>>(_user.Permissions);
                permissions.Sort();
                if (!permissions.Contains(instance.PermissionIdR.Value))
                {
                    return Ok();
                }
                FormDataVM result = new FormDataVM();
                var data = _formService.GetForm(_form.Id);
                result.Form = data.Adapt<FormVM>();
                result.FormGroups = new List<ViewModels.FormGroupVM>();
                dynamic formData = new Dictionary<String, Object>();
                if (!string.IsNullOrEmpty(_instance.FormData))
                {
                    dynamic temp = JsonConvert.DeserializeObject(_instance.FormData);
                    foreach (var item in temp)
                    {
                        string key = item.Name;
                        var value = item.Value;
                        if (value == null) continue;
                        formData[key] = value;
                    }
                }

                foreach (var item in data.FormGroups)
                {
                    if (!item.IsDeleted)
                    {
                        var formGroup = JsonConvert.DeserializeObject<FormGroupDataVM>(item.Data);
                        string _type = "";
                        if (permissions.BST(item.PermissionIdR)) _type = "read";
                        if (permissions.BST(item.PermissionIdW)) _type += "-write";
                        formGroup.FieldType = _type;
                        if (string.IsNullOrEmpty(_type)) continue;
                        if (formGroup.GlobalVariableId != null)
                        {
                            formGroup.Name = "GV_" + formGroup.GlobalVariableId;
                            if (formGroup.Type == "file")
                            {
                                string type = "";
                                if (permissions.BST(item.PermissionIdR)) type = "read";
                                if (permissions.BST(item.PermissionIdW)) type += "-write";
                                formGroup.FileConfig = new FileConfig(type);
                                var FileValues = _globalVariableValueService.GetGlobalVariableValues(_ =>
                                                    _.GlobalVariableId == formGroup.GlobalVariableId
                                                 && _.CustomerWorkflowId == _instance.CustomerWorkFlowId
                                                    ).OrderByDescending(_ => _.DateCreated);
                                formGroup.FileConfig.FileList = new List<FileVM>();
                                foreach (var value in FileValues)
                                {
                                    var fileName = Path.GetFileName(value.Value);
                                    //Cắt số trước tên file
                                    fileName = fileName.Substring(fileName.IndexOf('_') + 1);
                                    formGroup.FileConfig.FileList.Add(new FileVM
                                    {
                                        Id = value.Id,
                                        Date = value.DateCreated,
                                        // Name = _globalVariableService.GetGlobalVariable(value.GlobalVariableId).Name
                                        Name = fileName
                                    });
                                }
                            }
                            else
                            {
                                // merge data vào Form DATA
                                var value = _globalVariableValueService.GetGlobalVariableValues(
                                    _ => _.GlobalVariableId.Equals(formGroup.GlobalVariableId)
                                    && _.CustomerWorkflowId.Equals(_instance.CustomerWorkFlowId)).FirstOrDefault();
                                if (value != null && !value.IsObject)
                                {
                                    // merge data in form group
                                    formData["GV_" + value.GlobalVariableId] = value.Value;
                                }
                                if (value != null && value.IsObject)
                                {
                                    // merge data in form group
                                    formData["GV_" + value.GlobalVariableId] = JsonConvert.DeserializeObject(value.Value);
                                }
                            }
                        }

                        result.FormGroups.Add(formGroup);
                    }
                }

                result.FormData = formData;

                result.FormGroups = result.FormGroups.OrderBy(_ => _.Order).ToList();


                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        private bool IsPropertyExist(dynamic settings, string name)
        {
            foreach(var item in settings)
            {
                if(item.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        [HttpPost("form")]
        public ActionResult SaveForm([FromBody]SubmitFormModel model)
        {
            try
            {
                var process = _workFlowHistoryService.GetWorkFlowHistory(model.ProcessId);
                dynamic formData = new Dictionary<String, Object>();
                dynamic dataModel = model.FormData;
                #region COTRACT_AREA_SIGNAL
                if (IsPropertyExist(dataModel, COTRACT_AREA_SIGNAL))
                {
                    try
                    {
                        var customer = process.CustomerWorkFlow.Customer;
                        ContractQT11 tempContract = model.FormData.Adapt<ContractQT11>();
                        Contract contract = new Contract();
                        IList<ContractAppendix> contractAppendices = new List<ContractAppendix>();
                        // Object [DateTime] optional | null | 
                        // Adapt: A.a = B.a  null -> DateTime?  | lỗi. 
                        // Vì Adapt null -> DateTime? lỗi mà có trường ngày optional nên phải adapt 2 lớp VM.
                        ContractQT11Reduce reduce = tempContract.Adapt<ContractQT11Reduce>();
                        contract = reduce.Adapt(contract);


                        contract.StartDateRent = tempContract.StartDateRent.Value;
                        contract.LevelUpUnitPrice = dataModel["LevelUpUnitPrice"];
                        contract.UnitPrice = dataModel["contract_unitprice"];
                        contract.Square = dataModel["contract_square"];
                        contract.UpPriceDate = tempContract.UpPriceDate.Value;

                        contractAppendices.Add(new ContractAppendix
                        {
                            No = contract.ContractNo,
                            Building = contract.Building,
                            Floor = contract.Floor,
                            Room = contract.Room,
                            Square = null,
                            DateStart = contract.StartDateRent,
                            UnitPrice = contract.UnitPrice,
                            DateSign = contract.StartDate.Value,
                            Type = (int)ContractAppendixType.PriceRent,
                            Status = 0,
                            Key = "0",
                        });
                        contractAppendices.Add(new ContractAppendix
                        {
                            No = contract.ContractNo,
                            Building = contract.Building,
                            Floor = contract.Floor,
                            Room = contract.Room,
                            Square = contract.Square,
                            DateStart = contract.StartDateRent,
                            UnitPrice = null,
                            DateSign = contract.StartDate.Value,
                            Type = (int)ContractAppendixType.AreaRent,
                            Status = 0,
                            Key = "0",
                        });

                        if (!String.IsNullOrEmpty(tempContract.StartDateService))
                        {
                            contract.StartDateService = DateTime.Parse(tempContract.StartDateService);
                            contract.LevelUpUnitServicePrice = dataModel["LevelUpUnitServicePrice"];
                            contract.UnitServicePrice = dataModel["contract_serviceunitprice"];

                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor,
                                Room = contract.Room,
                                Square = null,
                                DateStart = contract.StartDateService.Value,
                                UnitServicePrice = contract.UnitServicePrice,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.PriceService,
                                Status = 0,
                                Key = "0",
                            });
                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor,
                                Room = contract.Room,
                                Square = contract.Square,
                                DateStart = contract.StartDateService.Value,
                                UnitPrice = null,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.AreaService,
                                Status = 0,
                                Key = "0",
                            });

                        }

                        // .. 2
                        if (!String.IsNullOrEmpty(tempContract.StartDateRent_2))
                        {
                            contract.StartDateRent_2 = DateTime.Parse(tempContract.StartDateRent_2);
                            contract.LevelUpUnitPrice_2 = dataModel["LevelUpUnitPrice_2"];
                            //contract.UnitServicePrice_2 = dataModel["contract_unitprice_2"];
                            contract.UnitPrice_2 = dataModel["contract_unitprice_2"];
                            contract.Square_2 = dataModel["contract_square_2"];
                            contract.UpPriceDate_2 = DateTime.Parse(tempContract.UpPriceDate_2);

                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor_2,
                                Room = contract.Room_2,
                                Square = null,
                                DateStart = contract.StartDateRent_2.Value,
                                UnitPrice = contract.UnitPrice_2,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.PriceRent,
                                Status = 0,
                                Key = "1",
                            });
                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor_2,
                                Room = contract.Room_2,
                                Square = contract.Square_2,
                                DateStart = contract.StartDateRent_2.Value,
                                UnitPrice = null,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.AreaRent,
                                Status = 0,
                                Key = "1",
                            });
                        }
                        if (!String.IsNullOrEmpty(tempContract.StartDateService_2))
                        {
                            contract.StartDateService_2 = DateTime.Parse(tempContract.StartDateService_2);
                            contract.LevelUpUnitServicePrice_2 = dataModel["LevelUpUnitServicePrice_2"];
                            contract.UnitServicePrice_2 = dataModel["contract_serviceunitprice_2"];

                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor_2,
                                Room = contract.Room_2,
                                Square = null,
                                DateStart = contract.StartDateService_2.Value,
                                UnitServicePrice = contract.UnitServicePrice_2,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.PriceService,
                                Status = 0,
                                Key = "1",
                            });
                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor_2,
                                Room = contract.Room_2,
                                Square = contract.Square_2,
                                DateStart = contract.StartDateService_2.Value,
                                UnitPrice = null,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.AreaService,
                                Status = 0,
                                Key = "1",
                            });
                        }
                        // .. 3
                        if (!String.IsNullOrEmpty(tempContract.StartDateRent_3))
                        {
                            contract.StartDateRent_3 = DateTime.Parse(tempContract.StartDateRent_3);
                            contract.LevelUpUnitPrice_3 = dataModel["LevelUpUnitPrice_3"];
                            //contract.UnitServicePrice_3 = dataModel["contract_unitprice_3"];
                            contract.UnitPrice_3 = dataModel["contract_unitprice_3"];
                            contract.Square_3 = dataModel["contract_square_3"];
                            contract.UpPriceDate_3 = DateTime.Parse(tempContract.UpPriceDate_3);

                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor_3,
                                Room = contract.Room_3,
                                Square = null,
                                DateStart = contract.StartDateRent_3.Value,
                                UnitPrice = contract.UnitPrice_3,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.PriceRent,
                                Status = 0,
                                Key = "2",
                            });
                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor_3,
                                Room = contract.Room_3,
                                Square = contract.Square_3,
                                DateStart = contract.StartDateRent_3.Value,
                                UnitPrice = null,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.AreaRent,
                                Status = 0,
                                Key = "2",
                            });
                        }
                        if (!String.IsNullOrEmpty(tempContract.StartDateService_3))
                        {
                            contract.StartDateService_3 = DateTime.Parse(tempContract.StartDateService_3);
                            contract.LevelUpUnitServicePrice_3 = dataModel["LevelUpUnitServicePrice_3"];
                            contract.UnitServicePrice_3 = dataModel["contract_serviceunitprice_3"];
                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor_3,
                                Room = contract.Room_3,
                                Square = null,
                                DateStart = contract.StartDateService_3.Value,
                                UnitServicePrice = contract.UnitServicePrice_3,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.PriceService,
                                Status = 0,
                                Key = "2",
                            });
                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor_3,
                                Room = contract.Room_3,
                                Square = contract.Square_3,
                                DateStart = contract.StartDateService_3.Value,
                                UnitPrice = null,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.AreaService,
                                Status = 0,
                                Key = "2",
                            });
                        }
                        // .. 4
                        if (!String.IsNullOrEmpty(tempContract.StartDateRent_4))
                        {
                            contract.StartDateRent_4 = DateTime.Parse(tempContract.StartDateRent_4);
                            contract.LevelUpUnitPrice_4 = dataModel["LevelUpUnitPrice_4"];
                            contract.UnitPrice_4 = dataModel["contract_unitprice_4"];
                            contract.Square_4 = dataModel["contract_square_4"];
                            contract.UpPriceDate_4 = DateTime.Parse(tempContract.UpPriceDate_4);

                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor_4,
                                Room = contract.Room_4,
                                Square = null,
                                DateStart = contract.StartDateRent_4.Value,
                                UnitPrice = contract.UnitPrice_4,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.PriceRent,
                                Status = 0,
                                Key = "3",
                            });
                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor_4,
                                Room = contract.Room_4,
                                Square = contract.Square_4,
                                DateStart = contract.StartDateRent_4.Value,
                                UnitPrice = null,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.AreaRent,
                                Status = 0,
                                Key = "3",
                            });
                        }
                        if (!String.IsNullOrEmpty(tempContract.StartDateService_4))
                        {
                            contract.StartDateService_4 = DateTime.Parse(tempContract.StartDateService_4);
                            contract.LevelUpUnitServicePrice_4 = dataModel["LevelUpUnitServicePrice_4"];
                            contract.UnitServicePrice_4 = dataModel["contract_serviceunitprice_4"];

                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor_4,
                                Room = contract.Room_4,
                                Square = null,
                                DateStart = contract.StartDateService_4.Value,
                                UnitServicePrice = contract.UnitServicePrice_4,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.PriceService,
                                Status = 0,
                                Key = "3",
                            });
                            contractAppendices.Add(new ContractAppendix
                            {
                                No = contract.ContractNo,
                                Building = contract.Building,
                                Floor = contract.Floor_4,
                                Room = contract.Room_4,
                                Square = contract.Square_4,
                                DateStart = contract.StartDateService_4.Value,
                                UnitPrice = null,
                                DateSign = contract.StartDate.Value,
                                Type = (int)ContractAppendixType.AreaService,
                                Status = 0,
                                Key = "3",
                            });
                        }



                        contract.CustomerId = customer.Id;
                        contract.CustomerWorkflowId = process.CustomerWorkFlowId;
                        contract.Status = (int)ContractStatus.CURRENT;
                        _contractService.Create(contract);


                        foreach (var appendix in contractAppendices)
                        {
                            appendix.ContractId = contract.Id;
                            _contractAppendixService.CreateContractAppendix(appendix);

                        }


                        _contractService.SaveContract();
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Mess = ErrorMess.CREATE_CONTRACT + e.Message });
                    }
                }
                #endregion
                #region CONTRACT_AREA_PAYOFF_SIGNAL
                if (IsPropertyExist(dataModel, WorkFlowHistoriesController.CONTRACT_AREA_PAYOFF_SIGNAL))
                {
                    try
                    {
                        var contractArea = _contractService
                            .GetContracts(_ => _.CustomerWorkflowId == process.CustomerWorkFlowId)
                            .FirstOrDefault();
                        if (contractArea == null)
                        {
                            //return BadRequest("Phát hiện QT không tạo HĐ mà đòi thanh lý");
                        }
                        else
                        {
                            contractArea.Status = (int)ContractStatus.PAYOFF;
                            foreach (var append in contractArea.ContractAppendices)
                            {
                                append.Status = (int)ContractStatus.PAYOFF;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Mess = ErrorMess.PAYOFF_CONTRACT });

                    }
                }
                #endregion
                #region COTRACT_TELECOM_SIGNAL
                if (IsPropertyExist(dataModel, WorkFlowHistoriesController.COTRACT_TELECOM_SIGNAL))
                {
                    try
                    {
                        var customer = process.CustomerWorkFlow.Customer;
                        ContractTelecom contractTelecom = model.FormData.Adapt<ContractTelecom>();
                        contractTelecom.CustomerId = customer.Id;
                        contractTelecom.CustomerWorkflowId = process.CustomerWorkFlowId;
                        //Validation
                        if (String.IsNullOrEmpty(contractTelecom.ContractNo))
                        {
                            return BadRequest();
                        }
                        String typeInvestment = dataModel["TypeInvestmentString"];
                        contractTelecom.TypeInvestment = (int)TypeInvestment.KHONG;

                        if (typeInvestment.Trim().Contains("Tập trung"))
                            contractTelecom.TypeInvestment = (int)TypeInvestment.TAPTRUNG;
                        if (typeInvestment.Trim().Contains("Đơn vị"))
                            contractTelecom.TypeInvestment = (int)TypeInvestment.DONVI;


                        _contractTelecomService.CreateContractTelecom(contractTelecom);
                        //_contractTelecomService.SaveContractTelecom();
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Mess = ErrorMess.CREATE_CONTRACT });
                    }
                }
                #endregion
                #region CONTRACT_TELECOM_PAYOFF_SIGNAL
                if (IsPropertyExist(dataModel, WorkFlowHistoriesController.CONTRACT_TELECOM_PAYOFF_SIGNAL))
                {
                    try
                    {
                        var contractTelecom = _contractTelecomService
                            .GetContractTelecoms(_ => _.CustomerWorkflowId == process.CustomerWorkFlowId)
                            .FirstOrDefault();
                        if (contractTelecom == null)
                        {
                            //return BadRequest("Phát hiện QT không tạo HĐ mà đòi thanh lý");
                        }
                        else
                        {
                            contractTelecom.Status = (int)ContractStatus.PAYOFF;
                            foreach (var append in contractTelecom.ContractTelecomAppendices)
                            {
                                append.Status = (int)ContractStatus.PAYOFF;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return BadRequest(new { Mess = ErrorMess.PAYOFF_CONTRACT });

                    }
                }
                #endregion
                foreach (var item in dataModel)
                {
                    string key = item.Name;
                    var value = item.Value;
                    if (value == null) continue;
                    if (key.Contains(GV))
                    {
                        var gvId = Guid.Parse(key.Split(DIVIDE_CHAR)[1]);
                        var gv = _globalVariableService.GetGlobalVariable(gvId);
                        if (gv == null) return BadRequest(ErrorMess.INVALID_GLOBAL_VARIABLE);

                        var valueDb = _globalVariableValueService
                                                .GetGlobalVariableValues(_ => _.GlobalVariableId.Equals(gvId) &&
                                                                         _.CustomerWorkflowId.Equals(process.CustomerWorkFlowId))
                                                .FirstOrDefault();
                        if (valueDb == null)
                        {
                            try
                            {
                                //create
                                _globalVariableValueService.CreateGlobalVariableValue(new GlobalVariableValue
                                {
                                    GlobalVariableId = gv.Id,
                                    Value = value,
                                    CustomerWorkflowId = process.CustomerWorkFlowId
                                });
                            }
                            catch (Exception)
                            {
                                //create
                                _globalVariableValueService.CreateGlobalVariableValue(new GlobalVariableValue
                                {
                                    GlobalVariableId = gv.Id,
                                    Value = JsonConvert.SerializeObject(value),
                                    CustomerWorkflowId = process.CustomerWorkFlowId,
                                    IsObject = true
                                });
                            }

                        }
                        else
                        {
                            try
                            {
                                valueDb.Value = value;
                            }
                            catch (Exception)
                            {

                                valueDb.Value = JsonConvert.SerializeObject(value);
                            }
                            //update
                        }
                    }
                    else
                    {
                        // post data in formdata
                        formData[key] = value;
                    }
                }
                process.FormData = JsonConvert.SerializeObject(formData);
                _workFlowHistoryService.EditWorkFlowHistory(process);
                _workFlowHistoryService.SaveWorkFlowHistory();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("files")]
        public async Task<ActionResult> PostFile(IFormFile file, Guid workFlowHistoryId, bool isTemplate)
        {
            await _workFlowHistoryFileService.UploadFile(file, workFlowHistoryId, isTemplate);
            return Ok();
        }

        [HttpGet("files/{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            var fileSupport = await _workFlowHistoryFileService.DownloadFile(id);

            if (fileSupport == null)
            {
                fileSupport = await _templateService.DownloadFile(id);
            }

            if (fileSupport != null)
            {
                return File(fileSupport.Stream, fileSupport.ContentType, fileSupport.FileName);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("templates")]
        public ActionResult GetTemplates(Guid id)
        {
            var instance = _workFlowHistoryService.GetWorkFlowHistory(id);
            if (instance != null)
            {
                var templates = _workFlowInstanceService
                    .GetTemplates(instance.InstanceId)
                    .Select(f => new
                    {
                        f.Id,
                        f.Name,
                        f.Date,
                        f.FormId
                    }).ToList();
                return Ok(templates);
            }
            return Ok();
        }

        [HttpGet("files")]
        public ActionResult GetFiles(Guid id)
        {
            var files = _workFlowHistoryFileService
                        .GetWorkFlowHistoryFiles()
                        .Where(f => f.WorkFlowHistoryId.Equals(id))
                        .Select(f => new
                        {
                            f.Id,
                            f.Name,
                            f.Date,
                            f.Path,
                        })
                        .ToList();
            return Ok(files);
        }
    }
}