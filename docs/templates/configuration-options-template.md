# FlowOrchestrator Configuration Options

## Introduction

This document provides a comprehensive reference for all configuration options available in the FlowOrchestrator system. It covers system-wide settings, component-specific options, and advanced configuration techniques.

## Configuration File Format

The FlowOrchestrator system uses a JSON configuration file. The file has the following structure:

```json
{
  "system": {
    // System-wide settings
  },
  "components": {
    // Component-specific settings
  },
  "security": {
    // Security settings
  },
  "telemetry": {
    // Telemetry settings
  }
}
```

## System Settings

### General Settings

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| logLevel | string | "Information" | Log level (Debug, Information, Warning, Error) |
| dataDirectory | string | "data" | Directory for storing data files |
| tempDirectory | string | "temp" | Directory for storing temporary files |
| maxConcurrentFlows | number | 10 | Maximum number of flows that can run concurrently |
| maxMemoryUsage | string | "2GB" | Maximum memory usage for the system |
| shutdownTimeout | number | 30 | Timeout in seconds for graceful shutdown |
| startupTimeout | number | 30 | Timeout in seconds for startup |

Example:

```json
"system": {
  "logLevel": "Information",
  "dataDirectory": "/path/to/data",
  "tempDirectory": "/path/to/temp",
  "maxConcurrentFlows": 10,
  "maxMemoryUsage": "2GB",
  "shutdownTimeout": 30,
  "startupTimeout": 30
}
```

### API Settings

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| enabled | boolean | true | Whether the API is enabled |
| port | number | 5000 | Port for the API |
| basePath | string | "/" | Base path for the API |
| corsOrigins | array | [] | Allowed CORS origins |
| rateLimit | object | {} | Rate limiting settings |

Example:

```json
"api": {
  "enabled": true,
  "port": 5000,
  "basePath": "/api",
  "corsOrigins": ["https://example.com"],
  "rateLimit": {
    "enabled": true,
    "limit": 100,
    "period": 60
  }
}
```

### Database Settings

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| provider | string | "InMemory" | Database provider (InMemory, SqlServer, PostgreSql) |
| connectionString | string | "" | Database connection string |
| poolSize | number | 10 | Connection pool size |
| commandTimeout | number | 30 | Command timeout in seconds |
| retryCount | number | 3 | Number of retry attempts |
| retryDelay | number | 1000 | Delay between retries in milliseconds |

Example:

```json
"database": {
  "provider": "SqlServer",
  "connectionString": "Server=localhost;Database=FlowOrch;User Id=sa;Password=P@ssw0rd;",
  "poolSize": 10,
  "commandTimeout": 30,
  "retryCount": 3,
  "retryDelay": 1000
}
```

## Component Settings

### Source Components

#### FileImporter

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| maxFileSize | string | "100MB" | Maximum file size |
| supportedFormats | array | ["json", "xml", "csv"] | Supported file formats |
| defaultEncoding | string | "utf-8" | Default file encoding |
| bufferSize | number | 4096 | Buffer size in bytes |
| watchForChanges | boolean | false | Whether to watch for file changes |

Example:

```json
"components": {
  "sources": {
    "fileImporter": {
      "maxFileSize": "100MB",
      "supportedFormats": ["json", "xml", "csv"],
      "defaultEncoding": "utf-8",
      "bufferSize": 4096,
      "watchForChanges": false
    }
  }
}
```

#### DatabaseImporter

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| connectionTimeout | number | 30 | Connection timeout in seconds |
| commandTimeout | number | 30 | Command timeout in seconds |
| maxBatchSize | number | 1000 | Maximum batch size |
| retryCount | number | 3 | Number of retry attempts |
| retryDelay | number | 1000 | Delay between retries in milliseconds |

Example:

```json
"components": {
  "sources": {
    "databaseImporter": {
      "connectionTimeout": 30,
      "commandTimeout": 30,
      "maxBatchSize": 1000,
      "retryCount": 3,
      "retryDelay": 1000
    }
  }
}
```

### Transformation Components

#### JsonProcessor

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| maxJsonDepth | number | 100 | Maximum JSON depth |
| maxStringLength | number | 10000 | Maximum string length |
| ignoreNullValues | boolean | false | Whether to ignore null values |
| allowComments | boolean | false | Whether to allow comments in JSON |
| allowTrailingCommas | boolean | false | Whether to allow trailing commas in JSON |

Example:

```json
"components": {
  "transformations": {
    "jsonProcessor": {
      "maxJsonDepth": 100,
      "maxStringLength": 10000,
      "ignoreNullValues": false,
      "allowComments": false,
      "allowTrailingCommas": false
    }
  }
}
```

#### XmlProcessor

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| maxXmlDepth | number | 100 | Maximum XML depth |
| maxStringLength | number | 10000 | Maximum string length |
| ignoreNamespaces | boolean | false | Whether to ignore namespaces |
| ignoreComments | boolean | true | Whether to ignore comments |
| ignoreProcessingInstructions | boolean | true | Whether to ignore processing instructions |

Example:

```json
"components": {
  "transformations": {
    "xmlProcessor": {
      "maxXmlDepth": 100,
      "maxStringLength": 10000,
      "ignoreNamespaces": false,
      "ignoreComments": true,
      "ignoreProcessingInstructions": true
    }
  }
}
```

### Validation Components

#### SchemaValidator

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| maxErrors | number | 100 | Maximum number of errors to report |
| stopOnFirstError | boolean | false | Whether to stop on the first error |
| cacheSchemas | boolean | true | Whether to cache schemas |
| schemaDirectory | string | "schemas" | Directory for schema files |

Example:

```json
"components": {
  "validations": {
    "schemaValidator": {
      "maxErrors": 100,
      "stopOnFirstError": false,
      "cacheSchemas": true,
      "schemaDirectory": "schemas"
    }
  }
}
```

#### RuleValidator

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| maxErrors | number | 100 | Maximum number of errors to report |
| stopOnFirstError | boolean | false | Whether to stop on the first error |
| cacheRules | boolean | true | Whether to cache rules |
| ruleDirectory | string | "rules" | Directory for rule files |

Example:

```json
"components": {
  "validations": {
    "ruleValidator": {
      "maxErrors": 100,
      "stopOnFirstError": false,
      "cacheRules": true,
      "ruleDirectory": "rules"
    }
  }
}
```

### Destination Components

#### FileExporter

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| overwriteExisting | boolean | true | Whether to overwrite existing files |
| createDirectories | boolean | true | Whether to create directories if they don't exist |
| bufferSize | number | 4096 | Buffer size in bytes |
| flushOnWrite | boolean | true | Whether to flush on write |
| defaultEncoding | string | "utf-8" | Default file encoding |

Example:

```json
"components": {
  "destinations": {
    "fileExporter": {
      "overwriteExisting": true,
      "createDirectories": true,
      "bufferSize": 4096,
      "flushOnWrite": true,
      "defaultEncoding": "utf-8"
    }
  }
}
```

#### DatabaseExporter

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| connectionTimeout | number | 30 | Connection timeout in seconds |
| commandTimeout | number | 30 | Command timeout in seconds |
| maxBatchSize | number | 1000 | Maximum batch size |
| retryCount | number | 3 | Number of retry attempts |
| retryDelay | number | 1000 | Delay between retries in milliseconds |
| useTransaction | boolean | true | Whether to use transactions |

Example:

```json
"components": {
  "destinations": {
    "databaseExporter": {
      "connectionTimeout": 30,
      "commandTimeout": 30,
      "maxBatchSize": 1000,
      "retryCount": 3,
      "retryDelay": 1000,
      "useTransaction": true
    }
  }
}
```

## Security Settings

### Authentication

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| enabled | boolean | true | Whether authentication is enabled |
| provider | string | "OAuth2" | Authentication provider |
| authority | string | "" | Authority URL |
| audience | string | "" | Audience |
| clientId | string | "" | Client ID |
| clientSecret | string | "" | Client secret |
| requireHttps | boolean | true | Whether to require HTTPS |

Example:

```json
"security": {
  "authentication": {
    "enabled": true,
    "provider": "OAuth2",
    "authority": "https://auth.example.com",
    "audience": "floworch-api",
    "clientId": "floworch-client",
    "clientSecret": "floworch-secret",
    "requireHttps": true
  }
}
```

### Authorization

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| enabled | boolean | true | Whether authorization is enabled |
| policyProvider | string | "RoleBasedPolicy" | Policy provider |
| policies | object | {} | Policy definitions |

Example:

```json
"security": {
  "authorization": {
    "enabled": true,
    "policyProvider": "RoleBasedPolicy",
    "policies": {
      "admin": {
        "roles": ["admin"]
      },
      "user": {
        "roles": ["user"]
      }
    }
  }
}
```

### Encryption

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| enabled | boolean | true | Whether encryption is enabled |
| algorithm | string | "AES256" | Encryption algorithm |
| keyProvider | string | "FileKeyProvider" | Key provider |
| keyFile | string | "" | Key file path |
| keyRotationInterval | string | "30d" | Key rotation interval |

Example:

```json
"security": {
  "encryption": {
    "enabled": true,
    "algorithm": "AES256",
    "keyProvider": "FileKeyProvider",
    "keyFile": "/path/to/keys.json",
    "keyRotationInterval": "30d"
  }
}
```

## Telemetry Settings

### Logging

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| enabled | boolean | true | Whether logging is enabled |
| provider | string | "Serilog" | Logging provider |
| sinks | array | ["Console"] | Log sinks |
| fileLogPath | string | "logs" | Path for file logs |
| rollingInterval | string | "Day" | Rolling interval for file logs |
| retainedFileCount | number | 31 | Number of log files to retain |

Example:

```json
"telemetry": {
  "logging": {
    "enabled": true,
    "provider": "Serilog",
    "sinks": ["Console", "File"],
    "fileLogPath": "/path/to/logs",
    "rollingInterval": "Day",
    "retainedFileCount": 31
  }
}
```

### Metrics

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| enabled | boolean | true | Whether metrics are enabled |
| provider | string | "Prometheus" | Metrics provider |
| endpoint | string | "/metrics" | Metrics endpoint |
| port | number | 9090 | Metrics port |
| interval | number | 5 | Collection interval in seconds |

Example:

```json
"telemetry": {
  "metrics": {
    "enabled": true,
    "provider": "Prometheus",
    "endpoint": "/metrics",
    "port": 9090,
    "interval": 5
  }
}
```

### Tracing

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| enabled | boolean | true | Whether tracing is enabled |
| provider | string | "OpenTelemetry" | Tracing provider |
| endpoint | string | "" | Tracing endpoint |
| sampleRate | number | 0.1 | Sampling rate (0.0-1.0) |
| exportBatchSize | number | 512 | Export batch size |
| exportInterval | number | 5 | Export interval in seconds |

Example:

```json
"telemetry": {
  "tracing": {
    "enabled": true,
    "provider": "OpenTelemetry",
    "endpoint": "https://telemetry.example.com",
    "sampleRate": 0.1,
    "exportBatchSize": 512,
    "exportInterval": 5
  }
}
```

## Advanced Configuration

### Environment Variables

[Information about environment variables]

### Command-Line Options

[Information about command-line options]

### Configuration Providers

[Information about configuration providers]

## Troubleshooting

[Common configuration issues and their solutions]

## References

[List of references and related documentation]
