//using System.Linq.Expressions;
//using ProySpaHelena.DTO;
//using ProySpaHelena.Models;

//public static class AsistenciaMapper
//{
//    public static Expression<Func<Trabajadora, AsistenciaDTO>> ToDtoLinq =>
//        res => new AsistenciaDTO
//        {
//            Id = res.Id,
//            Nombre = res.Nombre,
//            Apellido = res.Apellido,

//            // Primera hora "Disponible" válida (opcional: filtra por fecha de vigencia)
//            HoraInicio = res.Disponibilidads
//                .Where(d =>
//                    ((d.EstadoHorario ?? "").Trim().ToLower()) == "disponible" &&
//                    (d.ValidoDesde == null || d.ValidoDesde <= DateTime.Today)
//                )
//                .OrderBy(d => d.HoraInicio)
//                .Select(d => d.HoraInicio)             // TimeSpan?
//                .FirstOrDefault(),

//            // Última hora "Disponible"
//            HoraFin = res.Disponibilidads
//                .Where(d =>
//                    ((d.EstadoHorario ?? "").Trim().ToLower()) == "disponible" &&
//                    (d.ValidoDesde == null || d.ValidoDesde <= DateTime.Today)
//                )
//                .OrderByDescending(d => d.HoraFin)
//                .Select(d => d.HoraFin)               // TimeSpan?
//                .FirstOrDefault(),

//            // Estado de la asistencia más reciente (por fecha)
//            Estado = res.Asistencia
//                .OrderByDescending(a => a.Fecha)
//                .Select(a => a.Estado)
//                .FirstOrDefault()
//        };
//}
