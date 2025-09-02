using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProySpaHelena.Models
{
    public class Reporte_Trabajadores

    {
        public int id { get; set; }
        public string nombre { get; set; } = "";
        public int dni{ get; set; }
        public string telefono { get; set; } = "";
        public int HorasTotales { get; set; }
        public int DiasaTrabajar { get; set; }
    }
}
