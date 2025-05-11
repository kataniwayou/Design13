# FlowOrchestrator.Domain

This project contains the domain entities for the FlowOrchestrator system. These entities implement the interfaces defined in the FlowOrchestrator.Abstractions project and provide the core business logic for the system.

## Core Entities

### BaseEntity
The base implementation of the IEntity interface. It provides the basic properties that all entities must have:
- `Id`: A unique identifier for the entity
- `Name`: A human-readable name for the entity
- `Description`: A description of the entity

### VersionedEntity
Extends BaseEntity to add versioning support. This is used for entities that need to be versioned, such as flows and components:
- `Version`: The semantic version number (MAJOR.MINOR.PATCH)
- `CreatedTimestamp`: When the entity was first created
- `LastModifiedTimestamp`: When the entity was last modified
- `VersionDescription`: A human-readable description of this version
- `PreviousVersionId`: A reference to the previous version (if applicable)
- `VersionStatus`: The status of this version (ACTIVE, DEPRECATED, or ARCHIVED)

## Flow Entities

### Flow
Implements the IFlow interface. A flow defines a data processing workflow:
- `Steps`: The list of steps in the flow
- `Branches`: The list of branches in the flow
- `ValidationRules`: The validation rules for the flow
- `ErrorHandlingConfig`: The error handling configuration for the flow
- `MergeStrategyConfig`: The merge strategy configuration for branch convergence

### FlowStep
Implements the IFlowStep interface. A step in a flow:
- `StepType`: The type of the step (IMPORT, PROCESS, EXPORT)
- `StepId`: The position of the step within its branch
- `BranchPath`: The branch path to which this step belongs
- `ComponentId`: The component ID that implements this step
- `Configuration`: The configuration for this step

### FlowBranch
Implements the IFlowBranch interface. A branch in a flow:
- `BranchPath`: The path of the branch (e.g., main, branchA, branchB)
- `ParentBranchPath`: The parent branch path (null for the main branch)
- `ParentStepId`: The step ID in the parent branch where this branch splits off
- `Steps`: The list of steps in this branch
- `Priority`: The branch priority for resource allocation
- `MergeConfig`: The merge configuration for this branch

## Configuration Entities

### MergeConfig
Implements the IMergeConfig interface. The configuration for merging a branch:
- `TargetBranchPath`: The target branch path where this branch merges back
- `TargetStepId`: The step ID in the target branch where this branch merges back
- `MergeStrategy`: The merge strategy to use when merging this branch
- `MergeStrategyConfiguration`: The configuration for the merge strategy

### MergeStrategyConfig
Implements the IMergeStrategyConfig interface. The flow-level merge strategy configuration:
- `DefaultMergeStrategy`: The default merge strategy to use for branches that don't specify one
- `DefaultMergeStrategyConfiguration`: The default configuration for the default merge strategy
- `BranchMergeConfigs`: The branch-specific merge strategy overrides

### ErrorHandlingConfig
Implements the IErrorHandlingConfig interface. The flow-level error handling configuration:
- `DefaultStrategy`: The default error handling strategy to use for steps that don't specify one
- `MaxRetryAttempts`: The maximum number of retry attempts for retryable errors
- `RetryDelayMs`: The retry delay in milliseconds
- `UseExponentialBackoff`: Whether to use exponential backoff for retries
- `StepErrorHandlingStrategies`: The step-specific error handling strategy overrides
- `CompensatingActions`: The compensating actions to execute on failure

## Validation Entities

### ValidationRule
Implements the IValidationRule interface. A validation rule in a flow:
- `RuleType`: The rule type
- `Configuration`: The rule configuration
- `Severity`: The severity of the rule
- `ErrorMessageTemplate`: The error message template for when the rule fails

### CompensatingAction
Implements the ICompensatingAction interface. A compensating action to execute on failure:
- `ActionType`: The action type
- `Configuration`: The action configuration
- `StepId`: The step ID that this action compensates for
- `BranchPath`: The branch path that this action compensates for
- `ExecutionOrder`: The execution order of this action

## Processing Entities

### ProcessingChainEntity
Represents a processing chain that defines a sequence of processing steps:
- `Steps`: The list of steps in the processing chain
- `InputSchema`: The input schema for the processing chain
- `OutputSchema`: The output schema for the processing chain
- `ValidationRules`: The validation rules for the processing chain
- `ErrorHandlingConfig`: The error handling configuration for the processing chain

### ProcessingStepEntity
Represents a step in a processing chain:
- `StepOrder`: The order of this step in the processing chain
- `ProcessorId`: The processor component ID that implements this step
- `Configuration`: The configuration for this step
- `InputSchema`: The input schema for this step
- `OutputSchema`: The output schema for this step
- `IsEnabled`: Whether this step is enabled
- `ErrorHandlingStrategy`: The error handling strategy for this step

## Source and Destination Entities

### SourceEntity
Represents a data source in the system:
- `SourceType`: The source type
- `Protocol`: The protocol used to connect to the source
- `ConnectionConfiguration`: The connection string or configuration for the source
- `AuthenticationConfiguration`: The authentication configuration for the source
- `Schema`: The schema of the data provided by this source
- `DataFormat`: The data format provided by this source
- `ImporterServiceId`: The importer service ID that can import from this source
- `Metadata`: The metadata associated with this source

### DestinationEntity
Represents a data destination in the system:
- `DestinationType`: The destination type
- `Protocol`: The protocol used to connect to the destination
- `ConnectionConfiguration`: The connection string or configuration for the destination
- `AuthenticationConfiguration`: The authentication configuration for the destination
- `Schema`: The schema of the data expected by this destination
- `DataFormat`: The data format expected by this destination
- `ExporterServiceId`: The exporter service ID that can export to this destination
- `Metadata`: The metadata associated with this destination

### SourceAssignmentEntity
Represents an assignment of a source to a flow:
- `FlowId`: The flow ID
- `SourceId`: The source ID
- `StepId`: The step ID in the flow where this source is used
- `BranchPath`: The branch path in the flow where this source is used
- `Configuration`: The configuration for this source assignment
- `Filter`: The filter to apply to the source data
- `Transformation`: The transformation to apply to the source data
- `IsEnabled`: Whether this source assignment is enabled

### DestinationAssignmentEntity
Represents an assignment of a destination to a flow:
- `FlowId`: The flow ID
- `DestinationId`: The destination ID
- `StepId`: The step ID in the flow where this destination is used
- `BranchPath`: The branch path in the flow where this destination is used
- `Configuration`: The configuration for this destination assignment
- `Filter`: The filter to apply to the data before exporting
- `Transformation`: The transformation to apply to the data before exporting
- `IsEnabled`: Whether this destination assignment is enabled

## Scheduling Entities

### TaskSchedulerEntity
Represents a task scheduler that manages scheduled flows:
- `ScheduledFlows`: The list of scheduled flows managed by this scheduler
- `MaxConcurrentFlows`: The maximum number of concurrent flows that can be executed
- `PollingIntervalSeconds`: The polling interval in seconds
- `IsEnabled`: Whether this scheduler is enabled
- `TimeZoneId`: The time zone ID for this scheduler
- `Configuration`: The configuration for this scheduler

### ScheduledFlowEntity
Represents a scheduled flow in the system:
- `FlowId`: The flow ID
- `SchedulerId`: The scheduler ID
- `CronExpression`: The CRON expression for the schedule
- `StartDateTime`: The start date and time for the schedule
- `EndDateTime`: The end date and time for the schedule
- `MaxExecutions`: The maximum number of executions
- `CurrentExecutions`: The current number of executions
- `LastExecutionDateTime`: The last execution date and time
- `NextExecutionDateTime`: The next execution date and time
- `IsEnabled`: Whether this scheduled flow is enabled
- `Priority`: The priority of this scheduled flow
- `Configuration`: The configuration for this scheduled flow

## Execution Entities

### ExecutionContext
Represents the execution context for a flow:
- `FlowId`: The flow ID
- `FlowVersion`: The flow version
- `ExecutionId`: The execution ID
- `ParentExecutionId`: The parent execution ID (if this is a sub-flow execution)
- `StartTime`: The execution start time
- `EndTime`: The execution end time
- `Status`: The execution status
- `ErrorMessage`: The error message if the execution failed
- `BranchExecutionContexts`: The list of branch execution contexts
- `Parameters`: The execution parameters
- `Metrics`: The execution metrics
- `Tags`: The execution tags
- `UserId`: The user ID who initiated the execution
- `CorrelationId`: The correlation ID for distributed tracing

### BranchExecutionContext
Represents the execution context for a branch in a flow:
- `ExecutionContextId`: The execution context ID
- `BranchPath`: The branch path
- `ParentBranchPath`: The parent branch path
- `ParentStepId`: The parent step ID where this branch splits off
- `CurrentStepId`: The current step ID being executed
- `StartTime`: The execution start time
- `EndTime`: The execution end time
- `Status`: The execution status
- `ErrorMessage`: The error message if the execution failed
- `StepExecutionContexts`: The list of step execution contexts
- `MemoryAddress`: The memory address for the branch data
- `Metrics`: The branch execution metrics

### StepExecutionContext
Represents the execution context for a step in a flow:
- `BranchExecutionContextId`: The branch execution context ID
- `StepId`: The step ID
- `StepType`: The step type
- `ComponentId`: The component ID that implements this step
- `StartTime`: The execution start time
- `EndTime`: The execution end time
- `Status`: The execution status
- `ErrorMessage`: The error message if the execution failed
- `RetryAttempts`: The number of retry attempts
- `InputMemoryAddress`: The input memory address
- `OutputMemoryAddress`: The output memory address
- `Metrics`: The step execution metrics
- `Logs`: The step execution logs

### DataPackage
Represents a package of data that flows through the system:
- `MemoryAddress`: The memory address where the data is stored
- `DataFormat`: The data format
- `Schema`: The schema of the data
- `SizeInBytes`: The size of the data in bytes
- `RecordCount`: The number of records in the data
- `CreationTimestamp`: The creation timestamp
- `ExpirationTimestamp`: The expiration timestamp
- `SourceStepId`: The source step ID that produced this data
- `SourceBranchPath`: The source branch path that produced this data
- `ExecutionContextId`: The execution context ID associated with this data
- `Metadata`: The metadata associated with this data
- `Tags`: The tags associated with this data
