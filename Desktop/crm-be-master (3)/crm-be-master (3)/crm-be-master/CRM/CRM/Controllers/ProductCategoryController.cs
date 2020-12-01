using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Service;
using CRM.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpDelete]
        public ActionResult Delete(ProductCategoriesDM DM)
        {
            try
            {
                var productCategory = _productCategoryService.GetProductCategory(_=>_.CategoryId.Equals(DM.CategoryId) 
                                                                                        && _.ProductId.Equals(DM.ProductId));
                if (productCategory == null) return NotFound();
                _productCategoryService.DeleteProductCategory(productCategory);
                _productCategoryService.SaveChange();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}
