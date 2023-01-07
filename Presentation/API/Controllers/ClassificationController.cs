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

        public ClassificationController(IClassificationService classificationService)
        {
            _classificationService = classificationService;
        }

        [HttpPost("/classification")]
        public async Task<IActionResult> Save([FromBody] ClassificationDto classificationDto)
        {
           
           var result = await _classificationService.Save(classificationDto);

           if (!result)
           {
                return BadRequest();
           }
           else
           {
                return Ok(true);
           }

        }

        [HttpGet("/classifications")]
        public async Task<IActionResult> GetClassifications()
        {
            List<Classification> classification = await _classificationService.GetAllClassifications();
            return Ok(classification);
        }


        [HttpGet("/classifications/{code}")]
        public async Task<ActionResult> GetClassificationByCode([FromRoute] string code)
        {
            var result = await _classificationService.GetClassificationByCode(code);
            return Ok(result);
        }


    }
}
