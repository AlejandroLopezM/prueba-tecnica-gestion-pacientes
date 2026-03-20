using GestionPacientes.Aplicacion.Interfaces.Servicios;
using GestionPacientes.Aplicacion.Servicios;

namespace GestionPacientes.Api.Extensiones;

public static class ConfiguracionServicios
{
    public static IServiceCollection ConfigurarServiciosApi(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddCors(options =>
        {
            options.AddPolicy("PoliticaCors", politica =>
            {
                politica.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
            });
        });

        services.AddScoped<IPacienteServicio, PacienteServicio>();

        return services;
    }
}