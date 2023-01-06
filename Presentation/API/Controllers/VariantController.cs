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
        public async Task<IActionResult> AddVariant([FromBody] VariantDto variantDto)
        {

            var result = await _variantService.save(variantDto);

            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok(true);
            }

        }
    }
}
