using System;
using System.Collections.Generic;

namespace ProySpaHelena.Models;

public partial class DetallesReserva
{
    public int Id { get; set; }

    public int ReservaId { get; set; }

    public int VarianteId { get; set; }

    public int? Cantidad { get; set; }

    public decimal Precio { get; set; }

    public virtual Reserva? Reserva { get; set; } = null!;

    public virtual VariantesServicio? Variante { get; set; } = null!;
}
