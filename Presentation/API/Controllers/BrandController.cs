using Application.DataTransferObject;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost("/brand")]
        public async Task<IActionResult> Save([FromBody] BrandDto brandDto)
        {
            var result = await _brandService.Save(brandDto);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok(true);
            }

        }

        [HttpGet("/brands/{code}")]
        public async Task<ActionResult> GetBrandByCode([FromRoute] string code)
        {
            var result = await _brandService.getBrandByCode(code);
            return Ok(result);
        }
    }
}
