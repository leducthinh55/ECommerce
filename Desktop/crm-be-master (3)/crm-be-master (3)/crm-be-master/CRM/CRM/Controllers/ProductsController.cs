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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        public ProductsController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            List<ProductVM> result = new List<ProductVM>();
            var data = _productService.GetProducts(_ => _.IsDeleted == false);
            foreach(var item in data)
            {
                result.Add(item.Adapt<ProductVM>());
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id )
        {
            var product = _productService.GetProduct(id);
            if (product == null) return NotFound();
            return Ok(product.Adapt<ProductVM>());
        }

        [HttpGet("{id}/Categories")]
        public ActionResult GetCategories(Guid id)
        {
            var product = _productService.GetProduct(id);
            if (product == null) return NotFound();
            List<CategoryVM> result = new List<CategoryVM>();
            foreach(var item in product.ProductCategories)
            {
                result.Add(item.Category.Adapt<CategoryVM>());
            }
            return Ok(result);
        }

        [HttpGet("{id}/PriceHistories")]
        public ActionResult GetPriceHistories(Guid id)
        {
            var product = _productService.GetProduct(id);
            if (product == null) return NotFound();
            List<PriceHistoryVM> result = new List<PriceHistoryVM>();
            foreach (var item in product.PriceHistories)
            {
                result.Add(item.Adapt<PriceHistoryVM>());
            }
            return Ok(result);
        }

        [HttpGet("{id}/ProductAttributes")]
        public ActionResult GetProductAttributes(Guid id)
        {
            var product = _productService.GetProduct(id);
            if (product == null && product.IsDeleted) return NotFound();
            List<ProductAttributeVM> result = new List<ProductAttributeVM>();
            foreach (var item in product.Attributes)
            {
                var productAttribute = item.ProductAttribute.Adapt<ProductAttributeVM>();
                productAttribute.Value = item.PredefinedId != null ? item.Predefined.Value : item.Value;
                result.Add(productAttribute);
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult Create(ProductCM product)
        {
            try
            {
                _productService.CreateProduct(product.Adapt<Product>());
                _productService.SaveProduct();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return StatusCode(201);
        }

        [HttpPost("AddCategories")]
        public ActionResult AddCategories([FromBody]ProductCategoriesCM data)
        {
            try
            {
                foreach (var CategoryId in data.CategoryIds)
                {
                    _productCategoryService.CreateProductCategory(new Model.ProductCategory { CategoryId = CategoryId, ProductId = data.ProductId });
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
        public ActionResult Update(ProductUM productUM)
        {
            try
            {
                var product =  _productService.GetProduct(productUM.Id);
                if (product == null) return NotFound();
                product = productUM.Adapt(product);
                _productService.EditProduct(product);
                _productService.SaveProduct();
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (Guid id)
        {
            try
            {
                var product = _productService.GetProduct(id);
                if (product == null) return NotFound();
                _productService.RemoveProduct(product);
                _productService.SaveProduct();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }
    }
}