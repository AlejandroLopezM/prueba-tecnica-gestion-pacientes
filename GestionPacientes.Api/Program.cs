using GestionPacientes.Api.Extensiones;
using GestionPacientes.Api.Middlewares;
using GestionPacientes.Infraestructura.Extensiones;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurarServiciosApi();
builder.Services.RegistrarInfraestructura(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<MiddlewareManejoErrores>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("PoliticaCors");
app.UseAuthorization();
app.MapControllers();

app.Run();