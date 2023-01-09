using API.Utilities.ResponseData;
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
        private readonly ILogger<BrandController> _logger;
        public BrandController(IBrandService brandService, ILogger<BrandController> logger)
        {
            _brandService = brandService;
            _logger = logger;
        }

        [HttpPost("/brands")]
        public async Task<IActionResult> Save([FromBody] BrandDto brandDto)
        {
            _logger.LogInformation("Inside Save of BrandController", brandDto);
            await _brandService.Save(brandDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS
            };
            return Ok(response);
        }

        [HttpGet("/brands/{code}")]
        public async Task<ActionResult> GetBrandByCode([FromRoute] string code)
        {
            _logger.LogInformation("Inside GetBrandByCode of BrandController", code);
            var brand = await _brandService.GetBrandByCode(code);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = brand
                
            };
            return Ok(response);
        }

        [HttpPut("/brands")]
        public async Task<IActionResult> Update([FromBody] BrandDto brandDto)
        {
            _logger.LogInformation("Inside Update of BrandController", brandDto);
            await _brandService.Update(brandDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS

            };
            return Ok(response);

        }

        [HttpDelete("/brands/{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            _logger.LogInformation("Inside Delete of BrandController", code);
            await _brandService.Delete(code);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS

            };
            return Ok(response);

        }

        [HttpGet("/brands")]
        public async Task<IActionResult> GetBrands()
        {
            _logger.LogInformation("Inside GetBrands of BrandController");
            var brands = await _brandService.GetAllBrands();
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = brands
            };
            return Ok(response);
        }
    }
}
