using System;
using System.Collections.Generic;

namespace ProySpaHelena.Models;

public partial class Disponibilidad
{
    public int Id { get; set; }

    public int TrabajadoraId { get; set; }

    public TimeSpan? HoraInicio { get; set; }

    public TimeSpan? HoraFin { get; set; }

    public string? EstadoHorario { get; set; }

    public DateTime? ValidoDesde { get; set; }

    public virtual Trabajadora? Trabajadora { get; set; } = null!;
}
