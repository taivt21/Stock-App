using System;
using Microsoft.AspNetCore.Mvc;
using StockAppWebApi.Models;
using StockAppWebApi.Services;
using StockAppWebApi.ViewModels;

namespace StockAppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
	public class UserController : ControllerBase
	{
        private readonly IUserService _userService;
        public UserController(IUserService userService)
		{
			_userService = userService;
		}

        //https://localhost:port/api/user/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            try
            {
                User? user = await _userService.Register(registerViewModel);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                // Handle duplicate username or email
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                // Handle other exceptions
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                string jwt = await _userService.Login(loginViewModel);
                return Ok(new { jwt });
            }
            catch (ArgumentException ex)
            {
                // Handle duplicate username or email
                return BadRequest(new { Message = ex.Message });
            }
            //catch (Exception)
            //{
            //    // Handle other exceptions
            //    return StatusCode(500, "Internal Server Error");
            //}
        }
    }
}

