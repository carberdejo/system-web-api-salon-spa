using AutoMapper;
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
            return Ok(await _context.Reservas.ToListAsync());
        }

        // GET api/<ReservaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return Ok(reserva);
        }

        // POST api/<ReservaController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ReservaRequestDto value)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                ReservaMapper reservaMapper = new ReservaMapper();
                Reserva reserva = reservaMapper.toEntity(value);
                _context.Reservas.Add(reserva);
                await _context.SaveChangesAsync();

                var detalle = value.Detalles;
                List<DetallesReserva> lista = new List<DetallesReserva>();
                foreach (var item in detalle)
                {
                    DetallesReserva det = reservaMapper.toEntityDet(item);
                    det.ReservaId = reserva.Id;
                    lista.Add(det);
                }

                //Agregar los detalles de la reserva
                _context.DetallesReservas.AddRange(lista);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, value);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest($"Error al insertar la reserva: {ex.Message}");
            }
        }

        // GET api/<ReservaController>/5
        [HttpPut("{id}/estado")]
        public async Task<ActionResult> UpdateEstado(int id, [FromBody]EstadoReserva estado)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            reserva.Estado = estado.ToString();
            _context.Reservas.Update(reserva);
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
