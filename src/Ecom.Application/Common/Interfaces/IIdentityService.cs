using System.Threading.Tasks;
using Ecom.Application.Common.Models;

namespace Ecom.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(Result Result, string Token, string Message)> LoginAsync(string email, string password);
        Task<(Result Result, string Message)> RegisterAsync(string email, string password, string firstName, string lastName);
    }
}
