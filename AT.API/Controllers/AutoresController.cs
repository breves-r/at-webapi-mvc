using AT.Entidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutoresController : ControllerBase
    {
        private AutorService _service;

        public AutoresController(AutorService service)
        {
            _service = service;
        }


        // GET: api/<AutoresController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.ObterAutores());
        }

        // GET api/<AutoresController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var autor = _service.ObterAutorPorId(id);

            return autor != null ? Ok(autor) : NotFound();
        }

        // POST api/<AutoresController>
        [HttpPost]
        public IActionResult Post([FromBody] Autor autor)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var autorSalvo = _service.SalvarAutor(autor);

                return Created($"/autores/{autorSalvo.Id}", autorSalvo);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex);
            }
        }

        // PUT api/<AutoresController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Autor autor)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            if (_service.ObterAutorPorId(id) == null)
                return NotFound();

            var autorSalvo = _service.AtualizarAutor(autor);

            return Ok(autorSalvo);
        }

        // DELETE api/<AutoresController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var autor = _service.ObterAutorPorId(id);

            if (autor == null)
                return NotFound();

            _service.ExcluirAutor(autor);

            return NoContent();

        }
    }
}
