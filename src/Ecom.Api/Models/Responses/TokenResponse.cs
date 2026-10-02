namespace Ecom.Api.Models.Responses
{
    public class TokenResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
