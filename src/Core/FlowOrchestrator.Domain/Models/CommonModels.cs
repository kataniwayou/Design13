namespace FlowOrchestrator.Domain.Models;

/// <summary>
/// Represents a compatibility issue
/// </summary>
public class CompatibilityIssue
{
    /// <summary>
    /// Issue type
    /// </summary>
    public string IssueType { get; set; } = string.Empty;

    /// <summary>
    /// Issue description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Issue severity
    /// </summary>
    public IssueSeverity Severity { get; set; }

    /// <summary>
    /// Affected component
    /// </summary>
    public string AffectedComponent { get; set; } = string.Empty;

    /// <summary>
    /// Affected feature
    /// </summary>
    public string? AffectedFeature { get; set; }

    /// <summary>
    /// Workaround
    /// </summary>
    public string? Workaround { get; set; }
}

/// <summary>
/// Represents a validation issue
/// </summary>
public class ValidationIssue
{
    /// <summary>
    /// Issue code
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Issue message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Issue severity
    /// </summary>
    public IssueSeverity Severity { get; set; }

    /// <summary>
    /// Property path
    /// </summary>
    public string? PropertyPath { get; set; }

    /// <summary>
    /// Additional details
    /// </summary>
    public Dictionary<string, string> Details { get; set; } = new Dictionary<string, string>();
}

/// <summary>
/// Issue severity enumeration
/// </summary>
public enum IssueSeverity
{
    /// <summary>
    /// Information severity
    /// </summary>
    Information,

    /// <summary>
    /// Warning severity
    /// </summary>
    Warning,

    /// <summary>
    /// Error severity
    /// </summary>
    Error,

    /// <summary>
    /// Critical severity
    /// </summary>
    Critical
}

/// <summary>
/// Represents a time range
/// </summary>
public class TimeRange
{
    /// <summary>
    /// Start time
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// End time
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Creates a time range from now to the specified number of hours ago
    /// </summary>
    /// <param name="hours">Number of hours</param>
    /// <returns>Time range</returns>
    public static TimeRange LastHours(int hours)
    {
        return new TimeRange
        {
            Start = DateTime.UtcNow.AddHours(-hours),
            End = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a time range from now to the specified number of days ago
    /// </summary>
    /// <param name="days">Number of days</param>
    /// <returns>Time range</returns>
    public static TimeRange LastDays(int days)
    {
        return new TimeRange
        {
            Start = DateTime.UtcNow.AddDays(-days),
            End = DateTime.UtcNow
        };
    }
}
