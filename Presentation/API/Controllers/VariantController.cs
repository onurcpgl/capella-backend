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
        public VariantController(IVariantService variantService)
        {
            _variantService = variantService;
        }

        [HttpPost("/variant")]
        public async Task<IActionResult> Save([FromBody] VariantDto variantDto)
        {

            var result = await _variantService.Save(variantDto);

            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok(true);
            }

        }

        [HttpGet("/variants")]
        public async Task<IActionResult> GetVariants()
        {
            var result = await _variantService.GetAllVariants();
            return Ok(result);
        }

        [HttpGet("/variants/{code}")]
        public async Task<ActionResult> GetVariantByCode([FromRoute] string code)
        {
            var result = await _variantService.GetVariantByCode(code);
            return Ok(result);
        }
    }
}
