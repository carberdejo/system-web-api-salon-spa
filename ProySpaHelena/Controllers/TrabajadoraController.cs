using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProySpaHelena.DTO;
using ProySpaHelena.Mapper;
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
            return await _context.Trabajadoras.Where(t => t.Activa!.ToLower()=="si").ToListAsync();
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
        public async Task<ActionResult> Post([FromBody] TrabajadorCreateRequestDTO value)
        {
            try
            {
                var worker = TrabajadorMapper.toEntityCli(value);
                await _context.Trabajadoras.AddAsync(worker);
                await _context.SaveChangesAsync();

                // Insertar disponibilidad
                var horario = TrabajadorMapper.toEntityDispo(value, worker.Id);
                await _context.Disponibilidads.AddAsync(horario);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTrabajadoraById), new { id = worker.Id }, worker);
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

        [HttpPost("/login")]
        public async Task<ActionResult> LoginTrabajadora([FromBody]LoginRequestDTO obj)
        {
            var worker = await _context.Trabajadoras.Where
                        (x => x.Correo == obj.Email && x.Contrasena == obj.Password).FirstOrDefaultAsync();
            if (worker == null)
            {
                return NotFound();
            }
            
            return Ok(worker);

        }
    }
}
