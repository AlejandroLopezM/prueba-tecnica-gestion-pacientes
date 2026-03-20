namespace GestionPacientes.Aplicacion.DTOs.Responses;

public class CitaResponseDto
{
    public int CitaId { get; set; }
    public DateTime FechaCita { get; set; }
    public string? Observacion { get; set; }
}