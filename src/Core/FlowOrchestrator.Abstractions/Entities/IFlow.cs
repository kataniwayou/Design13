using System.Collections.Generic;

namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Interface for a flow entity that defines the structure and behavior of a data processing workflow.
/// </summary>
public interface IFlow : IVersionedEntity
{
    /// <summary>
    /// Gets the list of steps in the flow.
    /// </summary>
    IReadOnlyList<IFlowStep> Steps { get; }
    
    /// <summary>
    /// Gets the list of branches in the flow.
    /// </summary>
    IReadOnlyList<IFlowBranch> Branches { get; }
    
    /// <summary>
    /// Gets the validation rules for the flow.
    /// </summary>
    IReadOnlyList<IValidationRule> ValidationRules { get; }
    
    /// <summary>
    /// Gets the error handling configuration for the flow.
    /// </summary>
    IErrorHandlingConfig ErrorHandlingConfig { get; }
    
    /// <summary>
    /// Gets the merge strategy configuration for branch convergence.
    /// </summary>
    IMergeStrategyConfig MergeStrategyConfig { get; }
    
    /// <summary>
    /// Validates the flow structure and configuration.
    /// </summary>
    /// <returns>True if the flow is valid, false otherwise.</returns>
    bool Validate();
}
