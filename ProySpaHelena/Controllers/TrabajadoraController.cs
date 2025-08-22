using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProySpaHelena.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProySpaHelena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajadoraController : ControllerBase
    {
        private readonly BdSpaHelenaContext _context;
        public TrabajadoraController(BdSpaHelenaContext context)
        {
            _context = context;
        }

        // GET: api/<TrabajadoraController>
        [HttpGet]
        public async Task<IEnumerable<Trabajadora>> ListaTrabajadora()
        {
            return await _context.Trabajadoras.ToListAsync();
        }

        // GET api/<TrabajadoraController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTrabajadoraById(int id)
        {
            var buscar = await _context.Trabajadoras.FindAsync(id);
            if (buscar == null)
            {
                return NotFound();
            }

            return Ok(buscar);
        }

        // POST api/<TrabajadoraController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Trabajadora value)
        {
            try
            {
                _context.Trabajadoras.Add(value);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTrabajadoraById), new { id = value.Id }, value);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Error al insertar la db de trabajadora: {ex.Message}");
            }
        }

        // PUT api/<TrabajadoraController>/5
        [HttpPut]
        public async Task<ActionResult> PutTrabajadora([FromBody] Trabajadora value)
        {
            try
            {
                _context.Trabajadoras.Update(value);
                await _context.SaveChangesAsync();
                return Ok($"Trabajadora {value.Nombre} actualizada correctamente");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Error al actualizar la db de trabajadora: {ex.Message}");
            }


        }

        // DELETE api/<TrabajadoraController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrabajadora(int id)
        {
            var worker = await _context.Trabajadoras.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            worker.Activa = "No";
            _context.Trabajadoras.Update(worker);
            await _context.SaveChangesAsync();
            return Ok($"La trabajadora {worker.Nombre} se ha dado de baja");

        }
    }
}
