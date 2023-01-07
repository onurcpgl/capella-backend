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
        public async Task<IActionResult> Save([FromBody] CategoryDto categoryDto)
        {
            var result = await _categoryService.Save(categoryDto);
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
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetAllCategories();
            return Ok(result);
        }

        [HttpPost("/category/reorder")]
        public async Task<IActionResult> CategoryReorder([FromBody] CategoryReorderDto categoryReorderDto)
        {
            var result = await _categoryService.ChangeLocationCategory(categoryReorderDto);
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
        public async Task<IActionResult> GetCategoryByCode(string code)
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
