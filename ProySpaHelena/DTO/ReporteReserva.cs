namespace ProySpaHelena.DTO
{
    public class ReporteReserva
    {
        public int Id { get; set; }
        public string? Cliente { get; set; }
        public string? Telefono { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaAtencion { get; set; }
        public string? Servicio { get; set; }
        public string? Tratamiento { get; set; }
        public string? Trabajador { get; set; }
        public decimal MontoTotal { get; set; }
        public string? Estado { get; set; }
    }
}
