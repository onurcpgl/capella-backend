using Application.DataTransferObject;
using Application.Repositories;
using Application.Repositories.ProductAbstract;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;
      
        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;   
        }

        [HttpPost("/unit")]
        public async Task<IActionResult> Save([FromBody] UnitDto unitDto)
        {

            var result =await _unitService.Save(unitDto);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok(true);
            }
        }

        [HttpGet("/units")]
        public async Task<IActionResult> GetUnits()
        {
            var result = await _unitService.GetAllUnits();
            return Ok(result);
        }

        [HttpGet("/units/{code}")]
        public async Task<ActionResult> GetUnitByCode([FromRoute] string code)
        {
            var result = await _unitService.GetUnitByCode(code);
            return Ok(result);
        }
    }
}
