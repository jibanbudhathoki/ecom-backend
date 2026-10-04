using Ecom.Api.Models.Requests;
using Ecom.Api.Models.Responses;
using Ecom.Application.Features.Authentication.Commands;
using MediatR;
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
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = new LoginCommand
            {
                Email = request.Email,
                Password = request.Password
            };

            var response = await _mediator.Send(command);

            if (!response.Success)
            {
                return Unauthorized(new { Message = response.Message, Errors = response.Errors });
            }

            return Ok(new TokenResponse { 
                Success = true,
                Message = response.Message,
                Token = response.Token 
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var command = new RegisterCommand
            {
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var response = await _mediator.Send(command);

            if (!response.Success)
            {
                return BadRequest(new { Message = response.Message, Errors = response.Errors });
            }

            return Ok(new { Message = response.Message });
        }
    }
}
