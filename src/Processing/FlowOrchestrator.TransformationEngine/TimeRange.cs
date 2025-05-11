namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents a time range.
/// </summary>
public class TimeRange
{
    /// <summary>
    /// Gets or sets the start time.
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the end time.
    /// </summary>
    public DateTime EndTime { get; set; }
    
    /// <summary>
    /// Creates a new time range.
    /// </summary>
    /// <param name="startTime">The start time.</param>
    /// <param name="endTime">The end time.</param>
    public TimeRange(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
    
    /// <summary>
    /// Creates a time range for the last specified number of days.
    /// </summary>
    /// <param name="days">The number of days.</param>
    /// <returns>A time range for the last specified number of days.</returns>
    public static TimeRange LastDays(int days)
    {
        return new TimeRange(DateTime.UtcNow.AddDays(-days), DateTime.UtcNow);
    }
    
    /// <summary>
    /// Creates a time range for the last specified number of hours.
    /// </summary>
    /// <param name="hours">The number of hours.</param>
    /// <returns>A time range for the last specified number of hours.</returns>
    public static TimeRange LastHours(int hours)
    {
        return new TimeRange(DateTime.UtcNow.AddHours(-hours), DateTime.UtcNow);
    }
    
    /// <summary>
    /// Creates a time range for the last specified number of minutes.
    /// </summary>
    /// <param name="minutes">The number of minutes.</param>
    /// <returns>A time range for the last specified number of minutes.</returns>
    public static TimeRange LastMinutes(int minutes)
    {
        return new TimeRange(DateTime.UtcNow.AddMinutes(-minutes), DateTime.UtcNow);
    }
    
    /// <summary>
    /// Creates a time range for today.
    /// </summary>
    /// <returns>A time range for today.</returns>
    public static TimeRange Today()
    {
        var today = DateTime.UtcNow.Date;
        return new TimeRange(today, today.AddDays(1).AddTicks(-1));
    }
    
    /// <summary>
    /// Creates a time range for yesterday.
    /// </summary>
    /// <returns>A time range for yesterday.</returns>
    public static TimeRange Yesterday()
    {
        var yesterday = DateTime.UtcNow.Date.AddDays(-1);
        return new TimeRange(yesterday, yesterday.AddDays(1).AddTicks(-1));
    }
    
    /// <summary>
    /// Creates a time range for this week.
    /// </summary>
    /// <returns>A time range for this week.</returns>
    public static TimeRange ThisWeek()
    {
        var today = DateTime.UtcNow.Date;
        var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
        return new TimeRange(startOfWeek, startOfWeek.AddDays(7).AddTicks(-1));
    }
    
    /// <summary>
    /// Creates a time range for this month.
    /// </summary>
    /// <returns>A time range for this month.</returns>
    public static TimeRange ThisMonth()
    {
        var today = DateTime.UtcNow.Date;
        var startOfMonth = new DateTime(today.Year, today.Month, 1);
        return new TimeRange(startOfMonth, startOfMonth.AddMonths(1).AddTicks(-1));
    }
}
