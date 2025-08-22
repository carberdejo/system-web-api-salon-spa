using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProySpaHelena.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProySpaHelena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : ControllerBase
    {
        private readonly BdSpaHelenaContext _context;
        public ServicioController(BdSpaHelenaContext context)
        {
            _context = context;
        }
        // GET: api/<ServicioController>
        [HttpGet]
        public async Task<IEnumerable<Servicio>> GetServicios()
        {
            return await _context.Servicios.ToListAsync();
        }

        // GET api/<ServicioController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetServiciosById(int id)
        {
            var buscar = await _context.Servicios.FindAsync(id);
            if (buscar == null)
            {
                return NotFound();
            }
            return Ok(buscar);
        }

        // POST api/<ServicioController>
        [HttpPost]
        public async Task<ActionResult> PostServicios([FromBody] Servicio value)
        {
            if (value == null)
            {
                return BadRequest("Servicio cannot be null");
            }
            _context.Servicios.Add(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetServiciosById), new { id = value.Id }, value);
        }

        // PUT api/<ServicioController>/5
        [HttpPut]
        public async Task<ActionResult> PutServicios([FromBody] Servicio value)
        {
            if (value == null)
            {
                return BadRequest("Servicio cannot be null");
            }
            _context.Servicios.Update(value);
            await _context.SaveChangesAsync();
            return Ok($"Servicio {value.Nombre} actualizado correctamente");

        }

        // DELETE api/<ServicioController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletevServicios(int id)
        {
            var buscar = await _context.Servicios.FindAsync(id);
            if (buscar == null)
            {
                return NotFound();
            }
            buscar.Activo = "No";
            _context.Servicios.Update(buscar);
            await _context.SaveChangesAsync();
            return Ok($"Servicio {buscar.Nombre} eliminado correctamente");
        }
    }
}
