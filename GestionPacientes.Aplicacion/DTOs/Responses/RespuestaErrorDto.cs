namespace GestionPacientes.Aplicacion.DTOs.Responses;

public class RespuestaErrorDto
{
    public int StatusCode { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public List<string> Detalles { get; set; } = new();
}