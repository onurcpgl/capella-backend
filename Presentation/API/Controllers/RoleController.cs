﻿using Application.DataTransferObject;
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
        public async Task<IActionResult> AddRole([FromBody] RoleDto roleDto)
        {
            var result = await _roleService.save(roleDto);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok(true);
            }

        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleById(int roleId)
        {
            var role = await _roleService.getRoleById(roleId);
            return Ok(role);

        }

        [HttpGet("/roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _roleService.getAll();
            return Ok(result);
        }
    }
}
