namespace ProySpaHelena.DTO
{
    public class ReservaDetalleResponseDto
    {
        public int ReservaId { get; set; }
        public int VarianteId { get; set; }
        public string NomVariante { get; set; } = "";

        public int? Cantidad { get; set; }

        public decimal Precio { get; set; }
    }
}
