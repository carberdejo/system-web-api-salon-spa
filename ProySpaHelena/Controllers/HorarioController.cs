    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProySpaHelena.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProySpaHelena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : ControllerBase
    {
        private readonly BdSpaHelenaContext _context;
        public HorarioController(BdSpaHelenaContext context)
        {
            _context = context;
        }

        // GET: api/<HorarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<HorarioController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetHorarioById(int id)
        {
            var buscar = await _context.Disponibilidads.Where(d => d.TrabajadoraId == id).SingleOrDefaultAsync();
            if (buscar == null)
            {
                return NotFound();
            }

            return Ok(buscar);
        }

        // POST api/<HorarioController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HorarioController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutHorario(int id, [FromBody] Disponibilidad value)
        {
            _context.Disponibilidads.Update(value);
            await _context.SaveChangesAsync();
            return Ok($"Se edito el horario del trabajador con id {value.TrabajadoraId}");

        }

        // DELETE api/<HorarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
