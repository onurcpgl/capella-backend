using API.Utilities.ResponseData;
using Application.DataTransferObject;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;
        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpPost("/role")]
        public async Task<IActionResult> Save([FromBody] RoleDto roleDto)
        {
            _logger.LogInformation("Inside Save of RoleController", roleDto);
            await _roleService.Save(roleDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS
            };
            return Ok(response);

        }

        [HttpGet("/roles")]
        public async Task<IActionResult> GetRoles()
        {
            _logger.LogInformation("Inside GetRoles of RoleController");
            var roles = await _roleService.GetAllRoles();
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = roles
            };
            return Ok(response);
        }

        [HttpGet("/roles/{code}")]
        public async Task<ActionResult> GetRoleByCode([FromRoute] string code)
        {
            _logger.LogInformation("Inside GetRoleByCode of RoleController",code);
            var role = await _roleService.GetRoleByCode(code);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = role
            };
            return Ok(response);
        }

        [HttpPut("/roles")]
        public async Task<IActionResult> Update([FromBody] RoleDto roleDto)
        {
            _logger.LogInformation("Inside Update of RoleController", roleDto);
            await _roleService.Update(roleDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS

            };
            return Ok(response);

        }

        [HttpDelete("/roles/{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            _logger.LogInformation("Inside Delete of RoleController", code);
            await _roleService.Delete(code);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS

            };
            return Ok(response);

        }
    }
}
