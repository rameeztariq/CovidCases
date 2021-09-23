using CovidCases.API.Models;
using CovidCases.Contract;
using CovidCases.Contract.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCases.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly AppSettings _appSettings;
        public UsersController(IUsersService usersService, IOptions<AppSettings> appSettings)
        {
            _usersService = usersService;
            _appSettings = appSettings.Value;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult>RegisterUser(UsersViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _usersService.AddUserAsync(viewModel);

            return StatusCode(result.StatusCode,result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser(UserLoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _usersService.LoginUserAsync(viewModel);
            if(result.Succeeded==true)
            {
                //Generate Token
                var x  = new Security.JwtGenerator(_appSettings).GenerateToken(result.Payload);
                return StatusCode(result.StatusCode, x);
            }

            return StatusCode(result.StatusCode, result);
        }
    }
}
