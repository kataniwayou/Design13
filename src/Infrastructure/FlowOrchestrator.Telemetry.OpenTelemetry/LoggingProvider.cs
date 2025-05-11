using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Telemetry.OpenTelemetry
{
    /// <summary>
    /// Provider for logging in the FlowOrchestrator system.
    /// </summary>
    public class LoggingProvider
    {
        private readonly ILogger<LoggingProvider> _logger;
        private readonly TracingProvider _tracingProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingProvider"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="tracingProvider">The tracing provider.</param>
        public LoggingProvider(ILogger<LoggingProvider> logger, TracingProvider tracingProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tracingProvider = tracingProvider ?? throw new ArgumentNullException(nameof(tracingProvider));
        }

        /// <summary>
        /// Logs a message at the specified log level.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional arguments for the message.</param>
        public void Log(LogLevel logLevel, string message, params object[] args)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Message cannot be null or empty.", nameof(message));
            
            try
            {
                // Log the message
                _logger.Log(logLevel, message, args);
                
                // Add the log as an event to the current activity
                var activity = Activity.Current;
                if (activity != null)
                {
                    var tags = new Dictionary<string, object>
                    {
                        { "logLevel", logLevel.ToString() }
                    };
                    
                    _tracingProvider.AddEvent($"Log: {message}", tags);
                }
            }
            catch (Exception ex)
            {
                // Don't log the error to avoid infinite recursion
                Console.Error.WriteLine($"Error logging message: {ex.Message}");
            }
        }

        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional arguments for the message.</param>
        public void LogDebug(string message, params object[] args)
        {
            Log(LogLevel.Debug, message, args);
        }

        /// <summary>
        /// Logs an information message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional arguments for the message.</param>
        public void LogInformation(string message, params object[] args)
        {
            Log(LogLevel.Information, message, args);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional arguments for the message.</param>
        public void LogWarning(string message, params object[] args)
        {
            Log(LogLevel.Warning, message, args);
        }

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional arguments for the message.</param>
        public void LogError(string message, params object[] args)
        {
            Log(LogLevel.Error, message, args);
        }

        /// <summary>
        /// Logs an error message with an exception.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional arguments for the message.</param>
        public void LogError(Exception exception, string message, params object[] args)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));
            
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Message cannot be null or empty.", nameof(message));
            
            try
            {
                // Log the error
                _logger.LogError(exception, message, args);
                
                // Add the error as an event to the current activity
                var activity = Activity.Current;
                if (activity != null)
                {
                    var tags = new Dictionary<string, object>
                    {
                        { "logLevel", LogLevel.Error.ToString() },
                        { "exceptionType", exception.GetType().Name },
                        { "exceptionMessage", exception.Message }
                    };
                    
                    _tracingProvider.AddEvent($"Error: {message}", tags);
                    
                    // Set the activity status to error
                    _tracingProvider.SetStatus(ActivityStatusCode.Error, exception.Message);
                }
            }
            catch (Exception ex)
            {
                // Don't log the error to avoid infinite recursion
                Console.Error.WriteLine($"Error logging error: {ex.Message}");
            }
        }

        /// <summary>
        /// Logs a critical message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional arguments for the message.</param>
        public void LogCritical(string message, params object[] args)
        {
            Log(LogLevel.Critical, message, args);
        }

        /// <summary>
        /// Logs a critical message with an exception.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">Optional arguments for the message.</param>
        public void LogCritical(Exception exception, string message, params object[] args)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));
            
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException("Message cannot be null or empty.", nameof(message));
            
            try
            {
                // Log the critical error
                _logger.LogCritical(exception, message, args);
                
                // Add the critical error as an event to the current activity
                var activity = Activity.Current;
                if (activity != null)
                {
                    var tags = new Dictionary<string, object>
                    {
                        { "logLevel", LogLevel.Critical.ToString() },
                        { "exceptionType", exception.GetType().Name },
                        { "exceptionMessage", exception.Message }
                    };
                    
                    _tracingProvider.AddEvent($"Critical: {message}", tags);
                    
                    // Set the activity status to error
                    _tracingProvider.SetStatus(ActivityStatusCode.Error, exception.Message);
                }
            }
            catch (Exception ex)
            {
                // Don't log the error to avoid infinite recursion
                Console.Error.WriteLine($"Error logging critical error: {ex.Message}");
            }
        }
    }
}
