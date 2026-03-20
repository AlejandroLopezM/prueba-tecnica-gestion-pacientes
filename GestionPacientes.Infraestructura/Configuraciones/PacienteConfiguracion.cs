using GestionPacientes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestionPacientes.Infraestructura.Configuraciones;

public class PacienteConfiguracion : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Patients");

        builder.HasKey(p => p.PacienteId);

        builder.Property(p => p.PacienteId)
            .HasColumnName("PatientId")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.TipoDocumento)
            .HasColumnName("DocumentType")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.NumeroDocumento)
            .HasColumnName("DocumentNumber")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Nombres)
            .HasColumnName("FirstName")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Apellidos)
            .HasColumnName("LastName")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.FechaNacimiento)
            .HasColumnName("BirthDate")
            .IsRequired();

        builder.Property(p => p.Telefono)
            .HasColumnName("PhoneNumber")
            .HasMaxLength(20);

        builder.Property(p => p.CorreoElectronico)
            .HasColumnName("Email")
            .HasMaxLength(150);

        builder.Property(p => p.FechaCreacion)
            .HasColumnName("CreatedAt")
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.HasIndex(p => new { p.TipoDocumento, p.NumeroDocumento })
            .IsUnique();

        builder.HasMany(p => p.Citas)
            .WithOne(c => c.Paciente)
            .HasForeignKey(c => c.PacienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}