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
        private readonly ILogger<ProductController> _logger;
       
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost("/product")]
        //[ServiceFilter(typeof(CustomAuthorizationFilter)), PermissionAttribute("product_added")]
        //public async Task<IActionResult> AddProduct([FromForm] AddProductRequest addProductRequest)
        public async Task<IActionResult> Save([FromBody] ProductDto productDto)
        {
            //ProductDto productDto = JsonConvert.DeserializeObject<ProductDto>(addProductRequest.ProductData);
            //var result = await _productService.saveProduct(productDto, addProductRequest.Galleries);
            var result = await _productService.Save(productDto);
            if (!result)
            {
                return BadRequest();
            }else
            {
                return Ok(true);
            }
            
        }

        [HttpGet("/products")]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogInformation("Ürünlerin tamamını listeleme isteği geldi.");
            List<Product> products = await _productService.GetAllProducts();
            if(products.Count == 0)
            {
                throw new Exception("Hata liste boş");
            }
            return Ok(products);
        }

        [HttpGet("/products/{code}")]
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
