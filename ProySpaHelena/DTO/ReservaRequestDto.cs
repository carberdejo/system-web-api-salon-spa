using ProySpaHelena.Models;

namespace ProySpaHelena.DTO
{
    public class ReservaRequestDto
    {
        public int ClienteId { get; set; }

        public int RecepcionistaId { get; set; }

        public DateTime Fecha { get; set; }

        public string? Estado { get; set; }

        public string? Notas { get; set; }

        public int TrabajadoraId { get; set; }
        public List<ReservaDetallerequestDto> Detalles { get; set; } = new List<ReservaDetallerequestDto>();
    }
}
