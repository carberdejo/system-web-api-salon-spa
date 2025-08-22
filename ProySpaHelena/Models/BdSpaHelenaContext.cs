using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProySpaHelena.Models;

public partial class BdSpaHelenaContext : DbContext
{
    public BdSpaHelenaContext()
    {
    }

    public BdSpaHelenaContext(DbContextOptions<BdSpaHelenaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencia> Asistencias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallesReserva> DetallesReservas { get; set; }

    public virtual DbSet<Disponibilidad> Disponibilidads { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Trabajadora> Trabajadoras { get; set; }

    public virtual DbSet<VariantesServicio> VariantesServicios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=localhost;database=BD_SPA_HELENA;User ID=sa;Password=sql;TrustServerCertificate=false;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AI");

        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__asistenc__3213E83FE0CFA4D5");

            entity.ToTable("asistencias");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.HoraEntrada)
                .HasColumnType("datetime")
                .HasColumnName("hora_entrada");
            entity.Property(e => e.HoraSalida)
                .HasColumnType("datetime")
                .HasColumnName("hora_salida");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("observaciones");
            entity.Property(e => e.RegistradaEn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("registrada_en");
            entity.Property(e => e.TrabajadoraId).HasColumnName("trabajadora_id");

            entity.HasOne(d => d.Trabajadora).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.TrabajadoraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__asistenci__traba__571DF1D5");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__clientes__3213E83F8671DAE1");

            entity.ToTable("clientes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Correo)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.CreadoEn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("creado_en");
            entity.Property(e => e.Dni).HasColumnName("dni");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("nombre_completo");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<DetallesReserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalles__3213E83F672293D7");

            entity.ToTable("detalles_reserva");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasDefaultValueSql("((1))")
                .HasColumnName("cantidad");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.ReservaId).HasColumnName("reserva_id");
            entity.Property(e => e.VarianteId).HasColumnName("variante_id");

            entity.HasOne(d => d.Reserva).WithMany(p => p.DetallesReservas)
                .HasForeignKey(d => d.ReservaId)
                .HasConstraintName("FK__detalles___reser__4F7CD00D");

            entity.HasOne(d => d.Variante).WithMany(p => p.DetallesReservas)
                .HasForeignKey(d => d.VarianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__detalles___varia__5070F446");
        });

        modelBuilder.Entity<Disponibilidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__disponib__3213E83F663A0807");

            entity.ToTable("disponibilidad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DiaSemana).HasColumnName("dia_semana");
            entity.Property(e => e.HoraFin).HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio).HasColumnName("hora_inicio");
            entity.Property(e => e.TrabajadoraId).HasColumnName("trabajadora_id");
            entity.Property(e => e.ValidoDesde)
                .HasColumnType("date")
                .HasColumnName("valido_desde");
            entity.Property(e => e.ValidoHasta)
                .HasColumnType("date")
                .HasColumnName("valido_hasta");

            entity.HasOne(d => d.Trabajadora).WithMany(p => p.Disponibilidads)
                .HasForeignKey(d => d.TrabajadoraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__disponibi__traba__5441852A");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reserva__3213E83FAC10BED6");

            entity.ToTable("reserva");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.Notas)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("notas");
            entity.Property(e => e.RecepcionistaId).HasColumnName("recepcionista_id");
            entity.Property(e => e.TrabajadoraId).HasColumnName("trabajadora_id");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reserva__cliente__49C3F6B7");

            entity.HasOne(d => d.Recepcionista).WithMany(p => p.ReservaRecepcionista)
                .HasForeignKey(d => d.RecepcionistaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reserva__recepci__4AB81AF0");

            entity.HasOne(d => d.Trabajadora).WithMany(p => p.ReservaTrabajadoras)
                .HasForeignKey(d => d.TrabajadoraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reserva__trabaja__4CA06362");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__roles__6ABCB5E07CD19212");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nombre, "UQ__roles__72AFBCC609DF47F4").IsUnique();

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__servicio__3213E83FB77CF3B4");

            entity.ToTable("servicios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("('SI')")
                .IsFixedLength()
                .HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Trabajadora>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__trabajad__3213E83FD8B9C8AD");

            entity.ToTable("trabajadoras");

            entity.HasIndex(e => e.Correo, "UQ__trabajad__2A586E0BB15FE14E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activa)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("('SI')")
                .IsFixedLength()
                .HasColumnName("activa");
            entity.Property(e => e.Apellido)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Dni).HasColumnName("dni");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Trabajadoras)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__trabajado__id_ro__3E52440B");
        });

        modelBuilder.Entity<VariantesServicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__variante__3213E83FE843A0D8");

            entity.ToTable("variantes_servicio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("('SI')")
                .IsFixedLength()
                .HasColumnName("activo");
            entity.Property(e => e.DuracionMin).HasColumnName("duracion_min");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.ServicioId).HasColumnName("servicio_id");

            entity.HasOne(d => d.Servicio).WithMany(p => p.VariantesServicios)
                .HasForeignKey(d => d.ServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__variantes__servi__45F365D3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
