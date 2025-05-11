# FlowOrchestrator Component Relationships

This document provides a comprehensive view of the relationships between abstract classes and common components in the FlowOrchestrator system, including implementation details and project locations.

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

## Part 1: Abstract Class Relationships

### Core Abstract Classes

```
AbstractServiceBase (Core/FlowOrchestrator.Abstractions/Services) [See detailed business description: ComponentBusinessDescriptions.md#AbstractServiceBase]
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

AbstractEntity (Core/FlowOrchestrator.Abstractions/Entities) [See detailed business description: ComponentBusinessDescriptions.md#AbstractEntity]
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

### Service-Oriented Abstract Classes

```
AbstractServiceBase (Core/FlowOrchestrator.Abstractions/Services)
├── AbstractImporterService (Integration/FlowOrchestrator.Abstractions.Integration/Services) [See detailed business description: ComponentBusinessDescriptions.md#AbstractImporterService]
│   └── Base for all importer services
│       ├── Implements IConsumer<ImportCommand> for message-based import operations
│       ├── Defines protocol-specific functionality
│       ├── Provides data import operations
│       └── Implements error handling and recovery
│
├── AbstractProcessorService (Processing/FlowOrchestrator.Abstractions.Processing/Services) [See detailed business description: ComponentBusinessDescriptions.md#AbstractProcessorService]
│   └── Base for all processor services
│       ├── Implements IConsumer<ProcessCommand> for message-based processing
│       ├── Defines data transformation functionality
│       ├── Manages input/output schemas
│       └── Implements error handling and recovery
│
├── AbstractExporterService (Integration/FlowOrchestrator.Abstractions.Integration/Services) [See detailed business description: ComponentBusinessDescriptions.md#AbstractExporterService]
│   └── Base for all exporter services
│       ├── Implements IConsumer<ExportCommand> for message-based export operations
│       ├── Defines protocol-specific functionality
│       ├── Provides branch merging capabilities
│       └── Implements error handling and recovery
│
├── AbstractManagerService<TService, TServiceId> (Management/FlowOrchestrator.Abstractions.Management/Services)
│   └── Base for all manager services (both service managers and entity managers)
│       ├── Implements IConsumer<ServiceRegistrationCommand<TService>> for service registration
│       ├── Provides CRUD operations framework
│       │   └── Uses Repository Interfaces for persistence
│       ├── Handles lifecycle management
│       └── Maintains registry
│
├── AbstractOrchestratorService (Execution/FlowOrchestrator.Abstractions.Execution/Services) [See detailed business description: ComponentBusinessDescriptions.md#AbstractOrchestratorService]
│   └── Base for orchestrator services
│       ├── Implements IConsumer<FlowExecutionCommand> for flow execution
│       ├── Implements IConsumer<FlowControlCommand> for flow control
│       ├── Coordinates flow execution
│       └── Manages branch creation and completion
│
├── AbstractMemoryManagerService (Execution/FlowOrchestrator.Abstractions.Execution/Services)
│   └── Base for memory manager services
│       ├── Implements IConsumer<MemoryOperationCommand> for memory operations
│       ├── Manages shared memory operations
│       └── Handles memory lifecycle
│
├── AbstractBranchControllerService (Execution/FlowOrchestrator.Abstractions.Execution/Services)
│   └── Base for branch controller services
│       ├── Implements IConsumer<BranchControlCommand> for branch control
│       ├── Manages branch execution
│       └── Tracks branch status
│
└── AbstractTaskSchedulerService (Management/FlowOrchestrator.Abstractions.Management/Services)
    └── Base for task scheduler services
        ├── Implements IConsumer<ScheduleCommand> for schedule creation
        ├── Implements IConsumer<ScheduleModificationCommand> for schedule updates
        ├── Manages flow execution scheduling
        └── Handles schedule lifecycle
```

### Entity Abstract Classes

```
AbstractEntity (Core/FlowOrchestrator.Abstractions/Entities)
├── AbstractFlowEntity (Core/FlowOrchestrator.Domain/Entities) [See detailed business description: ComponentBusinessDescriptions.md#AbstractFlowEntity]
│   └── Base for flow entities
│       ├── Defines flow structure
│       ├── References processing chains
│       ├── Connects importers and exporters
│       └── Implemented by FlowEntity (concrete)
│
├── AbstractSourceEntity (Integration/FlowOrchestrator.Domain.Integration/Entities)
│   └── Base for source entities
│       ├── Defines source location
│       ├── Specifies connection protocol
│       └── Provides abstraction for data retrieval
│
├── AbstractDestinationEntity (Integration/FlowOrchestrator.Domain.Integration/Entities)
│   └── Base for destination entities
│       ├── Defines destination location
│       ├── Specifies delivery protocol
│       └── Provides abstraction for data delivery
│
├── AbstractSourceAssignmentEntity (Integration/FlowOrchestrator.Domain.Integration/Entities)
│   └── Base for source assignment entities
│       ├── Links sources to importers
│       ├── Validates protocol compatibility
│       └── Provides standardized abstraction for source-importer relationships
│
├── AbstractDestinationAssignmentEntity (Integration/FlowOrchestrator.Domain.Integration/Entities)
│   └── Base for destination assignment entities
│       ├── Links destinations to exporters
│       ├── Validates protocol compatibility
│       └── Provides standardized abstraction for destination-exporter relationships
│
├── AbstractScheduledFlowEntity (Management/FlowOrchestrator.Domain.Management/Entities)
│   └── Base for scheduled flow entities
│       ├── Combines flow, source, and destination
│       ├── Adds scheduling parameters
│       └── Enables automated flow execution
│
└── AbstractTaskSchedulerEntity (Management/FlowOrchestrator.Domain.Management/Entities)
    └── Base for task scheduler entities
        ├── Defines scheduling parameters
        ├── Manages execution triggers
        └── Provides standardized scheduling abstraction
```

### Protocol Abstract Classes

```
AbstractProtocol (Integration/FlowOrchestrator.Abstractions.Integration/Protocols) [See detailed business description: ComponentBusinessDescriptions.md#AbstractProtocol]
└── Base for all protocol implementations
    ├── Defines protocol identification
    ├── Specifies capability discovery
    ├── Manages connection parameters
    └── Implemented by concrete protocol classes

AbstractProtocolHandler (Integration/FlowOrchestrator.Abstractions.Integration/Protocols)
└── Base for all protocol handlers
    ├── Manages connections
    ├── Handles data operations
    ├── Provides error classification
    └── Implemented by concrete handler classes
```

### Strategy Abstract Classes

```
AbstractMergeStrategy (Integration/FlowOrchestrator.Abstractions.Integration/Strategies) [See detailed business description: ComponentBusinessDescriptions.md#AbstractMergeStrategy]
└── Base for all merge strategies
    ├── Defines branch merging logic
    ├── Validates input compatibility
    ├── Specifies output schema
    └── Implemented by concrete strategy classes
```

## Part 2: Common Components Relationship to Abstract Components

### 1. Infrastructure Integration Components

```
OpenTelemetry [See detailed business description: ComponentBusinessDescriptions.md#OpenTelemetry-Integration]
└── Used by AbstractServiceBase for observability
    ├── Logging through ILogger
    │   └── All abstract services use standardized logging patterns
    ├── Metrics through Meter
    │   └── All abstract services record operational metrics
    └── Distributed tracing through Tracer
        └── All abstract services participate in distributed tracing

MassTransit [See detailed business description: ComponentBusinessDescriptions.md#MassTransit-Integration]
└── Used by AbstractServiceBase for messaging
    ├── Message consumption through IConsumer interfaces
    │   ├── AbstractImporterService implements IConsumer<ImportCommand>
    │   ├── AbstractProcessorService implements IConsumer<ProcessCommand>
    │   ├── AbstractExporterService implements IConsumer<ExportCommand>
    │   ├── AbstractManagerService implements IConsumer<ServiceRegistrationCommand>
    │   ├── AbstractOrchestratorService implements IConsumer<FlowExecutionCommand>
    │   ├── AbstractMemoryManagerService implements IConsumer<MemoryOperationCommand>
    │   ├── AbstractBranchControllerService implements IConsumer<BranchControlCommand>
    │   └── AbstractTaskSchedulerService implements IConsumer<ScheduleCommand>
    └── Message publishing through IPublishEndpoint
        └── All abstract services publish domain-specific events
```

### 2. Configuration and Validation Components

```
ConfigurationParameters (FlowOrchestrator.Common.Configuration)
└── Used by all abstract components for configuration
    ├── AbstractServiceBase.Initialize(ConfigurationParameters)
    │   └── All service implementations receive configuration through this method
    ├── AbstractServiceBase.ValidateConfiguration(ConfigurationParameters)
    │   └── All service implementations validate their configuration
    └── Protocol-specific configuration in AbstractImporterService and AbstractExporterService
        └── ConnectionParameters property contains protocol-specific settings

ValidationResult (FlowOrchestrator.Common.Validation)
└── Used by all abstract components for validation
    ├── AbstractEntity.Validate() returns ValidationResult
    │   └── All entity implementations perform self-validation
    ├── AbstractServiceBase.ValidateConfiguration() returns ValidationResult
    │   └── All service implementations validate their configuration
    └── Protocol validation in AbstractProtocol
        └── ValidateConnectionParameters() returns ValidationResult

SchemaDefinition (FlowOrchestrator.Common.Schema)
└── Used by data processing components
    ├── AbstractProcessorService
    │   ├── GetInputSchema() returns SchemaDefinition
    │   └── GetOutputSchema() returns SchemaDefinition
    └── AbstractMergeStrategy
        └── GetOutputSchema() returns SchemaDefinition
```

### 3. Error Management Components

```
ErrorType (FlowOrchestrator.Common.Errors)
└── Used by all abstract components for error classification
    ├── AbstractImporterService.ClassifyException() returns ErrorType
    ├── AbstractProcessorService.ClassifyException() returns ErrorType
    ├── AbstractExporterService.ClassifyException() returns ErrorType
    └── AbstractProtocolHandler.ClassifyException() returns ErrorType

ErrorDetails (FlowOrchestrator.Common.Errors)
└── Used by all abstract components for error reporting
    ├── AbstractImporterService.GetErrorDetails() returns ErrorDetails
    ├── AbstractProcessorService.GetErrorDetails() returns ErrorDetails
    ├── AbstractExporterService.GetErrorDetails() returns ErrorDetails
    └── AbstractProtocolHandler.GetErrorDetails() returns ErrorDetails
```

### 4. State Management Components

```
ServiceState (FlowOrchestrator.Common.State)
└── Used by AbstractServiceBase for lifecycle management
    ├── _state field tracks current state
    ├── GetState() returns current state
    ├── SetState() updates state
    └── State transitions:
        UNINITIALIZED → INITIALIZING → READY → PROCESSING → READY
                                    ↘ ERROR ↗
                                    → TERMINATING → TERMINATED

VersionInfo (FlowOrchestrator.Common.Versioning)
└── Used by all abstract components for version management
    ├── AbstractServiceBase.GetVersionInfo() returns VersionInfo
    ├── AbstractEntity.Version property
    └── Version compatibility checking in AbstractManagerService

CompatibilityMatrix (FlowOrchestrator.Common.Versioning)
└── Used by AbstractServiceBase for version compatibility
    ├── GetCompatibilityMatrix() returns CompatibilityMatrix
    └── Used by managers and orchestrator to verify component compatibility
```

### 5. Execution Components

```
ExecutionContext (FlowOrchestrator.Common.Execution)
└── Used by service operations for execution context
    ├── AbstractImporterService.Import(ImportParameters, ExecutionContext)
    ├── AbstractProcessorService.Process(ProcessParameters, ExecutionContext)
    ├── AbstractExporterService.Export(ExportParameters, ExecutionContext)
    └── Contains:
        ├── FlowId and ExecutionId for correlation
        ├── Telemetry context for distributed tracing
        └── Security context for authorization

DataPackage (FlowOrchestrator.Common.Data)
└── Used for data transfer between components
    ├── AbstractImporterService.Import() returns DataPackage
    ├── AbstractProcessorService.Process() accepts and returns DataPackage
    ├── AbstractExporterService.Export() accepts DataPackage
    └── AbstractMemoryManagerService stores and retrieves DataPackage
```

### 6. Protocol Components

```
ProtocolCapabilities (FlowOrchestrator.Common.Protocols)
└── Used by protocol-aware components
    ├── AbstractImporterService.GetCapabilities() returns ProtocolCapabilities
    ├── AbstractExporterService.GetCapabilities() returns ProtocolCapabilities
    └── AbstractProtocol.GetCapabilities() returns ProtocolCapabilities

ConnectionParameters (FlowOrchestrator.Common.Protocols)
└── Used by protocol-aware components
    ├── AbstractSourceEntity.ConnectionParameters property
    ├── AbstractDestinationEntity.ConnectionParameters property
    ├── AbstractProtocol.GetConnectionParameters() returns ConnectionParameters
    └── AbstractProtocol.ValidateConnectionParameters() validates ConnectionParameters
```

### 7. Data Persistence Components

```
Repository Interfaces (FlowOrchestrator.Common.Data.Repositories) [See detailed business description: ComponentBusinessDescriptions.md#Repository-Interfaces]
└── Used by manager services for entity persistence
    ├── IEntityRepository<TEntity>
    │   ├── Defines CRUD operations for entities
    │   ├── Provides query capabilities
    │   └── Supports versioning
    ├── IServiceRepository<TService>
    │   ├── Defines CRUD operations for services
    │   ├── Provides service discovery
    │   └── Supports versioning
    └── Implemented by concrete repository classes

MongoDB Integration (FlowOrchestrator.Data.MongoDB) [See detailed business description: ComponentBusinessDescriptions.md#MongoDB-Integration]
└── Implements repository interfaces for MongoDB
    ├── MongoEntityRepository<TEntity>
    │   ├── Implements IEntityRepository<TEntity>
    │   ├── Maps entities to MongoDB documents
    │   └── Optimizes queries for MongoDB
    ├── MongoServiceRepository<TService>
    │   ├── Implements IServiceRepository<TService>
    │   ├── Maps services to MongoDB documents
    │   └── Optimizes queries for MongoDB
    └── Used by manager services through repository abstractions
```

## Part 3: Concrete Implementations

### Core Concrete Entities

```
ProcessingChainEntity (concrete) (Core/FlowOrchestrator.Domain/Entities) [See detailed business description: ComponentBusinessDescriptions.md#ProcessingChainEntity]
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

FlowEntity (concrete) (Core/FlowOrchestrator.Domain/Entities) [See detailed business description: ComponentBusinessDescriptions.md#FlowEntity]
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
```

### Manager Service Implementations

```
Manager Services (concrete) [See detailed business description: ComponentBusinessDescriptions.md#Manager-Services]
├── Uses Repository Interfaces for persistence
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

### Key Relationships

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

MongoDB
└── Integrated with Manager Services
    └── Provides
        ├── Entity persistence
        │   ├── Document-based storage for entities
        │   ├── Flexible schema evolution
        │   └── Efficient querying capabilities
        ├── Service registration
        │   ├── Service discovery and lookup
        │   ├── Version tracking and compatibility
        │   └── Service status management
        └── Data management
            ├── Indexing for performance optimization
            ├── Transaction support for consistency
            └── Sharding for scalability
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

IRepository<T>
├── IEntityRepository<TEntity>
│   ├── Defines entity persistence contract
│   │   ├── Create(TEntity) creates new entities
│   │   ├── Read(string) retrieves entities by ID
│   │   ├── Update(TEntity) updates existing entities
│   │   ├── Delete(string) removes entities
│   │   └── Query() provides query capabilities
│   └── Implemented by concrete repository classes
│       └── MongoEntityRepository<TEntity> for MongoDB
└── IServiceRepository<TService>
    ├── Defines service registration contract
    │   ├── Register(TService) registers services
    │   ├── GetService(string) retrieves services by ID
    │   ├── UpdateServiceStatus(string, ServiceStatus) updates status
    │   ├── UnregisterService(string) removes services
    │   └── GetAllServices() retrieves all services
    └── Implemented by concrete repository classes
        └── MongoServiceRepository<TService> for MongoDB
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

### Complete Inheritance Hierarchy

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