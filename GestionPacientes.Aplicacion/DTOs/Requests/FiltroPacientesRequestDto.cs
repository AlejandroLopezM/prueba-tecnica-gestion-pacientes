using System.ComponentModel.DataAnnotations;

namespace GestionPacientes.Aplicacion.DTOs.Requests;

public class FiltroPacientesRequestDto
{
    public string? Nombre { get; set; }
    public string? NumeroDocumento { get; set; }

    [Range(1, int.MaxValue)]
    public int Pagina { get; set; } = 1;

    [Range(1, 100)]
    public int TamanoPagina { get; set; } = 10;
}