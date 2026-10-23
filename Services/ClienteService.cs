using Microsoft.EntityFrameworkCore; using Prueba.Data; using Prueba.DTOs; using Prueba.Models;
namespace Prueba.Services;
public class ClienteService(AppDbContext db) : IClienteService
{
    public async Task<IReadOnlyCollection<ClienteDto>> GetAllAsync(CancellationToken ct) => await db.Clientes.AsNoTracking().OrderBy(x => x.Name).Select(x => new ClienteDto(x.Id, x.Name, x.Email)).ToListAsync(ct);
    public async Task<ServiceResult<ClienteDto>> CreateAsync(CrearClienteDto r, CancellationToken ct) { if (await db.Clientes.AnyAsync(x => x.Email == r.Email, ct)) return ServiceResult<ClienteDto>.Fail("Email already exists.", 409); var c = new Cliente { Name = r.Name.Trim(), Email = r.Email.Trim() }; db.Clientes.Add(c); await db.SaveChangesAsync(ct); return ServiceResult<ClienteDto>.Created(new(c.Id, c.Name, c.Email)); }
}
