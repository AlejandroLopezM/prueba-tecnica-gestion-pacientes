using GestionPacientes.Aplicacion.DTOs.Requests;
using GestionPacientes.Aplicacion.DTOs.Responses;
using GestionPacientes.Aplicacion.Interfaces.Repositorios;
using GestionPacientes.Aplicacion.Interfaces.Servicios;
using GestionPacientes.Aplicacion.Modelos.Paginacion;
using GestionPacientes.Dominio.Entidades;

namespace GestionPacientes.Aplicacion.Servicios;

public class PacienteServicio : IPacienteServicio
{
    private readonly IPacienteRepositorio _pacienteRepositorio;

    public PacienteServicio(IPacienteRepositorio pacienteRepositorio)
    {
        _pacienteRepositorio = pacienteRepositorio;
    }

    public async Task<PacienteResponseDto> CrearAsync(
        CrearPacienteRequestDto request,
        CancellationToken cancellationToken = default)
    {
        bool existe = await _pacienteRepositorio.ExisteDocumentoAsync(
            request.TipoDocumento.Trim(),
            request.NumeroDocumento.Trim(),
            null,
            cancellationToken);

        if (existe)
        {
            throw new InvalidOperationException("Ya existe un paciente con el mismo tipo y número de documento.");
        }

        var paciente = new Paciente
        {
            TipoDocumento = request.TipoDocumento.Trim(),
            NumeroDocumento = request.NumeroDocumento.Trim(),
            Nombres = request.Nombres.Trim(),
            Apellidos = request.Apellidos.Trim(),
            FechaNacimiento = request.FechaNacimiento,
            Telefono = string.IsNullOrWhiteSpace(request.Telefono) ? null : request.Telefono.Trim(),
            CorreoElectronico = string.IsNullOrWhiteSpace(request.CorreoElectronico) ? null : request.CorreoElectronico.Trim(),
            FechaCreacion = DateTime.UtcNow
        };

        Paciente creado = await _pacienteRepositorio.CrearAsync(paciente, cancellationToken);

        return MapearPaciente(creado);
    }

    public async Task<ResultadoPaginado<PacienteResponseDto>> ObtenerPaginadoAsync(
        FiltroPacientesRequestDto request,
        CancellationToken cancellationToken = default)
    {
        ResultadoPaginado<Paciente> resultado = await _pacienteRepositorio.ObtenerPaginadoAsync(request, cancellationToken);

        return new ResultadoPaginado<PacienteResponseDto>
        {
            Items = resultado.Items.Select(MapearPaciente).ToList(),
            TotalRegistros = resultado.TotalRegistros,
            PaginaActual = resultado.PaginaActual,
            TamanoPagina = resultado.TamanoPagina
        };
    }

    public async Task<DetallePacienteResponseDto> ObtenerPorIdAsync(
        int pacienteId,
        CancellationToken cancellationToken = default)
    {
        Paciente? paciente = await _pacienteRepositorio.ObtenerPorIdConCitasAsync(pacienteId, cancellationToken);

        if (paciente is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {pacienteId}.");
        }

        return MapearDetallePaciente(paciente);
    }

    public async Task<DetallePacienteResponseDto> ActualizarAsync(
        int pacienteId,
        ActualizarPacienteRequestDto request,
        CancellationToken cancellationToken = default)
    {
        Paciente? paciente = await _pacienteRepositorio.ObtenerPorIdAsync(pacienteId, cancellationToken);

        if (paciente is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {pacienteId}.");
        }

        bool existeDocumento = await _pacienteRepositorio.ExisteDocumentoAsync(
            request.TipoDocumento.Trim(),
            request.NumeroDocumento.Trim(),
            pacienteId,
            cancellationToken);

        if (existeDocumento)
        {
            throw new InvalidOperationException("Ya existe otro paciente con el mismo tipo y número de documento.");
        }

        paciente.TipoDocumento = request.TipoDocumento.Trim();
        paciente.NumeroDocumento = request.NumeroDocumento.Trim();
        paciente.Nombres = request.Nombres.Trim();
        paciente.Apellidos = request.Apellidos.Trim();
        paciente.FechaNacimiento = request.FechaNacimiento;
        paciente.Telefono = string.IsNullOrWhiteSpace(request.Telefono) ? null : request.Telefono.Trim();
        paciente.CorreoElectronico = string.IsNullOrWhiteSpace(request.CorreoElectronico) ? null : request.CorreoElectronico.Trim();

        await _pacienteRepositorio.ActualizarAsync(paciente, cancellationToken);

        Paciente? actualizado = await _pacienteRepositorio.ObtenerPorIdConCitasAsync(pacienteId, cancellationToken);

        if (actualizado is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {pacienteId} después de actualizar.");
        }

        return MapearDetallePaciente(actualizado);
    }

    public async Task EliminarAsync(
        int pacienteId,
        CancellationToken cancellationToken = default)
    {
        Paciente? paciente = await _pacienteRepositorio.ObtenerPorIdAsync(pacienteId, cancellationToken);

        if (paciente is null)
        {
            throw new KeyNotFoundException($"No se encontró el paciente con id {pacienteId}.");
        }

        await _pacienteRepositorio.EliminarAsync(paciente, cancellationToken);
    }

    public async Task<List<PacienteResponseDto>> ObtenerPorFechaCreacionAsync(
        FiltroPacientesPorFechaRequestDto request,
        CancellationToken cancellationToken = default)
    {
        List<Paciente> pacientes = await _pacienteRepositorio.ObtenerPorFechaCreacionAsync(
            request.FechaDesde,
            cancellationToken);

        return pacientes.Select(MapearPaciente).ToList();
    }

    private static PacienteResponseDto MapearPaciente(Paciente paciente)
    {
        return new PacienteResponseDto
        {
            PacienteId = paciente.PacienteId,
            TipoDocumento = paciente.TipoDocumento,
            NumeroDocumento = paciente.NumeroDocumento,
            Nombres = paciente.Nombres,
            Apellidos = paciente.Apellidos,
            Telefono = paciente.Telefono,
            CorreoElectronico = paciente.CorreoElectronico,
            FechaCreacion = paciente.FechaCreacion
        };
    }

    private static DetallePacienteResponseDto MapearDetallePaciente(Paciente paciente)
    {
        return new DetallePacienteResponseDto
        {
            PacienteId = paciente.PacienteId,
            TipoDocumento = paciente.TipoDocumento,
            NumeroDocumento = paciente.NumeroDocumento,
            Nombres = paciente.Nombres,
            Apellidos = paciente.Apellidos,
            FechaNacimiento = paciente.FechaNacimiento,
            Telefono = paciente.Telefono,
            CorreoElectronico = paciente.CorreoElectronico,
            FechaCreacion = paciente.FechaCreacion,
            Citas = paciente.Citas
                .OrderByDescending(c => c.FechaCita)
                .Select(c => new CitaResponseDto
                {
                    CitaId = c.CitaId,
                    FechaCita = c.FechaCita,
                    Observacion = c.Observacion
                })
                .ToList()
        };
    }
}