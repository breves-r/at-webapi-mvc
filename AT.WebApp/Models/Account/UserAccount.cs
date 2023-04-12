using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace AT.WebApp.Models.Account
{
    public class UserAccount
    {
        public const string SESSION_TOKEN_KEY = "UserAccountToken";

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Formato do email inválido")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Senha { get; set; }

        public string? Token { get; set; }

        public int Id
        {
            get
            {
                if (string.IsNullOrEmpty(Token))
                    return 0;

                var jwt = this.DecodeToken(Token);
                Console.WriteLine("no id" + jwt);
                var id = jwt.Claims.First(x => x.Type == "sub").Value;
                return Convert.ToInt32(id);
            }
        }
        private JwtSecurityToken DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            return jsonToken as JwtSecurityToken;
        }

    }
}
