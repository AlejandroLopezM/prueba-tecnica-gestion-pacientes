using GestionPacientes.Aplicacion.DTOs.Requests;
using GestionPacientes.Aplicacion.DTOs.Responses;
using GestionPacientes.Aplicacion.Modelos.Paginacion;

namespace GestionPacientes.Aplicacion.Interfaces.Servicios;

public interface IPacienteServicio
{
    Task<PacienteResponseDto> CrearAsync(
        CrearPacienteRequestDto request,
        CancellationToken cancellationToken = default);

    Task<ResultadoPaginado<PacienteResponseDto>> ObtenerPaginadoAsync(
        FiltroPacientesRequestDto request,
        CancellationToken cancellationToken = default);

    Task<DetallePacienteResponseDto> ObtenerPorIdAsync(
        int pacienteId,
        CancellationToken cancellationToken = default);

    Task<DetallePacienteResponseDto> ActualizarAsync(
        int pacienteId,
        ActualizarPacienteRequestDto request,
        CancellationToken cancellationToken = default);

    Task EliminarAsync(
        int pacienteId,
        CancellationToken cancellationToken = default);

    Task<List<PacienteResponseDto>> ObtenerPorFechaCreacionAsync(
        FiltroPacientesPorFechaRequestDto request,
        CancellationToken cancellationToken = default);
}