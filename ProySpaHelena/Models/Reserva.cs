using System;
using System.Collections.Generic;

namespace ProySpaHelena.Models;

public partial class Reserva
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public int RecepcionistaId { get; set; }

    public DateTime Fecha { get; set; }

    public string? Estado { get; set; }

    public string? Notas { get; set; }

    public int TrabajadoraId { get; set; }

    public virtual Cliente? Cliente { get; set; } = null!;

    public virtual ICollection<DetallesReserva> DetallesReservas { get; set; } = new List<DetallesReserva>();

    public virtual Trabajadora? Recepcionista { get; set; } = null!;

    public virtual Trabajadora? Trabajadora { get; set; } = null!;
}
