using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace FlowOrchestrator.ProtocolAdapters.Mqtt;

/// <summary>
/// Protocol adapter for MQTT.
/// </summary>
public class MqttProtocolAdapter : ProtocolAdapterBase
{
    private readonly MqttProtocolAdapterOptions _options;
    
    /// <inheritdoc />
    public override string ProtocolName => "MQTT";
    
    /// <inheritdoc />
    public override string ProtocolVersion => _options.MqttVersion;
    
    /// <inheritdoc />
    public override bool IsSecure => _options.UseTls;
    
    /// <inheritdoc />
    public override int DefaultPort => _options.UseTls ? 8883 : 1883;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="MqttProtocolAdapter"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options.</param>
    public MqttProtocolAdapter(ILogger<MqttProtocolAdapter> logger, MqttProtocolAdapterOptions options)
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
            SupportsCompression = false,
            SupportsStreaming = true,
            SupportsBinaryData = true,
            SupportsTextData = true,
            SupportsStructuredData = true,
            SupportsTransactions = false,
            SupportsMultiplexing = true,
            SupportsKeepAlive = true,
            SupportsReconnection = true,
            SupportsFlowControl = true,
            SupportsErrorCorrection = false,
            SupportsErrorDetection = true,
            SupportsQualityOfService = true,
            SupportedAuthenticationMethods = new List<string> { "Username/Password", "Certificate", "Token" },
            SupportedEncryptionMethods = new List<string> { "TLS 1.2", "TLS 1.3" },
            SupportedCompressionMethods = new List<string> { "none" },
            SupportedDataFormats = new List<string> { "JSON", "XML", "Text", "Binary" }
        };
    }
    
    /// <inheritdoc />
    public override object ConvertFromProtocol(object data, ProtocolConversionOptions? options = null)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        
        options ??= new ProtocolConversionOptions();
        
        LogInformation("Converting data from MQTT protocol");
        
        try
        {
            // Handle different types of MQTT data
            if (data is MqttMessageData messageData)
            {
                return ConvertFromMqttMessage(messageData, options);
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
            LogError(ex, "Error converting data from MQTT protocol");
            throw;
        }
    }
    
    /// <inheritdoc />
    public override object ConvertToProtocol(object data, ProtocolConversionOptions? options = null)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        
        options ??= new ProtocolConversionOptions();
        
        LogInformation("Converting data to MQTT protocol");
        
        try
        {
            // Create a new MQTT message
            var message = new MqttMessageData
            {
                Topic = GetTopic(options),
                QualityOfService = GetQualityOfService(options),
                Retain = GetRetain(options)
            };
            
            // Set the payload based on the data type
            if (data is string stringData)
            {
                message.Payload = stringData;
            }
            else if (data is byte[] byteData)
            {
                message.Payload = Encoding.UTF8.GetString(byteData);
            }
            else
            {
                // Serialize the data to JSON
                message.Payload = JsonSerializer.Serialize(data);
            }
            
            return message;
        }
        catch (Exception ex)
        {
            LogError(ex, "Error converting data to MQTT protocol");
            throw;
        }
    }
    
    private object ConvertFromMqttMessage(MqttMessageData messageData, ProtocolConversionOptions options)
    {
        // Try to parse the payload as JSON
        try
        {
            return JsonSerializer.Deserialize<object>(messageData.Payload) ?? new { };
        }
        catch
        {
            // If JSON parsing fails, return the payload as is
            return messageData.Payload;
        }
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
    
    private string GetTopic(ProtocolConversionOptions options)
    {
        // Check if the topic is specified in the options
        if (options.AdditionalOptions.TryGetValue("Topic", out var topicObj) && topicObj is string topic)
        {
            return topic;
        }
        
        // Otherwise, use the default topic from options
        return _options.DefaultTopic;
    }
    
    private int GetQualityOfService(ProtocolConversionOptions options)
    {
        // Check if the QoS is specified in the options
        if (options.AdditionalOptions.TryGetValue("QualityOfService", out var qosObj) && qosObj is int qos)
        {
            return qos;
        }
        
        // Otherwise, use the default QoS from options
        return _options.DefaultQualityOfService;
    }
    
    private bool GetRetain(ProtocolConversionOptions options)
    {
        // Check if the retain flag is specified in the options
        if (options.AdditionalOptions.TryGetValue("Retain", out var retainObj) && retainObj is bool retain)
        {
            return retain;
        }
        
        // Otherwise, use the default retain flag from options
        return _options.DefaultRetain;
    }
}
