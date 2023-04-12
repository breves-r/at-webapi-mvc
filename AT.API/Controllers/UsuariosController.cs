using AT.Entidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private UsuarioService _service;

        public UsuariosController(UsuarioService service)
        {
            _service = service;
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.ObterUsuario());
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _service.ObterUsuarioPorId(id);

            return user != null ? Ok(user) : NotFound();
        }

        // POST api/<UsuariosController>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var user = _service.SalvarUsuario(usuario);

                return Created($"/usuarios/{user.Id}", user);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex);
            }
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Usuario usuario)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            if (_service.ObterUsuarioPorId(id) == null)
                return NotFound();

            var user = _service.AtualizarUsuario(usuario);

            return Ok(user);
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _service.ObterUsuarioPorId(id);

            if (user == null)
                return NotFound();

            _service.ExcluirUsuario(user);

            return NoContent();
        }
    }
}
