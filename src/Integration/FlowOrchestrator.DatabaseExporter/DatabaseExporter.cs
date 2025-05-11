using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using System.Text;
using FlowOrchestrator.ExporterBase;

namespace FlowOrchestrator.DatabaseExporter;

/// <summary>
/// Exporter for exporting data to databases.
/// </summary>
public class DatabaseExporter : ExporterBase.ExporterBase
{
    private readonly ILogger<DatabaseExporter> _logger;
    private readonly DatabaseExporterOptions _options;

    /// <inheritdoc />
    public override string ExporterType => "Database";

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseExporter"/> class.
    /// </summary>
    /// <param name="exporterId">The unique identifier for this exporter.</param>
    /// <param name="name">The name of this exporter.</param>
    /// <param name="description">The description of this exporter.</param>
    /// <param name="connectionManager">The connection manager for this exporter.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options for this exporter.</param>
    public DatabaseExporter(
        string exporterId,
        string name,
        string description,
        IConnectionManager connectionManager,
        ILogger<DatabaseExporter> logger,
        DatabaseExporterOptions options)
        : base(exporterId, name, description, connectionManager, logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <inheritdoc />
    public override async Task<ExporterBase.ExportResult> ExportAsync(ExporterBase.ExportContext exportContext, CancellationToken cancellationToken = default)
    {
        if (exportContext == null) throw new ArgumentNullException(nameof(exportContext));

        _logger.LogInformation("Exporting data to database for exporter {ExporterId}", ExporterId);

        if (Status != ExporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot export data for exporter {ExporterId} in status {Status}");
        }

        Status = ExporterStatus.Exporting;

        try
        {
            var databaseConnectionManager = ConnectionManager as DatabaseConnectionManager;
            if (databaseConnectionManager == null)
            {
                throw new InvalidOperationException($"Connection manager for exporter {ExporterId} is not a DatabaseConnectionManager");
            }

            var connection = databaseConnectionManager.Connection;
            if (connection == null)
            {
                throw new InvalidOperationException($"Connection for exporter {ExporterId} is null");
            }

            // Get export parameters
            var tableName = GetTableName(exportContext);
            var data = GetData(exportContext);
            var operation = GetOperation(exportContext);

            int recordsExported;

            switch (operation)
            {
                case DatabaseOperation.Insert:
                    recordsExported = await InsertDataAsync(connection, tableName, data, databaseConnectionManager.Transaction, cancellationToken);
                    break;

                case DatabaseOperation.Update:
                    recordsExported = await UpdateDataAsync(connection, tableName, data, databaseConnectionManager.Transaction, cancellationToken);
                    break;

                case DatabaseOperation.Delete:
                    recordsExported = await DeleteDataAsync(connection, tableName, data, databaseConnectionManager.Transaction, cancellationToken);
                    break;

                case DatabaseOperation.Upsert:
                    recordsExported = await UpsertDataAsync(connection, tableName, data, databaseConnectionManager.Transaction, cancellationToken);
                    break;

                case DatabaseOperation.ExecuteNonQuery:
                    recordsExported = await ExecuteNonQueryAsync(connection, exportContext, databaseConnectionManager.Transaction, cancellationToken);
                    break;

                default:
                    throw new InvalidOperationException($"Unsupported database operation: {operation}");
            }

            var result = ExporterBase.ExportResult.Success(
                exportContext.ExportId,
                recordsExported, // Records exported
                recordsExported  // Total records
            );

            Status = ExporterStatus.Open;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting data to database for exporter {ExporterId}", ExporterId);
            Status = ExporterStatus.Error;
            return ExporterBase.ExportResult.Failure(exportContext.ExportId, ex.Message);
        }
    }

    /// <inheritdoc />
    public override async Task<Domain.Entities.DataSchema> GetSchemaAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting schema for exporter {ExporterId}", ExporterId);

        if (Status != ExporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot get schema for exporter {ExporterId} in status {Status}");
        }

        var databaseConnectionManager = ConnectionManager as DatabaseConnectionManager;
        if (databaseConnectionManager == null)
        {
            throw new InvalidOperationException($"Connection manager for exporter {ExporterId} is not a DatabaseConnectionManager");
        }

        var connection = databaseConnectionManager.Connection;
        if (connection == null)
        {
            throw new InvalidOperationException($"Connection for exporter {ExporterId} is null");
        }

        var schema = new Domain.Entities.DataSchema
        {
            Name = $"{Name} Schema",
            Description = $"Schema for {Description}",
            Version = "1.0.0",
            Tables = new List<Domain.Entities.DataTable>()
        };

        // Get database schema
        var tables = await GetTablesAsync(connection, cancellationToken);

        foreach (var table in tables)
        {
            var columns = await GetColumnsAsync(connection, table, cancellationToken);
            var primaryKeys = await GetPrimaryKeysAsync(connection, table, cancellationToken);

            var dataTable = new Domain.Entities.DataTable
            {
                Name = table,
                Description = $"Table {table}",
                Columns = columns,
                PrimaryKey = primaryKeys
            };

            schema.Tables.Add(dataTable);
        }

        return schema;
    }

    /// <inheritdoc />
    public override ExporterBase.ExporterCapabilities GetCapabilities()
    {
        return new ExporterBase.ExporterCapabilities
        {
            SupportsStreaming = false,
            SupportsBatching = true,
            SupportsFiltering = true,
            SupportsSorting = false,
            SupportsPagination = false,
            SupportsSchemaDiscovery = true,
            SupportsIncrementalExport = true,
            SupportsParallelExport = false,
            SupportsResumeExport = false,
            SupportsAuthentication = true,
            SupportsEncryption = true,
            SupportsCompression = false,
            SupportsTransactions = true,
            SupportsBulkOperations = true,
            SupportsUpserts = true,
            SupportsDeletes = true,
            MaxBatchSize = 1000,
            MaxParallelExports = 1,
            SupportedDataFormats = new List<string> { "sql" },
            SupportedAuthenticationMethods = new List<string> { "none", "sql", "windows", "azure-ad" },
            SupportedEncryptionMethods = new List<string> { "none", "ssl", "tls" }
        };
    }

    private string GetTableName(ExporterBase.ExportContext exportContext)
    {
        // Check if the table name is specified in the export context
        if (exportContext.Parameters.TryGetValue("TableName", out var tableNameObj) && tableNameObj is string tableName)
        {
            return tableName;
        }

        throw new InvalidOperationException("No table name specified in export context");
    }

    private IEnumerable<IDictionary<string, object>> GetData(ExporterBase.ExportContext exportContext)
    {
        // Check if the data is specified in the export context
        if (exportContext.Data == null)
        {
            return Enumerable.Empty<IDictionary<string, object>>();
        }

        // If the data is already a collection of dictionaries, use it
        if (exportContext.Data is IEnumerable<IDictionary<string, object>> data)
        {
            return data;
        }

        // If the data is a single dictionary, wrap it in a collection
        if (exportContext.Data is IDictionary<string, object> singleData)
        {
            return new[] { singleData };
        }

        throw new InvalidOperationException("Data in export context is not in a supported format");
    }

    private DatabaseOperation GetOperation(ExporterBase.ExportContext exportContext)
    {
        // Check if the operation is specified in the export context
        if (exportContext.Parameters.TryGetValue("Operation", out var operationObj) && operationObj is string operationStr)
        {
            if (Enum.TryParse<DatabaseOperation>(operationStr, true, out var operation))
            {
                return operation;
            }
        }

        // Default to insert
        return DatabaseOperation.Insert;
    }

    private async Task<int> InsertDataAsync(DbConnection connection, string tableName, IEnumerable<IDictionary<string, object>> data, DbTransaction? transaction, CancellationToken cancellationToken)
    {
        var recordsExported = 0;

        foreach (var row in data)
        {
            if (row.Count == 0)
            {
                continue;
            }

            var columns = string.Join(", ", row.Keys);
            var parameters = string.Join(", ", row.Keys.Select(k => $"@{k}"));

            var query = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            // Add parameters
            foreach (var kvp in row)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = $"@{kvp.Key}";
                parameter.Value = kvp.Value ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }

            var result = await command.ExecuteNonQueryAsync(cancellationToken);
            recordsExported += result;
        }

        return recordsExported;
    }

    private async Task<int> UpdateDataAsync(DbConnection connection, string tableName, IEnumerable<IDictionary<string, object>> data, DbTransaction? transaction, CancellationToken cancellationToken)
    {
        var recordsExported = 0;

        foreach (var row in data)
        {
            if (row.Count == 0)
            {
                continue;
            }

            // Get primary key columns
            var primaryKeys = await GetPrimaryKeysAsync(connection, tableName, cancellationToken);

            if (primaryKeys.Count == 0)
            {
                throw new InvalidOperationException($"Table {tableName} has no primary key columns");
            }

            // Split row into set columns and where columns
            var setColumns = row.Keys.Except(primaryKeys).ToList();
            var whereColumns = row.Keys.Intersect(primaryKeys).ToList();

            if (setColumns.Count == 0)
            {
                throw new InvalidOperationException("No columns to update");
            }

            if (whereColumns.Count == 0)
            {
                throw new InvalidOperationException("No primary key columns in data");
            }

            var setClause = string.Join(", ", setColumns.Select(c => $"{c} = @{c}"));
            var whereClause = string.Join(" AND ", whereColumns.Select(c => $"{c} = @{c}"));

            var query = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";

            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            // Add parameters
            foreach (var kvp in row)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = $"@{kvp.Key}";
                parameter.Value = kvp.Value ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }

            var result = await command.ExecuteNonQueryAsync(cancellationToken);
            recordsExported += result;
        }

        return recordsExported;
    }

    private async Task<int> DeleteDataAsync(DbConnection connection, string tableName, IEnumerable<IDictionary<string, object>> data, DbTransaction? transaction, CancellationToken cancellationToken)
    {
        var recordsExported = 0;

        foreach (var row in data)
        {
            if (row.Count == 0)
            {
                continue;
            }

            var whereClause = string.Join(" AND ", row.Keys.Select(c => $"{c} = @{c}"));

            var query = $"DELETE FROM {tableName} WHERE {whereClause}";

            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            // Add parameters
            foreach (var kvp in row)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = $"@{kvp.Key}";
                parameter.Value = kvp.Value ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }

            var result = await command.ExecuteNonQueryAsync(cancellationToken);
            recordsExported += result;
        }

        return recordsExported;
    }

    private async Task<int> UpsertDataAsync(DbConnection connection, string tableName, IEnumerable<IDictionary<string, object>> data, DbTransaction? transaction, CancellationToken cancellationToken)
    {
        // This is a simplified implementation that may not work for all database providers
        // For SQL Server, we would use MERGE statement

        var recordsExported = 0;

        foreach (var row in data)
        {
            if (row.Count == 0)
            {
                continue;
            }

            // Get primary key columns
            var primaryKeys = await GetPrimaryKeysAsync(connection, tableName, cancellationToken);

            if (primaryKeys.Count == 0)
            {
                // If no primary key, just insert
                recordsExported += await InsertDataAsync(connection, tableName, new[] { row }, transaction, cancellationToken);
                continue;
            }

            // Check if the record exists
            var whereClause = string.Join(" AND ", primaryKeys.Select(c => $"{c} = @{c}"));

            var checkQuery = $"SELECT COUNT(*) FROM {tableName} WHERE {whereClause}";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = checkQuery;
                command.CommandType = CommandType.Text;

                if (transaction != null)
                {
                    command.Transaction = transaction;
                }

                // Add parameters for primary keys
                foreach (var key in primaryKeys)
                {
                    if (row.TryGetValue(key, out var value))
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = $"@{key}";
                        parameter.Value = value ?? DBNull.Value;
                        command.Parameters.Add(parameter);
                    }
                }

                var count = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));

                if (count > 0)
                {
                    // Record exists, update it
                    recordsExported += await UpdateDataAsync(connection, tableName, new[] { row }, transaction, cancellationToken);
                }
                else
                {
                    // Record doesn't exist, insert it
                    recordsExported += await InsertDataAsync(connection, tableName, new[] { row }, transaction, cancellationToken);
                }
            }
        }

        return recordsExported;
    }

    private async Task<int> ExecuteNonQueryAsync(DbConnection connection, ExporterBase.ExportContext exportContext, DbTransaction? transaction, CancellationToken cancellationToken)
    {
        // Check if the query is specified in the export context
        if (!exportContext.Parameters.TryGetValue("Query", out var queryObj) || queryObj is not string query)
        {
            throw new InvalidOperationException("No query specified in export context");
        }

        using var command = connection.CreateCommand();
        command.CommandText = query;
        command.CommandType = CommandType.Text;

        if (transaction != null)
        {
            command.Transaction = transaction;
        }

        // Add parameters
        if (exportContext.Parameters.TryGetValue("Parameters", out var parametersObj) && parametersObj is Dictionary<string, object> parameters)
        {
            foreach (var kvp in parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = kvp.Key;
                parameter.Value = kvp.Value ?? DBNull.Value;
                command.Parameters.Add(parameter);
            }
        }

        return await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private async Task<List<string>> GetTablesAsync(DbConnection connection, CancellationToken cancellationToken)
    {
        var tables = new List<string>();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

        using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            tables.Add(reader.GetString(0));
        }

        return tables;
    }

    private async Task<List<Domain.Entities.DataColumn>> GetColumnsAsync(DbConnection connection, string tableName, CancellationToken cancellationToken)
    {
        var columns = new List<Domain.Entities.DataColumn>();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT
                COLUMN_NAME,
                DATA_TYPE,
                CHARACTER_MAXIMUM_LENGTH,
                NUMERIC_PRECISION,
                NUMERIC_SCALE,
                IS_NULLABLE,
                COLUMN_DEFAULT
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = @TableName
            ORDER BY ORDINAL_POSITION";

        var parameter = command.CreateParameter();
        parameter.ParameterName = "@TableName";
        parameter.Value = tableName;
        command.Parameters.Add(parameter);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            var column = new Domain.Entities.DataColumn
            {
                Name = reader.GetString(0),
                DataType = reader.GetString(1),
                IsNullable = reader.GetString(5) == "YES"
            };

            if (!reader.IsDBNull(2))
            {
                column.MaxLength = reader.GetInt32(2);
            }

            if (!reader.IsDBNull(3))
            {
                column.Precision = reader.GetInt32(3);
            }

            if (!reader.IsDBNull(4))
            {
                column.Scale = reader.GetInt32(4);
            }

            if (!reader.IsDBNull(6))
            {
                column.DefaultValue = reader.GetString(6);
            }

            columns.Add(column);
        }

        return columns;
    }

    private async Task<List<string>> GetPrimaryKeysAsync(DbConnection connection, string tableName, CancellationToken cancellationToken)
    {
        var primaryKeys = new List<string>();

        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT
                COLUMN_NAME
            FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
            WHERE TABLE_NAME = @TableName
            AND CONSTRAINT_NAME IN (
                SELECT CONSTRAINT_NAME
                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
                WHERE TABLE_NAME = @TableName
                AND CONSTRAINT_TYPE = 'PRIMARY KEY'
            )
            ORDER BY ORDINAL_POSITION";

        var parameter = command.CreateParameter();
        parameter.ParameterName = "@TableName";
        parameter.Value = tableName;
        command.Parameters.Add(parameter);

        using var reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            primaryKeys.Add(reader.GetString(0));
        }

        return primaryKeys;
    }
}
