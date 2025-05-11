using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Configuration;
using FlowOrchestrator.Domain.Entities;
using FlowOrchestrator.ImporterBase;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using System.Text;

namespace FlowOrchestrator.DatabaseImporter;

/// <summary>
/// Importer for importing data from databases.
/// </summary>
public class DatabaseImporter : ImporterBase.ImporterBase
{
    private readonly ILogger<DatabaseImporter> _logger;
    private readonly DatabaseImporterOptions _options;

    /// <inheritdoc />
    public override string ImporterType => "Database";

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseImporter"/> class.
    /// </summary>
    /// <param name="importerId">The unique identifier for this importer.</param>
    /// <param name="name">The name of this importer.</param>
    /// <param name="description">The description of this importer.</param>
    /// <param name="connectionManager">The connection manager for this importer.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options for this importer.</param>
    public DatabaseImporter(
        string importerId,
        string name,
        string description,
        IConnectionManager connectionManager,
        ILogger<DatabaseImporter> logger,
        DatabaseImporterOptions options)
        : base(importerId, name, description, connectionManager, logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <inheritdoc />
    public override async Task<ImporterBase.ImportResult> ImportAsync(ImportContext importContext, CancellationToken cancellationToken = default)
    {
        if (importContext == null) throw new ArgumentNullException(nameof(importContext));

        _logger.LogInformation("Importing data from database for importer {ImporterId}", ImporterId);

        if (Status != ImporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot import data for importer {ImporterId} in status {Status}");
        }

        Status = ImporterStatus.Importing;

        try
        {
            var query = GetQuery(importContext);
            var parameters = GetParameters(importContext);

            var databaseConnectionManager = ConnectionManager as DatabaseConnectionManager;
            if (databaseConnectionManager == null)
            {
                throw new InvalidOperationException($"Connection manager for importer {ImporterId} is not a DatabaseConnectionManager");
            }

            var connection = databaseConnectionManager.Connection;
            if (connection == null)
            {
                throw new InvalidOperationException($"Connection for importer {ImporterId} is null");
            }

            using var command = connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            // Add parameters
            foreach (var parameter in parameters)
            {
                var dbParameter = command.CreateParameter();
                dbParameter.ParameterName = parameter.Key;
                dbParameter.Value = parameter.Value ?? DBNull.Value;
                command.Parameters.Add(dbParameter);
            }

            // Execute query
            var data = new List<Dictionary<string, object>>();

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var schemaTable = reader.GetSchemaTable();
            var columnNames = new List<string>();

            if (schemaTable != null)
            {
                foreach (DataRow row in schemaTable.Rows)
                {
                    columnNames.Add(row["ColumnName"].ToString() ?? string.Empty);
                }
            }

            while (await reader.ReadAsync(cancellationToken))
            {
                var row = new Dictionary<string, object>();

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    var columnName = columnNames.Count > i ? columnNames[i] : reader.GetName(i);
                    var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    row[columnName] = value ?? DBNull.Value;
                }

                data.Add(row);
            }

            var result = ImporterBase.ImportResult.Success(
                importContext.ImportId,
                data.Count, // Records imported
                data.Count, // Total records
                data);

            Status = ImporterStatus.Open;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error importing data from database for importer {ImporterId}", ImporterId);
            Status = ImporterStatus.Error;
            return ImporterBase.ImportResult.Failure(importContext.ImportId, ex.Message);
        }
    }

    /// <inheritdoc />
    public override async Task<DataSchema> GetSchemaAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting schema for importer {ImporterId}", ImporterId);

        if (Status != ImporterStatus.Open)
        {
            throw new InvalidOperationException($"Cannot get schema for importer {ImporterId} in status {Status}");
        }

        var databaseConnectionManager = ConnectionManager as DatabaseConnectionManager;
        if (databaseConnectionManager == null)
        {
            throw new InvalidOperationException($"Connection manager for importer {ImporterId} is not a DatabaseConnectionManager");
        }

        var connection = databaseConnectionManager.Connection;
        if (connection == null)
        {
            throw new InvalidOperationException($"Connection for importer {ImporterId} is null");
        }

        var schema = new DataSchema
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
            var indexes = await GetIndexesAsync(connection, table, cancellationToken);

            var dataTable = new Domain.Entities.DataTable
            {
                Name = table,
                Description = $"Table {table}",
                Columns = columns,
                PrimaryKey = primaryKeys,
                Indexes = indexes
            };

            schema.Tables.Add(dataTable);
        }

        // Get relationships
        schema.Relationships = await GetRelationshipsAsync(connection, cancellationToken);

        return schema;
    }

    /// <inheritdoc />
    public override ImporterCapabilities GetCapabilities()
    {
        return new ImporterCapabilities
        {
            SupportsStreaming = false,
            SupportsBatching = true,
            SupportsFiltering = true,
            SupportsSorting = true,
            SupportsPagination = true,
            SupportsSchemaDiscovery = true,
            SupportsIncrementalImport = true,
            SupportsParallelImport = false,
            SupportsResumeImport = false,
            SupportsAuthentication = true,
            SupportsEncryption = true,
            SupportsCompression = false,
            MaxBatchSize = 1000,
            MaxParallelImports = 1,
            SupportedDataFormats = new List<string> { "sql" },
            SupportedAuthenticationMethods = new List<string> { "none", "sql", "windows", "azure-ad" },
            SupportedEncryptionMethods = new List<string> { "none", "ssl", "tls" }
        };
    }

    private string GetQuery(ImportContext importContext)
    {
        // Check if the query is specified in the import context
        if (importContext.Parameters.TryGetValue("Query", out var queryObj) && queryObj is string query)
        {
            return query;
        }

        // Otherwise, use the table name and build a query
        if (importContext.Parameters.TryGetValue("TableName", out var tableNameObj) && tableNameObj is string tableName)
        {
            var queryBuilder = new StringBuilder($"SELECT * FROM {tableName}");

            // Add WHERE clause if filter is specified
            if (!string.IsNullOrEmpty(importContext.Filter))
            {
                queryBuilder.Append($" WHERE {importContext.Filter}");
            }

            // Add ORDER BY clause if sort is specified
            if (!string.IsNullOrEmpty(importContext.Sort))
            {
                queryBuilder.Append($" ORDER BY {importContext.Sort}");
            }

            // Add pagination if specified
            if (importContext.PageNumber.HasValue && importContext.PageSize.HasValue)
            {
                var offset = (importContext.PageNumber.Value - 1) * importContext.PageSize.Value;
                queryBuilder.Append($" OFFSET {offset} ROWS FETCH NEXT {importContext.PageSize.Value} ROWS ONLY");
            }

            return queryBuilder.ToString();
        }

        throw new InvalidOperationException("No query or table name specified in import context");
    }

    private Dictionary<string, object?> GetParameters(ImportContext importContext)
    {
        // Check if the parameters are specified in the import context
        if (importContext.Parameters.TryGetValue("Parameters", out var parametersObj) && parametersObj is Dictionary<string, object> parameters)
        {
            return parameters;
        }

        return new Dictionary<string, object?>();
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

    private async Task<List<DataIndex>> GetIndexesAsync(DbConnection connection, string tableName, CancellationToken cancellationToken)
    {
        var indexes = new List<DataIndex>();

        // This is a simplified implementation that may not work for all database providers
        return indexes;
    }

    private async Task<List<DataRelationship>> GetRelationshipsAsync(DbConnection connection, CancellationToken cancellationToken)
    {
        var relationships = new List<DataRelationship>();

        // This is a simplified implementation that may not work for all database providers
        return relationships;
    }
}
