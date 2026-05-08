using Microsoft.AspNetCore.Mvc;
using PuntoSabor_Backend.Discovery.Domain.Model;
using PuntoSabor_Backend.Discovery.Domain.Repositories;

namespace PuntoSabor_Backend.Presentation.Controllers;

[ApiController]
[Route("categories")]
public class CategoriesController(ICategoryRepository categories) : ControllerBase
{
    /**
     * <summary>
     *     Devuelve todas las categorías de huariques.
     *     GET /categories
     * </summary>
     */
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll(CancellationToken ct)
    {
        var result = await categories.ListAsync(ct);
        return Ok(result);
    }
}