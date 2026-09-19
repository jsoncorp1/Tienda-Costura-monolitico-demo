namespace Status.Domain.Entidades;

public class Solicitud
{
    public int Id { get; set; }
    public string NumeroRef { get; set; } = null!; // COT-2026-00001
    
    public DateTime FechaCreacion { get; set; }
    public EstadoSolicitud Estado { get; set; }
    
    // Esporádico
    public string? NombreCliente { get; set; }
    public string? TelefonoCliente { get; set; }

    // Corporativo
    public int? ClienteCorporativoId { get; set; }
    public ClienteCorporativo? ClienteCorporativo { get; set; }

    public ICollection<SolicitudItem> Items { get; set; } = new List<SolicitudItem>();
}

public class SolicitudItem
{
    public int Id { get; set; }
    public int SolicitudId { get; set; }
    public Solicitud Solicitud { get; set; } = null!;

    public int ProductoId { get; set; }
    public Producto Producto { get; set; } = null!;

    public int Cantidad { get; set; }
    
    /// <summary>
    /// Precio unitario congelado al momento de la cotización/solicitud.
    /// </summary>
    public decimal PrecioUnitario { get; set; }
    
    public bool EsBonificacion { get; set; }
}

public enum EstadoSolicitud
{
    Recibida,
    Confirmada,
    Cotizada,
    Aceptada,
    Cancelada
}
