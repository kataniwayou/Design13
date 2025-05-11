namespace FlowOrchestrator.Common.Versioning;

/// <summary>
/// Represents a range of versions.
/// </summary>
public class VersionRange
{
    /// <summary>
    /// Gets the minimum version in the range.
    /// </summary>
    public VersionInfo MinVersion { get; }
    
    /// <summary>
    /// Gets the maximum version in the range.
    /// </summary>
    public VersionInfo MaxVersion { get; }
    
    /// <summary>
    /// Gets whether the minimum version is inclusive.
    /// </summary>
    public bool MinInclusive { get; }
    
    /// <summary>
    /// Gets whether the maximum version is inclusive.
    /// </summary>
    public bool MaxInclusive { get; }
    
    /// <summary>
    /// Creates a new instance of the VersionRange class with the specified minimum and maximum versions.
    /// </summary>
    /// <param name="minVersion">The minimum version in the range.</param>
    /// <param name="maxVersion">The maximum version in the range.</param>
    /// <param name="minInclusive">Whether the minimum version is inclusive.</param>
    /// <param name="maxInclusive">Whether the maximum version is inclusive.</param>
    public VersionRange(VersionInfo minVersion, VersionInfo maxVersion, bool minInclusive = true, bool maxInclusive = true)
    {
        MinVersion = minVersion ?? throw new ArgumentNullException(nameof(minVersion));
        MaxVersion = maxVersion ?? throw new ArgumentNullException(nameof(maxVersion));
        MinInclusive = minInclusive;
        MaxInclusive = maxInclusive;
        
        if (minVersion.CompareTo(maxVersion) > 0)
        {
            throw new ArgumentException("Minimum version cannot be greater than maximum version.");
        }
    }
    
    /// <summary>
    /// Checks if a version is within this range.
    /// </summary>
    /// <param name="version">The version to check.</param>
    /// <returns>True if the version is within this range, false otherwise.</returns>
    public bool Contains(VersionInfo version)
    {
        if (version == null)
        {
            throw new ArgumentNullException(nameof(version));
        }
        
        var minComparison = version.CompareTo(MinVersion);
        var maxComparison = version.CompareTo(MaxVersion);
        
        return (MinInclusive ? minComparison >= 0 : minComparison > 0) &&
               (MaxInclusive ? maxComparison <= 0 : maxComparison < 0);
    }
    
    /// <summary>
    /// Checks if this range overlaps with another range.
    /// </summary>
    /// <param name="other">The other range to check.</param>
    /// <returns>True if the ranges overlap, false otherwise.</returns>
    public bool Overlaps(VersionRange other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }
        
        // Check if this range's min version is less than or equal to the other range's max version
        // and this range's max version is greater than or equal to the other range's min version
        var thisMinToOtherMax = MinVersion.CompareTo(other.MaxVersion);
        var thisMaxToOtherMin = MaxVersion.CompareTo(other.MinVersion);
        
        return (thisMinToOtherMax <= 0 || (thisMinToOtherMax == 0 && MinInclusive && other.MaxInclusive)) &&
               (thisMaxToOtherMin >= 0 || (thisMaxToOtherMin == 0 && MaxInclusive && other.MinInclusive));
    }
    
    /// <summary>
    /// Returns a string representation of the version range.
    /// </summary>
    /// <returns>A string representation of the version range.</returns>
    public override string ToString()
    {
        return $"{(MinInclusive ? "[" : "(")}{MinVersion}, {MaxVersion}{(MaxInclusive ? "]" : ")")}";
    }
}
