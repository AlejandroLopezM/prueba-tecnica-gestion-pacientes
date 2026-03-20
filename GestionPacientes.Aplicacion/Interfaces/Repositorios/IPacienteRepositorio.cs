using GestionPacientes.Aplicacion.DTOs.Requests;
using GestionPacientes.Aplicacion.Modelos.Paginacion;
using GestionPacientes.Dominio.Entidades;

namespace GestionPacientes.Aplicacion.Interfaces.Repositorios;

public interface IPacienteRepositorio
{
    Task<bool> ExisteDocumentoAsync(
        string tipoDocumento,
        string numeroDocumento,
        int? pacienteIdExcluir = null,
        CancellationToken cancellationToken = default);

    Task<Paciente> CrearAsync(
        Paciente paciente,
        CancellationToken cancellationToken = default);

    Task<ResultadoPaginado<Paciente>> ObtenerPaginadoAsync(
        FiltroPacientesRequestDto filtro,
        CancellationToken cancellationToken = default);

    Task<Paciente?> ObtenerPorIdAsync(
        int pacienteId,
        CancellationToken cancellationToken = default);

    Task<Paciente?> ObtenerPorIdConCitasAsync(
        int pacienteId,
        CancellationToken cancellationToken = default);

    Task ActualizarAsync(
        Paciente paciente,
        CancellationToken cancellationToken = default);

    Task EliminarAsync(
        Paciente paciente,
        CancellationToken cancellationToken = default);

    Task<List<Paciente>> ObtenerPorFechaCreacionAsync(
        DateTime fechaDesde,
        CancellationToken cancellationToken = default);
}