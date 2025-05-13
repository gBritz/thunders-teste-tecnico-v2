using Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;
using Thunders.TechTest.ApiService.Domain;

namespace Thunders.TechTest.ApiService.Application.Tolls.RegisterPayment;

/// <summary>
/// Represents a command to register payment of toll.
/// </summary>
public record RegisterPaymentCommand : ICommand
{
    /// <summary>
    /// Toll plaza identifier.
    /// </summary>
    public required Guid TollPlazaId { get; init; }

    /// <summary>
    /// Date of payment.
    /// </summary>
    public required DateTime PaidAt { get; init; }

    /// <summary>
    /// Amount of the toll.
    /// </summary>
    public required decimal Amount { get; init; }

    /// <summary>
    /// Vehicle that paid.
    /// </summary>
    public required VehicleType Vehicle { get; init; }
}