using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Base implementation of the IEntity interface.
/// </summary>
public abstract class BaseEntity : IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public string Id { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the name of the entity.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the description of the entity.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Creates a new instance of the BaseEntity class.
    /// </summary>
    protected BaseEntity()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the BaseEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    protected BaseEntity(string id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}
