namespace Prueba.Models;
public class Item { public int Id { get; set; } public int OrdenId { get; set; } public Orden Orden { get; set; } = null!; public int ProductoId { get; set; } public Producto Producto { get; set; } = null!; public int Cantidad { get; set; } public decimal UnitPrice { get; set; } }
