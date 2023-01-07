using API.Utilities.ResponseData;
using Application.DataTransferObject;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger; 

        }
      
        [HttpPost("/category")]
        public async Task<IActionResult> Save([FromBody] CategoryDto categoryDto)
        {
            _logger.LogInformation("Inside Save of CategoryController", categoryDto);
            await _categoryService.Save(categoryDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS
            };
            return Ok(response);

        }

        [HttpGet("/categories")]
        public async Task<IActionResult> GetCategories()
        {
            _logger.LogInformation("Inside GetCategories of CategoryController");
            var categories = await _categoryService.GetAllCategories();
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = categories
            };
            return Ok(response);
        }

        [HttpPost("/category/reorder")]
        public async Task<IActionResult> CategoryReorder([FromBody] CategoryReorderDto categoryReorderDto)
        {
            _logger.LogInformation("Inside CategoryReorder of CategoryController", categoryReorderDto);
            await _categoryService.ChangeLocationCategory(categoryReorderDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
            };
            return Ok(response);

        }

        [HttpGet("/category/{code}")]
        public async Task<IActionResult> GetCategoryByCode(string code)
        {
            _logger.LogInformation("Inside GetCategoryByCode of CategoryController", code);
            var category = await _categoryService.GetCategoryByCode(code);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = category
            };
            return Ok(response);
        }
    }
}
