
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
    public class ReservaController : ControllerBase
    {
        private readonly BdSpaHelenaContext _context;

        

        public ReservaController(BdSpaHelenaContext context)
        {
            _context = context;

        }
        // GET: api/<ReservaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReserva()
        {
            var lista = await _context.Reservas.Select(ReservaMapper.toDTOLinq).ToListAsync();
         
            return Ok(lista);
        }

        [HttpGet("Pendiente")]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservaPendiente()
        {
            var lista = await _context.Reservas.Where(r => r.Estado!.ToLower() == "pendiente").Select(ReservaMapper.toDTOLinq).ToListAsync();

            return Ok(lista);
        }
        [HttpGet("Progreso")]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservaProgreso()
        {
            var lista = await _context.Reservas.Where(r => r.Estado!.ToLower() == "en_servicio").Select(ReservaMapper.toDTOLinq).ToListAsync();

            return Ok(lista);
        }

        // GET api/<ReservaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetReserva(int id)
        {
            //var reserva = await _context.Reservas.FindAsync(id);
            
            var response = await _context.Reservas.Where(x => x.Id == id)
                .Select(ReservaMapper.toDTOLinq).FirstOrDefaultAsync();
            if (response == null)
            {
                return NotFound();
            }

            var detalle = await _context.DetallesReservas.Where(x=>x.ReservaId == id).Select(ReservaMapper.DetalletoDTOLinq).ToListAsync();
            response!.Detalles = detalle;

            return Ok(response);
        }

        // POST api/<ReservaController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ReservaRequestDto value)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                
                Reserva reserva = ReservaMapper.toEntity(value);
                _context.Reservas.Add(reserva);
                await _context.SaveChangesAsync();

                var detalle = value.Detalles;
                List<DetallesReserva> lista = new List<DetallesReserva>();
                foreach (var item in detalle)
                {
                    DetallesReserva det = ReservaMapper.toEntityDet(item);
                    det.ReservaId = reserva.Id;
                    lista.Add(det);
                }

                //Agregar los detalles de la reserva
                _context.DetallesReservas.AddRange(lista);
                await _context.SaveChangesAsync();


                await transaction.CommitAsync();


                return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, reserva);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(ex.ToString());
                return BadRequest($"Error al insertar la reserva: {ex.Message}");
            }
        }

        // GET api/<ReservaController>/5
        [HttpPut("{id}/estado")]
        public async Task<ActionResult> UpdateEstado(int id, [FromBody]string estado)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            reserva.Estado = estado;
            _context.Reservas.Update(reserva);
            if(estado.ToLower() == "en_servicio")
            {
                await _context.Trabajadoras.Where(t => t.Id == reserva.TrabajadoraId)
                    .ExecuteUpdateAsync(t => t.SetProperty(t => t.Estado, "OCUPADO"));
            }else if (estado.ToLower() == "completada")
            {
                await _context.Trabajadoras.Where(t => t.Id == reserva.TrabajadoraId)
                    .ExecuteUpdateAsync(t => t.SetProperty(t => t.Estado, "DISPONIBLE"));
            }
            await _context.SaveChangesAsync();
            return Ok(reserva);
        }


        // PUT api/<ReservaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReservaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
