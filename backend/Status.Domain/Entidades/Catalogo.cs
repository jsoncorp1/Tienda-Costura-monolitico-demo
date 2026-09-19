namespace Status.Domain.Entidades;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? ImagenUrl { get; set; }
    public decimal PrecioLista { get; set; }
    
    /// <summary>
    /// Si true, es para clientes mayoristas. Si false, es retail/esporádico.
    /// </summary>
    public bool SoloMayorista { get; set; }
}

public class Servicio
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!; // Sublimado, Bordado, DTF, Costura
    public string Descripcion { get; set; } = null!;
}
