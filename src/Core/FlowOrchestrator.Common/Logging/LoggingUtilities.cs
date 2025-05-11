using System.Text;

namespace FlowOrchestrator.Common.Logging;

/// <summary>
/// Provides logging utilities for the system.
/// </summary>
public static class LoggingUtilities
{
    /// <summary>
    /// Formats a log message with the specified parameters.
    /// </summary>
    /// <param name="message">The log message template.</param>
    /// <param name="parameters">The parameters to use for formatting the message template.</param>
    /// <returns>The formatted log message.</returns>
    public static string FormatLogMessage(string message, params object[] parameters)
    {
        if (string.IsNullOrEmpty(message))
        {
            return string.Empty;
        }
        
        if (parameters == null || parameters.Length == 0)
        {
            return message;
        }
        
        try
        {
            return string.Format(message, parameters);
        }
        catch (Exception)
        {
            return message;
        }
    }
    
    /// <summary>
    /// Creates a structured log entry with the specified properties.
    /// </summary>
    /// <param name="message">The log message.</param>
    /// <param name="properties">The properties to include in the log entry.</param>
    /// <returns>A structured log entry.</returns>
    public static string CreateStructuredLogEntry(string message, Dictionary<string, object> properties)
    {
        if (string.IsNullOrEmpty(message))
        {
            return string.Empty;
        }
        
        if (properties == null || properties.Count == 0)
        {
            return message;
        }
        
        var sb = new StringBuilder();
        sb.Append(message);
        sb.Append(" {");
        
        var first = true;
        foreach (var property in properties)
        {
            if (!first)
            {
                sb.Append(", ");
            }
            
            sb.Append($"\"{property.Key}\": ");
            
            if (property.Value == null)
            {
                sb.Append("null");
            }
            else if (property.Value is string)
            {
                sb.Append($"\"{property.Value}\"");
            }
            else if (property.Value is bool)
            {
                sb.Append(property.Value.ToString()?.ToLowerInvariant());
            }
            else
            {
                sb.Append(property.Value);
            }
            
            first = false;
        }
        
        sb.Append("}");
        
        return sb.ToString();
    }
    
    /// <summary>
    /// Creates a log context with the specified properties.
    /// </summary>
    /// <param name="properties">The properties to include in the log context.</param>
    /// <returns>A log context.</returns>
    public static Dictionary<string, object> CreateLogContext(Dictionary<string, object> properties)
    {
        if (properties == null)
        {
            return new Dictionary<string, object>();
        }
        
        return new Dictionary<string, object>(properties);
    }
    
    /// <summary>
    /// Adds a property to a log context.
    /// </summary>
    /// <param name="context">The log context.</param>
    /// <param name="key">The property key.</param>
    /// <param name="value">The property value.</param>
    /// <returns>The updated log context.</returns>
    public static Dictionary<string, object> AddToLogContext(Dictionary<string, object> context, string key, object value)
    {
        if (context == null)
        {
            context = new Dictionary<string, object>();
        }
        
        if (string.IsNullOrEmpty(key))
        {
            return context;
        }
        
        context[key] = value;
        
        return context;
    }
    
    /// <summary>
    /// Removes a property from a log context.
    /// </summary>
    /// <param name="context">The log context.</param>
    /// <param name="key">The property key.</param>
    /// <returns>The updated log context.</returns>
    public static Dictionary<string, object> RemoveFromLogContext(Dictionary<string, object> context, string key)
    {
        if (context == null)
        {
            return new Dictionary<string, object>();
        }
        
        if (string.IsNullOrEmpty(key))
        {
            return context;
        }
        
        context.Remove(key);
        
        return context;
    }
    
    /// <summary>
    /// Creates a log scope with the specified properties.
    /// </summary>
    /// <param name="properties">The properties to include in the log scope.</param>
    /// <returns>A log scope.</returns>
    public static Dictionary<string, object> CreateLogScope(Dictionary<string, object> properties)
    {
        if (properties == null)
        {
            return new Dictionary<string, object>();
        }
        
        return new Dictionary<string, object>(properties);
    }
    
    /// <summary>
    /// Formats an exception for logging.
    /// </summary>
    /// <param name="exception">The exception to format.</param>
    /// <returns>A formatted exception message.</returns>
    public static string FormatException(Exception exception)
    {
        if (exception == null)
        {
            return string.Empty;
        }
        
        var sb = new StringBuilder();
        sb.AppendLine($"Exception: {exception.GetType().FullName}");
        sb.AppendLine($"Message: {exception.Message}");
        sb.AppendLine($"StackTrace: {exception.StackTrace}");
        
        if (exception.InnerException != null)
        {
            sb.AppendLine("Inner Exception:");
            sb.AppendLine(FormatException(exception.InnerException));
        }
        
        return sb.ToString();
    }
    
    /// <summary>
    /// Creates a log entry for a flow execution.
    /// </summary>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="executionId">The execution ID.</param>
    /// <param name="message">The log message.</param>
    /// <param name="properties">The properties to include in the log entry.</param>
    /// <returns>A log entry for a flow execution.</returns>
    public static string CreateFlowExecutionLogEntry(string flowId, string executionId, string message, Dictionary<string, object>? properties = null)
    {
        if (string.IsNullOrEmpty(flowId))
        {
            throw new ArgumentException("Flow ID cannot be null or empty.", nameof(flowId));
        }
        
        if (string.IsNullOrEmpty(executionId))
        {
            throw new ArgumentException("Execution ID cannot be null or empty.", nameof(executionId));
        }
        
        if (string.IsNullOrEmpty(message))
        {
            return string.Empty;
        }
        
        var context = new Dictionary<string, object>
        {
            ["FlowId"] = flowId,
            ["ExecutionId"] = executionId
        };
        
        if (properties != null)
        {
            foreach (var property in properties)
            {
                context[property.Key] = property.Value;
            }
        }
        
        return CreateStructuredLogEntry(message, context);
    }
    
    /// <summary>
    /// Creates a log entry for a branch execution.
    /// </summary>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="executionId">The execution ID.</param>
    /// <param name="branchPath">The branch path.</param>
    /// <param name="message">The log message.</param>
    /// <param name="properties">The properties to include in the log entry.</param>
    /// <returns>A log entry for a branch execution.</returns>
    public static string CreateBranchExecutionLogEntry(string flowId, string executionId, string branchPath, string message, Dictionary<string, object>? properties = null)
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
        
        if (string.IsNullOrEmpty(message))
        {
            return string.Empty;
        }
        
        var context = new Dictionary<string, object>
        {
            ["FlowId"] = flowId,
            ["ExecutionId"] = executionId,
            ["BranchPath"] = branchPath
        };
        
        if (properties != null)
        {
            foreach (var property in properties)
            {
                context[property.Key] = property.Value;
            }
        }
        
        return CreateStructuredLogEntry(message, context);
    }
    
    /// <summary>
    /// Creates a log entry for a step execution.
    /// </summary>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="executionId">The execution ID.</param>
    /// <param name="branchPath">The branch path.</param>
    /// <param name="stepId">The step ID.</param>
    /// <param name="message">The log message.</param>
    /// <param name="properties">The properties to include in the log entry.</param>
    /// <returns>A log entry for a step execution.</returns>
    public static string CreateStepExecutionLogEntry(string flowId, string executionId, string branchPath, string stepId, string message, Dictionary<string, object>? properties = null)
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
        
        if (string.IsNullOrEmpty(message))
        {
            return string.Empty;
        }
        
        var context = new Dictionary<string, object>
        {
            ["FlowId"] = flowId,
            ["ExecutionId"] = executionId,
            ["BranchPath"] = branchPath,
            ["StepId"] = stepId
        };
        
        if (properties != null)
        {
            foreach (var property in properties)
            {
                context[property.Key] = property.Value;
            }
        }
        
        return CreateStructuredLogEntry(message, context);
    }
}
