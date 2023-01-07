using API.Utilities.ResponseData;
using Application.DataTransferObject;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("/user")]
        public async Task<IActionResult> Save([FromBody] UserDto userDto)
        {
            _logger.LogInformation("Inside Save of UserController", userDto);
            await _userService.Save(userDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS
            };
            return Ok(response);

        }

        [HttpGet("/user")]
        public async Task<IActionResult> GetUsers()
        {
            _logger.LogInformation("Inside GetUsers of UserController");
            var users = await _userService.GetAllUsers();
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = users
            };
            return Ok(response);
        }

        [HttpGet("/users/{username}")]
        public async Task<ActionResult> GetUserByUsername([FromRoute] string username)
        {
            _logger.LogInformation("Inside GetUserByUsername of UserController", username);
            var user = await _userService.GetUserByUsername(username);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = user
            };
            return Ok(response);
        }

    }
}
