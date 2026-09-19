namespace Status.Domain.Entidades;

public class ClienteCorporativo
{
    public int Id { get; set; }
    public string NombreEmpresa { get; set; } = null!;
    public string Nit { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    
    /// <summary>
    /// Foreign Key al IdentityUser. (Se mantiene en infra, en Domain es string)
    /// </summary>
    public string UsuarioId { get; set; } = null!;

    public int? GrupoTarifarioId { get; set; }
    public GrupoTarifario? GrupoTarifario { get; set; }

    public bool Aprobado { get; set; }
}

public class GrupoTarifario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!; // Ej: Mayorista A, Distribuidor B
    public decimal PorcentajeDescuento { get; set; }
    
    // Podría tener una lista de precios específicos por producto
    public ICollection<PrecioTarifario> Precios { get; set; } = new List<PrecioTarifario>();
}

public class PrecioTarifario
{
    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;

    public int GrupoTarifarioId { get; set; }
    public GrupoTarifario GrupoTarifario { get; set; } = null!;

    public decimal Precio { get; set; }
}
