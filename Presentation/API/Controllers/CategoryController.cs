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
       

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }
      
        [HttpPost("/category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            var result = await _categoryService.saveCategory(categoryDto);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok(true);
            }
        }

        [HttpGet("/categories")]
        public async Task<IActionResult> CategoryList()
        {
            var result = await _categoryService.categoryList();
            return Ok(result);
        }

        [HttpPost("/category/reorder")]
        public async Task<IActionResult> CategoryReorder([FromBody] CategoryReorderDto categoryReorderDto)
        {
            var result = await _categoryService.changeLocationCategory(categoryReorderDto);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok(true);
            }
        }

        [HttpGet("/category/{code}")]
        public async Task<IActionResult> GetCategory(string code)
        {
            var result = await _categoryService.GetCategoryByCode(code);

            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
