using FlowOrchestrator.Abstractions.Services;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace FlowOrchestrator.RestExporter;

/// <summary>
/// Connection manager for REST API operations.
/// </summary>
public class RestConnectionManager : IConnectionManager
{
    private readonly ILogger<RestConnectionManager> _logger;
    private readonly RestExporterOptions _options;
    private readonly HttpClient _httpClient;
    private bool _isOpen;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="RestConnectionManager"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options.</param>
    /// <param name="httpClient">The HTTP client.</param>
    public RestConnectionManager(ILogger<RestConnectionManager> logger, RestExporterOptions options, HttpClient httpClient)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    
    /// <inheritdoc />
    public async Task OpenAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Opening REST connection");
        
        if (_isOpen)
        {
            _logger.LogWarning("REST connection is already open");
            return;
        }
        
        // Configure HTTP client
        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
        
        // Configure default headers
        foreach (var header in _options.DefaultHeaders)
        {
            _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
        
        // Configure authentication
        if (_options.UseAuthentication)
        {
            ConfigureAuthentication();
        }
        
        // Configure proxy
        if (_options.UseProxy && !string.IsNullOrEmpty(_options.ProxyUrl))
        {
            ConfigureProxy();
        }
        
        _isOpen = true;
        
        await Task.CompletedTask;
    }
    
    /// <inheritdoc />
    public async Task CloseAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Closing REST connection");
        
        if (!_isOpen)
        {
            _logger.LogWarning("REST connection is already closed");
            return;
        }
        
        _isOpen = false;
        
        await Task.CompletedTask;
    }
    
    /// <inheritdoc />
    public async Task TestConnectionAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Testing REST connection");
        
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Head, _options.BaseUrl);
            using var response = await _httpClient.SendAsync(request, cancellationToken);
            
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error testing REST connection to {BaseUrl}", _options.BaseUrl);
            throw new InvalidOperationException($"Cannot connect to REST API at {_options.BaseUrl}", ex);
        }
    }
    
    private void ConfigureAuthentication()
    {
        switch (_options.AuthenticationMethod.ToLowerInvariant())
        {
            case "basic":
                if (!string.IsNullOrEmpty(_options.Username) && !string.IsNullOrEmpty(_options.Password))
                {
                    var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_options.Username}:{_options.Password}"));
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                }
                break;
            
            case "bearer":
                if (!string.IsNullOrEmpty(_options.BearerToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.BearerToken);
                }
                break;
            
            case "api-key":
                if (!string.IsNullOrEmpty(_options.ApiKey))
                {
                    _httpClient.DefaultRequestHeaders.Add(_options.ApiKeyHeaderName, _options.ApiKey);
                }
                break;
        }
    }
    
    private void ConfigureProxy()
    {
        // Note: This is a simplified implementation. In a real-world scenario, you would use HttpClientHandler
        // to configure the proxy, but that would require creating a new HttpClient instance.
        _logger.LogInformation("Proxy configuration is not supported in this implementation");
    }
}
