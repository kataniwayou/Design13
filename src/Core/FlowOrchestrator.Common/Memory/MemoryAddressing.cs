namespace FlowOrchestrator.Common.Memory;

/// <summary>
/// Provides utilities for memory addressing in the system.
/// </summary>
public static class MemoryAddressing
{
    private static readonly Random Random = new Random();
    
    /// <summary>
    /// Generates a new memory address.
    /// </summary>
    /// <returns>A new memory address.</returns>
    public static string GenerateMemoryAddress()
    {
        return $"mem://{Guid.NewGuid():N}";
    }
    
    /// <summary>
    /// Generates a new memory address with the specified prefix.
    /// </summary>
    /// <param name="prefix">The prefix for the memory address.</param>
    /// <returns>A new memory address with the specified prefix.</returns>
    public static string GenerateMemoryAddress(string prefix)
    {
        if (string.IsNullOrEmpty(prefix))
        {
            return GenerateMemoryAddress();
        }
        
        return $"mem://{prefix}/{Guid.NewGuid():N}";
    }
    
    /// <summary>
    /// Generates a new memory address for the specified flow execution.
    /// </summary>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="executionId">The execution ID.</param>
    /// <returns>A new memory address for the specified flow execution.</returns>
    public static string GenerateFlowExecutionMemoryAddress(string flowId, string executionId)
    {
        if (string.IsNullOrEmpty(flowId))
        {
            throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));
        }
        
        if (string.IsNullOrEmpty(executionId))
        {
            throw new ArgumentException("Execution ID cannot be null or empty.", nameof(executionId));
        }
        
        return $"mem://flow/{flowId}/execution/{executionId}/{Guid.NewGuid():N}";
    }
    
    /// <summary>
    /// Generates a new memory address for the specified branch execution.
    /// </summary>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="executionId">The execution ID.</param>
    /// <param name="branchPath">The branch path.</param>
    /// <returns>A new memory address for the specified branch execution.</returns>
    public static string GenerateBranchExecutionMemoryAddress(string flowId, string executionId, string branchPath)
    {
        if (string.IsNullOrEmpty(flowId))
        {
            throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));
        }
        
        if (string.IsNullOrEmpty(executionId))
        {
            throw new ArgumentException("Execution ID cannot be null or empty.", nameof(executionId));
        }
        
        if (string.IsNullOrEmpty(branchPath))
        {
            throw new ArgumentException("Branch path cannot be null or empty.", nameof(branchPath));
        }
        
        return $"mem://flow/{flowId}/execution/{executionId}/branch/{branchPath}/{Guid.NewGuid():N}";
    }
    
    /// <summary>
    /// Generates a new memory address for the specified step execution.
    /// </summary>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="executionId">The execution ID.</param>
    /// <param name="branchPath">The branch path.</param>
    /// <param name="stepId">The step ID.</param>
    /// <returns>A new memory address for the specified step execution.</returns>
    public static string GenerateStepExecutionMemoryAddress(string flowId, string executionId, string branchPath, string stepId)
    {
        if (string.IsNullOrEmpty(flowId))
        {
            throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));
        }
        
        if (string.IsNullOrEmpty(executionId))
        {
            throw new ArgumentException("Execution ID cannot be null or empty.", nameof(executionId));
        }
        
        if (string.IsNullOrEmpty(branchPath))
        {
            throw new ArgumentException("Branch path cannot be null or empty.", nameof(branchPath));
        }
        
        if (string.IsNullOrEmpty(stepId))
        {
            throw new ArgumentException("Step ID cannot be null or empty.", nameof(stepId));
        }
        
        return $"mem://flow/{flowId}/execution/{executionId}/branch/{branchPath}/step/{stepId}/{Guid.NewGuid():N}";
    }
    
    /// <summary>
    /// Generates a new temporary memory address.
    /// </summary>
    /// <returns>A new temporary memory address.</returns>
    public static string GenerateTemporaryMemoryAddress()
    {
        return $"mem://temp/{Guid.NewGuid():N}";
    }
    
    /// <summary>
    /// Generates a new cache memory address.
    /// </summary>
    /// <param name="cacheKey">The cache key.</param>
    /// <returns>A new cache memory address.</returns>
    public static string GenerateCacheMemoryAddress(string cacheKey)
    {
        if (string.IsNullOrEmpty(cacheKey))
        {
            throw new ArgumentException("Cache key cannot be null or empty.", nameof(cacheKey));
        }
        
        return $"mem://cache/{cacheKey}";
    }
    
    /// <summary>
    /// Parses a memory address into its components.
    /// </summary>
    /// <param name="memoryAddress">The memory address to parse.</param>
    /// <returns>A dictionary containing the components of the memory address.</returns>
    public static Dictionary<string, string> ParseMemoryAddress(string memoryAddress)
    {
        if (string.IsNullOrEmpty(memoryAddress))
        {
            throw new ArgumentException("Memory address cannot be null or empty.", nameof(memoryAddress));
        }
        
        if (!memoryAddress.StartsWith("mem://"))
        {
            throw new ArgumentException("Memory address must start with 'mem://'.", nameof(memoryAddress));
        }
        
        var components = new Dictionary<string, string>();
        var path = memoryAddress.Substring(6); // Remove "mem://"
        var parts = path.Split('/');
        
        if (parts.Length == 1)
        {
            components["type"] = "generic";
            components["id"] = parts[0];
            return components;
        }
        
        components["type"] = parts[0];
        
        if (parts[0] == "flow")
        {
            if (parts.Length >= 2)
            {
                components["flowId"] = parts[1];
            }
            
            if (parts.Length >= 4 && parts[2] == "execution")
            {
                components["executionId"] = parts[3];
            }
            
            if (parts.Length >= 6 && parts[4] == "branch")
            {
                components["branchPath"] = parts[5];
            }
            
            if (parts.Length >= 8 && parts[6] == "step")
            {
                components["stepId"] = parts[7];
            }
            
            if (parts.Length >= 9)
            {
                components["id"] = parts[8];
            }
        }
        else if (parts[0] == "temp")
        {
            if (parts.Length >= 2)
            {
                components["id"] = parts[1];
            }
        }
        else if (parts[0] == "cache")
        {
            if (parts.Length >= 2)
            {
                components["cacheKey"] = parts[1];
            }
        }
        else
        {
            if (parts.Length >= 2)
            {
                components["prefix"] = parts[0];
                components["id"] = parts[1];
            }
        }
        
        return components;
    }
    
    /// <summary>
    /// Validates a memory address.
    /// </summary>
    /// <param name="memoryAddress">The memory address to validate.</param>
    /// <returns>True if the memory address is valid, false otherwise.</returns>
    public static bool IsValidMemoryAddress(string memoryAddress)
    {
        if (string.IsNullOrEmpty(memoryAddress))
        {
            return false;
        }
        
        if (!memoryAddress.StartsWith("mem://"))
        {
            return false;
        }
        
        var path = memoryAddress.Substring(6); // Remove "mem://"
        var parts = path.Split('/');
        
        if (parts.Length == 0)
        {
            return false;
        }
        
        return true;
    }
}
