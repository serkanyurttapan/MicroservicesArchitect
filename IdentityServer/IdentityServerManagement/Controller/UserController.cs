﻿using IdentityServerManagement.Dtos;
using IdentityServerManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServerManagement.Controller
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signupDto)
        {
            var user = new ApplicationUser()
            {
                UserName = signupDto.Username,
                Email = signupDto.Email,
                Country = signupDto.Country
            };
            var result = await _userManager.CreateAsync(user, signupDto.Password);
            if (!result.Succeeded)
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user is null)
                return BadRequest();
            return Ok(new { Id = user.Id, UserName = user.UserName, Email = user.Email, Country = user.Country });
        }
    }
}
