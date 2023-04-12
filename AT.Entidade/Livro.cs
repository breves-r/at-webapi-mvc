

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AT.Entidade
{
    public class Livro
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("titulo")]
        [Required(ErrorMessage = "Campo 'Titulo' Obrigatório")]
        public string Titulo { get; set; }

        [JsonPropertyName("isbn")]
        [Required(ErrorMessage = "Campo 'ISBN' Obrigatório")]
        public string Isbn { get; set; }

        [JsonPropertyName("ano")]
        [Required(ErrorMessage = "Campo 'Ano' Obrigatório")]
        public DateTime Ano { get; set; }

        [JsonPropertyName("autores")]
        public List<Autor>? Autores { get; set; }

        public Livro() {
            this.Autores = new List<Autor>();
        }
    }
}
