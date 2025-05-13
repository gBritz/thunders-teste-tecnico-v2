using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Controllers.Common;
using Thunders.TechTest.ApiService.Controllers.Tolls.RegisterPayment;
using Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;
using Thunders.TechTest.ApiService.Data;
using Thunders.TechTest.ApiService.Domain.Tolls;

namespace Thunders.TechTest.ApiService.Controllers.Tolls;

/// <summary>
/// Controller for managing toll operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
public class TollController : ControllerBase
{
    private readonly IMediator _mediator;

    public TollController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("list")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListTollsAsync(
        [FromServices] TollDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var plazas = await dbContext.Set<TollPlaza>()
            .Select(_ => new
            {
                PlazaId = _.Id,
                ConcessionaireName = _.Concessionaire.CompanyName,
                Address = $"{_.Highway}, {_.City}, {_.State}",
            })
            .ToListAsync(cancellationToken);

        return Ok(plazas);
    }

    /// <summary>
    /// Schedule a new toll payment.
    /// </summary>
    /// <param name="request">The toll payment request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created a toll payment</returns>
    [HttpPost("{tollPlazaId}/pay")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterPaymentAsync(
        [FromRoute] Guid tollPlazaId,
        [FromBody] RegisterPaymentRequest request,
        CancellationToken cancellationToken)
    {
        await _mediator.SendAsync(
            request.AsCommand(tollPlazaId),
            cancellationToken);

        return Created();
    }
}