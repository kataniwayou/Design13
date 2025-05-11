# FlowOrchestrator Abstract Class Relationships

This document provides a textual representation of the relationships between abstract classes in the FlowOrchestrator system, including implementation details from the documentation, project locations, and common relationships between components.

## Project Structure Overview

The FlowOrchestrator system is organized into the following project categories:

- **Core**: Fundamental abstractions, interfaces, and domain models
  - `FlowOrchestrator.Abstractions`: Core interfaces and abstract classes
  - `FlowOrchestrator.Domain`: Domain models and entities
  - `FlowOrchestrator.Common`: Common utilities and helpers

- **Integration**: Components for integrating with external systems
  - `FlowOrchestrator.Importers`: Importer service implementations
  - `FlowOrchestrator.Exporters`: Exporter service implementations
  - `FlowOrchestrator.Protocols`: Protocol implementations

- **Processing**: Components for processing and transforming data
  - `FlowOrchestrator.Processors`: Processor service implementations

- **Execution**: Components responsible for orchestrating and executing flows
  - `FlowOrchestrator.Orchestrator`: Orchestrator service
  - `FlowOrchestrator.MemoryManager`: Memory management service
  - `FlowOrchestrator.BranchController`: Branch control service

- **Management**: Components for managing the system
  - `FlowOrchestrator.ServiceManagement`: Service manager implementations
  - `FlowOrchestrator.EntityManagement`: Entity manager implementations
  - `FlowOrchestrator.TaskScheduler`: Task scheduling service

## Core Abstract Classes

```
AbstractServiceBase (Core/FlowOrchestrator.Abstractions/Services)
└── Foundation for all service-oriented components
    ├── Provides lifecycle management
    │   └── Manages state transitions (UNINITIALIZED, INITIALIZING, READY, PROCESSING, ERROR, TERMINATING, TERMINATED)
    ├── Integrates with OpenTelemetry for observability
    │   ├── Logging through ILogger
    │   ├── Metrics through Meter
    │   └── Distributed tracing through Tracer
    ├── Integrates with MassTransit for messaging
    │   ├── Message publishing through IPublishEndpoint
    │   └── Standardized message handling patterns
    └── Manages service state and versioning
        ├── Service identification (ServiceId, Version, ServiceType)
        ├── Version compatibility (GetVersionInfo, GetCompatibilityMatrix)
        └── Configuration validation (ValidateConfiguration)

AbstractEntity (Core/FlowOrchestrator.Abstractions/Entities)
└── Foundation for all domain entities
    ├── Provides version properties
    │   ├── Version (semantic versioning: MAJOR.MINOR.PATCH)
    │   ├── CreatedTimestamp and LastModifiedTimestamp
    │   ├── VersionDescription and PreviousVersionId
    │   └── VersionStatus (ACTIVE, DEPRECATED, ARCHIVED)
    ├── Defines identity management
    │   ├── GetEntityId() for unique identification
    │   └── GetEntityType() for entity classification
    ├── Implements validation framework
    │   └── Validate() for entity-specific validation rules
    └── Supports change tracking
        ├── IsModified() to check modification state
        ├── SetModified() to mark as modified
        └── ClearModified() to reset modification state
```

## Service-Oriented Abstract Classes

```
AbstractServiceBase (Core/FlowOrchestrator.Abstractions/Services)
├── AbstractImporterService (Integration/FlowOrchestrator.Abstractions.Integration/Services)
│   └── Base for all importer services
│       ├── Implements IConsumer<ImportCommand> for message-based import operations
│       │   ├── Consumes ImportCommand messages
│       │   ├── Publishes ImportCommandResult messages for successful operations
│       │   └── Publishes ImportCommandError messages for failed operations
│       ├── Defines protocol-specific functionality
│       │   ├── Protocol property identifies the supported protocol
│       │   ├── GetCapabilities() returns protocol capabilities
│       │   └── Validates protocol-specific parameters
│       ├── Provides data import operations
│       │   ├── Import(ImportParameters, ExecutionContext) retrieves data from sources
│       │   ├── Generates source metadata during import
│       │   └── Validates imported data
│       └── Implements error handling and recovery
│           ├── ClassifyException() categorizes errors
│           ├── GetErrorDetails() extracts diagnostic information
│           └── TryRecover() attempts to recover from errors
│
├── AbstractProcessorService (Processing/FlowOrchestrator.Abstractions.Processing/Services)
│   └── Base for all processor services
│       ├── Implements IConsumer<ProcessCommand> for message-based processing
│       │   ├── Consumes ProcessCommand messages
│       │   ├── Publishes ProcessCommandResult messages for successful operations
│       │   └── Publishes ProcessCommandError messages for failed operations
│       ├── Defines data transformation functionality
│       │   ├── Process(ProcessParameters, ExecutionContext) transforms input data
│       │   ├── Generates transformation metadata
│       │   └── Validates transformed data
│       ├── Manages input/output schemas
│       │   ├── GetInputSchema() defines expected input format
│       │   ├── GetOutputSchema() defines produced output format
│       │   └── Validates schema compatibility
│       └── Implements error handling and recovery
│           ├── ClassifyException() categorizes errors
│           ├── GetErrorDetails() extracts diagnostic information
│           └── TryRecover() attempts to recover from errors
│
├── AbstractExporterService (Integration/FlowOrchestrator.Abstractions.Integration/Services)
│   └── Base for all exporter services
│       ├── Implements IConsumer<ExportCommand> for message-based export operations
│       │   ├── Consumes ExportCommand messages
│       │   ├── Publishes ExportCommandResult messages for successful operations
│       │   └── Publishes ExportCommandError messages for failed operations
│       ├── Defines protocol-specific functionality
│       │   ├── Protocol property identifies the supported protocol
│       │   ├── GetCapabilities() returns protocol capabilities
│       │   └── Validates protocol-specific parameters
│       ├── Provides branch merging capabilities
│       │   ├── MergeBranches() combines data from multiple branches
│       │   ├── GetMergeCapabilities() defines supported merge strategies
│       │   └── Validates branch data compatibility
│       └── Implements error handling and recovery
│           ├── ClassifyException() categorizes errors
│           ├── GetErrorDetails() extracts diagnostic information
│           └── TryRecover() attempts to recover from errors
│
├── AbstractManagerService<TService, TServiceId> (Management/FlowOrchestrator.Abstractions.Management/Services)
│   └── Base for all manager services (both service managers and entity managers)
│       ├── Implements IConsumer<ServiceRegistrationCommand<TService>> for service registration
│       │   ├── Consumes ServiceRegistrationCommand messages
│       │   ├── Publishes ServiceRegistrationResult messages
│       │   └── Publishes ServiceStatusChanged events
│       ├── Provides CRUD operations framework
│       │   ├── Create: Register(TService) adds services/entities to registry
│       │   ├── Read: GetService(TServiceId, string) retrieves specific versions
│       │   ├── Read: GetAllServices() retrieves all registered services/entities
│       │   ├── Update: UpdateServiceStatus() and other update methods
│       │   └── Delete: UnregisterService(TServiceId, string) removes services/entities
│       ├── Handles lifecycle management
│       │   ├── GetServiceStatus(TServiceId, string) retrieves state
│       │   ├── UpdateServiceStatus(TServiceId, string, ServiceStatus) updates state
│       │   └── Validate(TService) verifies compatibility and integrity
│       └── Maintains registry
│           └── _registry dictionary stores items by ID and version
│
├── AbstractOrchestratorService (Execution/FlowOrchestrator.Abstractions.Execution/Services)
│   └── Base for orchestrator services
│       ├── Implements IConsumer<FlowExecutionCommand> for flow execution
│       │   ├── Consumes FlowExecutionCommand messages
│       │   └── Publishes flow execution events
│       ├── Implements IConsumer<FlowControlCommand> for flow control
│       │   ├── Consumes FlowControlCommand messages
│       │   └── Publishes flow control events
│       ├── Coordinates flow execution
│       │   ├── ExecuteFlow(FlowExecutionParameters) orchestrates end-to-end flow
│       │   ├── Publishes commands to importers, processors, and exporters
│       │   └── Tracks flow execution state
│       └── Manages branch creation and completion
│           ├── CreateBranch(string, string) initializes parallel execution paths
│           ├── CompleteBranch(string, string) finalizes branch execution
│           └── Publishes branch-related events
│
├── AbstractMemoryManagerService (Execution/FlowOrchestrator.Abstractions.Execution/Services)
│   └── Base for memory manager services
│       ├── Implements IConsumer<MemoryOperationCommand> for memory operations
│       │   ├── Consumes MemoryOperationCommand messages
│       │   └── Publishes memory operation results
│       ├── Manages shared memory operations
│       │   ├── StoreData(string, object) saves data to shared memory
│       │   ├── RetrieveData<T>(string) retrieves data from shared memory
│       │   └── ClearData(string) removes data from shared memory
│       └── Handles memory lifecycle
│           ├── Tracks memory usage
│           ├── Implements memory cleanup strategies
│           └── Publishes memory cleanup events
│
├── AbstractBranchControllerService (Execution/FlowOrchestrator.Abstractions.Execution/Services)
│   └── Base for branch controller services
│       ├── Implements IConsumer<BranchControlCommand> for branch control
│       │   ├── Consumes BranchControlCommand messages
│       │   └── Publishes branch execution events
│       ├── Manages branch execution
│       │   ├── InitializeBranch(string, string) prepares branch for execution
│       │   ├── ExecuteBranch(string, string) runs branch operations
│       │   └── Publishes branch execution results
│       └── Tracks branch status
│           ├── GetBranchStatus(string, string) retrieves branch state
│           └── Publishes branch status change events
│
└── AbstractTaskSchedulerService (Management/FlowOrchestrator.Abstractions.Management/Services)
    └── Base for task scheduler services
        ├── Implements IConsumer<ScheduleCommand> for schedule creation
        │   ├── Consumes ScheduleCommand messages
        │   └── Publishes schedule creation results
        ├── Implements IConsumer<ScheduleModificationCommand> for schedule updates
        │   ├── Consumes ScheduleModificationCommand messages
        │   └── Publishes schedule modification results
        ├── Manages flow execution scheduling
        │   ├── ScheduleFlow(ScheduleParameters) creates execution schedules
        │   ├── CancelSchedule(string) removes schedules
        │   └── Publishes FlowExecutionCommand messages when triggered
        └── Handles schedule lifecycle
            ├── GetScheduleStatus(string) retrieves schedule state
            └── Publishes schedule status change events
```

## Entity Abstract Classes

```
AbstractEntity (Core/FlowOrchestrator.Abstractions/Entities)
├── AbstractFlowEntity (Core/FlowOrchestrator.Domain/Entities)
│   └── Base for flow entities
│       ├── Defines flow structure
│       │   ├── FlowId uniquely identifies the flow
│       │   ├── Description provides human-readable information
│       │   ├── ImporterServiceId and ImporterServiceVersion identify entry point
│       │   └── Connections define data paths between components
│       ├── References processing chains
│       │   ├── ProcessingChains list contains references to chains
│       │   └── Each chain defines a transformation sequence
│       ├── Connects importers and exporters
│       │   ├── Begins with exactly one importer service
│       │   ├── Exporters list contains references to export endpoints
│       │   └── All data paths must terminate with an exporter
│       └── Implemented by FlowEntity (concrete)
│           ├── Defines complete data pipeline
│           ├── References ProcessingChainEntity
│           └── Connects sources to destinations
│
├── AbstractSourceEntity (Integration/FlowOrchestrator.Domain.Integration/Entities)
│   └── Base for source entities
│       ├── Defines source location
│       │   ├── SourceId uniquely identifies the source
│       │   ├── Address specifies location of source data
│       │   └── Ensures address uniqueness (protocol + address + version)
│       ├── Specifies connection protocol
│       │   ├── Protocol identifies the access method
│       │   └── ConnectionParameters configure protocol-specific settings
│       └── Provides abstraction for data retrieval
│           ├── Includes validation interfaces for configuration
│           └── Standardizes access to different source types
│
├── AbstractDestinationEntity (Integration/FlowOrchestrator.Domain.Integration/Entities)
│   └── Base for destination entities
│       ├── Defines destination location
│       │   ├── DestinationId uniquely identifies the destination
│       │   ├── Address specifies delivery location
│       │   └── Ensures address uniqueness (protocol + address + version)
│       ├── Specifies delivery protocol
│       │   ├── Protocol identifies the delivery method
│       │   └── ConnectionParameters configure protocol-specific settings
│       └── Provides abstraction for data delivery
│           ├── Includes validation interfaces for configuration
│           └── Standardizes delivery to different destination types
│
├── AbstractSourceAssignmentEntity (Integration/FlowOrchestrator.Domain.Integration/Entities)
│   └── Base for source assignment entities
│       ├── Links sources to importers
│       │   ├── Connects a source entity to an importer service
│       │   ├── Ensures uniqueness (each source used in only one assignment)
│       │   └── Provides connection-specific configuration
│       ├── Validates protocol compatibility
│       │   ├── Ensures importer supports source protocol
│       │   └── Validates connection parameters
│       └── Provides standardized abstraction for source-importer relationships
│           ├── Includes configuration validation interfaces
│           └── Enables consistent source connection management
│
├── AbstractDestinationAssignmentEntity (Integration/FlowOrchestrator.Domain.Integration/Entities)
│   └── Base for destination assignment entities
│       ├── Links destinations to exporters
│       │   ├── Connects a destination entity to an exporter service
│       │   ├── Ensures uniqueness (each destination used in only one assignment)
│       │   └── Provides delivery-specific configuration
│       ├── Validates protocol compatibility
│       │   ├── Ensures exporter supports destination protocol
│       │   └── Validates connection parameters
│       └── Provides standardized abstraction for destination-exporter relationships
│           ├── Includes configuration validation interfaces
│           └── Enables consistent destination delivery management
│
├── AbstractScheduledFlowEntity (Management/FlowOrchestrator.Domain.Management/Entities)
│   └── Base for scheduled flow entities
│       ├── Combines flow, source, and destination
│       │   ├── References source assignment, destination assignment, and flow entity
│       │   ├── Creates a complete executable flow unit
│       │   └── Ensures uniqueness of assignments
│       ├── Adds scheduling parameters
│       │   ├── Contains configuration for task scheduler activation
│       │   ├── Provides interface for execution status tracking
│       │   └── Specifies contract for flow execution parameters
│       └── Enables automated flow execution
│           ├── Links to task scheduler for execution timing
│           └── Tracks execution history
│
└── AbstractTaskSchedulerEntity (Management/FlowOrchestrator.Domain.Management/Entities)
    └── Base for task scheduler entities
        ├── Defines scheduling parameters
        │   ├── Supports one-time and recurring schedules
        │   ├── Configures execution timing
        │   └── Specifies execution conditions
        ├── Manages execution triggers
        │   ├── Defines when flows should execute
        │   ├── Supports various scheduling patterns
        │   └── Handles schedule activation and deactivation
        └── Provides standardized scheduling abstraction
            ├── Includes validation for scheduling parameters
            └── Enables consistent schedule management
```

## Protocol Abstract Classes

```
AbstractProtocol (Integration/FlowOrchestrator.Abstractions.Integration/Protocols)
└── Base for all protocol implementations
    ├── Defines protocol identification
    │   ├── Name property identifies the protocol
    │   └── Description provides human-readable information
    ├── Specifies capability discovery
    │   ├── GetCapabilities() returns supported features
    │   └── Defines protocol-specific operations
    ├── Manages connection parameters
    │   ├── GetConnectionParameters() returns required parameters
    │   ├── ValidateConnectionParameters() verifies parameter values
    │   └── CreateHandler() instantiates protocol-specific handlers
    └── Implemented by concrete protocol classes
        └── FileProtocol and other protocol implementations

AbstractProtocolHandler (Integration/FlowOrchestrator.Abstractions.Integration/Protocols)
└── Base for all protocol handlers
    ├── Manages connections
    │   ├── Connect() establishes connection to external system
    │   └── Disconnect() terminates connection
    ├── Handles data operations
    │   ├── Retrieve() gets data from external system
    │   └── Deliver() sends data to external system
    ├── Provides error classification
    │   ├── ClassifyException() categorizes errors
    │   └── GetErrorDetails() extracts diagnostic information
    └── Implemented by concrete handler classes
        └── FileProtocolHandler and other handler implementations
```

## Strategy Abstract Classes

```
AbstractMergeStrategy (Integration/FlowOrchestrator.Abstractions.Integration/Strategies)
└── Base for all merge strategies
    ├── Defines branch merging logic
    │   ├── StrategyName identifies the strategy
    │   ├── Description provides human-readable information
    │   └── MergeData() combines data from multiple branches
    ├── Validates input compatibility
    │   ├── ValidateInputs() verifies branch data compatibility
    │   └── Ensures data can be merged according to strategy rules
    ├── Specifies output schema
    │   ├── GetOutputSchema() defines resulting data structure
    │   └── Ensures consistent output format
    └── Implemented by concrete strategy classes
        ├── UnionMergeStrategy combines all data
        ├── IntersectionMergeStrategy keeps only common data
        ├── SequentialMergeStrategy orders data by branch
        ├── PriorityMergeStrategy applies priority rules
        └── CustomMergeStrategy for specialized merging
```

## Concrete Implementations

```
ProcessingChainEntity (concrete) (Core/FlowOrchestrator.Domain/Entities)
└── Implements a directed acyclic graph of processors
    ├── Contains processor references
    │   ├── Processors list defines transformation sequence
    │   └── Each processor performs specific data transformation
    ├── Defines branch paths
    │   ├── Branches dictionary maps branch names to definitions
    │   └── Each branch represents a parallel processing path
    ├── Ensures forward-only data flow
    │   ├── Prevents cycles in processing graph
    │   └── Validates processor compatibility
    └── Managed by ProcessingChainEntityManager

FlowEntity (concrete) (Core/FlowOrchestrator.Domain/Entities)
└── Implements AbstractFlowEntity
    ├── Defines complete data pipeline
    │   ├── Implements all AbstractFlowEntity properties
    │   └── Provides concrete validation logic
    ├── References ProcessingChainEntity
    │   ├── Links to concrete processing chain implementations
    │   └── Configures chain execution order
    ├── Connects sources to destinations
    │   ├── Defines complete end-to-end data flow
    │   └── Ensures all paths are properly connected
    └── Managed by FlowEntityManager

Manager Services (concrete)
├── Service Manager Implementations
│   ├── ImporterServiceManager (Management/FlowOrchestrator.ServiceManagement/Services)
│   │   └── Applies CRUD operations on AbstractImporterService implementations
│   │       ├── Creates, reads, updates, and deletes importer services
│   │       └── Validates interface compliance and protocol implementation
│   ├── ProcessorServiceManager (Management/FlowOrchestrator.ServiceManagement/Services)
│   │   └── Applies CRUD operations on AbstractProcessorService implementations
│   │       ├── Creates, reads, updates, and deletes processor services
│   │       └── Validates interface compliance and schema compatibility
│   └── ExporterServiceManager (Management/FlowOrchestrator.ServiceManagement/Services)
│       └── Applies CRUD operations on AbstractExporterService implementations
│           ├── Creates, reads, updates, and deletes exporter services
│           └── Validates interface compliance and protocol implementation
│
└── Entity Manager Implementations
    ├── FlowEntityManager (Management/FlowOrchestrator.EntityManagement/Services)
    │   └── Applies CRUD operations on FlowEntity instances
    │       ├── Creates, reads, updates, and deletes flow entities
    │       └── Validates flow entity integrity
    ├── ProcessingChainEntityManager (Management/FlowOrchestrator.EntityManagement/Services)
    │   └── Applies CRUD operations on ProcessingChainEntity instances
    │       ├── Creates, reads, updates, and deletes processing chain entities
    │       └── Validates processing chain integrity
    ├── SourceEntityManager (Management/FlowOrchestrator.EntityManagement/Services)
    │   └── Applies CRUD operations on AbstractSourceEntity implementations
    │       ├── Creates, reads, updates, and deletes source entities
    │       └── Validates source entity integrity
    ├── DestinationEntityManager (Management/FlowOrchestrator.EntityManagement/Services)
    │   └── Applies CRUD operations on AbstractDestinationEntity implementations
    │       ├── Creates, reads, updates, and deletes destination entities
    │       └── Validates destination entity integrity
    ├── SourceAssignmentEntityManager (Management/FlowOrchestrator.EntityManagement/Services)
    │   └── Applies CRUD operations on AbstractSourceAssignmentEntity implementations
    │       ├── Creates, reads, updates, and deletes source assignment entities
    │       └── Validates source-importer compatibility
    ├── DestinationAssignmentEntityManager (Management/FlowOrchestrator.EntityManagement/Services)
    │   └── Applies CRUD operations on AbstractDestinationAssignmentEntity implementations
    │       ├── Creates, reads, updates, and deletes destination assignment entities
    │       └── Validates destination-exporter compatibility
    ├── TaskSchedulerEntityManager (Management/FlowOrchestrator.EntityManagement/Services)
    │   └── Applies CRUD operations on AbstractTaskSchedulerEntity implementations
    │       ├── Creates, reads, updates, and deletes task scheduler entities
    │       └── Validates scheduler configuration
    └── ScheduledFlowEntityManager (Management/FlowOrchestrator.EntityManagement/Services)
        └── Applies CRUD operations on AbstractScheduledFlowEntity implementations
            ├── Creates, reads, updates, and deletes scheduled flow entities
            └── Validates scheduled flow integrity
```

## Key Relationships

### Service Relationships

```
AbstractTaskSchedulerService
└── Triggers flow execution via
    └── AbstractOrchestratorService
        ├── Coordinates data flow through
        │   ├── AbstractImporterService
        │   │   └── Retrieves data from external sources using protocol handlers
        │   ├── AbstractProcessorService
        │   │   └── Transforms data according to processing chain definition
        │   └── AbstractExporterService
        │       └── Delivers data to external destinations using protocol handlers
        ├── Manages memory via
        │   └── AbstractMemoryManagerService
        │       └── Provides shared memory for efficient data transfer between services
        └── Controls parallel execution via
            └── AbstractBranchControllerService
                └── Manages execution of parallel processing branches
```

### Entity Relationships

```
AbstractFlowEntity / FlowEntity (concrete)
├── References
│   ├── ProcessingChainEntity (concrete)
│   │   ├── Defines the data transformation logic
│   │   └── Contains processor references and branch definitions
│   ├── AbstractSourceAssignmentEntity
│   │   ├── Links flow to data sources
│   │   └── References AbstractSourceEntity
│   │       └── Defines source location and protocol
│   └── AbstractDestinationAssignmentEntity
│       ├── Links flow to data destinations
│       └── References AbstractDestinationEntity
│           └── Defines destination location and protocol
└── Referenced by
    └── AbstractScheduledFlowEntity
        ├── Combines flow with scheduling information
        └── References AbstractTaskSchedulerEntity
            └── Defines when the flow should execute
```

### Management Relationships

```
AbstractManagerService<TService, TServiceId>
├── Service Manager Implementations
│   ├── ImporterServiceManager (concrete)
│   │   └── Applies CRUD operations on importer services
│   │       ├── Create() registers new importer service implementations
│   │       ├── Read() retrieves existing importer services
│   │       ├── Update() updates existing importer service configurations
│   │       ├── Delete() removes deprecated importer service implementations
│   │       └── Validate() verifies interface compliance and protocol implementation
│   │
│   ├── ProcessorServiceManager (concrete)
│   │   └── Applies CRUD operations on processor services
│   │       ├── Create() registers new processor service implementations
│   │       ├── Read() retrieves existing processor services
│   │       ├── Update() updates existing processor service configurations
│   │       ├── Delete() removes deprecated processor service implementations
│   │       └── Validate() verifies interface compliance and schema compatibility
│   │
│   └── ExporterServiceManager (concrete)
│       └── Applies CRUD operations on exporter services
│           ├── Create() registers new exporter service implementations
│           ├── Read() retrieves existing exporter services
│           ├── Update() updates existing exporter service configurations
│           ├── Delete() removes deprecated exporter service implementations
│           └── Validate() verifies interface compliance and protocol implementation
│
└── Entity Manager Implementations
    ├── FlowEntityManager (concrete)
    │   └── Applies CRUD operations on FlowEntity instances
    │       ├── Create() creates new flow entities
    │       ├── Read() retrieves existing flow entities
    │       ├── Update() modifies existing flow entities
    │       ├── Delete() removes flow entities
    │       └── Validate() verifies flow entity integrity
    │
    ├── ProcessingChainEntityManager (concrete)
    │   └── Applies CRUD operations on ProcessingChainEntity instances
    │       ├── Create() creates new processing chain entities
    │       ├── Read() retrieves existing processing chain entities
    │       ├── Update() modifies existing processing chain entities
    │       ├── Delete() removes processing chain entities
    │       └── Validate() verifies processing chain integrity
    │
    ├── SourceEntityManager (concrete)
    │   └── Applies CRUD operations on source entities
    │       ├── Create() creates new source entities
    │       ├── Read() retrieves existing source entities
    │       ├── Update() modifies existing source entities
    │       ├── Delete() removes source entities
    │       └── Validate() verifies source entity integrity
    │
    ├── DestinationEntityManager (concrete)
    │   └── Applies CRUD operations on destination entities
    │       ├── Create() creates new destination entities
    │       ├── Read() retrieves existing destination entities
    │       ├── Update() modifies existing destination entities
    │       ├── Delete() removes destination entities
    │       └── Validate() verifies destination entity integrity
    │
    ├── SourceAssignmentEntityManager (concrete)
    │   └── Applies CRUD operations on source assignment entities
    │       ├── Create() creates new source assignment entities
    │       ├── Read() retrieves existing source assignment entities
    │       ├── Update() modifies existing source assignment entities
    │       ├── Delete() removes source assignment entities
    │       └── Validate() verifies source-importer compatibility
    │
    ├── DestinationAssignmentEntityManager (concrete)
    │   └── Applies CRUD operations on destination assignment entities
    │       ├── Create() creates new destination assignment entities
    │       ├── Read() retrieves existing destination assignment entities
    │       ├── Update() modifies existing destination assignment entities
    │       ├── Delete() removes destination assignment entities
    │       └── Validate() verifies destination-exporter compatibility
    │
    ├── TaskSchedulerEntityManager (concrete)
    │   └── Applies CRUD operations on task scheduler entities
    │       ├── Create() creates new task scheduler entities
    │       ├── Read() retrieves existing task scheduler entities
    │       ├── Update() modifies existing task scheduler entities
    │       ├── Delete() removes task scheduler entities
    │       └── Validate() verifies scheduler configuration
    │
    └── ScheduledFlowEntityManager (concrete)
        └── Applies CRUD operations on scheduled flow entities
            ├── Create() creates new scheduled flow entities
            ├── Read() retrieves existing scheduled flow entities
            ├── Update() modifies existing scheduled flow entities
            ├── Delete() removes scheduled flow entities
            └── Validate() verifies scheduled flow integrity
```

### Infrastructure Integration

```
OpenTelemetry
└── Integrated with AbstractServiceBase
    └── Provides
        ├── Logging
        │   ├── Structured logging with context enrichment
        │   ├── Log level configuration based on environment
        │   └── Correlation ID propagation across service boundaries
        ├── Metrics
        │   ├── Service-level metrics (requests, latency, errors)
        │   ├── Custom business metrics specific to service type
        │   └── Histogram metrics for performance distribution analysis
        └── Distributed tracing
            ├── Trace context propagation across service boundaries
            ├── Span creation for key operations
            └── Attribute enrichment for spans

MassTransit
└── Integrated with AbstractServiceBase
    └── Provides
        ├── Message consumption
        │   ├── Consumer registration and lifecycle management
        │   ├── Error handling and retry policies
        │   └── Consumer health monitoring
        ├── Message publishing
        │   ├── Outbox pattern for reliable messaging
        │   ├── Message versioning support
        │   └── Correlation ID propagation
        └── Request-response patterns
            ├── Synchronous request-response operations
            ├── Timeout handling
            └── Circuit breaker patterns
```

### Interface Implementations

```
IService
└── Implemented by AbstractServiceBase
    ├── Defines core service contract
    │   ├── ServiceId, Version, ServiceType properties
    │   ├── Initialize() and Terminate() lifecycle methods
    │   ├── GetState() for status reporting
    │   └── ValidateConfiguration() for parameter validation
    └── Inherited by all service abstract classes

IConsumer<TMessage> (MassTransit)
├── IConsumer<ImportCommand>
│   └── Implemented by AbstractImporterService
├── IConsumer<ProcessCommand>
│   └── Implemented by AbstractProcessorService
├── IConsumer<ExportCommand>
│   └── Implemented by AbstractExporterService
├── IConsumer<ServiceRegistrationCommand<TService>>
│   └── Implemented by AbstractManagerService<TService, TServiceId>
├── IConsumer<FlowExecutionCommand>
│   └── Implemented by AbstractOrchestratorService
├── IConsumer<MemoryOperationCommand>
│   └── Implemented by AbstractMemoryManagerService
├── IConsumer<BranchControlCommand>
│   └── Implemented by AbstractBranchControllerService
└── IConsumer<ScheduleCommand>
    └── Implemented by AbstractTaskSchedulerService
```

### Cross-Cutting Concerns

```
Resilience Patterns
├── Implemented across all service abstract classes
│   ├── Retry Pattern
│   │   ├── Configurable retry policies
│   │   ├── Exponential backoff strategies
│   │   └── Circuit breaker integration
│   ├── Fallback Pattern
│   │   ├── Alternative processing paths
│   │   ├── Degraded functionality modes
│   │   └── Cached results when appropriate
│   ├── Bulkhead Pattern
│   │   ├── Branch isolation
│   │   ├── Service isolation
│   │   └── Resource partitioning
│   ├── Timeout Pattern
│   │   ├── Operation timeouts
│   │   ├── Flow timeouts
│   │   └── Graceful termination
│   └── Compensation Pattern
│       ├── Transaction-like semantics
│       ├── Ordered compensation actions
│       └── Partial completion handling

Security Patterns
├── Applied across all components
│   ├── Authentication and Authorization
│   │   ├── Mutual TLS authentication
│   │   ├── Service identity verification
│   │   └── Authorization token propagation
│   ├── Data Protection
│   │   ├── Data-at-Rest Protection
│   │   │   ├── Configuration data encryption
│   │   │   ├── Credential secure storage
│   │   │   └── Sensitive parameter protection
│   │   └── Data-in-Transit Protection
│   │       ├── TLS for all communications
│   │       ├── Message encryption
│   │       └── Secure parameter passing
│   └── Audit and Compliance
│       ├── Operation logging
│       ├── Configuration change tracking
│       └── Access control enforcement

Version Management
├── Implemented by all abstract components
│   ├── Version-Aware Orchestration
│   │   ├── Version information in execution context
│   │   ├── Compatibility validation
│   │   └── Version-specific error handling
│   ├── Version Compatibility
│   │   ├── GetVersionInfo() provides detailed version data
│   │   ├── GetCompatibilityMatrix() defines version constraints
│   │   └── Version validation during service registration
│   └── Version Tracking
│       ├── Version properties on all entities
│       ├── Version history maintenance
│       └── Version status management (ACTIVE, DEPRECATED, ARCHIVED)
```

### Inheritance Relationships

```
AbstractServiceBase
├── AbstractImporterService
│   └── FileImporterService (concrete)
├── AbstractProcessorService
│   └── FileProcessorService (concrete)
├── AbstractExporterService
│   └── FileExporterService (concrete)
├── AbstractManagerService<TService, TServiceId>
│   ├── ImporterServiceManager (concrete)
│   ├── ProcessorServiceManager (concrete)
│   ├── ExporterServiceManager (concrete)
│   ├── FlowEntityManager (concrete)
│   ├── ProcessingChainEntityManager (concrete)
│   ├── SourceEntityManager (concrete)
│   ├── DestinationEntityManager (concrete)
│   ├── SourceAssignmentEntityManager (concrete)
│   ├── DestinationAssignmentEntityManager (concrete)
│   ├── TaskSchedulerEntityManager (concrete)
│   └── ScheduledFlowEntityManager (concrete)
├── AbstractOrchestratorService
│   └── FlowOrchestratorService (concrete)
├── AbstractMemoryManagerService
│   └── SharedMemoryManagerService (concrete)
├── AbstractBranchControllerService
│   └── ParallelBranchControllerService (concrete)
└── AbstractTaskSchedulerService
    └── CronTaskSchedulerService (concrete)

AbstractEntity
├── AbstractFlowEntity
│   └── FlowEntity (concrete)
├── AbstractSourceEntity
│   └── FileSourceEntity (concrete)
├── AbstractDestinationEntity
│   └── FileDestinationEntity (concrete)
├── AbstractSourceAssignmentEntity
│   └── SourceAssignmentEntity (concrete)
├── AbstractDestinationAssignmentEntity
│   └── DestinationAssignmentEntity (concrete)
├── AbstractScheduledFlowEntity
│   └── ScheduledFlowEntity (concrete)
└── AbstractTaskSchedulerEntity
    └── CronTaskSchedulerEntity (concrete)

AbstractProtocol
└── FileProtocol (concrete)

AbstractProtocolHandler
└── FileProtocolHandler (concrete)

AbstractMergeStrategy
└── UnionMergeStrategy (concrete)
```