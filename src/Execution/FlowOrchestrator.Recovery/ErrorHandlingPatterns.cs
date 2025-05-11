using FlowOrchestrator.Common.Errors;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Recovery;

/// <summary>
/// Provides common error handling patterns for the recovery framework.
/// </summary>
public class ErrorHandlingPatterns
{
    private readonly ILogger<ErrorHandlingPatterns> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorHandlingPatterns"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public ErrorHandlingPatterns(ILogger<ErrorHandlingPatterns> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Implements the retry pattern with exponential backoff.
    /// </summary>
    /// <typeparam name="T">The return type of the operation.</typeparam>
    /// <param name="operation">The operation to retry.</param>
    /// <param name="maxRetries">The maximum number of retry attempts.</param>
    /// <param name="initialDelay">The initial delay between retries in milliseconds.</param>
    /// <param name="maxDelay">The maximum delay between retries in milliseconds.</param>
    /// <param name="errorContext">The error context.</param>
    /// <param name="executionId">The execution ID.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The result of the operation if successful.</returns>
    public async Task<T> RetryWithExponentialBackoffAsync<T>(
        Func<CancellationToken, Task<T>> operation,
        int maxRetries,
        int initialDelay,
        int maxDelay,
        ErrorContext errorContext,
        string executionId,
        CancellationToken cancellationToken)
    {
        int retryCount = 0;
        int delay = initialDelay;
        Exception? lastException = null;

        while (retryCount < maxRetries)
        {
            try
            {
                if (retryCount > 0)
                {
                    _logger.LogInformation("Retry attempt {RetryCount} for operation in execution {ExecutionId} with error {ErrorId}",
                        retryCount, executionId, errorContext.ErrorId);

                    await Task.Delay(delay, cancellationToken);

                    // Exponential backoff
                    delay = Math.Min(delay * 2, maxDelay);
                }

                return await operation(cancellationToken);
            }
            catch (Exception ex) when (ShouldRetry(ex))
            {
                lastException = ex;
                retryCount++;

                _logger.LogWarning(ex, "Operation failed on retry attempt {RetryCount} for execution {ExecutionId} with error {ErrorId}",
                    retryCount, executionId, errorContext.ErrorId);
            }
        }

        _logger.LogError(lastException, "Operation failed after {MaxRetries} retry attempts for execution {ExecutionId} with error {ErrorId}",
            maxRetries, executionId, errorContext.ErrorId);

        throw new MaxRetryAttemptsExceededException($"Operation failed after {maxRetries} retry attempts", lastException);
    }

    /// <summary>
    /// Implements the circuit breaker pattern.
    /// </summary>
    /// <typeparam name="T">The return type of the operation.</typeparam>
    /// <param name="operation">The operation to execute.</param>
    /// <param name="circuitBreaker">The circuit breaker implementation.</param>
    /// <param name="executionId">The execution ID.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The result of the operation if successful.</returns>
    public async Task<T> WithCircuitBreakerAsync<T>(
        Func<CancellationToken, Task<T>> operation,
        CircuitBreakerImplementation circuitBreaker,
        string executionId,
        CancellationToken cancellationToken)
    {
        if (circuitBreaker.IsOpen(executionId))
        {
            _logger.LogWarning("Circuit breaker is open for execution {ExecutionId}. Operation aborted.",
                executionId);

            throw new CircuitBreakerOpenException($"Circuit breaker is open for execution {executionId}");
        }

        try
        {
            var result = await operation(cancellationToken);
            circuitBreaker.RecordSuccess(executionId);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Operation failed for execution {ExecutionId}. Recording failure in circuit breaker.",
                executionId);

            circuitBreaker.RecordFailure(executionId);
            throw;
        }
    }

    /// <summary>
    /// Implements the timeout pattern.
    /// </summary>
    /// <typeparam name="T">The return type of the operation.</typeparam>
    /// <param name="operation">The operation to execute.</param>
    /// <param name="timeout">The timeout duration.</param>
    /// <param name="executionId">The execution ID.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The result of the operation if successful.</returns>
    public async Task<T> WithTimeoutAsync<T>(
        Func<CancellationToken, Task<T>> operation,
        TimeSpan timeout,
        string executionId,
        CancellationToken cancellationToken)
    {
        using var timeoutCts = new CancellationTokenSource(timeout);
        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

        try
        {
            return await operation(linkedCts.Token);
        }
        catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested)
        {
            _logger.LogWarning("Operation timed out after {Timeout}ms for execution {ExecutionId}",
                timeout.TotalMilliseconds, executionId);

            throw new TimeoutException($"Operation timed out after {timeout.TotalMilliseconds}ms for execution {executionId}");
        }
    }

    /// <summary>
    /// Determines whether an exception should be retried.
    /// </summary>
    /// <param name="exception">The exception to check.</param>
    /// <returns><c>true</c> if the exception is transient and should be retried; otherwise, <c>false</c>.</returns>
    private bool ShouldRetry(Exception exception)
    {
        // Check for transient exceptions that should be retried
        return exception is TimeoutException ||
               exception is System.Net.Http.HttpRequestException ||
               exception is System.IO.IOException ||
               exception is System.Net.Sockets.SocketException ||
               (exception.InnerException != null && ShouldRetry(exception.InnerException));
    }
}

/// <summary>
/// Exception thrown when the maximum number of retry attempts is exceeded.
/// </summary>
public class MaxRetryAttemptsExceededException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MaxRetryAttemptsExceededException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public MaxRetryAttemptsExceededException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown when the circuit breaker is open.
/// </summary>
public class CircuitBreakerOpenException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CircuitBreakerOpenException"/> class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public CircuitBreakerOpenException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
