using Thunders.TechTest.ApiService.Abstractions;

namespace Thunders.TechTest.ApiService.Domain.Tolls;

/// <summary>
/// Represents a plaza of toll.
/// </summary>
public class TollPlaza : IEntity
{
    /// <inheritdoc/>
    public Guid Id { get; private set; }

    /// <summary>
    /// Name of highway.
    /// </summary>
    public required string Highway { get; init; }

    /// <summary>
    /// Kilometers of highway.
    /// </summary>
    public decimal Kms { get; init; }

    /// <summary>
    /// State of toll plaza.
    /// </summary>
    public required string State { get; init; }

    /// <summary>
    /// City of toll plaza.
    /// </summary>
    public required string City { get; init; }

    /// <summary>
    /// Latitude of toll plaza.
    /// </summary>
    public decimal Latitude { get; init; }

    /// <summary>
    /// Longitude of toll plaza.
    /// </summary>
    public decimal Longitude { get; init; }

    /// <summary>
    /// Date record creation.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Concessionarie company of toll.
    /// </summary>
    public required TollConcessionaire Concessionaire { get; init; }

    public static TollPlaza New(Guid id) =>
        new()
        {
            Id = id,
            City = default!,
            Concessionaire = default!,
            Highway = default!,
            State = default!,
        };
}