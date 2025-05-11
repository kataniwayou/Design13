using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowOrchestrator.Abstractions.Entities;
using FlowOrchestrator.Common;

namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Interface for entity manager services.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by this service.</typeparam>
/// <typeparam name="TEntityId">The type of entity ID.</typeparam>
public interface IEntityManagerService<TEntity, TEntityId> : IManagerService
    where TEntity : IEntity
{
    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    Task<IEnumerable<TEntity>> GetAllEntitiesAsync();

    /// <summary>
    /// Gets an entity by ID.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>The entity, or default if not found.</returns>
    Task<TEntity?> GetEntityAsync(TEntityId id);

    /// <summary>
    /// Gets the status of an entity.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>The entity status.</returns>
    Task<ResourceStatus> GetEntityStatusAsync(TEntityId id);

    /// <summary>
    /// Registers a new entity.
    /// </summary>
    /// <param name="entity">The entity to register.</param>
    /// <returns>The registration result.</returns>
    Task<RegistrationResult> RegisterAsync(TEntity entity);

    /// <summary>
    /// Unregisters an entity.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>True if the entity was unregistered, false otherwise.</returns>
    Task<bool> UnregisterEntityAsync(TEntityId id);

    /// <summary>
    /// Updates the status of an entity.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <param name="status">The new status.</param>
    /// <returns>True if the status was updated, false otherwise.</returns>
    Task<bool> UpdateEntityStatusAsync(TEntityId id, ResourceStatus status);

    /// <summary>
    /// Validates an entity.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <returns>The validation result.</returns>
    Task<ValidationResult> ValidateAsync(TEntity entity);
}

/// <summary>
/// Abstract base class for manager services.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by this service.</typeparam>
/// <typeparam name="TEntityId">The type of entity ID.</typeparam>
public abstract class AbstractManagerService<TEntity, TEntityId> : AbstractServiceBase, IEntityManagerService<TEntity, TEntityId>
    where TEntity : IEntity
{
    /// <summary>
    /// Gets the manager type that this manager implements.
    /// </summary>
    public abstract string ManagerType { get; }

    /// <summary>
    /// Creates a new instance of the AbstractManagerService class.
    /// </summary>
    /// <param name="id">The unique identifier for the service.</param>
    /// <param name="name">The name of the service.</param>
    /// <param name="description">The description of the service.</param>
    /// <param name="version">The version of the service.</param>
    protected AbstractManagerService(string id, string name, string description, string version)
        : base(id, name, description, version)
    {
    }

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public abstract Task<IEnumerable<TEntity>> GetAllEntitiesAsync();

    /// <summary>
    /// Gets an entity by ID.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>The entity, or default if not found.</returns>
    public abstract Task<TEntity?> GetEntityAsync(TEntityId id);

    /// <summary>
    /// Gets the status of an entity.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>The entity status.</returns>
    public abstract Task<ResourceStatus> GetEntityStatusAsync(TEntityId id);

    /// <summary>
    /// Registers a new entity.
    /// </summary>
    /// <param name="entity">The entity to register.</param>
    /// <returns>The registration result.</returns>
    public abstract Task<RegistrationResult> RegisterAsync(TEntity entity);

    /// <summary>
    /// Unregisters an entity.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>True if the entity was unregistered, false otherwise.</returns>
    public abstract Task<bool> UnregisterEntityAsync(TEntityId id);

    /// <summary>
    /// Updates the status of an entity.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <param name="status">The new status.</param>
    /// <returns>True if the status was updated, false otherwise.</returns>
    public abstract Task<bool> UpdateEntityStatusAsync(TEntityId id, ResourceStatus status);

    /// <summary>
    /// Validates an entity.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <returns>The validation result.</returns>
    public abstract Task<ValidationResult> ValidateAsync(TEntity entity);

    /// <summary>
    /// Gets the list of managed services or resources.
    /// </summary>
    /// <returns>The list of managed services or resources.</returns>
    public virtual async Task<IEnumerable<string>> GetManagedResourcesAsync()
    {
        var entities = await GetAllEntitiesAsync();
        return entities.Select(e => e.Id.ToString());
    }

    /// <summary>
    /// Gets the status of a managed service or resource.
    /// </summary>
    /// <param name="resourceId">The ID of the managed service or resource.</param>
    /// <returns>The status of the managed service or resource.</returns>
    public virtual async Task<ResourceStatus> GetResourceStatusAsync(string resourceId)
    {
        if (resourceId == null)
        {
            throw new ArgumentNullException(nameof(resourceId));
        }

        try
        {
            var entityId = (TEntityId)Convert.ChangeType(resourceId, typeof(TEntityId));
            return await GetEntityStatusAsync(entityId);
        }
        catch
        {
            return new ResourceStatus
            {
                ResourceId = resourceId,
                Status = ServiceStatus.NotInitialized,
                HealthStatus = HealthStatus.Unknown,
                LastUpdated = DateTime.UtcNow
            };
        }
    }

    /// <summary>
    /// Starts a managed service or resource.
    /// </summary>
    /// <param name="resourceId">The ID of the managed service or resource.</param>
    /// <returns>True if the service or resource was started successfully, false otherwise.</returns>
    public virtual async Task<bool> StartResourceAsync(string resourceId)
    {
        if (resourceId == null)
        {
            throw new ArgumentNullException(nameof(resourceId));
        }

        try
        {
            var entityId = (TEntityId)Convert.ChangeType(resourceId, typeof(TEntityId));
            var status = new ResourceStatus
            {
                ResourceId = resourceId,
                Status = ServiceStatus.Running,
                HealthStatus = HealthStatus.Healthy,
                LastUpdated = DateTime.UtcNow,
                StartTime = DateTime.UtcNow
            };
            return await UpdateEntityStatusAsync(entityId, status);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Stops a managed service or resource.
    /// </summary>
    /// <param name="resourceId">The ID of the managed service or resource.</param>
    /// <returns>True if the service or resource was stopped successfully, false otherwise.</returns>
    public virtual async Task<bool> StopResourceAsync(string resourceId)
    {
        if (resourceId == null)
        {
            throw new ArgumentNullException(nameof(resourceId));
        }

        try
        {
            var entityId = (TEntityId)Convert.ChangeType(resourceId, typeof(TEntityId));
            var status = new ResourceStatus
            {
                ResourceId = resourceId,
                Status = ServiceStatus.Stopped,
                HealthStatus = HealthStatus.Unknown,
                LastUpdated = DateTime.UtcNow
            };
            return await UpdateEntityStatusAsync(entityId, status);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Configures a managed service or resource.
    /// </summary>
    /// <param name="resourceId">The ID of the managed service or resource.</param>
    /// <param name="configuration">The configuration for the service or resource.</param>
    /// <returns>True if the service or resource was configured successfully, false otherwise.</returns>
    public virtual Task<bool> ConfigureResourceAsync(string resourceId, string configuration)
    {
        // Default implementation does nothing
        return Task.FromResult(false);
    }

    /// <summary>
    /// Gets the configuration of a managed service or resource.
    /// </summary>
    /// <param name="resourceId">The ID of the managed service or resource.</param>
    /// <returns>The configuration of the managed service or resource.</returns>
    public virtual Task<string> GetResourceConfigurationAsync(string resourceId)
    {
        // Default implementation returns empty string
        return Task.FromResult(string.Empty);
    }
}
