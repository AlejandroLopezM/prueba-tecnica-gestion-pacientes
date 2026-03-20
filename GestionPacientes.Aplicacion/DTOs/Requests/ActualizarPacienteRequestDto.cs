using System.ComponentModel.DataAnnotations;

namespace GestionPacientes.Aplicacion.DTOs.Requests;

public class ActualizarPacienteRequestDto
{
    [Required]
    [StringLength(20)]
    public string TipoDocumento { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string NumeroDocumento { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Nombres { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Apellidos { get; set; } = string.Empty;

    [Required]
    public DateTime FechaNacimiento { get; set; }

    [StringLength(20)]
    [Phone]
    public string? Telefono { get; set; }

    [StringLength(150)]
    [EmailAddress]
    public string? CorreoElectronico { get; set; }
}