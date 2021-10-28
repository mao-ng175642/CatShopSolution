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
        public async Task<IActionResult> Authenticate([FromBody]LoginRequest request)
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
            return Ok( resultToken);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Resgister ([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(); 
            }
            var result = await _userService.Register(request);
            if (!result)
            {
                return BadRequest("Resgister is UnSuccessfull!");
            }
            return Ok();
        }

        //http:localhost/api/users/paging?pageIndex=1&pageSize=10&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaginf([FromQuery]GetUserPagingRequest request)
        {
            var lstUser = await _userService.getUserPaging(request);
            return Ok(lstUser);
        }
    }
}
