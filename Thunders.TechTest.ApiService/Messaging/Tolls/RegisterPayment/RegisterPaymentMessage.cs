using Thunders.TechTest.ApiService.Domain;
using Thunders.TechTest.ApiService.Domain.Tolls;

namespace Thunders.TechTest.ApiService.Messaging.Tolls.RegisterPayment;

/// <summary>
/// Represents a message to register payment.
/// </summary>
public record RegisterPaymentMessage
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

    /// <summary>
    /// Fill domain with message data.
    /// </summary>
    /// <returns>Domain filled.</returns>
    public TollPayment ToDomain() =>
        new()
        {
            CreatedAt = DateTime.UtcNow,
            PaidAt = PaidAt,
            Amount = Amount,
            Vehicle = Vehicle,
            Plaza = TollPlaza.New(TollPlazaId),
        };
}