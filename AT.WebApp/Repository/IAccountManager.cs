using Microsoft.AspNetCore.Identity;

namespace AT.WebApp.Repository
{
    public interface IAccountManager
    {
        Task<SignInResult> Login(string email, string senha);
        Task Logout();
    }
}
