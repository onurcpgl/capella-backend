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
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("/role")]
        public async Task<IActionResult> Save([FromBody] RoleDto roleDto)
        {
            var result = await _roleService.Save(roleDto);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok(true);
            }

        }

        [HttpGet("/roles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _roleService.GetAllRoles();
            return Ok(result);
        }

        [HttpGet("/roles/{code}")]
        public async Task<ActionResult> GetRoleByCode([FromRoute] string code)
        {
            var result = await _roleService.GetRoleByCode(code);
            return Ok(result);
        }
    }
}
