using System.Text.Json;
using GestionPacientes.Aplicacion.DTOs.Responses;

namespace GestionPacientes.Api.Middlewares;

public class MiddlewareManejoErrores
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MiddlewareManejoErrores> _logger;

    public MiddlewareManejoErrores(
        RequestDelegate next,
        ILogger<MiddlewareManejoErrores> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Se produjo una excepción no controlada.");

            int statusCode = ex switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                InvalidOperationException => StatusCodes.Status409Conflict,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            string mensaje = statusCode == StatusCodes.Status500InternalServerError
                ? "Ocurrió un error interno en el servidor."
                : ex.Message;

            var respuesta = new RespuestaErrorDto
            {
                StatusCode = statusCode,
                Mensaje = mensaje,
                Detalles = statusCode == StatusCodes.Status500InternalServerError
                    ? new List<string>()
                    : new List<string> { ex.Message }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(respuesta);
            await context.Response.WriteAsync(json);
        }
    }
}