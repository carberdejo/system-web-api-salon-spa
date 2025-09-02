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
    public class AsistenciaController : ControllerBase
    {
        private readonly BdSpaHelenaContext _context;
        public AsistenciaController(BdSpaHelenaContext context)
        {
            _context = context;
        }

        // GET: api/<AsistenciaController>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<AsistenciaDTO>>> GetAsistencia()
        //{
        //    var lista = await _context.Trabajadoras.Select(AsistenciaMapper.ToDtoLinq).ToListAsync();

        //    return Ok(lista);
        //}

        // GET api/<AsistenciaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AsistenciaController>
        [HttpPost]
        public async Task<ActionResult> PostAsistencia([FromBody] Asistencia value)
        {

            await _context.Asistencias.AddAsync(value);
            if (value.Estado == "PRESENTE" || value.Estado == "TARDE")
            {
                var per = await _context.Trabajadoras.FindAsync(value.TrabajadoraId);
                per!.Estado = "DISPONIBLE";
                _context.Trabajadoras.Update(per);
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);


        }


        // PUT api/<AsistenciaController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsistenciaSalida(int id, [FromBody] TimeSpan salida)
        {
            var asistencia = _context.Asistencias.Find(id);
            if (asistencia != null)
            {
                asistencia.HoraSalida = salida;
                //asistencia.Estado = "Finalizado";
                _context.Asistencias.Update(asistencia);
                await _context.SaveChangesAsync();
                return Ok($"La hora de salida del trabajador con codigo " +
                    $"{asistencia.TrabajadoraId} ha sido marcada");
            }
            else
            {
                return NotFound($"No se encontró asistencia con ID {id}.");
            }
        }

        // DELETE api/<AsistenciaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
