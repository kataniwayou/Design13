using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlowOrchestrator.Abstractions;
using FlowOrchestrator.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

using DomainVersionInfo = FlowOrchestrator.Domain.VersionInfo;

namespace FlowOrchestrator.Data.MongoDB
{
    /// <summary>
    /// Repository for version information stored in MongoDB.
    /// </summary>
    public class VersionRepository
    {
        private readonly MongoDbDataStore _dataStore;
        private readonly IMongoCollection<BsonDocument> _collection;
        private readonly string _collectionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionRepository"/> class.
        /// </summary>
        /// <param name="dataStore">The MongoDB data store.</param>
        public VersionRepository(MongoDbDataStore dataStore)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            _collectionName = "Versions";
            _collection = _dataStore.GetCollection<BsonDocument>(_collectionName);
        }

        /// <summary>
        /// Saves version information for an entity.
        /// </summary>
        /// <param name="entityId">The ID of the entity.</param>
        /// <param name="entityType">The type of the entity.</param>
        /// <param name="versionInfo">The version information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task SaveVersionInfoAsync(string entityId, string entityType, DomainVersionInfo versionInfo)
        {
            if (string.IsNullOrEmpty(entityId))
                throw new ArgumentException("Entity ID cannot be null or empty.", nameof(entityId));

            if (string.IsNullOrEmpty(entityType))
                throw new ArgumentException("Entity type cannot be null or empty.", nameof(entityType));

            if (versionInfo == null)
                throw new ArgumentNullException(nameof(versionInfo));

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("EntityId", entityId),
                Builders<BsonDocument>.Filter.Eq("EntityType", entityType),
                Builders<BsonDocument>.Filter.Eq("Version", versionInfo.Version)
            );

            var existingVersion = await _collection.Find(filter).FirstOrDefaultAsync();

            if (existingVersion != null)
            {
                // Update existing version
                var update = Builders<BsonDocument>.Update
                    .Set("Version", versionInfo.Version)
                    .Set("Build", versionInfo.Build)
                    .Set("ReleaseDate", versionInfo.ReleaseDate)
                    .Set("LastUpdated", DateTime.UtcNow);

                await _collection.UpdateOneAsync(filter, update);
            }
            else
            {
                // Create new version
                var document = new BsonDocument
                {
                    { "EntityId", entityId },
                    { "EntityType", entityType },
                    { "Version", versionInfo.Version },
                    { "Build", versionInfo.Build },
                    { "ReleaseDate", versionInfo.ReleaseDate },
                    { "Description", versionInfo.Description },
                    { "ReleaseNotes", versionInfo.ReleaseNotes },
                    { "Created", DateTime.UtcNow },
                    { "LastUpdated", DateTime.UtcNow }
                };

                await _collection.InsertOneAsync(document);
            }
        }

        /// <summary>
        /// Gets version information for an entity.
        /// </summary>
        /// <param name="entityId">The ID of the entity.</param>
        /// <param name="entityType">The type of the entity.</param>
        /// <param name="version">The specific version to get, or null to get the latest version.</param>
        /// <returns>The version information, or null if not found.</returns>
        public async Task<DomainVersionInfo> GetVersionInfoAsync(string entityId, string entityType, string version = null)
        {
            if (string.IsNullOrEmpty(entityId))
                throw new ArgumentException("Entity ID cannot be null or empty.", nameof(entityId));

            if (string.IsNullOrEmpty(entityType))
                throw new ArgumentException("Entity type cannot be null or empty.", nameof(entityType));

            FilterDefinition<BsonDocument> filter;

            if (string.IsNullOrEmpty(version))
            {
                // Get latest version
                filter = Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Eq("EntityId", entityId),
                    Builders<BsonDocument>.Filter.Eq("EntityType", entityType)
                );

                var versions = await _collection.Find(filter)
                    .Sort(Builders<BsonDocument>.Sort.Descending("Major")
                        .Descending("Minor")
                        .Descending("Patch")
                        .Descending("Build"))
                    .Limit(1)
                    .ToListAsync();

                if (versions.Count == 0)
                    return null;

                var latestVersion = versions[0];

                return new VersionInfo
                {
                    Version = latestVersion["Version"].AsString,
                    Build = latestVersion["Build"].AsInt32,
                    ReleaseDate = latestVersion["ReleaseDate"].ToUniversalTime(),
                    Description = latestVersion.Contains("Description") ? latestVersion["Description"].AsString : null,
                    ReleaseNotes = latestVersion.Contains("ReleaseNotes") ? latestVersion["ReleaseNotes"].AsString : null
                };
            }
            else
            {
                // Get specific version
                filter = Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Eq("EntityId", entityId),
                    Builders<BsonDocument>.Filter.Eq("EntityType", entityType),
                    Builders<BsonDocument>.Filter.Eq("Version", version)
                );

                var versionDoc = await _collection.Find(filter).FirstOrDefaultAsync();

                if (versionDoc == null)
                    return null;

                return new VersionInfo
                {
                    Version = versionDoc["Version"].AsString,
                    Build = versionDoc["Build"].AsInt32,
                    ReleaseDate = versionDoc["ReleaseDate"].ToUniversalTime(),
                    Description = versionDoc.Contains("Description") ? versionDoc["Description"].AsString : null,
                    ReleaseNotes = versionDoc.Contains("ReleaseNotes") ? versionDoc["ReleaseNotes"].AsString : null
                };
            }
        }

        /// <summary>
        /// Gets all versions for an entity.
        /// </summary>
        /// <param name="entityId">The ID of the entity.</param>
        /// <param name="entityType">The type of the entity.</param>
        /// <returns>A list of all versions for the entity.</returns>
        public async Task<List<DomainVersionInfo>> GetAllVersionsAsync(string entityId, string entityType)
        {
            if (string.IsNullOrEmpty(entityId))
                throw new ArgumentException("Entity ID cannot be null or empty.", nameof(entityId));

            if (string.IsNullOrEmpty(entityType))
                throw new ArgumentException("Entity type cannot be null or empty.", nameof(entityType));

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("EntityId", entityId),
                Builders<BsonDocument>.Filter.Eq("EntityType", entityType)
            );

            var versions = await _collection.Find(filter)
                .Sort(Builders<BsonDocument>.Sort.Descending("Major")
                    .Descending("Minor")
                    .Descending("Patch")
                    .Descending("Build"))
                .ToListAsync();

            var result = new List<DomainVersionInfo>();

            foreach (var version in versions)
            {
                result.Add(new DomainVersionInfo
                {
                    Version = version["Version"].AsString,
                    Build = version["Build"].AsInt32,
                    ReleaseDate = version["ReleaseDate"].ToUniversalTime(),
                    Description = version.Contains("Description") ? version["Description"].AsString : null,
                    ReleaseNotes = version.Contains("ReleaseNotes") ? version["ReleaseNotes"].AsString : null
                });
            }

            return result;
        }

        /// <summary>
        /// Deletes a specific version of an entity.
        /// </summary>
        /// <param name="entityId">The ID of the entity.</param>
        /// <param name="entityType">The type of the entity.</param>
        /// <param name="version">The version to delete.</param>
        /// <returns>True if the version was deleted, false otherwise.</returns>
        public async Task<bool> DeleteVersionAsync(string entityId, string entityType, string version)
        {
            if (string.IsNullOrEmpty(entityId))
                throw new ArgumentException("Entity ID cannot be null or empty.", nameof(entityId));

            if (string.IsNullOrEmpty(entityType))
                throw new ArgumentException("Entity type cannot be null or empty.", nameof(entityType));

            if (string.IsNullOrEmpty(version))
                throw new ArgumentException("Version cannot be null or empty.", nameof(version));

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Eq("EntityId", entityId),
                Builders<BsonDocument>.Filter.Eq("EntityType", entityType),
                Builders<BsonDocument>.Filter.Eq("Version", version)
            );

            var result = await _collection.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }
    }
}
