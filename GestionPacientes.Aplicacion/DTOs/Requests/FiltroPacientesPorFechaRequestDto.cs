using System.ComponentModel.DataAnnotations;

namespace GestionPacientes.Aplicacion.DTOs.Requests;

public class FiltroPacientesPorFechaRequestDto
{
    [Required]
    public DateTime FechaDesde { get; set; }
}