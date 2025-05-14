using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Application.Reports.GetInfoReport;
using Thunders.TechTest.ApiService.Application.Reports.ScheduleReport;
using Thunders.TechTest.ApiService.Controllers.Common;
using Thunders.TechTest.ApiService.Controllers.Reports.GetInfoReport;
using Thunders.TechTest.ApiService.Controllers.Reports.ScheduleReport;
using Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;

namespace Thunders.TechTest.ApiService.Controllers.Reports;

/// <summary>
/// Controller to report.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get report info.
    /// </summary>
    /// <param name="request">The scheduled report request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created a scheduled report.</returns>
    [HttpGet("{id}", Name = nameof(GetInfoAsync))]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetInfoAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var reportResult = await _mediator.SendAsync<GetReportInfoCommand, GetReportInfoResult>(
            new() { ReportId = id },
            cancellationToken);

        return Ok(new GetReportInfoResponse(reportResult));
    }

    /// <summary>
    /// Schedule a report generation.
    /// </summary>
    /// <param name="request">The scheduled report request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created a scheduled report.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ScheduleAsync(
        [FromBody] ScheduleReportRequest request,
        CancellationToken cancellationToken)
    {
        ScheduleReportResult result = await _mediator.SendAsync<ScheduleReportCommand, ScheduleReportResult>(
            request.AsCommand(),
            cancellationToken);

        return Created(nameof(GetInfoAsync), new
        {
            Id = result.ReportId,
        });
    }
}