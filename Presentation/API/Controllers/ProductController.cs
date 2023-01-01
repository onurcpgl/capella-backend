using API.Filters;
using Application.DataTransferObject;
using Application.Repositories.ProductAbstract;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
       

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("/product")]
        //[ServiceFilter(typeof(CustomAuthorizationFilter)), PermissionAttribute("product_added")]
        public async Task<IActionResult> AddProduct([FromForm] AddProductRequest addProductRequest)
        {
            ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(addProductRequest.ProductData);
            var result = await _productService.saveProduct(productDto, addProductRequest.Galleries);
            if (!result)
            {
                return BadRequest();
            }else
            {
                return Ok(true);
            }
            
        }

        [HttpGet("/products")]
        public async Task<IActionResult> ProductList()
        {
            List<Product> products = await _productService.productList();
            return Ok(products);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetProductByCode(string code)
        {
            var product = await _productService.GetProductByCode(code);

            if(product is null)
            {
                return BadRequest();
            }

            return Ok(product);
           
        }
    }
}
