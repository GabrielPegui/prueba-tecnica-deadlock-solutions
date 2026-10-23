using Microsoft.EntityFrameworkCore;
using Prueba.Models;
namespace Prueba.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>(); public DbSet<Producto> Productos => Set<Producto>(); public DbSet<Orden> Ordenes => Set<Orden>(); public DbSet<Item> Items => Set<Item>();
    protected override void OnModelCreating(ModelBuilder m)
    {
        m.Entity<Producto>().Property(x => x.Price).HasPrecision(18, 2); m.Entity<Item>().Property(x => x.UnitPrice).HasPrecision(18, 2);
        m.Entity<Orden>().HasOne(x => x.Cliente).WithMany(x => x.Ordenes).HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Restrict);
        m.Entity<Item>().HasOne(x => x.Orden).WithMany(x => x.Items).HasForeignKey(x => x.OrdenId).OnDelete(DeleteBehavior.Cascade);
        m.Entity<Item>().HasOne(x => x.Producto).WithMany(x => x.Items).HasForeignKey(x => x.ProductoId).OnDelete(DeleteBehavior.Restrict);
    }
}
