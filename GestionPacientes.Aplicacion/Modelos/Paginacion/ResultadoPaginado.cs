namespace GestionPacientes.Aplicacion.Modelos.Paginacion;

public class ResultadoPaginado<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalRegistros { get; set; }
    public int PaginaActual { get; set; }
    public int TamanoPagina { get; set; }

    public int TotalPaginas =>
        TamanoPagina <= 0
            ? 0
            : (int)Math.Ceiling((double)TotalRegistros / TamanoPagina);
}