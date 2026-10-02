using Ecom.Api.Models.Requests;
using Ecom.Api.Models.Responses;
using Ecom.Infrastructure.Authentication.Jwt;
using Ecom.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using System.Threading.Tasks;

namespace Ecom.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthenticationController(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user, roles);

            return Ok(new TokenResponse { 
                Success = true,
                Message = "Logged in successfully!",
                Token = token 
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest(new { Message = "Passwords do not match." });
            }

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return BadRequest(new { Message = "Email is already in use." });
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // By default, a newly registered user via API is a "Customer"
            // You can assign a role here if roles exist in the DB, e.g.,
            // await _userManager.AddToRoleAsync(user, "Customer");

            return Ok(new { Message = "User registered successfully." });
        }
    }
}
