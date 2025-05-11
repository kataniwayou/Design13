using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace FlowOrchestrator.ProtocolAdapters.Http;

/// <summary>
/// Protocol adapter for HTTP.
/// </summary>
public class HttpProtocolAdapter : ProtocolAdapterBase
{
    private readonly HttpProtocolAdapterOptions _options;
    
    /// <inheritdoc />
    public override string ProtocolName => "HTTP";
    
    /// <inheritdoc />
    public override string ProtocolVersion => _options.HttpVersion;
    
    /// <inheritdoc />
    public override bool IsSecure => _options.UseHttps;
    
    /// <inheritdoc />
    public override int DefaultPort => _options.UseHttps ? 443 : 80;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="HttpProtocolAdapter"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options.</param>
    public HttpProtocolAdapter(ILogger<HttpProtocolAdapter> logger, HttpProtocolAdapterOptions options)
        : base(logger)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }
    
    /// <inheritdoc />
    public override ProtocolCapabilities GetCapabilities()
    {
        return new ProtocolCapabilities
        {
            SupportsAuthentication = true,
            SupportsEncryption = true,
            SupportsCompression = true,
            SupportsStreaming = true,
            SupportsBinaryData = true,
            SupportsTextData = true,
            SupportsStructuredData = true,
            SupportsTransactions = false,
            SupportsMultiplexing = _options.HttpVersion == "HTTP/2",
            SupportsKeepAlive = true,
            SupportsReconnection = false,
            SupportsFlowControl = _options.HttpVersion == "HTTP/2",
            SupportsErrorCorrection = false,
            SupportsErrorDetection = true,
            SupportsQualityOfService = false,
            SupportedAuthenticationMethods = new List<string> { "Basic", "Bearer", "Digest", "OAuth", "API Key" },
            SupportedEncryptionMethods = new List<string> { "TLS 1.2", "TLS 1.3" },
            SupportedCompressionMethods = new List<string> { "gzip", "deflate", "br" },
            SupportedDataFormats = new List<string> { "JSON", "XML", "Form", "Multipart", "Text", "Binary" }
        };
    }
    
    /// <inheritdoc />
    public override object ConvertFromProtocol(object data, ProtocolConversionOptions? options = null)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        
        options ??= new ProtocolConversionOptions();
        
        LogInformation("Converting data from HTTP protocol");
        
        try
        {
            // Handle different types of HTTP data
            if (data is HttpRequestData requestData)
            {
                return ConvertFromHttpRequest(requestData, options);
            }
            
            if (data is HttpResponseData responseData)
            {
                return ConvertFromHttpResponse(responseData, options);
            }
            
            if (data is string stringData)
            {
                return ConvertFromString(stringData, options);
            }
            
            if (data is byte[] byteData)
            {
                return ConvertFromBytes(byteData, options);
            }
            
            throw new ArgumentException($"Unsupported data type: {data.GetType().Name}", nameof(data));
        }
        catch (Exception ex)
        {
            LogError(ex, "Error converting data from HTTP protocol");
            throw;
        }
    }
    
    /// <inheritdoc />
    public override object ConvertToProtocol(object data, ProtocolConversionOptions? options = null)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        
        options ??= new ProtocolConversionOptions();
        
        LogInformation("Converting data to HTTP protocol");
        
        try
        {
            // Determine the target format
            var targetFormat = options.TargetFormat.ToLowerInvariant();
            
            if (string.IsNullOrEmpty(targetFormat))
            {
                targetFormat = _options.DefaultContentType.ToLowerInvariant();
            }
            
            // Convert to the target format
            switch (targetFormat)
            {
                case "json":
                    return ConvertToJson(data, options);
                
                case "xml":
                    return ConvertToXml(data, options);
                
                case "form":
                    return ConvertToForm(data, options);
                
                case "multipart":
                    return ConvertToMultipart(data, options);
                
                case "text":
                    return ConvertToText(data, options);
                
                case "binary":
                    return ConvertToBinary(data, options);
                
                default:
                    throw new ArgumentException($"Unsupported target format: {targetFormat}", nameof(options));
            }
        }
        catch (Exception ex)
        {
            LogError(ex, "Error converting data to HTTP protocol");
            throw;
        }
    }
    
    private object ConvertFromHttpRequest(HttpRequestData requestData, ProtocolConversionOptions options)
    {
        // Extract the content type from the headers
        var contentType = requestData.Headers.GetValueOrDefault("Content-Type", _options.DefaultContentType);
        
        // Convert based on content type
        if (contentType.StartsWith("application/json"))
        {
            return JsonSerializer.Deserialize<object>(requestData.Body) ?? new { };
        }
        
        if (contentType.StartsWith("application/xml"))
        {
            // For simplicity, we'll just return the body as a string
            return requestData.Body;
        }
        
        if (contentType.StartsWith("application/x-www-form-urlencoded"))
        {
            // Parse form data
            var formData = new Dictionary<string, string>();
            var pairs = requestData.Body.Split('&');
            
            foreach (var pair in pairs)
            {
                var keyValue = pair.Split('=');
                
                if (keyValue.Length == 2)
                {
                    formData[Uri.UnescapeDataString(keyValue[0])] = Uri.UnescapeDataString(keyValue[1]);
                }
            }
            
            return formData;
        }
        
        // Default to returning the body as a string
        return requestData.Body;
    }
    
    private object ConvertFromHttpResponse(HttpResponseData responseData, ProtocolConversionOptions options)
    {
        // Extract the content type from the headers
        var contentType = responseData.Headers.GetValueOrDefault("Content-Type", _options.DefaultContentType);
        
        // Convert based on content type
        if (contentType.StartsWith("application/json"))
        {
            return JsonSerializer.Deserialize<object>(responseData.Body) ?? new { };
        }
        
        if (contentType.StartsWith("application/xml"))
        {
            // For simplicity, we'll just return the body as a string
            return responseData.Body;
        }
        
        // Default to returning the body as a string
        return responseData.Body;
    }
    
    private object ConvertFromString(string data, ProtocolConversionOptions options)
    {
        // Try to parse as JSON
        try
        {
            return JsonSerializer.Deserialize<object>(data) ?? new { };
        }
        catch
        {
            // If JSON parsing fails, return the string as is
            return data;
        }
    }
    
    private object ConvertFromBytes(byte[] data, ProtocolConversionOptions options)
    {
        // Try to convert to string using the specified encoding
        try
        {
            var encoding = Encoding.GetEncoding(options.CharacterEncoding);
            var stringData = encoding.GetString(data);
            
            // Try to parse as JSON
            try
            {
                return JsonSerializer.Deserialize<object>(stringData) ?? new { };
            }
            catch
            {
                // If JSON parsing fails, return the string as is
                return stringData;
            }
        }
        catch
        {
            // If string conversion fails, return the bytes as is
            return data;
        }
    }
    
    private object ConvertToJson(object data, ProtocolConversionOptions options)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        
        return new HttpResponseData
        {
            StatusCode = 200,
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            },
            Body = json
        };
    }
    
    private object ConvertToXml(object data, ProtocolConversionOptions options)
    {
        // For simplicity, we'll just convert to string
        var xml = data.ToString() ?? string.Empty;
        
        return new HttpResponseData
        {
            StatusCode = 200,
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/xml" }
            },
            Body = xml
        };
    }
    
    private object ConvertToForm(object data, ProtocolConversionOptions options)
    {
        // Convert to form data
        var formData = new StringBuilder();
        
        if (data is IDictionary<string, object> dictionary)
        {
            var first = true;
            
            foreach (var kvp in dictionary)
            {
                if (!first)
                {
                    formData.Append('&');
                }
                
                formData.Append(Uri.EscapeDataString(kvp.Key));
                formData.Append('=');
                formData.Append(Uri.EscapeDataString(kvp.Value?.ToString() ?? string.Empty));
                
                first = false;
            }
        }
        
        return new HttpResponseData
        {
            StatusCode = 200,
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/x-www-form-urlencoded" }
            },
            Body = formData.ToString()
        };
    }
    
    private object ConvertToMultipart(object data, ProtocolConversionOptions options)
    {
        // For simplicity, we'll just convert to string
        var multipart = data.ToString() ?? string.Empty;
        
        return new HttpResponseData
        {
            StatusCode = 200,
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "multipart/form-data" }
            },
            Body = multipart
        };
    }
    
    private object ConvertToText(object data, ProtocolConversionOptions options)
    {
        // Convert to text
        var text = data.ToString() ?? string.Empty;
        
        return new HttpResponseData
        {
            StatusCode = 200,
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "text/plain" }
            },
            Body = text
        };
    }
    
    private object ConvertToBinary(object data, ProtocolConversionOptions options)
    {
        // For simplicity, we'll just convert to string
        var binary = data.ToString() ?? string.Empty;
        
        return new HttpResponseData
        {
            StatusCode = 200,
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/octet-stream" }
            },
            Body = binary
        };
    }
}
