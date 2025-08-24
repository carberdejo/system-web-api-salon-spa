using ProySpaHelena.DTO;
using ProySpaHelena.Models;

namespace ProySpaHelena.Mapper
{
    public static class TrabajadorMapper
    {
        public static Trabajadora toEntityCli(TrabajadorCreateRequestDTO req)
        {
            return new Trabajadora
            {
                Nombre = req.Nombre,
                Apellido = req.Apellido,
                Correo = req.Correo,
                Telefono = req.Telefono,
                Dni = req.Dni,
                Contrasena = req.Contrasena,
                IdRol = req.IdRol,
                Activa = "Si",
                FechaInicio = req.FechaInicio

            };
        }
        public static Disponibilidad toEntityDispo(TrabajadorCreateRequestDTO req,int id)
        {
            return new Disponibilidad
            {
                TrabajadoraId = id,
                DiaSemana = req.DiaSemana,
                HoraInicio = req.HoraInicio,
                HoraFin = req.HoraFin,
                ValidoDesde = req.ValidoDesde,
                ValidoHasta = req.ValidoHasta
            };
        }
    }
}
