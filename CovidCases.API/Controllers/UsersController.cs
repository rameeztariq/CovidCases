using CovidCases.API.Models;
using CovidCases.Contract;
using CovidCases.Contract.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CovidCases.API.Controllers
{/// <summary>
 /// The API will be used for user management.
 /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly AppSettings _appSettings;
        /// <summary>
        /// The user service will be responsible the user management. The AppSettings class defines the JWT properties.
        /// </summary>
        /// <param name="usersService"></param>
        /// <param name="appSettings"></param>
    
        public UsersController(IUsersService usersService, IOptions<AppSettings> appSettings)
        {
            _usersService = usersService;
            _appSettings = appSettings.Value;
        }
        /// <summary>
        /// Save user into the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult>RegisterUser(UserInputViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _usersService.AddUserAsync(viewModel);
            return StatusCode(result.StatusCode,result);
        }

        /// <summary>
        /// Generate the token after successfull login
        /// </summary>
        /// <returns></returns>
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
                var token  = new Security.JwtGenerator(_appSettings).GenerateToken(result.Payload);
                return StatusCode(result.StatusCode, token);
            }
            // In case of error
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Get user basic information
        /// </summary>
        /// <header>
        /// User token is required
        /// </header>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // read the userId value from the claims
            var userId = this.User.FindFirst("Id").Value;
            // make the database call to fetch the user information
            var result = await _usersService.GetAsync(o => o.Id == Convert.ToInt32(userId));
            // display only certain information for the user
            var CurrentUser =  result.Select(i => new { i.Email, i.FirstName,i.LastName,i.Id }).ToList();
            return Ok(CurrentUser);
        }

        /// <summary>
        /// Update the user into the database
        /// </summary>
        /// <header>
        /// User token is required
        /// </header>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UsersViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // read the userId value from the claims
            var userId = this.User.FindFirst("Id").Value;
            // Update the user into the database
            var result = await _usersService.UpdateUserAsync(Convert.ToInt32(userId),viewModel);    
            return StatusCode(result.StatusCode, result);
        }
        /// <summary>
        /// Delete the user from the database.
        /// </summary>
        /// <header>
        /// User token is required
        /// </header>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // read the userId value from the claims
            var userId = this.User.FindFirst("Id").Value;
            // Delete the user into the database
            var result = await _usersService.DeleteUserAsync(Convert.ToInt32(userId));
            return StatusCode(result.StatusCode, result);
        }

    }
}
