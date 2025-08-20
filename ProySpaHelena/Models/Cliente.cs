using System;
using System.Collections.Generic;

namespace ProySpaHelena.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public int Dni { get; set; }

    public DateTime? CreadoEn { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
