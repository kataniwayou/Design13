using FlowOrchestrator.ProtocolAdapters.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace FlowOrchestrator.ProtocolAdapters.Tests.Http;

public class HttpProtocolAdapterTests
{
    private readonly Mock<ILogger<HttpProtocolAdapter>> _loggerMock;
    private readonly HttpProtocolAdapterOptions _options;

    public HttpProtocolAdapterTests()
    {
        _loggerMock = new Mock<ILogger<HttpProtocolAdapter>>();
        _options = new HttpProtocolAdapterOptions
        {
            UseHttps = true,
            HttpVersion = "HTTP/1.1",
            DefaultContentType = "application/json",
            DefaultCharacterSet = "utf-8",
            UseCompression = true,
            DefaultCompressionMethod = "gzip",
            UseAuthentication = false,
            DefaultAuthenticationMethod = "Bearer",
            FollowRedirects = true,
            MaxRedirects = 10,
            TimeoutSeconds = 30,
            UseKeepAlive = true
        };
    }

    [Fact]
    public void Constructor_WithValidParameters_CreatesInstance()
    {
        // Arrange & Act
        var adapter = new HttpProtocolAdapter(_loggerMock.Object, _options);

        // Assert
        Assert.NotNull(adapter);
        Assert.Equal("HTTP", adapter.ProtocolName);
        Assert.Equal("HTTP/1.1", adapter.ProtocolVersion);
        Assert.True(adapter.IsSecure);
        Assert.Equal(443, adapter.DefaultPort);
    }

    [Fact]
    public void Constructor_WithNullParameters_ThrowsArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => new HttpProtocolAdapter(null!, _options));
        Assert.Throws<ArgumentNullException>(() => new HttpProtocolAdapter(_loggerMock.Object, null!));
    }

    [Fact]
    public void GetCapabilities_ReturnsExpectedCapabilities()
    {
        // Arrange
        var adapter = new HttpProtocolAdapter(_loggerMock.Object, _options);

        // Act
        var capabilities = adapter.GetCapabilities();

        // Assert
        Assert.NotNull(capabilities);
        Assert.True(capabilities.SupportsAuthentication);
        Assert.True(capabilities.SupportsEncryption);
        Assert.True(capabilities.SupportsCompression);
        Assert.True(capabilities.SupportsStreaming);
        Assert.True(capabilities.SupportsBinaryData);
        Assert.True(capabilities.SupportsTextData);
        Assert.True(capabilities.SupportsStructuredData);
        Assert.False(capabilities.SupportsTransactions);
        Assert.False(capabilities.SupportsMultiplexing);
        Assert.True(capabilities.SupportsKeepAlive);
        Assert.False(capabilities.SupportsReconnection);
        Assert.False(capabilities.SupportsFlowControl);
        Assert.False(capabilities.SupportsErrorCorrection);
        Assert.True(capabilities.SupportsErrorDetection);
        Assert.False(capabilities.SupportsQualityOfService);
        
        Assert.Contains("Basic", capabilities.SupportedAuthenticationMethods);
        Assert.Contains("Bearer", capabilities.SupportedAuthenticationMethods);
        Assert.Contains("Digest", capabilities.SupportedAuthenticationMethods);
        Assert.Contains("OAuth", capabilities.SupportedAuthenticationMethods);
        Assert.Contains("API Key", capabilities.SupportedAuthenticationMethods);
        
        Assert.Contains("TLS 1.2", capabilities.SupportedEncryptionMethods);
        Assert.Contains("TLS 1.3", capabilities.SupportedEncryptionMethods);
        
        Assert.Contains("gzip", capabilities.SupportedCompressionMethods);
        Assert.Contains("deflate", capabilities.SupportedCompressionMethods);
        Assert.Contains("br", capabilities.SupportedCompressionMethods);
        
        Assert.Contains("JSON", capabilities.SupportedDataFormats);
        Assert.Contains("XML", capabilities.SupportedDataFormats);
        Assert.Contains("Form", capabilities.SupportedDataFormats);
        Assert.Contains("Multipart", capabilities.SupportedDataFormats);
        Assert.Contains("Text", capabilities.SupportedDataFormats);
        Assert.Contains("Binary", capabilities.SupportedDataFormats);
    }

    [Fact]
    public void ConvertFromProtocol_WithHttpRequestData_ReturnsExpectedResult()
    {
        // Arrange
        var adapter = new HttpProtocolAdapter(_loggerMock.Object, _options);
        var requestData = new HttpRequestData
        {
            Method = "GET",
            Url = "https://example.com/api/data",
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            },
            Body = "{\"name\":\"Test\",\"value\":123}"
        };

        // Act
        var result = adapter.ConvertFromProtocol(requestData);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<System.Text.Json.JsonElement>(result);
        var jsonElement = (System.Text.Json.JsonElement)result;
        Assert.Equal("Test", jsonElement.GetProperty("name").GetString());
        Assert.Equal(123, jsonElement.GetProperty("value").GetInt32());
    }

    [Fact]
    public void ConvertToProtocol_WithObject_ReturnsExpectedResult()
    {
        // Arrange
        var adapter = new HttpProtocolAdapter(_loggerMock.Object, _options);
        var data = new { Name = "Test", Value = 123 };
        var options = new ProtocolConversionOptions
        {
            TargetFormat = "json"
        };

        // Act
        var result = adapter.ConvertToProtocol(data, options);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<HttpResponseData>(result);
        var responseData = (HttpResponseData)result;
        Assert.Equal(200, responseData.StatusCode);
        Assert.Equal("application/json", responseData.Headers["Content-Type"]);
        Assert.Contains("\"Name\":\"Test\"", responseData.Body);
        Assert.Contains("\"Value\":123", responseData.Body);
    }
}
