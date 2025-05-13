using Thunders.TechTest.ApiService.Application.Tolls.RegisterPayment;
using Thunders.TechTest.ApiService.Domain;

namespace Thunders.TechTest.ApiService.Controllers.Tolls.RegisterPayment;

/// <summary>
/// Represents a request that register payment of toll.
/// </summary>
public record RegisterPaymentRequest
{
    /// <summary>
    /// Toll plaza identifier.
    /// </summary>
    private Guid TollPlazaId { get; init; }

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

    public RegisterPaymentCommand AsCommand(Guid tollPlazaId) =>
        new()
        {
            TollPlazaId = tollPlazaId,
            PaidAt = PaidAt,
            Amount = Amount,
            Vehicle = Vehicle,
        };
}