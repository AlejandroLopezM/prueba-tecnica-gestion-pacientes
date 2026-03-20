using GestionPacientes.Aplicacion.DTOs.Requests;
using GestionPacientes.Aplicacion.Interfaces.Repositorios;
using GestionPacientes.Aplicacion.Modelos.Paginacion;
using GestionPacientes.Aplicacion.Servicios;
using GestionPacientes.Dominio.Entidades;
using Moq;
using Xunit;

namespace GestionPacientes.Pruebas.Aplicacion.Servicios;

public class PacienteServicioTests
{
    private readonly Mock<IPacienteRepositorio> _pacienteRepositorioMock;
    private readonly PacienteServicio _pacienteServicio;

    public PacienteServicioTests()
    {
        _pacienteRepositorioMock = new Mock<IPacienteRepositorio>();
        _pacienteServicio = new PacienteServicio(_pacienteRepositorioMock.Object);
    }

    [Fact]
    public async Task CrearAsync_DebeLanzarExcepcion_CuandoDocumentoYaExiste()
    {
        var request = new CrearPacienteRequestDto
        {
            TipoDocumento = "CC",
            NumeroDocumento = "12345",
            Nombres = "Alejandro",
            Apellidos = "Lopez",
            FechaNacimiento = new DateTime(1990, 1, 1)
        };

        _pacienteRepositorioMock
            .Setup(x => x.ExisteDocumentoAsync(
                request.TipoDocumento,
                request.NumeroDocumento,
                null,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _pacienteServicio.CrearAsync(request, CancellationToken.None));
    }

    [Fact]
    public async Task ObtenerPorIdAsync_DebeLanzarExcepcion_CuandoPacienteNoExiste()
    {
        _pacienteRepositorioMock
            .Setup(x => x.ObtenerPorIdConCitasAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Paciente?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _pacienteServicio.ObtenerPorIdAsync(1, CancellationToken.None));
    }

    [Fact]
    public async Task ObtenerPaginadoAsync_DebeRetornarResultadoPaginadoCorrectamente()
    {
        var filtro = new FiltroPacientesRequestDto
        {
            Pagina = 1,
            TamanoPagina = 10
        };

        var pacientes = new List<Paciente>
        {
            new()
            {
                PacienteId = 1,
                TipoDocumento = "CC",
                NumeroDocumento = "123",
                Nombres = "Ana",
                Apellidos = "Perez",
                FechaNacimiento = new DateTime(1995, 5, 10),
                FechaCreacion = DateTime.UtcNow
            },
            new()
            {
                PacienteId = 2,
                TipoDocumento = "CC",
                NumeroDocumento = "456",
                Nombres = "Luis",
                Apellidos = "Gomez",
                FechaNacimiento = new DateTime(1992, 8, 20),
                FechaCreacion = DateTime.UtcNow
            }
        };

        var resultadoRepositorio = new ResultadoPaginado<Paciente>
        {
            Items = pacientes,
            TotalRegistros = 2,
            PaginaActual = 1,
            TamanoPagina = 10
        };

        _pacienteRepositorioMock
            .Setup(x => x.ObtenerPaginadoAsync(filtro, It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultadoRepositorio);

        var resultado = await _pacienteServicio.ObtenerPaginadoAsync(filtro, CancellationToken.None);

        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.TotalRegistros);
        Assert.Equal(2, resultado.Items.Count);
        Assert.Equal("Ana", resultado.Items[0].Nombres);
        Assert.Equal("Luis", resultado.Items[1].Nombres);
    }
}