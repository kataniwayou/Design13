# FlowOrchestrator Configuration Guide

## Introduction

This guide provides comprehensive information about configuring the FlowOrchestrator system. It covers configuration options for the core system, components, and flows.

## System Configuration

### Configuration File

The FlowOrchestrator system is configured using a JSON configuration file. By default, the system looks for a file named `floworch.json` in the current directory. You can specify a different configuration file using the `--config` command-line option:

```
floworch --config=/path/to/config.json
```

### Configuration Structure

The configuration file has the following structure:

```json
{
  "system": {
    "logLevel": "Information",
    "dataDirectory": "/path/to/data",
    "tempDirectory": "/path/to/temp",
    "maxConcurrentFlows": 10,
    "maxMemoryUsage": "2GB"
  },
  "components": {
    "sources": {
      "fileImporter": {
        "maxFileSize": "100MB",
        "supportedFormats": ["json", "xml", "csv"]
      },
      "databaseImporter": {
        "connectionTimeout": 30,
        "maxBatchSize": 1000
      }
    },
    "transformations": {
      "jsonProcessor": {
        "maxJsonDepth": 100,
        "maxStringLength": 10000
      },
      "xmlProcessor": {
        "maxXmlDepth": 100,
        "maxStringLength": 10000
      }
    },
    "destinations": {
      "fileExporter": {
        "overwriteExisting": true,
        "createDirectories": true
      },
      "databaseExporter": {
        "connectionTimeout": 30,
        "maxBatchSize": 1000
      }
    }
  },
  "security": {
    "authentication": {
      "enabled": true,
      "provider": "OAuth2",
      "authority": "https://auth.example.com",
      "audience": "floworch-api",
      "clientId": "floworch-client"
    },
    "authorization": {
      "enabled": true,
      "policyProvider": "RoleBasedPolicy"
    },
    "encryption": {
      "enabled": true,
      "algorithm": "AES256",
      "keyProvider": "FileKeyProvider",
      "keyFile": "/path/to/keys.json"
    }
  },
  "telemetry": {
    "logging": {
      "enabled": true,
      "provider": "Serilog",
      "sinks": ["Console", "File"],
      "fileLogPath": "/path/to/logs"
    },
    "metrics": {
      "enabled": true,
      "provider": "Prometheus",
      "endpoint": "/metrics",
      "port": 9090
    },
    "tracing": {
      "enabled": true,
      "provider": "OpenTelemetry",
      "endpoint": "https://telemetry.example.com"
    }
  }
}
```

## Component Configuration

### Source Components

#### FileImporter

The FileImporter component imports data from files. It supports the following configuration options:

| Option | Type | Default | Description |
|--------|------|---------|-------------|
| filePath | string | - | Path to the file to import (required) |
| format | string | auto | File format (auto, json, xml, csv) |
| encoding | string | utf-8 | File encoding |
| skipLines | number | 0 | Number of lines to skip at the beginning |
| maxLines | number | -1 | Maximum number of lines to read (-1 for all) |

Example:

```json
{
  "filePath": "data.json",
  "format": "json",
  "encoding": "utf-8"
}
```

#### DatabaseImporter

[Configuration options for DatabaseImporter]

### Transformation Components

#### JsonProcessor

[Configuration options for JsonProcessor]

#### XmlProcessor

[Configuration options for XmlProcessor]

### Validation Components

#### SchemaValidator

[Configuration options for SchemaValidator]

#### RuleValidator

[Configuration options for RuleValidator]

### Destination Components

#### FileExporter

[Configuration options for FileExporter]

#### DatabaseExporter

[Configuration options for DatabaseExporter]

## Flow Configuration

### Flow Definition

A flow definition includes the following configuration:

| Option | Type | Description |
|--------|------|-------------|
| flowId | string | Unique identifier for the flow (required) |
| name | string | Display name for the flow (required) |
| description | string | Description of the flow |
| version | string | Version of the flow (required) |
| components | array | Array of component definitions (required) |
| connections | array | Array of connection definitions (required) |

### Component Definition

Each component in a flow has the following configuration:

| Option | Type | Description |
|--------|------|-------------|
| componentId | string | Unique identifier for the component (required) |
| name | string | Display name for the component (required) |
| componentType | string | Type of the component (required) |
| configuration | object | Component-specific configuration (required) |

### Connection Definition

Each connection in a flow has the following configuration:

| Option | Type | Description |
|--------|------|-------------|
| connectionId | string | Unique identifier for the connection (required) |
| sourceComponentId | string | ID of the source component (required) |
| targetComponentId | string | ID of the target component (required) |

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
