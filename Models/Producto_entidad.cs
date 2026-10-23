namespace Prueba.Models;
public class Producto { public int Id { get; set; } public string Name { get; set; } = string.Empty; public decimal Price { get; set; } public int Stock { get; set; } public ICollection<Item> Items { get; set; } = new List<Item>(); }
