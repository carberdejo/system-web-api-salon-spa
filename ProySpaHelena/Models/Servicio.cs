using System;
using System.Collections.Generic;

namespace ProySpaHelena.Models;

public partial class Servicio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Activo { get; set; }

    public virtual ICollection<VariantesServicio> VariantesServicios { get; set; } = new List<VariantesServicio>();
}
