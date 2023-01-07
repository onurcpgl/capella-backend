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
    public class ClassificationController : ControllerBase
    {

        private readonly IClassificationService _classificationService;
        private readonly ILogger<ClassificationController> _logger;
        public ClassificationController(IClassificationService classificationService, ILogger<ClassificationController> logger)
        {
            _classificationService = classificationService;
            _logger = logger;
        }

        [HttpPost("/classification")]
        public async Task<IActionResult> Save([FromBody] ClassificationDto classificationDto)
        {

            await _classificationService.Save(classificationDto);

            _logger.LogInformation("Inside Save of CategoryController", classificationDto);
            await _classificationService.Save(classificationDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS
            };
            return Ok(response);

        }

        [HttpGet("/classifications")]
        public async Task<IActionResult> GetClassifications()
        {
            _logger.LogInformation("Inside GetClassification of ClassificationController");
            List<Classification> classifications = await _classificationService.GetAllClassifications();
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = classifications
            };
            return Ok(response);
        }


        [HttpGet("/classifications/{code}")]
        public async Task<ActionResult> GetClassificationByCode([FromRoute] string code)
        {
            _logger.LogInformation("Inside GetClassifications of ClassificationController");

            var classification = await _classificationService.GetClassificationByCode(code);

            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = classification
            };

            return Ok(response);
        }


    }
}
