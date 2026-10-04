
using Ecom.Application.Common.Interfaces;
using MediatR;

namespace Ecom.Application.Features.Authentication.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResponse>
    {
        private readonly IIdentityService _identityService;

        public RegisterCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<AuthenticationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return new AuthenticationResponse
                {
                    Success = false,
                    Message = "Passwords do not match.",
                    Errors = new[] { "Passwords do not match." }
                };
            }

            var (result, message) = await _identityService.RegisterAsync(request.Email, request.Password, request.FirstName, request.LastName);

            return new AuthenticationResponse
            {
                Success = result.Succeeded,
                Message = message,
                Errors = result.Errors
            };
        }
    }
}
