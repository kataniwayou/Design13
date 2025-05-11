using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace FlowOrchestrator.ProtocolAdapters;

/// <summary>
/// Factory for creating protocol adapters.
/// </summary>
public class ProtocolAdapterFactory : IProtocolAdapterFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ProtocolAdapterFactory> _logger;
    private readonly ConcurrentDictionary<string, IProtocolAdapter> _adapters = new ConcurrentDictionary<string, IProtocolAdapter>();
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ProtocolAdapterFactory"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="logger">The logger.</param>
    public ProtocolAdapterFactory(IServiceProvider serviceProvider, ILogger<ProtocolAdapterFactory> logger)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    /// <inheritdoc />
    public IProtocolAdapter GetAdapter(string protocolName)
    {
        if (string.IsNullOrEmpty(protocolName))
        {
            throw new ArgumentException("Protocol name cannot be null or empty", nameof(protocolName));
        }
        
        // Try to get the adapter from the cache
        if (_adapters.TryGetValue(protocolName, out var adapter))
        {
            return adapter;
        }
        
        // Create a new adapter
        adapter = CreateAdapter(protocolName);
        
        // Add the adapter to the cache
        _adapters.TryAdd(protocolName, adapter);
        
        return adapter;
    }
    
    /// <inheritdoc />
    public IEnumerable<IProtocolAdapter> GetAllAdapters()
    {
        // Get all registered adapters
        var adapterTypes = _serviceProvider.GetServices<IProtocolAdapter>();
        
        foreach (var adapter in adapterTypes)
        {
            // Add the adapter to the cache if it's not already there
            _adapters.TryAdd(adapter.ProtocolName, adapter);
        }
        
        return _adapters.Values;
    }
    
    /// <inheritdoc />
    public bool HasAdapter(string protocolName)
    {
        if (string.IsNullOrEmpty(protocolName))
        {
            throw new ArgumentException("Protocol name cannot be null or empty", nameof(protocolName));
        }
        
        // Check if the adapter is in the cache
        if (_adapters.ContainsKey(protocolName))
        {
            return true;
        }
        
        // Try to create the adapter
        try
        {
            var adapter = CreateAdapter(protocolName);
            return adapter != null;
        }
        catch
        {
            return false;
        }
    }
    
    private IProtocolAdapter CreateAdapter(string protocolName)
    {
        _logger.LogInformation("Creating protocol adapter for {ProtocolName}", protocolName);
        
        // Try to get the adapter from the service provider
        var adapters = _serviceProvider.GetServices<IProtocolAdapter>();
        var adapter = adapters.FirstOrDefault(a => a.ProtocolName.Equals(protocolName, StringComparison.OrdinalIgnoreCase));
        
        if (adapter == null)
        {
            throw new InvalidOperationException($"Protocol adapter for {protocolName} not found");
        }
        
        return adapter;
    }
}
