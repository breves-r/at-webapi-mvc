using AT.WebApp.Models;
using AT.WebApp.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AT.WebApp.Repository
{
    public class AccountManager : IAccountManager
    {
        private SignInManager<UserAccount> SignInManager { get; set; }
        private HttpContext HttpContext { get; set; }

        public AccountManager(SignInManager<UserAccount> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            SignInManager = signInManager;
            HttpContext = httpContextAccessor.HttpContext;
        }

        public async Task<SignInResult> Login(string email, string senha)
        {
            var token = GetToken(email, senha);

            if (string.IsNullOrEmpty(token) == true)
            {
                return SignInResult.Failed;
            }

            var user = new UserAccount
            {
                Email = email,
                Senha = senha,
                Token = token
            };

            await SignInManager.SignInAsync(user, true);

            //Grava o token na Sessão
            HttpContext.Session.SetString(UserAccount.SESSION_TOKEN_KEY, token);

            return SignInResult.Success;

        }

        public async Task Logout()
        {
            await SignInManager.SignOutAsync();
        }

        private string GetToken(string email, string senha)
        {
            var user = new
            {
                email = email,
                senha = senha
            };

            var body = new StringContent(JsonSerializer.Serialize(user), new MediaTypeHeaderValue("application/json"));

            HttpClient httpClient = new HttpClient();
            var response = httpClient.PostAsync("https://localhost:7023/api/Token", body).Result;

            if (response.IsSuccessStatusCode == false)
                return String.Empty;

            var json = response.Content.ReadAsStringAsync().Result;
            var token = JsonSerializer.Deserialize<Token>(json);

            return token.AccessToken;
        }
    }
}
