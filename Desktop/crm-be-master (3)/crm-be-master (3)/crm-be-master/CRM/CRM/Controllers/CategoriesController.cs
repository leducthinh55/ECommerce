using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductCategoryService _productCategoryService;

        public CategoriesController(ICategoryService categoryService, IProductCategoryService productCategoryService)
        {
            _categoryService = categoryService;
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            List<CategoryVM> result = new List<CategoryVM>();
            var data = _categoryService.GetCategories(_ => _.IsDeleted == false);
            foreach (var item in data)
            {
                result.Add(item.Adapt<CategoryVM>());
            }
            return Ok(result);
        }


        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var category = _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.Adapt<CategoryVM>());
        }

        [HttpGet("{id}/Products")]
        public ActionResult GetProducts(Guid id)
        {
            var category = _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            List<ProductVM> result = new List<ProductVM>();
            foreach(var item in category.ProductCategories)
            {
                result.Add(item.Product.Adapt<ProductVM>());
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult Create([FromBody]CategoryCM category)
        {
            try
            {
                _categoryService.CreateCategory(category.Adapt<Category>());
                _categoryService.SaveCategory();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpPost("AddProducts")]
        public ActionResult AddProducts([FromBody]ProductsCategoryCM data)
        {
            try
            {
                foreach (var productId in data.ProductIds)
                {
                    _productCategoryService.CreateProductCategory(new Model.ProductCategory { CategoryId = data.CategoryId, ProductId = productId });
                }
                _productCategoryService.SaveChange();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpPut]
        public ActionResult Update([FromBody] CategoryUM categoryUM)
        {
            try
            {
                var category = _categoryService.GetCategory(categoryUM.Id);
                if (category == null) return NotFound();
                category = categoryUM.Adapt(category);
                _categoryService.EditCategory(category);
                _categoryService.SaveCategory();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201); 
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var category = _categoryService.GetCategory(id);
            if (category == null) return NotFound();
            _categoryService.RemoveCategory(category);
            _categoryService.SaveCategory();
            return Ok();
        }
    }
}