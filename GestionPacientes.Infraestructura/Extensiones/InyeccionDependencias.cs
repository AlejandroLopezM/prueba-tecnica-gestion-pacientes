using GestionPacientes.Aplicacion.Interfaces.Repositorios;
using GestionPacientes.Infraestructura.Persistencia;
using GestionPacientes.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestionPacientes.Infraestructura.Extensiones;

public static class InyeccionDependencias
{
    public static IServiceCollection RegistrarInfraestructura(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<GestionPacientesDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConexionSqlServer")));

        services.AddScoped<IPacienteRepositorio, PacienteRepositorio>();

        return services;
    }
}