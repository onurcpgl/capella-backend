using API.Utilities.ResponseData;
using Application.DataTransferObject;
using Application.Services.Variant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariantController : ControllerBase
    {
        private readonly IVariantService _variantService;
        private readonly ILogger<VariantController> _logger;
        public VariantController(IVariantService variantService, ILogger<VariantController> logger)
        {
            _variantService = variantService;
            _logger = logger;
        }

        [HttpPost("/variant")]
        public async Task<IActionResult> Save([FromBody] VariantDto variantDto)
        {
            _logger.LogInformation("Inside Save of VariantController", variantDto);
            await _variantService.Save(variantDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS
            };
            return Ok(response);

        }

        [HttpGet("/variants")]
        public async Task<IActionResult> GetVariants()
        {
            _logger.LogInformation("Inside GetVariants of VariantController");
            var variants = await _variantService.GetAllVariants();
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = variants
            };
            return Ok(response);
        }

        [HttpGet("/variants/{code}")]
        public async Task<ActionResult> GetVariantByCode([FromRoute] string code)
        {
            _logger.LogInformation("Inside GetVariantByCode of VariantController",code);
            var variant = await _variantService.GetVariantByCode(code);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = variant
            };
            return Ok(response);
        }
    }
}
