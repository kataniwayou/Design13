using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.ProtocolAdapters;

/// <summary>
/// Base class for protocol adapters.
/// </summary>
public abstract class ProtocolAdapterBase : IProtocolAdapter
{
    private readonly ILogger _logger;
    
    /// <inheritdoc />
    public abstract string ProtocolName { get; }
    
    /// <inheritdoc />
    public abstract string ProtocolVersion { get; }
    
    /// <inheritdoc />
    public abstract bool IsSecure { get; }
    
    /// <inheritdoc />
    public abstract int DefaultPort { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ProtocolAdapterBase"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    protected ProtocolAdapterBase(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    /// <inheritdoc />
    public abstract ProtocolCapabilities GetCapabilities();
    
    /// <inheritdoc />
    public abstract object ConvertFromProtocol(object data, ProtocolConversionOptions? options = null);
    
    /// <inheritdoc />
    public abstract object ConvertToProtocol(object data, ProtocolConversionOptions? options = null);
    
    /// <summary>
    /// Logs a message at the debug level.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The message arguments.</param>
    protected void LogDebug(string message, params object[] args)
    {
        _logger.LogDebug(message, args);
    }
    
    /// <summary>
    /// Logs a message at the information level.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The message arguments.</param>
    protected void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }
    
    /// <summary>
    /// Logs a message at the warning level.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The message arguments.</param>
    protected void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }
    
    /// <summary>
    /// Logs a message at the error level.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The message arguments.</param>
    protected void LogError(string message, params object[] args)
    {
        _logger.LogError(message, args);
    }
    
    /// <summary>
    /// Logs an exception at the error level.
    /// </summary>
    /// <param name="exception">The exception to log.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The message arguments.</param>
    protected void LogError(Exception exception, string message, params object[] args)
    {
        _logger.LogError(exception, message, args);
    }
}
