namespace Prueba.Models;
public class Orden { public int Id { get; set; } public int ClienteId { get; set; } public Cliente Cliente { get; set; } = null!; public DateTime CreatedAt { get; set; } = DateTime.UtcNow; public ICollection<Item> Items { get; set; } = new List<Item>(); }
