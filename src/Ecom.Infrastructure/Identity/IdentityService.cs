using System.Linq;
using System.Threading.Tasks;
using Ecom.Application.Common.Interfaces;
using Ecom.Application.Common.Models;
using Ecom.Infrastructure.Authentication.Jwt;
using Microsoft.AspNetCore.Identity;

namespace Ecom.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public IdentityService(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<(Result Result, string Token, string Message)> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return (Result.Failure(new[] { "Invalid email or password." }), string.Empty, "Invalid email or password.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user, roles);

            return (Result.Success(), token, "Logged in successfully.");
        }

        public async Task<(Result Result, string Message)> RegisterAsync(string email, string password, string firstName, string lastName)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return (Result.Failure(new[] { "Email is already in use." }), "Email is already in use.");
            }

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return (Result.Failure(result.Errors.Select(e => e.Description)), string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return (Result.Success(), "User registered successfully.");
        }
    }
}
