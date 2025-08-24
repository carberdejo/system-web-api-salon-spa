using ProySpaHelena.Models;

namespace ProySpaHelena.DTO
{
    public class ReservaReponseDto
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; } = "";

        public string NomRecepcionista { get; set; } = "";

        public DateTime Fecha { get; set; }

        public string? Estado { get; set; }

        public string? Notas { get; set; }

        public string NomTrabajadora { get; set; } = "";
        public List<ReservaDetalleResponseDto> Detalles { get; set; } = new List<ReservaDetalleResponseDto>();
    }
}
