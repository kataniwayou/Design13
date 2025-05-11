namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Base interface for all entities in the FlowOrchestrator system.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets the unique identifier for the entity.
    /// </summary>
    string Id { get; }
    
    /// <summary>
    /// Gets the name of the entity.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gets the description of the entity.
    /// </summary>
    string Description { get; }
}
