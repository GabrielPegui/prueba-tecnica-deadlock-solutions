using Microsoft.EntityFrameworkCore; using Prueba.Data; using Prueba.DTOs; using Prueba.Models;
namespace Prueba.Services;
public class OrdenService(AppDbContext db) : IOrdenService
{
    public async Task<IReadOnlyCollection<ProductoDto>> GetProductosAsync(CancellationToken ct) => await db.Productos.AsNoTracking().OrderBy(x => x.Name).Select(x => new ProductoDto(x.Id, x.Name, x.Price, x.Stock)).ToListAsync(ct);
    public async Task<ServiceResult<OrdenDto>> CrearAsync(CrearOrdenDto r, CancellationToken ct)
    {
        if (r.Items.Select(x => x.ProductoId).Distinct().Count() != r.Items.Count) return ServiceResult<OrdenDto>.Fail("A product cannot be repeated.", 400);
        var c = await db.Clientes.FindAsync([r.ClienteId], ct); if (c is null) return ServiceResult<OrdenDto>.Fail("Client not found.", 404);
        var ps = await db.Productos.Where(x => r.Items.Select(i => i.ProductoId).Contains(x.Id)).ToDictionaryAsync(x => x.Id, ct);
        foreach (var i in r.Items) { if (!ps.TryGetValue(i.ProductoId, out var p)) return ServiceResult<OrdenDto>.Fail("Product not found.", 404); if (p.Stock < i.Cantidad) return ServiceResult<OrdenDto>.Fail($"Insufficient stock for {p.Name}.", 409); }
        await using var tx = await db.Database.BeginTransactionAsync(ct); var o = new Orden { ClienteId = c.Id, Cliente = c };
        foreach (var i in r.Items) { var p = ps[i.ProductoId]; p.Stock -= i.Cantidad; o.Items.Add(new Item { ProductoId = p.Id, Producto = p, Cantidad = i.Cantidad, UnitPrice = p.Price }); }
        db.Ordenes.Add(o); await db.SaveChangesAsync(ct); await tx.CommitAsync(ct); return ServiceResult<OrdenDto>.Created(Map(o));
    }
    public async Task<ServiceResult<OrdenDto>> ActualizarItemAsync(int oid, int iid, int cantidad, CancellationToken ct)
    { var i = await db.Items.Include(x => x.Producto).Include(x => x.Orden).ThenInclude(x => x.Cliente).FirstOrDefaultAsync(x => x.Id == iid && x.OrdenId == oid, ct); if (i is null) return ServiceResult<OrdenDto>.Fail("Order item not found.", 404); var d = cantidad - i.Cantidad; if (d > 0 && i.Producto.Stock < d) return ServiceResult<OrdenDto>.Fail("Insufficient stock.", 409); i.Producto.Stock -= d; i.Cantidad = cantidad; await db.SaveChangesAsync(ct); await db.Entry(i.Orden).Collection(x => x.Items).Query().Include(x => x.Producto).LoadAsync(ct); return ServiceResult<OrdenDto>.Ok(Map(i.Orden)); }
    public async Task<ServiceResult<bool>> EliminarItemAsync(int oid, int iid, CancellationToken ct)
    { var i = await db.Items.Include(x => x.Producto).FirstOrDefaultAsync(x => x.Id == iid && x.OrdenId == oid, ct); if (i is null) return ServiceResult<bool>.Fail("Order item not found.", 404); i.Producto.Stock += i.Cantidad; db.Items.Remove(i); await db.SaveChangesAsync(ct); return ServiceResult<bool>.Ok(true); }
    public async Task<ServiceResult<FacturaDto>> FacturarAsync(int oid, CancellationToken ct)
    { var o = await db.Ordenes.AsNoTracking().Include(x => x.Cliente).Include(x => x.Items).ThenInclude(x => x.Producto).FirstOrDefaultAsync(x => x.Id == oid, ct); if (o is null) return ServiceResult<FacturaDto>.Fail("Order not found.", 404); var items = o.Items.Select(MapItem).ToList(); var total = items.Sum(x => x.Subtotal); return ServiceResult<FacturaDto>.Ok(new(o.Id, DateTime.UtcNow, o.ClienteId, o.Cliente.Name, items, total, total)); }
    private static OrdenDto Map(Orden o) { var x = o.Items.Select(MapItem).ToList(); return new(o.Id, o.ClienteId, o.Cliente.Name, o.CreatedAt, x.Sum(i => i.Subtotal), x); }
    private static ItemDto MapItem(Item i) => new(i.Id, i.ProductoId, i.Producto.Name, i.Cantidad, i.UnitPrice, i.Cantidad * i.UnitPrice);
}
