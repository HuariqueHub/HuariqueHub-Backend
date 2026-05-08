using Microsoft.AspNetCore.Mvc;
using PuntoSabor_Backend.Promotions.Domain.Model;
using PuntoSabor_Backend.Promotions.Domain.Repositories;

namespace PuntoSabor_Backend.Presentation.Controllers;

[ApiController]
[Route("promos")]
public class PromosController(IPromoRepository promos) : ControllerBase
{
    /**
     * <summary>
     *     Devuelve la lista de promociones.
     *     GET /promos
     * </summary>
     */
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Promo>>> GetAll(CancellationToken ct)
    {
        var result = await promos.ListAsync(ct);
        return Ok(result);
    }
}