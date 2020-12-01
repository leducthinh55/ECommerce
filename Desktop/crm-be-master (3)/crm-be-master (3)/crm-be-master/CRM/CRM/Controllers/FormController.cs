using CRM.Helpers;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;
        private readonly IFormGroupService _formGroupService;
        private readonly IPermissionService _permissionService;

        public FormController(IFormService formService, IFormGroupService formGroupService, IPermissionService permissionService)
        {
            _formService = formService;
            _formGroupService = formGroupService;
            _permissionService = permissionService;
        }

        [HttpGet]
        public ActionResult GetAllForm()
        {
            List<FormVM> result = new List<FormVM>();
            var data = _formService.GetForms(_=>_.IsDeleted == false);
            foreach (var item in data)
            {
                result.Add(item.Adapt<FormVM>());
            }
            return Ok(result);
        }

        [HttpGet("AuthRead")]
        public ActionResult GetAuthRead(List<Guid> permissionIds)
        {
            permissionIds.Sort();
            List<FormVM> result = new List<FormVM>();
            var data = _formService.GetForms(_ => _.IsDeleted == false);
            foreach (var item in data)
            {
                if(permissionIds.BST(item.PermissionIdR))
                {
                    result.Add(item.Adapt<FormVM>());
                }
            }
            return Ok(result);
        }

        [HttpGet("AuthWrite")]
        public ActionResult GetAuthWrite(List<Guid> permissionIds)
        {
            permissionIds.Sort();
            List<FormVM> result = new List<FormVM>();
            var data = _formService.GetForms(_ => _.IsDeleted == false);
            foreach (var item in data)
            {
                if (permissionIds.BST(item.PermissionIdW))
                {
                    result.Add(item.Adapt<FormVM>());
                }
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetForm(Guid id)
        {
            var data = _formService.GetForm(id);

            return Ok(data.Adapt<FormVM>());
        }

        [HttpGet("{id}/FormGroups")]
        public ActionResult GetFormData(Guid id)
        {
            FormDataVM result = new FormDataVM();
            var data = _formService.GetForm(id);
            result.Form = data.Adapt<FormVM>();
            result.FormGroups = new List<ViewModels.FormGroupVM>();
            foreach (var item in data.FormGroups)
            {
                if(item.IsDeleted==false)
                {
                    var fg = JsonConvert.DeserializeObject<ViewModels.FormGroupDataVM>(item.Data);
                    if(fg.Type == "file")
                    {
                        fg.FieldType = "read-write";
                        fg.FileConfig = new FileConfig();
                        fg.FileConfig.FileType = "read-write";
                        fg.FileConfig.FileList = null;
                    }
                    result.FormGroups.Add(fg);
                }
                result.FormGroups =  result.FormGroups.OrderBy(_ => _.Order).ToList();
            }
            return Ok(result);
        }

        [HttpGet("{id}/FormGroupsAuthRead")]
        public ActionResult GetFormDataAuthRead(Guid id ,List<Guid> permissionIds)
        {
            permissionIds.Sort();
            FormDataVM result = new FormDataVM();
            var data = _formService.GetForm(id);
            result.Form = data.Adapt<FormVM>();
            result.FormGroups = new List<ViewModels.FormGroupVM>();
            foreach (var item in data.FormGroups)
            {
                if (item.IsDeleted == false && permissionIds.BST(item.PermissionIdR))
                {
                    result.FormGroups.Add(JsonConvert.DeserializeObject<ViewModels.FormGroupVM>(item.Data));
                }
                result.FormGroups = result.FormGroups.OrderBy(_ => _.Order).ToList();
            }
            return Ok(result);
        }

        [HttpGet("{id}/FormGroupsAuthWrite")]
        public ActionResult GetFormDataAuthWrite(Guid id, List<Guid> permissionIds)
        {
            permissionIds.Sort();
            FormDataVM result = new FormDataVM();
            var data = _formService.GetForm(id);
            result.Form = data.Adapt<FormVM>();
            result.FormGroups = new List<ViewModels.FormGroupVM>();
            foreach (var item in data.FormGroups)
            {
                if (item.IsDeleted == false && permissionIds.BST(item.PermissionIdW))
                {
                    result.FormGroups.Add(JsonConvert.DeserializeObject<ViewModels.FormGroupVM>(item.Data));
                }
                result.FormGroups = result.FormGroups.OrderBy(_ => _.Order).ToList();
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult Post([FromBody]FormDataCM formData)
        {
            try
            {
                var form = formData.Form.Adapt<Form>();
                _formService.CreateForm(form);
                _formService.SaveChange();

                var permissionR = new HsPermission
                {
                    Name = form.Name + "-" + "R"
                };
                var permissionW = new HsPermission
                {
                    Name = form.Name + "-" + "W"
                };
                //Save Change
                _permissionService.CreatePermission(permissionR);
                _permissionService.CreatePermission(permissionW);
                form.PermissionIdR = permissionR.Id;
                form.PermissionIdW = permissionW.Id;
                _formService.SaveChange();

                foreach (var _formGroup in formData.FormGroups)
                {
                    var formGroup = new Model.FormGroup
                    {
                        FormId = form.Id,
                        Data = JsonConvert.SerializeObject(_formGroup)
                    };
                    _formGroupService.CreateFormGroup(formGroup);
                    var _permissionR = new HsPermission
                    {
                        Name = form.Name + "-" + _formGroup.Name+"-" + "R"
                    };
                    var _permissionW = new HsPermission
                    {
                        Name = form.Name + "-" + _formGroup.Name + "-" + "W"
                    };
                    _permissionService.CreatePermission(_permissionR);
                    _permissionService.CreatePermission(_permissionW);
                    formGroup.PermissionIdR = _permissionR.Id;
                    formGroup.PermissionIdW = _permissionW.Id;
                    _formGroupService.UpdateFormGroup(formGroup);
                    _formService.SaveChange();
                }
                _formGroupService.SaveChange();

                return StatusCode(201, new { FormId = form.Id });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody]FormDataUM formData)
        {
            try
            {
                var form = _formService.GetForm(formData.Form.Id);
                if (form == null) return NotFound();
                form = formData.Form.Adapt(form);
                _permissionService.EditPermission(new
                            HsPermission
                {
                    Id = form.PermissionIdR.Value,
                    Name = form.Name + "-" + "R",
                });
                _permissionService.EditPermission(new
                            HsPermission
                {
                    Id = form.PermissionIdW.Value,
                    Name = form.Name + "-" + "W"
                });
                _formService.UpdateForm(form);
                var formGruopDb = _formGroupService.GetFormGroups(_ => _.FormId.Equals(form.Id));

                foreach (var item in formGruopDb)
                {
                    try
                    {
                        _formGroupService.DeleteFormGroup(item.Id);
                        _permissionService.RemovePermission(item.PermissionIdR.Value);
                        _permissionService.RemovePermission(item.PermissionIdW.Value);
                    }
                    catch { continue; }
                    
                }
                foreach (var _formGroup in formData.FormGroups)
                {
                    var formGroup = new Model.FormGroup
                    {
                        FormId = form.Id,
                        Data = JsonConvert.SerializeObject(_formGroup)
                    };
                    _formGroupService.CreateFormGroup(formGroup);
                    var _permissionR = new HsPermission
                    {
                        Name = form.Name + "-" + _formGroup.Name + "-" + "R"
                    };
                    var _permissionW = new HsPermission
                    {
                        Name = form.Name + "-" + _formGroup.Name + "-" + "W"
                    };
                    _permissionService.CreatePermission(_permissionR);
                    _permissionService.CreatePermission(_permissionW);
                    formGroup.PermissionIdR = _permissionR.Id;
                    formGroup.PermissionIdW = _permissionW.Id;
                    _formGroupService.UpdateFormGroup(formGroup);
                    _formService.SaveChange();
                }
                _formService.SaveChange();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message); 
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _formService.DeleteForm(id);
                _formService.SaveChange();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

    }
}
