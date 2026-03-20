namespace GestionPacientes.Aplicacion.DTOs.Responses;

public class PacienteResponseDto
{
    public int PacienteId { get; set; }
    public string TipoDocumento { get; set; } = string.Empty;
    public string NumeroDocumento { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? CorreoElectronico { get; set; }
    public DateTime FechaCreacion { get; set; }
}