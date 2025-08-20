using System;
using System.Collections.Generic;

namespace ProySpaHelena.Models;

public partial class Role
{
    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Trabajadora> Trabajadoras { get; set; } = new List<Trabajadora>();
}
