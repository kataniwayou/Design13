namespace FlowOrchestrator.Common.Utilities;

/// <summary>
/// Utility class for generating IDs.
/// </summary>
public static class IdGenerator
{
    /// <summary>
    /// Generates a new GUID-based ID.
    /// </summary>
    /// <returns>A new GUID-based ID.</returns>
    public static string GenerateId()
    {
        return Guid.NewGuid().ToString("N");
    }
    
    /// <summary>
    /// Generates a new GUID-based ID with a prefix.
    /// </summary>
    /// <param name="prefix">The prefix to use.</param>
    /// <returns>A new GUID-based ID with the specified prefix.</returns>
    public static string GenerateId(string prefix)
    {
        return $"{prefix}_{Guid.NewGuid():N}";
    }
    
    /// <summary>
    /// Generates a new GUID-based ID for a flow.
    /// </summary>
    /// <returns>A new GUID-based ID for a flow.</returns>
    public static string GenerateFlowId()
    {
        return GenerateId("flow");
    }
    
    /// <summary>
    /// Generates a new GUID-based ID for a flow step.
    /// </summary>
    /// <returns>A new GUID-based ID for a flow step.</returns>
    public static string GenerateFlowStepId()
    {
        return GenerateId("step");
    }
    
    /// <summary>
    /// Generates a new GUID-based ID for a flow branch.
    /// </summary>
    /// <returns>A new GUID-based ID for a flow branch.</returns>
    public static string GenerateFlowBranchId()
    {
        return GenerateId("branch");
    }
    
    /// <summary>
    /// Generates a new GUID-based ID for a validation rule.
    /// </summary>
    /// <returns>A new GUID-based ID for a validation rule.</returns>
    public static string GenerateValidationRuleId()
    {
        return GenerateId("rule");
    }
    
    /// <summary>
    /// Generates a new GUID-based ID for a compensating action.
    /// </summary>
    /// <returns>A new GUID-based ID for a compensating action.</returns>
    public static string GenerateCompensatingActionId()
    {
        return GenerateId("action");
    }
}
