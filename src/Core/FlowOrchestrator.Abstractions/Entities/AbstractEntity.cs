using System;
using FlowOrchestrator.Common;

namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Abstract base class for all entities.
/// </summary>
public abstract class AbstractEntity : IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for this entity.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name of this entity.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of this entity.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the tags associated with this entity.
    /// </summary>
    public string[] Tags { get; set; }

    /// <summary>
    /// Gets or sets the metadata associated with this entity.
    /// </summary>
    public System.Collections.Generic.Dictionary<string, string> Metadata { get; set; }

    /// <summary>
    /// Creates a new instance of the AbstractEntity class.
    /// </summary>
    protected AbstractEntity()
    {
        Id = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
        Tags = Array.Empty<string>();
        Metadata = new System.Collections.Generic.Dictionary<string, string>();
    }

    /// <summary>
    /// Creates a new instance of the AbstractEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    /// <param name="name">The name of the entity.</param>
    /// <param name="description">The description of the entity.</param>
    protected AbstractEntity(string id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        Tags = Array.Empty<string>();
        Metadata = new System.Collections.Generic.Dictionary<string, string>();
    }

    /// <summary>
    /// Gets the entity ID.
    /// </summary>
    /// <returns>The entity ID.</returns>
    public virtual string GetEntityId() => Id;

    /// <summary>
    /// Gets the entity type.
    /// </summary>
    /// <returns>The entity type.</returns>
    public virtual string GetEntityType() => GetType().Name;

    /// <summary>
    /// Validates the entity.
    /// </summary>
    /// <returns>The validation result.</returns>
    public virtual ValidationResult Validate()
    {
        var result = new ValidationResult();

        if (string.IsNullOrEmpty(Id))
        {
            result.AddError("Id is required.");
        }

        if (string.IsNullOrEmpty(Name))
        {
            result.AddError("Name is required.");
        }

        return result;
    }

    /// <summary>
    /// Adds a tag to the entity.
    /// </summary>
    /// <param name="tag">The tag to add.</param>
    public void AddTag(string tag)
    {
        if (string.IsNullOrEmpty(tag))
        {
            return;
        }

        var tagsList = new System.Collections.Generic.List<string>(Tags);
        if (!tagsList.Contains(tag))
        {
            tagsList.Add(tag);
            Tags = tagsList.ToArray();
        }
    }

    /// <summary>
    /// Removes a tag from the entity.
    /// </summary>
    /// <param name="tag">The tag to remove.</param>
    public void RemoveTag(string tag)
    {
        if (string.IsNullOrEmpty(tag))
        {
            return;
        }

        var tagsList = new System.Collections.Generic.List<string>(Tags);
        if (tagsList.Contains(tag))
        {
            tagsList.Remove(tag);
            Tags = tagsList.ToArray();
        }
    }

    /// <summary>
    /// Adds or updates metadata for the entity.
    /// </summary>
    /// <param name="key">The metadata key.</param>
    /// <param name="value">The metadata value.</param>
    public void SetMetadata(string key, string value)
    {
        if (string.IsNullOrEmpty(key))
        {
            return;
        }

        Metadata[key] = value;
    }

    /// <summary>
    /// Gets metadata for the entity.
    /// </summary>
    /// <param name="key">The metadata key.</param>
    /// <returns>The metadata value, or null if not found.</returns>
    public string? GetMetadata(string key)
    {
        if (string.IsNullOrEmpty(key) || !Metadata.ContainsKey(key))
        {
            return null;
        }

        return Metadata[key];
    }

    /// <summary>
    /// Removes metadata from the entity.
    /// </summary>
    /// <param name="key">The metadata key.</param>
    public void RemoveMetadata(string key)
    {
        if (string.IsNullOrEmpty(key) || !Metadata.ContainsKey(key))
        {
            return;
        }

        Metadata.Remove(key);
    }
}
