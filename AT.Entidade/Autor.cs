
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AT.Entidade
{
    public class Autor
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        [Required(ErrorMessage = "Campo 'Nome' Obrigatório")]
        public string Nome { get; set; }

        [JsonPropertyName("sobrenome")]
        [Required(ErrorMessage = "Campo 'Sobrenome' Obrigatório")]
        public string Sobrenome { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Campo 'Email' Obrigatório")]
        public string Email { get; set; }

        [JsonPropertyName("dataNascimento")]
        [Required(ErrorMessage = "Campo 'DataNascimento' Obrigatório")]
        public DateTime DataNascimento { get; set; }

        [JsonPropertyName("livros")]
        public List<Livro>? Livros { get; set; }
    }
}
