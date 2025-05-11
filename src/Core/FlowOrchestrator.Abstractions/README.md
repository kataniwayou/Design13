# FlowOrchestrator.Abstractions

This project contains the core abstractions and interfaces for the FlowOrchestrator system. These interfaces define the contract between different components of the system and provide a foundation for the entire architecture.

## Core Entities

### IEntity
The base interface for all entities in the FlowOrchestrator system. It defines the basic properties that all entities must have:
- `Id`: A unique identifier for the entity
- `Name`: A human-readable name for the entity
- `Description`: A description of the entity

### IVersionedEntity
Extends `IEntity` to add versioning support. This is used for entities that need to be versioned, such as flows and components:
- `Version`: The semantic version number (MAJOR.MINOR.PATCH)
- `CreatedTimestamp`: When the entity was first created
- `LastModifiedTimestamp`: When the entity was last modified
- `VersionDescription`: A human-readable description of this version
- `PreviousVersionId`: A reference to the previous version (if applicable)
- `VersionStatus`: The status of this version (ACTIVE, DEPRECATED, or ARCHIVED)

## Flow Entities

### IFlow
Represents a flow, which is the main entity in the FlowOrchestrator system. A flow defines a data processing workflow:
- `Steps`: The list of steps in the flow
- `Branches`: The list of branches in the flow
- `ValidationRules`: The validation rules for the flow
- `ErrorHandlingConfig`: The error handling configuration for the flow
- `MergeStrategyConfig`: The merge strategy configuration for branch convergence

### IFlowStep
Represents a step in a flow:
- `StepType`: The type of the step (IMPORT, PROCESS, EXPORT)
- `StepId`: The position of the step within its branch
- `BranchPath`: The branch path to which this step belongs
- `ComponentId`: The component ID that implements this step
- `Configuration`: The configuration for this step

### IFlowBranch
Represents a branch in a flow:
- `BranchPath`: The path of the branch (e.g., main, branchA, branchB)
- `ParentBranchPath`: The parent branch path (null for the main branch)
- `ParentStepId`: The step ID in the parent branch where this branch splits off
- `Steps`: The list of steps in this branch
- `Priority`: The branch priority for resource allocation
- `MergeConfig`: The merge configuration for this branch

## Configuration Entities

### IMergeConfig
Represents the configuration for merging a branch:
- `TargetBranchPath`: The target branch path where this branch merges back
- `TargetStepId`: The step ID in the target branch where this branch merges back
- `MergeStrategy`: The merge strategy to use when merging this branch
- `MergeStrategyConfiguration`: The configuration for the merge strategy

### IMergeStrategyConfig
Represents the flow-level merge strategy configuration:
- `DefaultMergeStrategy`: The default merge strategy to use for branches that don't specify one
- `DefaultMergeStrategyConfiguration`: The default configuration for the default merge strategy
- `BranchMergeConfigs`: The branch-specific merge strategy overrides

### IErrorHandlingConfig
Represents the flow-level error handling configuration:
- `DefaultStrategy`: The default error handling strategy to use for steps that don't specify one
- `MaxRetryAttempts`: The maximum number of retry attempts for retryable errors
- `RetryDelayMs`: The retry delay in milliseconds
- `UseExponentialBackoff`: Whether to use exponential backoff for retries
- `StepErrorHandlingStrategies`: The step-specific error handling strategy overrides
- `CompensatingActions`: The compensating actions to execute on failure

## Validation Entities

### IValidationRule
Represents a validation rule in a flow:
- `RuleType`: The rule type
- `Configuration`: The rule configuration
- `Severity`: The severity of the rule
- `ErrorMessageTemplate`: The error message template for when the rule fails

### ICompensatingAction
Represents a compensating action to execute on failure:
- `ActionType`: The action type
- `Configuration`: The action configuration
- `StepId`: The step ID that this action compensates for
- `BranchPath`: The branch path that this action compensates for
- `ExecutionOrder`: The execution order of this action

## Service Interfaces

### IService
Core service interface that all services must implement:
- `Id`: A unique identifier for this service
- `Name`: A human-readable name for this service
- `Description`: A description of this service
- `Version`: The version of this service
- `Initialize()`: Initializes the service with the specified configuration
- `Start()`: Starts the service
- `Stop()`: Stops the service
- `GetStatus()`: Gets the current status of the service
- `GetHealthStatus()`: Gets the health status of the service

### IImporterService
Interface for importer services that import data from external sources:
- `SourceType`: The source type that this importer supports
- `SupportedProtocols`: The supported protocols for this importer
- `SupportedDataFormats`: The supported data formats for this importer
- `ImportAsync()`: Imports data from the specified source
- `TestConnectionAsync()`: Tests the connection to the specified source
- `GetSchemaAsync()`: Gets the schema of the data from the specified source

### IProcessorService
Interface for processor services that process data:
- `ProcessorType`: The processor type that this processor implements
- `SupportedDataFormats`: The supported data formats for this processor
- `ProcessAsync()`: Processes data from the specified memory address
- `ValidateOptions()`: Validates the processing options
- `GetOutputSchema()`: Gets the schema of the output data based on the input schema and processing options

### IExporterService
Interface for exporter services that export data to external destinations:
- `DestinationType`: The destination type that this exporter supports
- `SupportedProtocols`: The supported protocols for this exporter
- `SupportedDataFormats`: The supported data formats for this exporter
- `ExportAsync()`: Exports data to the specified destination
- `TestConnectionAsync()`: Tests the connection to the specified destination
- `ValidateSchemaAsync()`: Validates that the data schema is compatible with the destination

### IManagerService
Interface for manager services that manage other services or resources:
- `ManagerType`: The manager type that this manager implements
- `GetManagedResourcesAsync()`: Gets the list of managed services or resources
- `GetResourceStatusAsync()`: Gets the status of a managed service or resource
- `StartResourceAsync()`: Starts a managed service or resource
- `StopResourceAsync()`: Stops a managed service or resource
- `ConfigureResourceAsync()`: Configures a managed service or resource
- `GetResourceConfigurationAsync()`: Gets the configuration of a managed service or resource

### AbstractServiceBase
Abstract base class that implements IService:
- Provides default implementations for the IService methods
- Includes constructors for easy service creation
- Manages service state (initialized, running, etc.)

## Protocol Interfaces

### IProtocol
Interface for protocols used for communication with external systems:
- `Id`: A unique identifier for this protocol
- `Name`: A human-readable name for this protocol
- `Description`: A description of this protocol
- `Version`: The version of this protocol
- `ProtocolType`: The protocol type (e.g., HTTP, FTP, JDBC, etc.)
- `SupportedDataFormats`: The supported data formats for this protocol
- `Capabilities`: The capabilities of this protocol
- `SecurityRequirements`: The security requirements for this protocol
- `CreateHandler()`: Creates a protocol handler for the specified configuration
- `ValidateConfiguration()`: Validates the configuration for this protocol

### IProtocolHandler
Interface for protocol handlers that handle communication with external systems:
- `Protocol`: The protocol that this handler implements
- `Configuration`: The configuration for this handler
- `ConnectionStatus`: The connection status of this handler
- `OpenConnectionAsync()`: Opens a connection to the external system
- `CloseConnectionAsync()`: Closes the connection to the external system
- `TestConnectionAsync()`: Tests the connection to the external system
- `ReadAsync()`: Reads data from the external system
- `WriteAsync()`: Writes data to the external system
- `DeleteAsync()`: Deletes a resource from the external system
- `ListAsync()`: Lists resources from the external system
- `QueryAsync()`: Executes a query on the external system

## Strategy Interfaces

### IRecoveryStrategy
Interface for recovery strategies that handle error recovery in flows:
- `Id`: A unique identifier for this recovery strategy
- `Name`: A human-readable name for this recovery strategy
- `Description`: A description of this recovery strategy
- `StrategyType`: The strategy type
- `CanHandle()`: Determines whether this strategy can handle the specified error
- `HandleAsync()`: Handles the specified error
- `ValidateConfiguration()`: Validates the configuration for this recovery strategy

## Abstract Classes

### AbstractEntity
Abstract base class that implements IEntity:
- Provides default implementations for the IEntity properties
- Includes constructors for easy entity creation

## Enums

- `StepType`: The type of a flow step (IMPORT, PROCESS, EXPORT)
- `MergeStrategy`: The strategy to use when merging branches (WaitForAll, FirstComplete, Conditional, PriorityBased)
- `ErrorHandlingStrategy`: The strategy to use when handling errors (FailImmediately, Retry, SkipAndContinue, CompensateAndFail, CompensateAndContinue)
- `ValidationSeverity`: The severity of a validation rule (Information, Warning, Error)
- `VersionStatus`: The status of a versioned entity (Active, Deprecated, Archived)
- `ServiceStatus`: The status of a service (NotInitialized, Initialized, Starting, Running, Stopping, Stopped, Failed)
- `HealthStatus`: The health status of a service (Healthy, Degraded, Unhealthy, Unknown)
- `ConnectionStatus`: The status of a connection (NotInitialized, Initialized, Connecting, Connected, Disconnecting, Disconnected, Failed)
- `SecurityLevel`: The security level of a protocol (None, Low, Medium, High, VeryHigh)
- `RecoveryAction`: The action taken during a recovery operation (None, Retry, Skip, AbortBranch, AbortFlow, Compensate, Redirect, Pause, RepairData, AlternativePath)
