namespace ProySpaHelena.DTO
{
    public class AsistenciaDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;
        public TimeSpan? HoraInicio { get; set; }

        public TimeSpan? HoraFin { get; set; }
        public string? Estado { get; set; }

    }
}
