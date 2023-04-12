using AT.Entidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LivrosController : ControllerBase
    {
        private LivroService _service;
        private AutorService _autorService;

        public LivrosController(LivroService service, AutorService autorService)
        {
            _service = service;
            _autorService = autorService;
        }

        // GET: api/<LivrosController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.ObterLivros());
        }

        // GET api/<LivrosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var livro = _service.ObterLivroPorId(id);

            return livro != null ? Ok(livro) : NotFound();
        }

        // POST api/<LivrosController>
        [HttpPost]
        public IActionResult Post([FromBody] LivroDTO body)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            foreach(var id in body.idAutores)
            {
                Autor autor = _autorService.ObterAutorPorId(id);
                body.livro.Autores.Add(autor);
            }

            try
            {
                var livroSalvo = _service.SalvarLivro(body.livro);

                return Created($"/livros/{livroSalvo.Id}", livroSalvo);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex);
            }
        }

        // PUT api/<LivrosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Livro livro)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            if (_service.ObterLivroPorId(id) == null)
                return NotFound();

            var livroSalvo = _service.AtualizarLivro(livro);

            return Ok(livroSalvo);
        }

        // DELETE api/<LivrosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var livro = _service.ObterLivroPorId(id);

            if (livro == null)
                return NotFound();

            _service.ExcluirLivro(livro);

            return NoContent();
        }
    }

    public class LivroDTO
    {
        [Required]
        public Livro livro { get; set;}
        [Required(ErrorMessage = "Campo Autores é obrigatório")]
        public List<int>? idAutores { get; set;}
    }
}
