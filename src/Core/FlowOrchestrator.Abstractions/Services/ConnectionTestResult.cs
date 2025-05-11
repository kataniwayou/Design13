using System;
using System.Collections.Generic;

namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Represents the result of a connection test.
/// </summary>
public class ConnectionTestResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the test was successful.
    /// </summary>
    public bool IsSuccess { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the test failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the exception if the test failed.
    /// </summary>
    public Exception? Exception { get; set; }
    
    /// <summary>
    /// Gets or sets the connection details.
    /// </summary>
    public Dictionary<string, string> ConnectionDetails { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the timestamp of the test.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the duration of the test in milliseconds.
    /// </summary>
    public long DurationMs { get; set; }
    
    /// <summary>
    /// Creates a successful test result.
    /// </summary>
    /// <param name="details">Optional connection details.</param>
    /// <param name="durationMs">The duration of the test in milliseconds.</param>
    /// <returns>A successful test result.</returns>
    public static ConnectionTestResult Success(Dictionary<string, string>? details = null, long durationMs = 0)
    {
        return new ConnectionTestResult
        {
            IsSuccess = true,
            ConnectionDetails = details ?? new Dictionary<string, string>(),
            DurationMs = durationMs
        };
    }
    
    /// <summary>
    /// Creates a failed test result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="exception">The exception that caused the failure.</param>
    /// <param name="durationMs">The duration of the test in milliseconds.</param>
    /// <returns>A failed test result.</returns>
    public static ConnectionTestResult Failure(string errorMessage, Exception? exception = null, long durationMs = 0)
    {
        return new ConnectionTestResult
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            Exception = exception,
            DurationMs = durationMs
        };
    }
}
