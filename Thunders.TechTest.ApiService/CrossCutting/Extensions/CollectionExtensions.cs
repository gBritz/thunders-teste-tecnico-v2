namespace Thunders.TechTest.ApiService.CrossCutting.Extensions;

/// <summary>
/// Extensions for collections.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Indicates if the value is inside of a collection.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="items"></param>
    /// <returns>True case the value has in collection.</returns>
    public static bool In<T>(this T value, params T[] items)
    {
        ArgumentNullException.ThrowIfNull(items, nameof(items));

        return items.Contains(value);
    }
}