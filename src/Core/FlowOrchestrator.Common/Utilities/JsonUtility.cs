using System.Text.Json;
using System.Text.Json.Serialization;

namespace FlowOrchestrator.Common.Utilities;

/// <summary>
/// Utility class for working with JSON.
/// </summary>
public static class JsonUtility
{
    /// <summary>
    /// The default JSON serializer options.
    /// </summary>
    public static readonly JsonSerializerOptions DefaultSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter() }
    };
    
    /// <summary>
    /// Serializes an object to a JSON string.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <param name="options">The JSON serializer options to use.</param>
    /// <returns>The JSON string representation of the object.</returns>
    public static string Serialize<T>(T obj, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(obj, options ?? DefaultSerializerOptions);
    }
    
    /// <summary>
    /// Deserializes a JSON string to an object.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="options">The JSON serializer options to use.</param>
    /// <returns>The deserialized object.</returns>
    public static T? Deserialize<T>(string json, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize<T>(json, options ?? DefaultSerializerOptions);
    }
    
    /// <summary>
    /// Tries to deserialize a JSON string to an object.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="result">The deserialized object, or default if deserialization fails.</param>
    /// <param name="options">The JSON serializer options to use.</param>
    /// <returns>True if deserialization succeeded, false otherwise.</returns>
    public static bool TryDeserialize<T>(string json, out T? result, JsonSerializerOptions? options = null)
    {
        try
        {
            result = Deserialize<T>(json, options);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
    
    /// <summary>
    /// Validates that a string is valid JSON.
    /// </summary>
    /// <param name="json">The JSON string to validate.</param>
    /// <returns>True if the string is valid JSON, false otherwise.</returns>
    public static bool IsValidJson(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
