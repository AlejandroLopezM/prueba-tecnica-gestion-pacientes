using GestionPacientes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionPacientes.Infraestructura.Configuraciones;

public class CitaConfiguracion : IEntityTypeConfiguration<Cita>
{
    public void Configure(EntityTypeBuilder<Cita> builder)
    {
        builder.ToTable("Appointments");

        builder.HasKey(c => c.CitaId);

        builder.Property(c => c.CitaId)
            .HasColumnName("AppointmentId")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.PacienteId)
            .HasColumnName("PatientId")
            .IsRequired();

        builder.Property(c => c.FechaCita)
            .HasColumnName("AppointmentDate")
            .IsRequired();

        builder.Property(c => c.Observacion)
            .HasColumnName("Notes")
            .HasMaxLength(500);
    }
}