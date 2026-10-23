using Microsoft.AspNetCore.Mvc; using Prueba.DTOs; using Prueba.Services; namespace Prueba.Controllers;
[ApiController, Route("api/productos")] public class ProductosController(IOrdenService s) : ControllerBase { [HttpGet] public async Task<ActionResult<IReadOnlyCollection<ProductoDto>>> GetAll(CancellationToken ct) => Ok(await s.GetProductosAsync(ct)); }
