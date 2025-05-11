using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents a processing chain that defines a sequence of processing steps.
/// </summary>
public class ProcessingChainEntity : VersionedEntity
{
    /// <summary>
    /// Gets or sets the list of steps in the processing chain.
    /// </summary>
    public IReadOnlyList<ProcessingStepEntity> Steps => _steps.AsReadOnly();
    private readonly List<ProcessingStepEntity> _steps = new();
    
    /// <summary>
    /// Gets or sets the input schema for the processing chain.
    /// </summary>
    public string InputSchema { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the output schema for the processing chain.
    /// </summary>
    public string OutputSchema { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the validation rules for the processing chain.
    /// </summary>
    public IReadOnlyList<IValidationRule> ValidationRules => _validationRules.AsReadOnly();
    private readonly List<IValidationRule> _validationRules = new();
    
    /// <summary>
    /// Gets or sets the error handling configuration for the processing chain.
    /// </summary>
    public IErrorHandlingConfig ErrorHandlingConfig { get; set; } = new ErrorHandlingConfig();
    
    /// <summary>
    /// Creates a new instance of the ProcessingChainEntity class.
    /// </summary>
    public ProcessingChainEntity()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ProcessingChainEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the processing chain.</param>
    /// <param name="name">The name of the processing chain.</param>
    /// <param name="description">The description of the processing chain.</param>
    public ProcessingChainEntity(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ProcessingChainEntity class with the specified ID, name, description, and version information.
    /// </summary>
    /// <param name="id">The unique identifier for the processing chain.</param>
    /// <param name="name">The name of the processing chain.</param>
    /// <param name="description">The description of the processing chain.</param>
    /// <param name="version">The semantic version number.</param>
    /// <param name="versionDescription">The human-readable description of this version.</param>
    /// <param name="previousVersionId">The reference to the previous version (if applicable).</param>
    /// <param name="versionStatus">The status of this version.</param>
    public ProcessingChainEntity(
        string id,
        string name,
        string description,
        string version,
        string versionDescription,
        string? previousVersionId,
        VersionStatus versionStatus)
        : base(id, name, description, version, versionDescription, previousVersionId, versionStatus)
    {
    }
    
    /// <summary>
    /// Adds a step to the processing chain.
    /// </summary>
    /// <param name="step">The step to add.</param>
    public void AddStep(ProcessingStepEntity step)
    {
        _steps.Add(step);
    }
    
    /// <summary>
    /// Adds a validation rule to the processing chain.
    /// </summary>
    /// <param name="validationRule">The validation rule to add.</param>
    public void AddValidationRule(IValidationRule validationRule)
    {
        _validationRules.Add(validationRule);
    }
    
    /// <summary>
    /// Validates the processing chain structure and configuration.
    /// </summary>
    /// <returns>True if the processing chain is valid, false otherwise.</returns>
    public bool Validate()
    {
        // Validate that there is at least one step
        if (!Steps.Any())
        {
            return false;
        }
        
        // Validate that the steps are in the correct order
        for (int i = 0; i < Steps.Count; i++)
        {
            if (Steps[i].StepOrder != i)
            {
                return false;
            }
        }
        
        return true;
    }
}
