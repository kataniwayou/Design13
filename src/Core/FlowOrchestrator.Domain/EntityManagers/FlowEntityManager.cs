using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowOrchestrator.Abstractions.Entities;
using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common;
using FlowOrchestrator.Domain.Utilities;

namespace FlowOrchestrator.Domain.EntityManagers;

/// <summary>
/// Manager service for flow entities.
/// </summary>
public class FlowEntityManager : AbstractManagerService<AbstractFlowEntity, string>
{
    private readonly Dictionary<string, AbstractFlowEntity> _flows = new();
    private readonly object _lock = new();

    /// <summary>
    /// Gets the manager type that this manager implements.
    /// </summary>
    public override string ManagerType => "FlowEntityManager";

    /// <summary>
    /// Creates a new instance of the FlowEntityManager class.
    /// </summary>
    /// <param name="id">The unique identifier for the service.</param>
    /// <param name="name">The name of the service.</param>
    /// <param name="description">The description of the service.</param>
    /// <param name="version">The version of the service.</param>
    public FlowEntityManager(string id, string name, string description, string version)
        : base(id, name, description, version)
    {
    }

    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    public override Task<IEnumerable<AbstractFlowEntity>> GetAllEntitiesAsync()
    {
        lock (_lock)
        {
            return Task.FromResult<IEnumerable<AbstractFlowEntity>>(_flows.Values.ToList());
        }
    }

    /// <summary>
    /// Gets an entity by ID.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>The entity, or default if not found.</returns>
    public override Task<AbstractFlowEntity?> GetEntityAsync(string id)
    {
        lock (_lock)
        {
            _flows.TryGetValue(id, out var entity);
            return Task.FromResult(entity);
        }
    }

    /// <summary>
    /// Gets the status of an entity.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>The entity status.</returns>
    public override Task<ResourceStatus> GetEntityStatusAsync(string id)
    {
        lock (_lock)
        {
            if (_flows.TryGetValue(id, out var entity))
            {
                var status = new ResourceStatus
                {
                    ResourceId = id,
                    Status = ServiceStatus.Running,
                    HealthStatus = HealthStatus.Healthy,
                    LastUpdated = DateTime.UtcNow
                };
                return Task.FromResult(status);
            }

            return Task.FromResult(new ResourceStatus
            {
                ResourceId = id,
                Status = ServiceStatus.Failed,
                HealthStatus = HealthStatus.Unknown,
                LastUpdated = DateTime.UtcNow
            });
        }
    }

    /// <summary>
    /// Registers a new entity.
    /// </summary>
    /// <param name="entity">The entity to register.</param>
    /// <returns>The registration result.</returns>
    public override Task<RegistrationResult> RegisterAsync(AbstractFlowEntity entity)
    {
        if (entity == null)
        {
            return Task.FromResult(RegistrationResult.Failure("Entity cannot be null"));
        }

        var validationResult = entity.Validate().ToAbstractionsValidationResult();

        if (!validationResult.IsValid)
        {
            return Task.FromResult(RegistrationResult.ValidationFailure(validationResult));
        }

        lock (_lock)
        {
            if (_flows.ContainsKey(entity.Id))
            {
                return Task.FromResult(RegistrationResult.Failure($"Entity with ID {entity.Id} already exists"));
            }

            _flows[entity.Id] = entity;
            return Task.FromResult(RegistrationResult.Success(entity.Id, validationResult));
        }
    }

    /// <summary>
    /// Unregisters an entity.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <returns>True if the entity was unregistered, false otherwise.</returns>
    public override Task<bool> UnregisterEntityAsync(string id)
    {
        lock (_lock)
        {
            return Task.FromResult(_flows.Remove(id));
        }
    }

    /// <summary>
    /// Updates the status of an entity.
    /// </summary>
    /// <param name="id">The entity ID.</param>
    /// <param name="status">The new status.</param>
    /// <returns>True if the status was updated, false otherwise.</returns>
    public override Task<bool> UpdateEntityStatusAsync(string id, ResourceStatus status)
    {
        lock (_lock)
        {
            if (_flows.TryGetValue(id, out _))
            {
                // In a real implementation, we would update the status of the entity
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }

    /// <summary>
    /// Validates an entity.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <returns>The validation result.</returns>
    public override Task<Abstractions.Services.ValidationResult> ValidateAsync(AbstractFlowEntity entity)
    {
        if (entity == null)
        {
            var result = new Abstractions.Services.ValidationResult();
            result.AddError("Entity cannot be null");
            return Task.FromResult(result);
        }

        return Task.FromResult(entity.Validate().ToAbstractionsValidationResult());
    }
}
