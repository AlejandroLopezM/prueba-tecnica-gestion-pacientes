using GestionPacientes.Aplicacion.DTOs.Requests;
using GestionPacientes.Aplicacion.Interfaces.Repositorios;
using GestionPacientes.Aplicacion.Modelos.Paginacion;
using GestionPacientes.Dominio.Entidades;
using GestionPacientes.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace GestionPacientes.Infraestructura.Repositorios;

public class PacienteRepositorio : IPacienteRepositorio
{
    private readonly GestionPacientesDbContext _contexto;

    public PacienteRepositorio(GestionPacientesDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> ExisteDocumentoAsync(
        string tipoDocumento,
        string numeroDocumento,
        int? pacienteIdExcluir = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Paciente> query = _contexto.Pacientes.AsQueryable();

        query = query.Where(p =>
            p.TipoDocumento == tipoDocumento &&
            p.NumeroDocumento == numeroDocumento);

        if (pacienteIdExcluir.HasValue)
        {
            query = query.Where(p => p.PacienteId != pacienteIdExcluir.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<Paciente> CrearAsync(
        Paciente paciente,
        CancellationToken cancellationToken = default)
    {
        _contexto.Pacientes.Add(paciente);
        await _contexto.SaveChangesAsync(cancellationToken);
        return paciente;
    }

    public async Task<ResultadoPaginado<Paciente>> ObtenerPaginadoAsync(
        FiltroPacientesRequestDto filtro,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Paciente> query = _contexto.Pacientes
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtro.Nombre))
        {
            string nombre = filtro.Nombre.Trim();

            query = query.Where(p =>
                p.Nombres.Contains(nombre) ||
                p.Apellidos.Contains(nombre) ||
                (p.Nombres + " " + p.Apellidos).Contains(nombre));
        }

        if (!string.IsNullOrWhiteSpace(filtro.NumeroDocumento))
        {
            string numeroDocumento = filtro.NumeroDocumento.Trim();
            query = query.Where(p => p.NumeroDocumento == numeroDocumento);
        }

        int totalRegistros = await query.CountAsync(cancellationToken);

        List<Paciente> items = await query
            .OrderByDescending(p => p.FechaCreacion)
            .Skip((filtro.Pagina - 1) * filtro.TamanoPagina)
            .Take(filtro.TamanoPagina)
            .ToListAsync(cancellationToken);

        return new ResultadoPaginado<Paciente>
        {
            Items = items,
            TotalRegistros = totalRegistros,
            PaginaActual = filtro.Pagina,
            TamanoPagina = filtro.TamanoPagina
        };
    }

    public async Task<Paciente?> ObtenerPorIdAsync(
        int pacienteId,
        CancellationToken cancellationToken = default)
    {
        return await _contexto.Pacientes
            .FirstOrDefaultAsync(p => p.PacienteId == pacienteId, cancellationToken);
    }

    public async Task<Paciente?> ObtenerPorIdConCitasAsync(
        int pacienteId,
        CancellationToken cancellationToken = default)
    {
        return await _contexto.Pacientes
            .AsNoTracking()
            .Include(p => p.Citas)
            .FirstOrDefaultAsync(p => p.PacienteId == pacienteId, cancellationToken);
    }

    public async Task ActualizarAsync(
        Paciente paciente,
        CancellationToken cancellationToken = default)
    {
        _contexto.Pacientes.Update(paciente);
        await _contexto.SaveChangesAsync(cancellationToken);
    }

    public async Task EliminarAsync(
        Paciente paciente,
        CancellationToken cancellationToken = default)
    {
        _contexto.Pacientes.Remove(paciente);
        await _contexto.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Paciente>> ObtenerPorFechaCreacionAsync(
        DateTime fechaDesde,
        CancellationToken cancellationToken = default)
    {
        return await _contexto.Pacientes
            .FromSqlInterpolated($"EXEC dbo.sp_ObtenerPacientesCreadosDespuesDe {fechaDesde}")
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}