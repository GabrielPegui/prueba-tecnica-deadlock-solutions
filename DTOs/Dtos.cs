using System.ComponentModel.DataAnnotations;

namespace Prueba.DTOs;

public class CrearClienteDto
{
    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = string.Empty;
}

public record ClienteDto(int Id, string Name, string Email);

public class CrearOrdenDto
{
    [Range(1, int.MaxValue)]
    public int ClienteId { get; init; }

    [Required, MinLength(2)]
    public List<CrearItemDto> Items { get; init; } = [];
}

public class CrearItemDto
{
    [Range(1, int.MaxValue)]
    public int ProductoId { get; init; }

    [Range(1, int.MaxValue)]
    public int Cantidad { get; init; }
}

public class ActualizarItemDto
{
    [Range(1, int.MaxValue)]
    public int Cantidad { get; init; }
}

public record ProductoDto(int Id, string Name, decimal Price, int Stock);
public record ItemDto(int Id, int ProductoId, string Producto, int Cantidad, decimal UnitPrice, decimal Subtotal);
public record OrdenDto(int Id, int ClienteId, string Cliente, DateTime CreatedAt, decimal Total, IReadOnlyCollection<ItemDto> Items);
public record FacturaDto(int OrdenId, DateTime IssuedAt, int ClienteId, string Cliente, IReadOnlyCollection<ItemDto> Items, decimal Subtotal, decimal Total);
