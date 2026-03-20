using GestionPacientes.Aplicacion.DTOs.Requests;
using GestionPacientes.Aplicacion.Interfaces.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GestionPacientes.Api.Controllers;

[ApiController]
[Route("api/patients")]
public class PacientesController : ControllerBase
{
    private readonly IPacienteServicio _pacienteServicio;

    public PacientesController(IPacienteServicio pacienteServicio)
    {
        _pacienteServicio = pacienteServicio;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CrearPacienteRequestDto request,
        CancellationToken cancellationToken)
    {
        var resultado = await _pacienteServicio.CrearAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = resultado.PacienteId },
            resultado);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerPaginado(
        [FromQuery] FiltroPacientesRequestDto request,
        CancellationToken cancellationToken)
    {
        var resultado = await _pacienteServicio.ObtenerPaginadoAsync(request, cancellationToken);
        return Ok(resultado);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObtenerPorId(
        int id,
        CancellationToken cancellationToken)
    {
        var resultado = await _pacienteServicio.ObtenerPorIdAsync(id, cancellationToken);
        return Ok(resultado);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(
        int id,
        [FromBody] ActualizarPacienteRequestDto request,
        CancellationToken cancellationToken)
    {
        var resultado = await _pacienteServicio.ActualizarAsync(id, request, cancellationToken);
        return Ok(resultado);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(
        int id,
        CancellationToken cancellationToken)
    {
        await _pacienteServicio.EliminarAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("created-after")]
    public async Task<IActionResult> ObtenerCreadosDespuesDe(
        [FromQuery] FiltroPacientesPorFechaRequestDto request,
        CancellationToken cancellationToken)
    {
        var resultado = await _pacienteServicio.ObtenerPorFechaCreacionAsync(request, cancellationToken);
        return Ok(resultado);
    }
}