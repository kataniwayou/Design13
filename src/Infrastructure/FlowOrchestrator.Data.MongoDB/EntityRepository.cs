using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowOrchestrator.Abstractions.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace FlowOrchestrator.Data.MongoDB
{
    /// <summary>
    /// Repository for entity objects stored in MongoDB.
    /// </summary>
    /// <typeparam name="T">The type of entity stored in the repository.</typeparam>
    public class EntityRepository<T> where T : IEntity
    {
        private readonly MongoDbDataStore _dataStore;
        private readonly IMongoCollection<T> _collection;
        private readonly string _collectionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRepository{T}"/> class.
        /// </summary>
        /// <param name="dataStore">The MongoDB data store.</param>
        /// <param name="collectionName">The name of the collection.</param>
        public EntityRepository(MongoDbDataStore dataStore, string collectionName)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));

            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentException("Collection name cannot be null or empty.", nameof(collectionName));

            _collectionName = collectionName;
            _collection = _dataStore.GetCollection<T>(_collectionName);
        }

        /// <summary>
        /// Gets all entities in the repository.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        /// <summary>
        /// Gets an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to get.</param>
        /// <returns>The entity with the specified ID, or null if not found.</returns>
        public async Task<T> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("ID cannot be null or empty.", nameof(id));

            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Creates a new entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The created entity.</returns>
        public async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _collection.InsertOneAsync(entity);
            return entity;
        }

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="id">The ID of the entity to update.</param>
        /// <param name="entity">The updated entity.</param>
        /// <returns>True if the entity was updated, false otherwise.</returns>
        public async Task<bool> UpdateAsync(string id, T entity)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("ID cannot be null or empty.", nameof(id));

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await _collection.ReplaceOneAsync(filter, entity);

            return result.ModifiedCount > 0;
        }

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>True if the entity was deleted, false otherwise.</returns>
        public async Task<bool> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("ID cannot be null or empty.", nameof(id));

            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await _collection.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }

        /// <summary>
        /// Finds entities that match the specified filter.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>A list of entities that match the filter.</returns>
        public async Task<List<T>> FindAsync(FilterDefinition<T> filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));

            return await _collection.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Counts the number of entities that match the specified filter.
        /// </summary>
        /// <param name="filter">The filter to apply.</param>
        /// <returns>The number of entities that match the filter.</returns>
        public async Task<long> CountAsync(FilterDefinition<T> filter = null)
        {
            filter ??= new BsonDocument();
            return await _collection.CountDocumentsAsync(filter);
        }
    }
}
