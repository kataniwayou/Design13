using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowOrchestrator.Common.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FlowOrchestrator.Data.MongoDB
{
    /// <summary>
    /// Repository for configuration objects stored in MongoDB.
    /// </summary>
    public class ConfigurationRepository
    {
        private readonly MongoDbDataStore _dataStore;
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly string _collectionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationRepository"/> class.
        /// </summary>
        /// <param name="dataStore">The MongoDB data store.</param>
        public ConfigurationRepository(MongoDbDataStore dataStore)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            _collectionName = "Configurations";
            _collection = _dataStore.GetCollection<BsonDocument>(_collectionName);
        }

        /// <summary>
        /// Saves a configuration to the repository.
        /// </summary>
        /// <param name="configKey">The key of the configuration.</param>
        /// <param name="configParameters">The configuration parameters.</param>
        /// <param name="scope">The scope of the configuration (e.g., "global", "service", "flow").</param>
        /// <param name="scopeId">The ID of the scope (e.g., service ID, flow ID).</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SaveConfigurationAsync(string configKey, ConfigurationParameters configParameters, string scope = "global", string scopeId = null)
        {
            if (string.IsNullOrEmpty(configKey))
                throw new ArgumentException("Configuration key cannot be null or empty.", nameof(configKey));
            
            if (configParameters == null)
                throw new ArgumentNullException(nameof(configParameters));
            
            if (string.IsNullOrEmpty(scope))
                throw new ArgumentException("Scope cannot be null or empty.", nameof(scope));
            
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("ConfigKey", configKey),
                Builders<BsonDocument>.Filter.Eq("Scope", scope),
                Builders<BsonDocument>.Filter.Eq("ScopeId", scopeId ?? string.Empty)
            );
            
            var existingConfig = await _collection.Find(filter).FirstOrDefaultAsync();
            
            var parametersDoc = new BsonDocument();
            foreach (var parameter in configParameters.ToDictionary())
            {
                parametersDoc.Add(parameter.Key, BsonValue.Create(parameter.Value));
            }
            
            if (existingConfig != null)
            {
                // Update existing configuration
                var update = Builders<BsonDocument>.Update
                    .Set("Parameters", parametersDoc)
                    .Set("LastUpdated", DateTime.UtcNow);
                
                await _collection.UpdateOneAsync(filter, update);
            }
            else
            {
                // Create new configuration
                var document = new BsonDocument
                {
                    { "ConfigKey", configKey },
                    { "Scope", scope },
                    { "ScopeId", scopeId ?? string.Empty },
                    { "Parameters", parametersDoc },
                    { "Created", DateTime.UtcNow },
                    { "LastUpdated", DateTime.UtcNow }
                };
                
                await _collection.InsertOneAsync(document);
            }
        }

        /// <summary>
        /// Gets a configuration from the repository.
        /// </summary>
        /// <param name="configKey">The key of the configuration.</param>
        /// <param name="scope">The scope of the configuration.</param>
        /// <param name="scopeId">The ID of the scope.</param>
        /// <returns>The configuration parameters, or null if not found.</returns>
        public async Task<ConfigurationParameters> GetConfigurationAsync(string configKey, string scope = "global", string scopeId = null)
        {
            if (string.IsNullOrEmpty(configKey))
                throw new ArgumentException("Configuration key cannot be null or empty.", nameof(configKey));
            
            if (string.IsNullOrEmpty(scope))
                throw new ArgumentException("Scope cannot be null or empty.", nameof(scope));
            
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("ConfigKey", configKey),
                Builders<BsonDocument>.Filter.Eq("Scope", scope),
                Builders<BsonDocument>.Filter.Eq("ScopeId", scopeId ?? string.Empty)
            );
            
            var config = await _collection.Find(filter).FirstOrDefaultAsync();
            
            if (config == null)
                return null;
            
            var parameters = new ConfigurationParameters();
            var parametersDoc = config["Parameters"].AsBsonDocument;
            
            foreach (var element in parametersDoc.Elements)
            {
                parameters.SetParameter(element.Name, BsonTypeMapper.MapToDotNetValue(element.Value));
            }
            
            return parameters;
        }

        /// <summary>
        /// Deletes a configuration from the repository.
        /// </summary>
        /// <param name="configKey">The key of the configuration.</param>
        /// <param name="scope">The scope of the configuration.</param>
        /// <param name="scopeId">The ID of the scope.</param>
        /// <returns>True if the configuration was deleted, false otherwise.</returns>
        public async Task<bool> DeleteConfigurationAsync(string configKey, string scope = "global", string scopeId = null)
        {
            if (string.IsNullOrEmpty(configKey))
                throw new ArgumentException("Configuration key cannot be null or empty.", nameof(configKey));
            
            if (string.IsNullOrEmpty(scope))
                throw new ArgumentException("Scope cannot be null or empty.", nameof(scope));
            
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("ConfigKey", configKey),
                Builders<BsonDocument>.Filter.Eq("Scope", scope),
                Builders<BsonDocument>.Filter.Eq("ScopeId", scopeId ?? string.Empty)
            );
            
            var result = await _collection.DeleteOneAsync(filter);
            
            return result.DeletedCount > 0;
        }

        /// <summary>
        /// Gets all configurations for a specific scope.
        /// </summary>
        /// <param name="scope">The scope of the configurations.</param>
        /// <param name="scopeId">The ID of the scope.</param>
        /// <returns>A dictionary of configuration keys and parameters.</returns>
        public async Task<Dictionary<string, ConfigurationParameters>> GetConfigurationsByScopeAsync(string scope, string scopeId = null)
        {
            if (string.IsNullOrEmpty(scope))
                throw new ArgumentException("Scope cannot be null or empty.", nameof(scope));
            
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("Scope", scope),
                Builders<BsonDocument>.Filter.Eq("ScopeId", scopeId ?? string.Empty)
            );
            
            var configs = await _collection.Find(filter).ToListAsync();
            var result = new Dictionary<string, ConfigurationParameters>();
            
            foreach (var config in configs)
            {
                var configKey = config["ConfigKey"].AsString;
                var parameters = new ConfigurationParameters();
                var parametersDoc = config["Parameters"].AsBsonDocument;
                
                foreach (var element in parametersDoc.Elements)
                {
                    parameters.SetParameter(element.Name, BsonTypeMapper.MapToDotNetValue(element.Value));
                }
                
                result[configKey] = parameters;
            }
            
            return result;
        }
    }
}
