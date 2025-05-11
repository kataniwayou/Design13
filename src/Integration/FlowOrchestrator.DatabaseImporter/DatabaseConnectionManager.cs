using FlowOrchestrator.Abstractions.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;

namespace FlowOrchestrator.DatabaseImporter;

/// <summary>
/// Connection manager for database operations.
/// </summary>
public class DatabaseConnectionManager : IConnectionManager, IDisposable
{
    private readonly ILogger<DatabaseConnectionManager> _logger;
    private readonly DatabaseImporterOptions _options;
    private DbConnection? _connection;
    private DbTransaction? _transaction;
    private bool _isOpen;
    private bool _disposed;
    
    /// <summary>
    /// Gets the database connection.
    /// </summary>
    public DbConnection? Connection => _connection;
    
    /// <summary>
    /// Gets the database transaction.
    /// </summary>
    public DbTransaction? Transaction => _transaction;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseConnectionManager"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options.</param>
    public DatabaseConnectionManager(ILogger<DatabaseConnectionManager> logger, DatabaseImporterOptions options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }
    
    /// <inheritdoc />
    public async Task OpenAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Opening database connection");
        
        if (_isOpen)
        {
            _logger.LogWarning("Database connection is already open");
            return;
        }
        
        try
        {
            // Create connection based on provider name
            _connection = CreateConnection();
            
            // Open connection
            await _connection.OpenAsync(cancellationToken);
            
            // Begin transaction if enabled
            if (_options.UseTransactions)
            {
                var isolationLevel = GetIsolationLevel(_options.IsolationLevel);
                _transaction = await _connection.BeginTransactionAsync(isolationLevel, cancellationToken);
            }
            
            _isOpen = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error opening database connection");
            throw;
        }
    }
    
    /// <inheritdoc />
    public async Task CloseAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Closing database connection");
        
        if (!_isOpen)
        {
            _logger.LogWarning("Database connection is already closed");
            return;
        }
        
        try
        {
            // Commit transaction if exists
            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
                _transaction.Dispose();
                _transaction = null;
            }
            
            // Close connection
            if (_connection != null)
            {
                await _connection.CloseAsync();
            }
            
            _isOpen = false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error closing database connection");
            throw;
        }
    }
    
    /// <inheritdoc />
    public async Task TestConnectionAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Testing database connection");
        
        try
        {
            using var connection = CreateConnection();
            await connection.OpenAsync(cancellationToken);
            await connection.CloseAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error testing database connection");
            throw new InvalidOperationException("Cannot connect to database", ex);
        }
    }
    
    /// <summary>
    /// Commits the current transaction.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            _logger.LogWarning("No transaction to commit");
            return;
        }
        
        await _transaction.CommitAsync(cancellationToken);
        _transaction.Dispose();
        _transaction = null;
        
        // Begin a new transaction if transactions are enabled
        if (_options.UseTransactions && _connection != null && _connection.State == ConnectionState.Open)
        {
            var isolationLevel = GetIsolationLevel(_options.IsolationLevel);
            _transaction = await _connection.BeginTransactionAsync(isolationLevel, cancellationToken);
        }
    }
    
    /// <summary>
    /// Rolls back the current transaction.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
        {
            _logger.LogWarning("No transaction to roll back");
            return;
        }
        
        await _transaction.RollbackAsync(cancellationToken);
        _transaction.Dispose();
        _transaction = null;
        
        // Begin a new transaction if transactions are enabled
        if (_options.UseTransactions && _connection != null && _connection.State == ConnectionState.Open)
        {
            var isolationLevel = GetIsolationLevel(_options.IsolationLevel);
            _transaction = await _connection.BeginTransactionAsync(isolationLevel, cancellationToken);
        }
    }
    
    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="DatabaseConnectionManager"/> and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        
        if (disposing)
        {
            // Dispose managed resources
            _transaction?.Dispose();
            _connection?.Dispose();
        }
        
        _transaction = null;
        _connection = null;
        _isOpen = false;
        _disposed = true;
    }
    
    /// <summary>
    /// Finalizes an instance of the <see cref="DatabaseConnectionManager"/> class.
    /// </summary>
    ~DatabaseConnectionManager()
    {
        Dispose(false);
    }
    
    private DbConnection CreateConnection()
    {
        // For simplicity, we'll only support SQL Server for now
        return new SqlConnection(GetConnectionString());
    }
    
    private string GetConnectionString()
    {
        // If a connection string is provided, use it
        if (!string.IsNullOrEmpty(_options.ConnectionString))
        {
            return _options.ConnectionString;
        }
        
        // Otherwise, build a connection string from the options
        var builder = new SqlConnectionStringBuilder
        {
            ConnectTimeout = _options.CommandTimeoutSeconds,
            Pooling = _options.UseConnectionPooling,
            MinPoolSize = _options.MinPoolSize,
            MaxPoolSize = _options.MaxPoolSize,
            Encrypt = _options.UseEncryption,
            TrustServerCertificate = _options.TrustServerCertificate,
            IntegratedSecurity = _options.UseIntegratedSecurity,
            MultipleActiveResultSets = _options.UseMultipleActiveResultSets
        };
        
        if (!_options.UseIntegratedSecurity)
        {
            builder.UserID = _options.Username ?? string.Empty;
            builder.Password = _options.Password ?? string.Empty;
        }
        
        return builder.ConnectionString;
    }
    
    private IsolationLevel GetIsolationLevel(string isolationLevelName)
    {
        return isolationLevelName.ToLowerInvariant() switch
        {
            "readuncommitted" => IsolationLevel.ReadUncommitted,
            "readcommitted" => IsolationLevel.ReadCommitted,
            "repeatableread" => IsolationLevel.RepeatableRead,
            "serializable" => IsolationLevel.Serializable,
            "snapshot" => IsolationLevel.Snapshot,
            "chaos" => IsolationLevel.Chaos,
            "unspecified" => IsolationLevel.Unspecified,
            _ => IsolationLevel.ReadCommitted
        };
    }
}
