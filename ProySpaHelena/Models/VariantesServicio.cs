using System;
using System.Collections.Generic;

namespace ProySpaHelena.Models;

public partial class VariantesServicio
{
    public int Id { get; set; }

    public int ServicioId { get; set; }

    public string Nombre { get; set; } = null!;

    public int DuracionMin { get; set; }

    public decimal Precio { get; set; }

    public string? Activo { get; set; }

    public virtual ICollection<DetallesReserva> DetallesReservas { get; set; } = new List<DetallesReserva>();

    public virtual Servicio? Servicio { get; set; } = null!;
}
