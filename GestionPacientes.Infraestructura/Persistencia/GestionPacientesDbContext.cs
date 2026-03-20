using GestionPacientes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GestionPacientes.Infraestructura.Persistencia;

public class GestionPacientesDbContext : DbContext
{
    public GestionPacientesDbContext(DbContextOptions<GestionPacientesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Cita> Citas => Set<Cita>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestionPacientesDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}