using API.Utilities.ResponseData;
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
        private readonly ILogger<UnitController> _logger;
        public UnitController(IUnitService unitService, ILogger<UnitController> logger)
        {
            _unitService = unitService;
            _logger = logger;
        }

        [HttpPost("/units")]
        public async Task<IActionResult> Save([FromBody] UnitDto unitDto)
        {
            _logger.LogInformation("Inside Save of UnitController", unitDto);
            await _unitService.Save(unitDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS
            };
            return Ok(response);
        }

        [HttpGet("/units")]
        public async Task<IActionResult> GetUnits()
        {
            _logger.LogInformation("Inside GetUnits of UnitController");
            var units = await _unitService.GetAllUnits();
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = units
            };
            return Ok(response);
        }

        [HttpGet("/units/{code}")]
        public async Task<ActionResult> GetUnitByCode([FromRoute] string code)
        {
            _logger.LogInformation("Inside GetUnitByCode of UnitController", code);
            var unit = await _unitService.GetUnitByCode(code);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = unit
            };
            return Ok(response);
        }

        [HttpPut("/units")]
        public async Task<IActionResult> Update([FromBody] UnitDto unitDto)
        {
            _logger.LogInformation("Inside Update of UnitController", unitDto);
            await _unitService.Update(unitDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS

            };
            return Ok(response);

        }

        [HttpDelete("/units/{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            _logger.LogInformation("Inside Delete of UnitController", code);
            await _unitService.Delete(code);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS

            };
            return Ok(response);

        }
    }
}
