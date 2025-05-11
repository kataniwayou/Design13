using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Recovery;

/// <summary>
/// Implements the circuit breaker pattern for error handling.
/// </summary>
public class CircuitBreakerImplementation
{
    private readonly ILogger<CircuitBreakerImplementation> _logger;
    private readonly Dictionary<string, CircuitBreakerState> _circuitStates = new();
    private readonly int _failureThreshold;
    private readonly TimeSpan _resetTimeout;

    /// <summary>
    /// Initializes a new instance of the <see cref="CircuitBreakerImplementation"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="failureThreshold">The number of consecutive failures before the circuit breaker opens.</param>
    /// <param name="resetTimeoutSeconds">The time in seconds after which to try resetting the circuit breaker.</param>
    public CircuitBreakerImplementation(
        ILogger<CircuitBreakerImplementation> logger,
        int failureThreshold = 5,
        int resetTimeoutSeconds = 60)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _failureThreshold = failureThreshold > 0 ? failureThreshold : throw new ArgumentOutOfRangeException(nameof(failureThreshold));
        _resetTimeout = TimeSpan.FromSeconds(resetTimeoutSeconds > 0 ? resetTimeoutSeconds : throw new ArgumentOutOfRangeException(nameof(resetTimeoutSeconds)));
    }

    /// <summary>
    /// Determines whether the circuit breaker is open for the specified execution.
    /// </summary>
    /// <param name="executionId">The execution ID.</param>
    /// <returns><c>true</c> if the circuit breaker is open; otherwise, <c>false</c>.</returns>
    public bool IsOpen(string executionId)
    {
        if (string.IsNullOrEmpty(executionId)) throw new ArgumentNullException(nameof(executionId));

        lock (_circuitStates)
        {
            if (!_circuitStates.TryGetValue(executionId, out var state))
            {
                return false;
            }

            if (state.State == CircuitState.Open)
            {
                // Check if it's time to try half-open state
                if (DateTime.UtcNow - state.LastStateChange >= _resetTimeout)
                {
                    _logger.LogInformation("Circuit breaker for execution {ExecutionId} transitioning from Open to Half-Open after timeout",
                        executionId);
                    
                    state.State = CircuitState.HalfOpen;
                    state.LastStateChange = DateTime.UtcNow;
                    return false;
                }
                
                return true;
            }
            
            return false;
        }
    }

    /// <summary>
    /// Records a successful operation for the specified execution.
    /// </summary>
    /// <param name="executionId">The execution ID.</param>
    public void RecordSuccess(string executionId)
    {
        if (string.IsNullOrEmpty(executionId)) throw new ArgumentNullException(nameof(executionId));

        lock (_circuitStates)
        {
            if (!_circuitStates.TryGetValue(executionId, out var state))
            {
                return;
            }

            if (state.State == CircuitState.HalfOpen)
            {
                _logger.LogInformation("Circuit breaker for execution {ExecutionId} transitioning from Half-Open to Closed after successful operation",
                    executionId);
                
                state.State = CircuitState.Closed;
                state.FailureCount = 0;
                state.LastStateChange = DateTime.UtcNow;
            }
            else if (state.State == CircuitState.Closed)
            {
                state.FailureCount = 0;
            }
        }
    }

    /// <summary>
    /// Records a failed operation for the specified execution.
    /// </summary>
    /// <param name="executionId">The execution ID.</param>
    public void RecordFailure(string executionId)
    {
        if (string.IsNullOrEmpty(executionId)) throw new ArgumentNullException(nameof(executionId));

        lock (_circuitStates)
        {
            if (!_circuitStates.TryGetValue(executionId, out var state))
            {
                state = new CircuitBreakerState
                {
                    State = CircuitState.Closed,
                    FailureCount = 0,
                    LastStateChange = DateTime.UtcNow
                };
                
                _circuitStates[executionId] = state;
            }

            if (state.State == CircuitState.HalfOpen)
            {
                _logger.LogWarning("Circuit breaker for execution {ExecutionId} transitioning from Half-Open to Open after failed operation",
                    executionId);
                
                state.State = CircuitState.Open;
                state.LastStateChange = DateTime.UtcNow;
            }
            else if (state.State == CircuitState.Closed)
            {
                state.FailureCount++;
                
                if (state.FailureCount >= _failureThreshold)
                {
                    _logger.LogWarning("Circuit breaker for execution {ExecutionId} transitioning from Closed to Open after {FailureCount} consecutive failures",
                        executionId, state.FailureCount);
                    
                    state.State = CircuitState.Open;
                    state.LastStateChange = DateTime.UtcNow;
                }
            }
        }
    }

    /// <summary>
    /// Resets the circuit breaker for the specified execution.
    /// </summary>
    /// <param name="executionId">The execution ID.</param>
    public void Reset(string executionId)
    {
        if (string.IsNullOrEmpty(executionId)) throw new ArgumentNullException(nameof(executionId));

        lock (_circuitStates)
        {
            if (_circuitStates.TryGetValue(executionId, out var state))
            {
                _logger.LogInformation("Resetting circuit breaker for execution {ExecutionId} from {CurrentState} to Closed",
                    executionId, state.State);
                
                state.State = CircuitState.Closed;
                state.FailureCount = 0;
                state.LastStateChange = DateTime.UtcNow;
            }
        }
    }

    /// <summary>
    /// Gets the current state of the circuit breaker for the specified execution.
    /// </summary>
    /// <param name="executionId">The execution ID.</param>
    /// <returns>The current state of the circuit breaker.</returns>
    public CircuitState GetState(string executionId)
    {
        if (string.IsNullOrEmpty(executionId)) throw new ArgumentNullException(nameof(executionId));

        lock (_circuitStates)
        {
            return _circuitStates.TryGetValue(executionId, out var state) ? state.State : CircuitState.Closed;
        }
    }

    /// <summary>
    /// Gets the failure count for the specified execution.
    /// </summary>
    /// <param name="executionId">The execution ID.</param>
    /// <returns>The current failure count.</returns>
    public int GetFailureCount(string executionId)
    {
        if (string.IsNullOrEmpty(executionId)) throw new ArgumentNullException(nameof(executionId));

        lock (_circuitStates)
        {
            return _circuitStates.TryGetValue(executionId, out var state) ? state.FailureCount : 0;
        }
    }

    /// <summary>
    /// Represents the state of a circuit breaker.
    /// </summary>
    private class CircuitBreakerState
    {
        /// <summary>
        /// Gets or sets the current state of the circuit breaker.
        /// </summary>
        public CircuitState State { get; set; }
        
        /// <summary>
        /// Gets or sets the number of consecutive failures.
        /// </summary>
        public int FailureCount { get; set; }
        
        /// <summary>
        /// Gets or sets the timestamp of the last state change.
        /// </summary>
        public DateTime LastStateChange { get; set; }
    }
}

/// <summary>
/// Represents the state of a circuit breaker.
/// </summary>
public enum CircuitState
{
    /// <summary>
    /// The circuit is closed and operations are allowed.
    /// </summary>
    Closed,
    
    /// <summary>
    /// The circuit is open and operations are not allowed.
    /// </summary>
    Open,
    
    /// <summary>
    /// The circuit is half-open and a single operation is allowed to test if the system has recovered.
    /// </summary>
    HalfOpen
}
