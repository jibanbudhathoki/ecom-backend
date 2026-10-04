using MediatR;

namespace Ecom.Application.Features.Authentication.Commands
{
    public class AuthenticationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string[] Errors { get; set; } = System.Array.Empty<string>();
    }

    public class LoginCommand : IRequest<AuthenticationResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
