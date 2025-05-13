using Thunders.TechTest.ApiService.Abstractions;

namespace Thunders.TechTest.ApiService.Domain.Tolls;

/// <summary>
/// Represents a company concessionarie of toll.
/// </summary>
public class TollConcessionaire : IEntity
{
    /// <inheritdoc/>
    public Guid Id { get; private set; }

    /// <summary>
    /// Company name of toll.
    /// </summary>
    public required string CompanyName { get; init; }

    /// <summary>
    /// Company legal document.
    /// </summary>
    public required string LegalDocument { get; init; }

    /// <summary>
    /// Date record creation.
    /// </summary>
    public DateTime CreatedAt { get; init; }
}