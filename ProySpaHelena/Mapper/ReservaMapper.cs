using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProySpaHelena.DTO;
using ProySpaHelena.Models;

namespace ProySpaHelena.Mapper
{
    public static class ReservaMapper
    {
       
        public static Reserva toEntity(ReservaRequestDto dto)
        {
            return new Reserva()
            {
                ClienteId = dto.ClienteId,
                RecepcionistaId = dto.RecepcionistaId,
                Fecha = dto.Fecha,
                Estado = dto.Estado,
                Notas = dto.Notas,
                TrabajadoraId = dto.TrabajadoraId,

            };
        }
        public static DetallesReserva toEntityDet(ReservaDetallerequestDto dto)
        {
            return new DetallesReserva()
            {
                VarianteId = dto.VarianteId,
                Precio = dto.Precio,
                Cantidad = dto.Cantidad
            };
        }

        public static Expression<Func<Reserva, ReservaReponseDto>> toDTOLinq =>
        res => new ReservaReponseDto
        {
            Id = res.Id,
            NombreCliente = res.Cliente.NombreCompleto,
            Fecha = res.Fecha,
            Estado = res.Estado,
            Notas = res.Notas,
            NomRecepcionista = res.Recepcionista.Nombre,
            NomTrabajadora = res.Trabajadora.Nombre
        };

        public static Expression<Func<DetallesReserva, ReservaDetalleResponseDto>> DetalletoDTOLinq =>
      det => new ReservaDetalleResponseDto
      {
          ReservaId = det.ReservaId,
          Cantidad = det.Cantidad,
          VarianteId = det.VarianteId,
          NomVariante = det.Variante.Nombre,
          Precio = det.Precio
      };



        public static ReservaReponseDto toDTO(Reserva res)
        {
            return new ReservaReponseDto()
            {
                Id = res.Id,
                NomRecepcionista = res.Recepcionista.Nombre ?? "No existe",
                Fecha = res.Fecha,
                Estado = res.Estado,
                Notas = res.Notas,
                NomTrabajadora = res.Trabajadora.Nombre ?? "No existe",

            };
        }

        public static ReservaDetalleResponseDto toDettDTO(DetallesReserva det)
        {
            return new ReservaDetalleResponseDto()
            {
                ReservaId = det.ReservaId,
                Cantidad = det.Cantidad,
                VarianteId = det.VarianteId,
                NomVariante = det.Variante.Nombre ?? "No existe",
                Precio = det.Precio
            };
        }
    }
}
