using ProySpaHelena.DTO;
using ProySpaHelena.Models;

namespace ProySpaHelena.Mapper
{
    public class ReservaMapper
    {
        public Reserva toEntity(ReservaRequestDto dto)
        {
            return new Reserva()
            {
                ClienteId = dto.ClienteId,
                RecepcionistaId = dto.RecepcionistaId,
                Fecha = dto.Fecha,
                Estado = dto.Estado.ToString(),
                Notas = dto.Notas,
                TrabajadoraId = dto.TrabajadoraId,

            };
        }
        public DetallesReserva toEntityDet(ReservaDetallerequestDto dto)
        {
            return new DetallesReserva()
            {
                VarianteId = dto.VarianteId,
                Precio = dto.Precio,
                Cantidad = dto.Cantidad
            };
        }
    }
}
