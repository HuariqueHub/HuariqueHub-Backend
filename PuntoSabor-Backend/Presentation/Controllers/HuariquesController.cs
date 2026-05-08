using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using PuntoSabor_Backend.Discovery.Domain.Repositories;
using PuntoSabor_Backend.Presentation.Resources;
using PuntoSabor_Backend.Presentation.Transform;
using PuntoSabor_Backend.Shared.Domain.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace PuntoSabor_Backend.Presentation.Controllers;

[ApiController]
[Route("huariques")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Huariques Endpoints.")]
public class HuariquesController(
    IHuariqueRepository huariques,
    IUnitOfWork unitOfWork) : ControllerBase
{
    /**
     * <summary>
     *     Busca huariques por texto y/o cercanos.
     * </summary>
     */
    
    [HttpGet]
    [SwaggerOperation("Search Huariques", "Search huariques by text and/or near filter.", OperationId = "SearchHuariques")]
    [SwaggerResponse(200, "Huariques encontrados y retornados.", typeof(IEnumerable<HuariqueResource>))]
    [SwaggerResponse(400, "Solicitud inválida. Verifica los parámetros enviados.", typeof(IEnumerable<HuariqueResource>))]
    [SwaggerResponse(404, "No se encontraron huariques con los filtros enviados.", typeof(IEnumerable<HuariqueResource>))]
    public async Task<IActionResult> Search([FromQuery] string? q, [FromQuery] bool? near, CancellationToken ct)
    {
        if (q is not null)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest(new ErrorResource("El parámetro 'q' no puede estar vacío."));

            q = q.Trim();

            if (q.Length < 2)
                return BadRequest(new ErrorResource("El parámetro 'q' debe tener al menos 2 caracteres."));

            if (q.Length > 80)
                return BadRequest(new ErrorResource("El parámetro 'q' excede el máximo permitido (80 caracteres)."));
        }

        var result = await huariques.SearchAsync(q, near, ct);

        var hasFilters = q is not null || near is not null;
        if (hasFilters && !result.Any())
            return NotFound(new ErrorResource("No se encontraron huariques con los filtros enviados."));

        var resources = result.Select(HuariqueResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /**
     * <summary>
     *     Devuelve el detalle de un huarique por id.
     *     GET /huariques/:id
     * </summary>
     */
    
    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Huarique by Id", "Get a huarique by its unique identifier.", OperationId = "GetHuariqueById")]
    [SwaggerResponse(200, "El huarique fue encontrado y retornado.", typeof(HuariqueResource))]
    [SwaggerResponse(404, "El huarique no fue encontrado.", typeof(HuariqueResource))]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var entity = await huariques.FindByIdAsync(id, ct);
        if (entity is null) return NotFound(new ErrorResource("El huarique no fue encontrado."));

        var resource = HuariqueResourceFromEntityAssembler.ToResourceFromEntity(entity);
        return Ok(resource);
    }

    /**
    * <summary>
    *     Crea un nuevo huarique.
    * </summary>
    */
    
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Create Huarique", "Create a new huarique.", OperationId = "CreateHuarique")]
    [SwaggerResponse(201, "El huarique fue creado.", typeof(HuariqueResource))]
    [SwaggerResponse(400, "El huarique no pudo ser creado.", typeof(HuariqueResource))]
    public async Task<IActionResult> Create([FromBody] CreateHuariqueResource resource, CancellationToken ct)
    {
        if (resource is null)
            return BadRequest(new ErrorResource("Body inválido."));

        if (string.IsNullOrWhiteSpace(resource.Name))
            return BadRequest(new ErrorResource("El campo 'name' es obligatorio."));

        if (string.IsNullOrWhiteSpace(resource.District))
            return BadRequest(new ErrorResource("El campo 'district' es obligatorio."));

        if (resource.CategoryId <= 0)
            return BadRequest(new ErrorResource("El campo 'categoryId' debe ser mayor a 0."));

        if (resource.Price < 0)
            return BadRequest(new ErrorResource("El campo 'price' no puede ser negativo."));

        var entity = CreateHuariqueEntityFromResourceAssembler.ToEntityFromResource(resource);

        await huariques.AddAsync(entity, ct);
        await unitOfWork.CompleteAsync(ct);

        var createdResource = HuariqueResourceFromEntityAssembler.ToResourceFromEntity(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, createdResource);
    }

    /**
     * <summary>
     *     Actualiza parcialmente un huarique.
     *     PATCH /huariques/:id
     * </summary>
     */
    
    [HttpPatch("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Patch Huarique", "Update specific fields of an existing huarique.", OperationId = "PatchHuarique")]
    [SwaggerResponse(200, "El huarique fue actualizado parcialmente.", typeof(HuariqueResource))]
    [SwaggerResponse(400, "Solicitud inválida. Verifica el contenido enviado.", typeof(HuariqueResource))]
    [SwaggerResponse(404, "El huarique no fue encontrado.", typeof(HuariqueResource))]
    public async Task<IActionResult> Patch(int id, [FromBody] Dictionary<string, object> patch, CancellationToken ct)
    {
        if (patch is null || patch.Count == 0)
            return BadRequest(new ErrorResource("No se enviaron campos para actualizar."));

        var existing = await huariques.FindByIdAsync(id, ct);
        if (existing is null)
            return NotFound(new ErrorResource("El huarique no fue encontrado."));

        await huariques.PatchAsync(existing, patch, ct);
        await unitOfWork.CompleteAsync(ct);

        var updatedResource = HuariqueResourceFromEntityAssembler.ToResourceFromEntity(existing);
        return Ok(updatedResource);
    }
}
