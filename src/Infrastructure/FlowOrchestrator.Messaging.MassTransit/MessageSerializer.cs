using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Messaging.MassTransit
{
    /// <summary>
    /// Serializer for messages in the FlowOrchestrator system.
    /// </summary>
    public class MessageSerializer
    {
        private readonly ILogger<MessageSerializer> _logger;
        private readonly JsonSerializerOptions _serializerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageSerializer"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public MessageSerializer(ILogger<MessageSerializer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false,
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Serializes an object to JSON.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized JSON string.</returns>
        public string Serialize<T>(T obj) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            try
            {
                _logger.LogDebug("Serializing object of type {ObjectType}", typeof(T).Name);

                // Serialize the object to JSON
                var json = JsonSerializer.Serialize(obj, _serializerOptions);

                _logger.LogDebug("Object of type {ObjectType} serialized successfully", typeof(T).Name);

                return json;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error serializing object of type {ObjectType}", typeof(T).Name);
                throw;
            }
        }

        /// <summary>
        /// Deserializes a JSON string to an object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public T Deserialize<T>(string json) where T : class
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentException("JSON string cannot be null or empty.", nameof(json));

            try
            {
                _logger.LogDebug("Deserializing JSON to object of type {ObjectType}", typeof(T).Name);

                // Deserialize the JSON to an object
                var obj = JsonSerializer.Deserialize<T>(json, _serializerOptions);

                _logger.LogDebug("JSON deserialized successfully to object of type {ObjectType}", typeof(T).Name);

                return obj ?? throw new InvalidOperationException($"Deserialization of type {typeof(T).Name} resulted in null object");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing JSON to object of type {ObjectType}", typeof(T).Name);
                throw;
            }
        }

        /// <summary>
        /// Serializes an object to a byte array.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized byte array.</returns>
        public byte[] SerializeToBytes<T>(T obj) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            try
            {
                _logger.LogDebug("Serializing object of type {ObjectType} to bytes", typeof(T).Name);

                // Serialize the object to a byte array
                var bytes = JsonSerializer.SerializeToUtf8Bytes(obj, _serializerOptions);

                _logger.LogDebug("Object of type {ObjectType} serialized successfully to bytes", typeof(T).Name);

                return bytes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error serializing object of type {ObjectType} to bytes", typeof(T).Name);
                throw;
            }
        }

        /// <summary>
        /// Deserializes a byte array to an object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="bytes">The byte array to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public T DeserializeFromBytes<T>(byte[] bytes) where T : class
        {
            if (bytes == null || bytes.Length == 0)
                throw new ArgumentException("Byte array cannot be null or empty.", nameof(bytes));

            try
            {
                _logger.LogDebug("Deserializing bytes to object of type {ObjectType}", typeof(T).Name);

                // Deserialize the byte array to an object
                var obj = JsonSerializer.Deserialize<T>(bytes, _serializerOptions);

                _logger.LogDebug("Bytes deserialized successfully to object of type {ObjectType}", typeof(T).Name);

                return obj ?? throw new InvalidOperationException($"Deserialization of type {typeof(T).Name} resulted in null object");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing bytes to object of type {ObjectType}", typeof(T).Name);
                throw;
            }
        }

        /// <summary>
        /// Serializes an object to a stream.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SerializeToStreamAsync<T>(T obj, Stream stream) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            try
            {
                _logger.LogDebug("Serializing object of type {ObjectType} to stream", typeof(T).Name);

                // Serialize the object to the stream
                await JsonSerializer.SerializeAsync(stream, obj, _serializerOptions);

                _logger.LogDebug("Object of type {ObjectType} serialized successfully to stream", typeof(T).Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error serializing object of type {ObjectType} to stream", typeof(T).Name);
                throw;
            }
        }

        /// <summary>
        /// Deserializes a stream to an object.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The deserialized object.</returns>
        public async Task<T> DeserializeFromStreamAsync<T>(Stream stream) where T : class
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            try
            {
                _logger.LogDebug("Deserializing stream to object of type {ObjectType}", typeof(T).Name);

                // Deserialize the stream to an object
                var obj = await JsonSerializer.DeserializeAsync<T>(stream, _serializerOptions);

                _logger.LogDebug("Stream deserialized successfully to object of type {ObjectType}", typeof(T).Name);

                return obj ?? throw new InvalidOperationException($"Deserialization of type {typeof(T).Name} resulted in null object");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing stream to object of type {ObjectType}", typeof(T).Name);
                throw;
            }
        }
    }
}
