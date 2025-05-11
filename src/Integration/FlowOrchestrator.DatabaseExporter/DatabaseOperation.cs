namespace FlowOrchestrator.DatabaseExporter;

/// <summary>
/// Represents the type of database operation to perform.
/// </summary>
public enum DatabaseOperation
{
    /// <summary>
    /// Insert new records.
    /// </summary>
    Insert,
    
    /// <summary>
    /// Update existing records.
    /// </summary>
    Update,
    
    /// <summary>
    /// Delete existing records.
    /// </summary>
    Delete,
    
    /// <summary>
    /// Insert or update records based on primary key.
    /// </summary>
    Upsert,
    
    /// <summary>
    /// Execute a custom non-query command.
    /// </summary>
    ExecuteNonQuery
}
