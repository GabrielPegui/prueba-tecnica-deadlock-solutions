using Microsoft.AspNetCore.Mvc; using Prueba.DTOs; using Prueba.Services; namespace Prueba.Controllers;
[ApiController, Route("api/ordenes")] public class OrdenesController(IOrdenService s) : ControllerBase
{
    [HttpPost] public async Task<ActionResult<OrdenDto>> Create(CrearOrdenDto r, CancellationToken ct) { var x = await s.CrearAsync(r, ct); return x.Success ? Created($"/api/ordenes/{x.Value!.Id}", x.Value) : Problem(x.Error, statusCode: x.StatusCode); }
    [HttpPatch("{ordenId:int}/items/{itemId:int}")] public async Task<ActionResult<OrdenDto>> UpdateItem(int ordenId, int itemId, ActualizarItemDto r, CancellationToken ct) { var x = await s.ActualizarItemAsync(ordenId, itemId, r.Cantidad, ct); return x.Success ? Ok(x.Value) : Problem(x.Error, statusCode: x.StatusCode); }
    [HttpDelete("{ordenId:int}/items/{itemId:int}")] public async Task<IActionResult> DeleteItem(int ordenId, int itemId, CancellationToken ct) { var x = await s.EliminarItemAsync(ordenId, itemId, ct); return x.Success ? NoContent() : Problem(x.Error, statusCode: x.StatusCode); }
    [HttpPost("{ordenId:int}/factura")] public async Task<ActionResult<FacturaDto>> Invoice(int ordenId, CancellationToken ct) { var x = await s.FacturarAsync(ordenId, ct); return x.Success ? Ok(x.Value) : Problem(x.Error, statusCode: x.StatusCode); }
}
