using Microsoft.EntityFrameworkCore; using Prueba.Models;
namespace Prueba.Data;
public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext db)
    {
        await db.Database.EnsureCreatedAsync(); if (await db.Productos.AnyAsync()) return;
        db.Productos.AddRange(new Producto { Name = "Laptop", Price = 1200, Stock = 10 }, new Producto { Name = "Monitor", Price = 300, Stock = 20 }, new Producto { Name = "Keyboard", Price = 80, Stock = 30 }, new Producto { Name = "Mouse", Price = 40, Stock = 30 }); await db.SaveChangesAsync();
    }
}
