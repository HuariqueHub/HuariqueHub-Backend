using Microsoft.AspNetCore.Mvc;
using PuntoSabor_Backend.Reviews.Domain.Model;
using PuntoSabor_Backend.Reviews.Domain.Repositories;
using PuntoSabor_Backend.Shared.Domain.Repositories;

namespace PuntoSabor_Backend.Presentation.Controllers;

[ApiController]
[Route("reviews")]
public class ReviewsController(
    IReviewRepository reviews,
    IUnitOfWork unitOfWork) : ControllerBase
{
    /**
     * <summary>
     *     Lista de reviews.
     *     GET /reviews
     *     GET /reviews?huariqueId=1&_sort=createdAt&_order=desc
     * </summary>
     */
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetAll(
        [FromQuery] int? huariqueId,
        [FromQuery(Name = "_sort")] string? sort,
        [FromQuery(Name = "_order")] string? order,
        CancellationToken ct)
    {
        var result = await reviews.ListAsync(huariqueId, sort, order, ct);
        return Ok(result);
    }

    /**
     * <summary>
     *     Crea una nueva review.
     *     POST /reviews
     * </summary>
     */
    [HttpPost]
    public async Task<ActionResult<Review>> Create(
        [FromBody] Review dto,
        CancellationToken ct)
    {
        if (dto.CreatedAt == default)
            dto.CreatedAt = DateTime.UtcNow;

        await reviews.AddAsync(dto, ct);
        await unitOfWork.CompleteAsync(ct);

        return StatusCode(StatusCodes.Status201Created, dto);
    }
}