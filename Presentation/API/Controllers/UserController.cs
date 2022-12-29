﻿using Application.DataTransferObject;
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
        public UserController(IUserService userService)
        {
            _userService = userService; 
        }

        [HttpPost("/user")]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            var result = await _userService.save(userDto);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok(true);
            }

        }

        [HttpGet("/user")]
        public async Task<IActionResult> UserList()
        {
            var result = await _userService.userList();
            return Ok(result);
        }

        [HttpGet("/users/{username}")]
        public async Task<ActionResult> GetUserByUsername([FromRoute] string username)
        {
            var result = await _userService.getUserByUsername(username);
            return Ok(result);
        }

    }
}
