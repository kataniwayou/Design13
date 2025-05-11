using System.Text.RegularExpressions;

namespace FlowOrchestrator.Common.Versioning;

/// <summary>
/// Represents version information for a component or entity.
/// </summary>
public class VersionInfo
{
    private static readonly Regex SemVerRegex = new Regex(
        @"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$",
        RegexOptions.Compiled);
    
    /// <summary>
    /// Gets the major version number.
    /// </summary>
    public int Major { get; }
    
    /// <summary>
    /// Gets the minor version number.
    /// </summary>
    public int Minor { get; }
    
    /// <summary>
    /// Gets the patch version number.
    /// </summary>
    public int Patch { get; }
    
    /// <summary>
    /// Gets the pre-release version string.
    /// </summary>
    public string? PreRelease { get; }
    
    /// <summary>
    /// Gets the build metadata string.
    /// </summary>
    public string? BuildMetadata { get; }
    
    /// <summary>
    /// Creates a new instance of the VersionInfo class with the specified version components.
    /// </summary>
    /// <param name="major">The major version number.</param>
    /// <param name="minor">The minor version number.</param>
    /// <param name="patch">The patch version number.</param>
    /// <param name="preRelease">The pre-release version string.</param>
    /// <param name="buildMetadata">The build metadata string.</param>
    public VersionInfo(int major, int minor, int patch, string? preRelease = null, string? buildMetadata = null)
    {
        if (major < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(major), "Major version cannot be negative.");
        }
        
        if (minor < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minor), "Minor version cannot be negative.");
        }
        
        if (patch < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(patch), "Patch version cannot be negative.");
        }
        
        Major = major;
        Minor = minor;
        Patch = patch;
        PreRelease = preRelease;
        BuildMetadata = buildMetadata;
    }
    
    /// <summary>
    /// Parses a version string into a VersionInfo object.
    /// </summary>
    /// <param name="versionString">The version string to parse.</param>
    /// <returns>A VersionInfo object representing the parsed version.</returns>
    /// <exception cref="ArgumentException">Thrown when the version string is not a valid semantic version.</exception>
    public static VersionInfo Parse(string versionString)
    {
        if (string.IsNullOrWhiteSpace(versionString))
        {
            throw new ArgumentException("Version string cannot be null or empty.", nameof(versionString));
        }
        
        var match = SemVerRegex.Match(versionString);
        if (!match.Success)
        {
            throw new ArgumentException($"Version string '{versionString}' is not a valid semantic version.", nameof(versionString));
        }
        
        var major = int.Parse(match.Groups[1].Value);
        var minor = int.Parse(match.Groups[2].Value);
        var patch = int.Parse(match.Groups[3].Value);
        var preRelease = match.Groups[4].Success ? match.Groups[4].Value : null;
        var buildMetadata = match.Groups[5].Success ? match.Groups[5].Value : null;
        
        return new VersionInfo(major, minor, patch, preRelease, buildMetadata);
    }
    
    /// <summary>
    /// Tries to parse a version string into a VersionInfo object.
    /// </summary>
    /// <param name="versionString">The version string to parse.</param>
    /// <param name="versionInfo">When this method returns, contains the VersionInfo object representing the parsed version, if the parsing succeeded, or null if the parsing failed.</param>
    /// <returns>true if the parsing succeeded; otherwise, false.</returns>
    public static bool TryParse(string versionString, out VersionInfo? versionInfo)
    {
        versionInfo = null;
        
        if (string.IsNullOrWhiteSpace(versionString))
        {
            return false;
        }
        
        var match = SemVerRegex.Match(versionString);
        if (!match.Success)
        {
            return false;
        }
        
        try
        {
            var major = int.Parse(match.Groups[1].Value);
            var minor = int.Parse(match.Groups[2].Value);
            var patch = int.Parse(match.Groups[3].Value);
            var preRelease = match.Groups[4].Success ? match.Groups[4].Value : null;
            var buildMetadata = match.Groups[5].Success ? match.Groups[5].Value : null;
            
            versionInfo = new VersionInfo(major, minor, patch, preRelease, buildMetadata);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Compares this version to another version.
    /// </summary>
    /// <param name="other">The other version to compare to.</param>
    /// <returns>A negative value if this version is less than the other version, zero if they are equal, or a positive value if this version is greater than the other version.</returns>
    public int CompareTo(VersionInfo other)
    {
        if (other == null)
        {
            return 1;
        }
        
        var majorComparison = Major.CompareTo(other.Major);
        if (majorComparison != 0)
        {
            return majorComparison;
        }
        
        var minorComparison = Minor.CompareTo(other.Minor);
        if (minorComparison != 0)
        {
            return minorComparison;
        }
        
        var patchComparison = Patch.CompareTo(other.Patch);
        if (patchComparison != 0)
        {
            return patchComparison;
        }
        
        // Pre-release versions have lower precedence than the associated normal version
        if (PreRelease == null && other.PreRelease == null)
        {
            return 0;
        }
        
        if (PreRelease == null)
        {
            return 1;
        }
        
        if (other.PreRelease == null)
        {
            return -1;
        }
        
        // Compare pre-release identifiers
        var thisIdentifiers = PreRelease.Split('.');
        var otherIdentifiers = other.PreRelease.Split('.');
        
        var minLength = Math.Min(thisIdentifiers.Length, otherIdentifiers.Length);
        
        for (int i = 0; i < minLength; i++)
        {
            var thisIdentifier = thisIdentifiers[i];
            var otherIdentifier = otherIdentifiers[i];
            
            var thisIsNumeric = int.TryParse(thisIdentifier, out var thisNumeric);
            var otherIsNumeric = int.TryParse(otherIdentifier, out var otherNumeric);
            
            if (thisIsNumeric && otherIsNumeric)
            {
                var numericComparison = thisNumeric.CompareTo(otherNumeric);
                if (numericComparison != 0)
                {
                    return numericComparison;
                }
            }
            else if (thisIsNumeric)
            {
                return -1; // Numeric identifiers have lower precedence than non-numeric identifiers
            }
            else if (otherIsNumeric)
            {
                return 1; // Numeric identifiers have lower precedence than non-numeric identifiers
            }
            else
            {
                var stringComparison = string.Compare(thisIdentifier, otherIdentifier, StringComparison.Ordinal);
                if (stringComparison != 0)
                {
                    return stringComparison;
                }
            }
        }
        
        // A larger set of pre-release fields has a higher precedence than a smaller set,
        // if all of the preceding identifiers are equal.
        return thisIdentifiers.Length.CompareTo(otherIdentifiers.Length);
    }
    
    /// <summary>
    /// Returns a string representation of the version.
    /// </summary>
    /// <returns>A string representation of the version.</returns>
    public override string ToString()
    {
        var version = $"{Major}.{Minor}.{Patch}";
        
        if (!string.IsNullOrEmpty(PreRelease))
        {
            version += $"-{PreRelease}";
        }
        
        if (!string.IsNullOrEmpty(BuildMetadata))
        {
            version += $"+{BuildMetadata}";
        }
        
        return version;
    }
}
