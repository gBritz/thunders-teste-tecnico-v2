using Thunders.TechTest.ApiService.Abstractions;

namespace Thunders.TechTest.ApiService.Domain.Tolls;

/// <summary>
/// Represents a payment of the toll.
/// </summary>
public class TollPayment : IEntity
{
    /// <inheritdoc/>
    public Guid Id { get; private set; }

    /// <summary>
    /// Date record creation.
    /// </summary>
    public DateTime CreatedAt { get; init; }

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
    /// Plaza of toll.
    /// </summary>
    public required TollPlaza Plaza { get; init; }
}