namespace Thunders.TechTest.ApiService.Abstractions;

/// <summary>
/// Represents a entity.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public Guid Id { get; }
}