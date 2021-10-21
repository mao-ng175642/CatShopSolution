using CatShopSolution.Application.System.Users;
using CatShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromForm]LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var resultToken = await _userService.Authencate(request);
            if(string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Username or password is incorrect");
            }
            return Ok(new { token = resultToken });
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Resgister ([FromForm] RegisterRequest request)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var result = await _userService.Register(request);
            if (!result)
            {
                return BadRequest("Resgister  UnSuccessfull!");
            }
            return Ok();
        }
    }
}
