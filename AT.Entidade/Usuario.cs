using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AT.Entidade
{
    public class Usuario
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("senha")]
        public string Senha { get; set; }

        public void CriptografarPassword()
        {
            this.Senha = Convert.ToBase64String(Encoding.Default.GetBytes(this.Senha));
        }

    }
}
