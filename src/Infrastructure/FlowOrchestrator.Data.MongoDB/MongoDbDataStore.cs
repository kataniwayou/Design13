using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowOrchestrator.Common.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;

namespace FlowOrchestrator.Data.MongoDB
{
    /// <summary>
    /// MongoDB data store implementation for the FlowOrchestrator system.
    /// </summary>
    public class MongoDbDataStore : IDisposable
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly ConfigurationParameters _configuration;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDbDataStore"/> class.
        /// </summary>
        /// <param name="configuration">The configuration parameters for the MongoDB connection.</param>
        public MongoDbDataStore(ConfigurationParameters configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            string connectionString = _configuration.GetParameter<string>("ConnectionString")
                ?? throw new ArgumentException("MongoDB connection string not found in configuration.");

            string databaseName = _configuration.GetParameter<string>("DatabaseName")
                ?? throw new ArgumentException("MongoDB database name not found in configuration.");

            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        /// <summary>
        /// Gets a MongoDB collection with the specified name.
        /// </summary>
        /// <typeparam name="T">The type of documents stored in the collection.</typeparam>
        /// <param name="collectionName">The name of the collection.</param>
        /// <returns>An IMongoCollection instance for the specified collection.</returns>
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentException("Collection name cannot be null or empty.", nameof(collectionName));

            return _database.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Creates a MongoDB collection with the specified name if it doesn't exist.
        /// </summary>
        /// <param name="collectionName">The name of the collection to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateCollectionIfNotExistsAsync(string collectionName)
        {
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentException("Collection name cannot be null or empty.", nameof(collectionName));

            var filter = new BsonDocument("name", collectionName);
            var collections = await _database.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });

            if (!await collections.AnyAsync())
            {
                await _database.CreateCollectionAsync(collectionName);
            }
        }

        /// <summary>
        /// Gets the names of all collections in the database.
        /// </summary>
        /// <returns>A list of collection names.</returns>
        public async Task<List<string>> GetCollectionNamesAsync()
        {
            var collections = await _database.ListCollectionsAsync();
            var collectionNames = new List<string>();

            await collections.ForEachAsync(collection =>
            {
                collectionNames.Add(collection["name"].AsString);
            });

            return collectionNames;
        }

        /// <summary>
        /// Disposes the MongoDB data store.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the MongoDB data store.
        /// </summary>
        /// <param name="disposing">Whether to dispose managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    // Note: MongoClient doesn't implement IDisposable, so no need to dispose it
                }

                _disposed = true;
            }
        }
    }
}
