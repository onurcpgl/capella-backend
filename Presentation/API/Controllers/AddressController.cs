using API.Utilities.ResponseData;
using Application.DataTransferObject;
using Application.Services.Address;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly ILogger<AddressController> _logger;
        public AddressController(IAddressService addressService, ILogger<AddressController> logger)
        {
            _addressService = addressService;
            _logger = logger;
        }

        [HttpPost("/address")]
        public async Task<IActionResult> Save([FromBody] AddressDto addressDto)
        {
            _logger.LogInformation("Inside Save of AddressController", addressDto);
            await _addressService.Save(addressDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS
            };
            return Ok(response);

        }
    }
}
