using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowOrchestrator.Abstractions;
using MongoDB.Driver;
using MongoDB.Bson;

namespace FlowOrchestrator.Data.MongoDB
{
    /// <summary>
    /// Repository for service objects stored in MongoDB.
    /// </summary>
    public class ServiceRepository
    {
        private readonly MongoDbDataStore _dataStore;
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly string _collectionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRepository"/> class.
        /// </summary>
        /// <param name="dataStore">The MongoDB data store.</param>
        public ServiceRepository(MongoDbDataStore dataStore)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            _collectionName = "Services";
            _collection = _dataStore.GetCollection<BsonDocument>(_collectionName);
        }

        /// <summary>
        /// Registers a service in the repository.
        /// </summary>
        /// <param name="serviceId">The ID of the service.</param>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="metadata">Additional metadata for the service.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task RegisterServiceAsync(string serviceId, string serviceType, Dictionary<string, object> metadata = null)
        {
            if (string.IsNullOrEmpty(serviceId))
                throw new ArgumentException("Service ID cannot be null or empty.", nameof(serviceId));
            
            if (string.IsNullOrEmpty(serviceType))
                throw new ArgumentException("Service type cannot be null or empty.", nameof(serviceType));
            
            var filter = Builders<BsonDocument>.Filter.Eq("ServiceId", serviceId);
            var existingService = await _collection.Find(filter).FirstOrDefaultAsync();
            
            if (existingService != null)
            {
                // Update existing service
                var update = Builders<BsonDocument>.Update
                    .Set("ServiceType", serviceType)
                    .Set("LastUpdated", DateTime.UtcNow);
                
                if (metadata != null)
                {
                    foreach (var item in metadata)
                    {
                        update = update.Set($"Metadata.{item.Key}", item.Value);
                    }
                }
                
                await _collection.UpdateOneAsync(filter, update);
            }
            else
            {
                // Create new service
                var document = new BsonDocument
                {
                    { "ServiceId", serviceId },
                    { "ServiceType", serviceType },
                    { "Created", DateTime.UtcNow },
                    { "LastUpdated", DateTime.UtcNow },
                    { "Status", "Registered" }
                };
                
                if (metadata != null)
                {
                    var metadataDoc = new BsonDocument();
                    foreach (var item in metadata)
                    {
                        metadataDoc.Add(item.Key, BsonValue.Create(item.Value));
                    }
                    document.Add("Metadata", metadataDoc);
                }
                
                await _collection.InsertOneAsync(document);
            }
        }

        /// <summary>
        /// Unregisters a service from the repository.
        /// </summary>
        /// <param name="serviceId">The ID of the service to unregister.</param>
        /// <returns>True if the service was unregistered, false otherwise.</returns>
        public async Task<bool> UnregisterServiceAsync(string serviceId)
        {
            if (string.IsNullOrEmpty(serviceId))
                throw new ArgumentException("Service ID cannot be null or empty.", nameof(serviceId));
            
            var filter = Builders<BsonDocument>.Filter.Eq("ServiceId", serviceId);
            var result = await _collection.DeleteOneAsync(filter);
            
            return result.DeletedCount > 0;
        }

        /// <summary>
        /// Updates the status of a service in the repository.
        /// </summary>
        /// <param name="serviceId">The ID of the service.</param>
        /// <param name="status">The new status of the service.</param>
        /// <returns>True if the service status was updated, false otherwise.</returns>
        public async Task<bool> UpdateServiceStatusAsync(string serviceId, string status)
        {
            if (string.IsNullOrEmpty(serviceId))
                throw new ArgumentException("Service ID cannot be null or empty.", nameof(serviceId));
            
            if (string.IsNullOrEmpty(status))
                throw new ArgumentException("Status cannot be null or empty.", nameof(status));
            
            var filter = Builders<BsonDocument>.Filter.Eq("ServiceId", serviceId);
            var update = Builders<BsonDocument>.Update
                .Set("Status", status)
                .Set("LastUpdated", DateTime.UtcNow);
            
            var result = await _collection.UpdateOneAsync(filter, update);
            
            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Gets a service by its ID.
        /// </summary>
        /// <param name="serviceId">The ID of the service to get.</param>
        /// <returns>The service with the specified ID, or null if not found.</returns>
        public async Task<BsonDocument> GetServiceByIdAsync(string serviceId)
        {
            if (string.IsNullOrEmpty(serviceId))
                throw new ArgumentException("Service ID cannot be null or empty.", nameof(serviceId));
            
            var filter = Builders<BsonDocument>.Filter.Eq("ServiceId", serviceId);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all services of a specific type.
        /// </summary>
        /// <param name="serviceType">The type of services to get.</param>
        /// <returns>A list of services of the specified type.</returns>
        public async Task<List<BsonDocument>> GetServicesByTypeAsync(string serviceType)
        {
            if (string.IsNullOrEmpty(serviceType))
                throw new ArgumentException("Service type cannot be null or empty.", nameof(serviceType));
            
            var filter = Builders<BsonDocument>.Filter.Eq("ServiceType", serviceType);
            return await _collection.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Gets all services in the repository.
        /// </summary>
        /// <returns>A list of all services.</returns>
        public async Task<List<BsonDocument>> GetAllServicesAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }
    }
}
