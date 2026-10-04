using Ecom.Application.Common.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Authentication.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticationResponse>
    {
        private readonly IIdentityService _identityService;

        public LoginCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<AuthenticationResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var (result, token, message) = await _identityService.LoginAsync(request.Email, request.Password);

            return new AuthenticationResponse
            {
                Success = result.Succeeded,
                Message = message,
                Token = token,
                Errors = result.Errors
            };
        }
    }
}
