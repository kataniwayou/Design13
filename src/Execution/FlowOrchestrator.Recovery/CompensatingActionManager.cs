using FlowOrchestrator.Common.Errors;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Recovery;

/// <summary>
/// Manages compensating actions for the recovery framework.
/// </summary>
public class CompensatingActionManager
{
    private readonly ILogger<CompensatingActionManager> _logger;
    private readonly Dictionary<string, List<CompensatingAction>> _registeredActions = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CompensatingActionManager"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public CompensatingActionManager(ILogger<CompensatingActionManager> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Registers a compensating action for a specific execution.
    /// </summary>
    /// <param name="executionId">The execution ID.</param>
    /// <param name="action">The compensating action to register.</param>
    public void RegisterCompensatingAction(string executionId, CompensatingAction action)
    {
        if (string.IsNullOrEmpty(executionId)) throw new ArgumentNullException(nameof(executionId));
        if (action == null) throw new ArgumentNullException(nameof(action));

        lock (_registeredActions)
        {
            if (!_registeredActions.TryGetValue(executionId, out var actions))
            {
                actions = new List<CompensatingAction>();
                _registeredActions[executionId] = actions;
            }

            actions.Add(action);

            _logger.LogInformation("Registered compensating action {ActionId} for execution {ExecutionId}",
                action.ActionId, executionId);
        }
    }

    /// <summary>
    /// Applies all registered compensating actions for a specific execution.
    /// </summary>
    /// <param name="errorContext">The error context.</param>
    /// <param name="executionContext">The execution context.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task ApplyCompensatingActionsAsync(
        ErrorContext errorContext,
        object executionContext,
        CancellationToken cancellationToken)
    {
        if (errorContext == null) throw new ArgumentNullException(nameof(errorContext));
        if (executionContext == null) throw new ArgumentNullException(nameof(executionContext));

        var executionId = executionContext.ToString() ?? "unknown";
        List<CompensatingAction>? actions;

        lock (_registeredActions)
        {
            if (!_registeredActions.TryGetValue(executionId, out actions) || actions.Count == 0)
            {
                _logger.LogInformation("No compensating actions registered for execution {ExecutionId}", executionId);
                return;
            }

            // Get a copy of the actions and remove them from the registry
            actions = new List<CompensatingAction>(actions);
            _registeredActions.Remove(executionId);
        }

        _logger.LogInformation("Applying {ActionCount} compensating actions for execution {ExecutionId}",
            actions.Count, executionId);

        // Apply compensating actions in reverse order (LIFO)
        for (int i = actions.Count - 1; i >= 0; i--)
        {
            var action = actions[i];

            try
            {
                _logger.LogInformation("Applying compensating action {ActionId} for execution {ExecutionId}",
                    action.ActionId, executionId);

                await action.ExecuteAsync(errorContext, executionContext, cancellationToken);

                _logger.LogInformation("Successfully applied compensating action {ActionId} for execution {ExecutionId}",
                    action.ActionId, executionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error applying compensating action {ActionId} for execution {ExecutionId}",
                    action.ActionId, executionId);

                // Continue with other compensating actions even if one fails
            }
        }
    }

    /// <summary>
    /// Clears all registered compensating actions for a specific execution.
    /// </summary>
    /// <param name="executionId">The execution ID.</param>
    public void ClearCompensatingActions(string executionId)
    {
        if (string.IsNullOrEmpty(executionId)) throw new ArgumentNullException(nameof(executionId));

        lock (_registeredActions)
        {
            if (_registeredActions.Remove(executionId))
            {
                _logger.LogInformation("Cleared all compensating actions for execution {ExecutionId}", executionId);
            }
        }
    }
}

/// <summary>
/// Represents a compensating action that can be executed to undo a previous operation.
/// </summary>
public class CompensatingAction
{
    private readonly Func<ErrorContext, object, CancellationToken, Task> _action;

    /// <summary>
    /// Gets the unique identifier for this compensating action.
    /// </summary>
    public string ActionId { get; }

    /// <summary>
    /// Gets the description of this compensating action.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CompensatingAction"/> class.
    /// </summary>
    /// <param name="actionId">The unique identifier for this compensating action.</param>
    /// <param name="description">The description of this compensating action.</param>
    /// <param name="action">The action to execute.</param>
    public CompensatingAction(
        string actionId,
        string description,
        Func<ErrorContext, object, CancellationToken, Task> action)
    {
        ActionId = actionId ?? throw new ArgumentNullException(nameof(actionId));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        _action = action ?? throw new ArgumentNullException(nameof(action));
    }

    /// <summary>
    /// Executes the compensating action.
    /// </summary>
    /// <param name="errorContext">The error context.</param>
    /// <param name="executionContext">The execution context.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task ExecuteAsync(ErrorContext errorContext, object executionContext, CancellationToken cancellationToken)
    {
        return _action(errorContext, executionContext, cancellationToken);
    }
}
