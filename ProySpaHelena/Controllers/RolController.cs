using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProySpaHelena.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProySpaHelena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly BdSpaHelenaContext _context;
        public RolController(BdSpaHelenaContext context)
        {
            _context = context;
        }

        // GET: api/<RolController>
        [HttpGet]
        public async Task<IEnumerable<Role>> ListaRol()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET api/<RolController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTrabajadoraById(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            return Ok(rol);
        }

        // PUT api/<RolController>/5
        [HttpPut]
        public async Task<ActionResult> PutRol([FromBody] Role value)
        {
            try
            {
                _context.Roles.Update(value);
                await _context.SaveChangesAsync();
                return Ok($"Rol {value.Nombre} actualizado correctamente");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Error al actualizar la db del rol: {ex.Message}");
            }
        }

        // POST api/<RolController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // DELETE api/<RolController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
