using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using ProySpaHelena.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProySpaHelena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly BdSpaHelenaContext _context;
        public EspecialidadesController(BdSpaHelenaContext context)
        {
            _context = context;
        }
        // GET: api/<EspecialidadesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VariantesServicio>>> GetEspecialidades()
        {
            return Ok(await _context.VariantesServicios.ToListAsync());
        }

        // GET api/<EspecialidadesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEspecialidades(int id)
        {
            var buscar = await _context.VariantesServicios.FindAsync(id);
            if (buscar == null)
            {
                return NotFound();
            }
            return Ok(buscar);
        }

        // POST api/<EspecialidadesController>
        [HttpPost]
        public async Task<ActionResult> PostEspecialidades([FromBody] VariantesServicio value)
        {
            if (value == null)
            {
                return BadRequest("El objeto  no puede ser nulo.");
            }

            _context.VariantesServicios.Add(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEspecialidades), new { id = value.Id }, value);
        }

        // PUT api/<EspecialidadesController>/5
        [HttpPut]
        public async Task<ActionResult> PutEspecialidades([FromBody] VariantesServicio value)
        {
            if (value == null)
            {
                return BadRequest("El objeto  no puede ser nulo.");
            }
            _context.VariantesServicios.Update(value);
            await _context.SaveChangesAsync();
            return Ok($"Especialidad {value.Nombre} actualizada correctamente");

        }

        // DELETE api/<EspecialidadesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEspecialidades(int id)
        {
            var buscar = _context.VariantesServicios.Find(id);
            if (buscar == null)
            {
                return NotFound();
            }

            buscar.Activo = "No";
            _context.VariantesServicios.Update(buscar);
            await _context.SaveChangesAsync();

            return Ok($"Especialidad {buscar.Nombre} eliminada correctamente");

        }
    }
}
