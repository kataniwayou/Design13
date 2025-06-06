# 10. Abstract Classes Specification

## 10.1 Abstract Class Framework

The FlowOrchestrator system is built upon a robust abstract class framework that provides standardized interfaces, behaviors, and messaging capabilities across the system. This document clarifies the structure and inheritance hierarchies of all abstract classes in the system.

### 10.1.1 Abstract Class Design Principles

All abstract classes in the FlowOrchestrator system adhere to the following design principles:

1. **Interface Consistency**: Abstract classes define consistent interfaces that all concrete implementations must satisfy
2. **Responsibility Isolation**: Each abstract class has clear, focused responsibilities
3. **Implementation Flexibility**: Abstract classes define what must be done, not how it should be done
4. **Lifecycle Management**: Service-oriented abstract classes manage consistent lifecycle states
5. **Message Processing**: Service-oriented abstract classes provide standardized message handling
6. **Error Propagation**: Abstract classes define consistent error handling and propagation patterns
7. **Versioning Support**: Abstract classes implement version-aware behaviors
8. **Telemetry Integration**: Abstract classes incorporate standardized instrumentation points

## 10.2 Service-Oriented Abstract Classes

### 10.2.1 AbstractServiceBase

```csharp
/*
 * Base abstract class for all services in the system
 */
public abstract class AbstractServiceBase : IService
{
    // Lifecycle state management
    protected ServiceState _state = ServiceState.UNINITIALIZED;
    
    // Required by all implementations
    public abstract string ServiceId { get; }
    public abstract string Version { get; }
    public abstract string ServiceType { get; }
    
    // Lifecycle methods required by IService interface
    public abstract void Initialize(ConfigurationParameters parameters);
    public abstract void Terminate();
    
    // State management
    public ServiceState GetState() => _state;
    protected void SetState(ServiceState newState) => _state = newState;
    
    // Version information
    public abstract VersionInfo GetVersionInfo();
    public abstract CompatibilityMatrix GetCompatibilityMatrix();
    
    // Telemetry hooks
    protected abstract void RecordMetric(string name, double value, Dictionary<string, string> attributes);
    protected abstract void StartOperation(string operationName);
    protected abstract void EndOperation(string operationName, OperationResult result);
    
    // Configuration validation
    public abstract ValidationResult ValidateConfiguration(ConfigurationParameters parameters);
}
```

**Messaging Capabilities**:
- Does not directly implement message handling
- Provides foundational support for messaging in derived classes
- Concrete implementations utilize messaging through domain-specific interfaces

**Required Implementations**:
1. All lifecycle methods (Initialize, Terminate)
2. Configuration validation
3. Version information methods
4. Telemetry hooks

**Inheritance Hierarchy**:
```
AbstractServiceBase
├── AbstractImporterService
├── AbstractProcessorService
├── AbstractExporterService
├── AbstractManagerService
```

### 10.2.2 AbstractImporterService

```csharp
/*
 * Abstract base class for all importer services
 */
public abstract class AbstractImporterService : AbstractServiceBase, IImporterService, IMessageConsumer<ImportCommand>
{
    // Protocol-specific functionality
    public abstract string Protocol { get; }
    public abstract ProtocolCapabilities GetCapabilities();
    
    // Core importer functionality
    public abstract ImportResult Import(ImportParameters parameters, ExecutionContext context);
    
    // Message consumer implementation
    public abstract Task Consume(ConsumeContext<ImportCommand> context);
    
    // Error handling and recovery
    protected abstract string ClassifyException(Exception ex);
    protected abstract Dictionary<string, object> GetErrorDetails(Exception ex);
    protected abstract void TryRecover();
    
    // Lifecycle hooks
    protected abstract void OnInitialize();
    protected abstract void OnReady();
    protected abstract void OnProcessing();
    protected abstract void OnError(Exception ex);
    protected abstract void OnTerminate();
}
```

**Messaging Capabilities**:
- Consumes `ImportCommand` messages through `IMessageConsumer<ImportCommand>` interface
- Publishes `ImportCommandResult` messages for successful operations
- Publishes `ImportCommandError` messages for failed operations

**Required Implementations**:
1. Protocol-specific properties and capabilities
2. Import operation logic
3. Exception classification and error handling
4. Recovery mechanisms

**Inheritance Hierarchy**:
```
AbstractImporterService
├── FileImporterService
├── RestImporterService
├── DatabaseImporterService
├── MessageQueueImporterService
└── CustomImporterService
```

### 10.2.3 AbstractProcessorService

```csharp
/*
 * Abstract base class for all processor services
 */
public abstract class AbstractProcessorService : AbstractServiceBase, IProcessorService, IMessageConsumer<ProcessCommand>
{
    // Processor-specific functionality
    public abstract ProcessingResult Process(ProcessParameters parameters, ExecutionContext context);
    public abstract SchemaDefinition GetInputSchema();
    public abstract SchemaDefinition GetOutputSchema();
    
    // Message consumer implementation
    public abstract Task Consume(ConsumeContext<ProcessCommand> context);
    
    // Error handling and recovery
    protected abstract string ClassifyException(Exception ex);
    protected abstract Dictionary<string, object> GetErrorDetails(Exception ex);
    protected abstract void TryRecover();
    
    // Lifecycle hooks
    protected abstract void OnInitialize();
    protected abstract void OnReady();
    protected abstract void OnProcessing();
    protected abstract void OnError(Exception ex);
    protected abstract void OnTerminate();
}
```

**Messaging Capabilities**:
- Consumes `ProcessCommand` messages through `IMessageConsumer<ProcessCommand>` interface
- Publishes `ProcessCommandResult` messages for successful operations
- Publishes `ProcessCommandError` messages for failed operations

**Required Implementations**:
1. Process method for data transformation
2. Input and output schema definitions
3. Exception classification and error handling
4. Recovery mechanisms

**Inheritance Hierarchy**:
```
AbstractProcessorService
├── FileProcessorService
├── JsonTransformationProcessor
├── DataValidationProcessor
├── DataEnrichmentProcessor
└── CustomProcessor
```

### 10.2.4 AbstractExporterService

```csharp
/*
 * Abstract base class for all exporter services
 */
public abstract class AbstractExporterService : AbstractServiceBase, IExporterService, IMessageConsumer<ExportCommand>
{
    // Protocol-specific functionality
    public abstract string Protocol { get; }
    public abstract ProtocolCapabilities GetCapabilities();
    public abstract MergeCapabilities GetMergeCapabilities();
    
    // Core exporter functionality
    public abstract ExportResult Export(ExportParameters parameters, ExecutionContext context);
    
    // Message consumer implementation
    public abstract Task Consume(ConsumeContext<ExportCommand> context);
    
    // Branch merge implementation
    public abstract ExportResult MergeBranches(Dictionary<string, DataPackage> branchData, MergeStrategy strategy, ExecutionContext context);
    
    // Error handling and recovery
    protected abstract string ClassifyException(Exception ex);
    protected abstract Dictionary<string, object> GetErrorDetails(Exception ex);
    protected abstract void TryRecover();
    
    // Lifecycle hooks
    protected abstract void OnInitialize();
    protected abstract void OnReady();
    protected abstract void OnProcessing();
    protected abstract void OnError(Exception ex);
    protected abstract void OnTerminate();
}
```

**Messaging Capabilities**:
- Consumes `ExportCommand` messages through `IMessageConsumer<ExportCommand>` interface
- Publishes `ExportCommandResult` messages for successful operations
- Publishes `ExportCommandError` messages for failed operations

**Required Implementations**:
1. Protocol-specific properties and capabilities
2. Export operation logic
3. Branch merging functionality
4. Exception classification and error handling
5. Recovery mechanisms

**Inheritance Hierarchy**:
```
AbstractExporterService
├── FileExporterService
├── RestExporterService
├── DatabaseExporterService
├── MessageQueueExporterService
└── CustomExporterService
```

### 10.2.5 AbstractManagerService

```csharp
/*
 * Abstract base class for all manager services
 */
public abstract class AbstractManagerService<TService, TServiceId> : AbstractServiceBase, IServiceManager<TService, TServiceId>
{
    // Registry management
    protected Dictionary<string, Dictionary<string, TService>> _registry = new Dictionary<string, Dictionary<string, TService>>();
    
    // Core manager functionality
    public abstract RegistrationResult Register(TService service);
    public abstract ValidationResult Validate(TService service);
    public abstract TService GetService(TServiceId serviceId, string version);
    public abstract IEnumerable<TService> GetAllServices(TServiceId serviceId);
    public abstract IEnumerable<TService> GetAllServices();
    public abstract bool UnregisterService(TServiceId serviceId, string version);
    public abstract ServiceStatus GetServiceStatus(TServiceId serviceId, string version);
    public abstract void UpdateServiceStatus(TServiceId serviceId, string version, ServiceStatus status);
    
    // Message handling implementation
    public abstract Task Consume(ConsumeContext<ServiceRegistrationCommand<TService>> context);
    
    // Error handling and recovery
    protected abstract string ClassifyException(Exception ex);
    protected abstract Dictionary<string, object> GetErrorDetails(Exception ex);
    protected abstract void TryRecover();
    
    // Lifecycle hooks
    protected abstract void OnInitialize();
    protected abstract void OnReady();
    protected abstract void OnProcessing();
    protected abstract void OnError(Exception ex);
    protected abstract void OnTerminate();
}
```

**Messaging Capabilities**:
- Consumes `ServiceRegistrationCommand<TService>` messages
- Publishes `ServiceRegistrationResult` messages for successful operations
- Publishes `ServiceRegistrationError` messages for failed operations
- Additional message handlers for service lifecycle management

**Required Implementations**:
1. Service registry management
2. Service validation
3. Service lifecycle management
4. Exception classification and error handling
5. Recovery mechanisms

**Inheritance Hierarchy**:
```
AbstractManagerService<TService, TServiceId>
├── ImporterServiceManager
├── ProcessorServiceManager
├── ExporterServiceManager
├── SourceEntityManager
├── DestinationEntityManager
├── SourceAssignmentEntityManager
├── DestinationAssignmentEntityManager
├── TaskSchedulerEntityManager
└── ScheduledFlowEntityManager
```

## 10.3 Protocol Abstract Classes

### 10.3.1 AbstractProtocol

```csharp
/*
 * Abstract base class for all protocol implementations
 */
public abstract class AbstractProtocol : IProtocol
{
    // Protocol identification
    public abstract string Name { get; }
    public abstract string Description { get; }
    
    // Capability discovery
    public abstract ProtocolCapabilities GetCapabilities();
    
    // Connection parameter management
    public abstract ConnectionParameters GetConnectionParameters();
    public abstract ValidationResult ValidateConnectionParameters(Dictionary<string, string> parameters);
    
    // Handler creation
    public abstract ProtocolHandler CreateHandler(Dictionary<string, string> parameters);
}
```

**Messaging Capabilities**:
- Does not directly implement message handling
- Provides protocol-specific functionality used by service classes that do implement messaging

**Required Implementations**:
1. Protocol identification
2. Capability discovery
3. Connection parameter management
4. Handler creation

**Inheritance Hierarchy**:
```
AbstractProtocol
├── FileProtocol
├── RestProtocol
├── DatabaseProtocol
├── MessageQueueProtocol
└── CustomProtocol
```

### 10.3.2 AbstractProtocolHandler

```csharp
/*
 * Abstract base class for all protocol handlers
 */
public abstract class AbstractProtocolHandler : ProtocolHandler
{
    // Handler configuration
    protected Dictionary<string, string> _parameters;
    
    public AbstractProtocolHandler(Dictionary<string, string> parameters)
    {
        _parameters = parameters;
    }
    
    // Connection management
    public abstract override Connection Connect();
    public abstract override void Disconnect(Connection connection);
    
    // Data operations
    public abstract override DataPackage Retrieve(Connection connection, RetrieveParameters parameters);
    public abstract override void Deliver(Connection connection, DeliverParameters parameters);
    
    // Error handling
    protected abstract string ClassifyException(Exception ex);
    protected abstract Dictionary<string, object> GetErrorDetails(Exception ex);
}
```

**Messaging Capabilities**:
- Does not directly implement message handling
- Provides data retrieval and delivery operations used by service classes that do implement messaging

**Required Implementations**:
1. Connection management
2. Data operations
3. Error handling

**Inheritance Hierarchy**:
```
AbstractProtocolHandler
├── FileProtocolHandler
├── RestProtocolHandler
├── DatabaseProtocolHandler
├── MessageQueueProtocolHandler
└── CustomProtocolHandler
```

## 10.4 Entity Abstract Classes

### 10.4.1 AbstractEntity

```csharp
/*
 * Abstract base class for all entities in the system
 */
public abstract class AbstractEntity : IEntity
{
    // Version properties
    public string Version { get; set; }
    public DateTime CreatedTimestamp { get; set; }
    public DateTime LastModifiedTimestamp { get; set; }
    public string VersionDescription { get; set; }
    public string PreviousVersionId { get; set; }
    public VersionStatus VersionStatus { get; set; }
    
    // Identity properties
    public abstract string GetEntityId();
    public abstract string GetEntityType();
    
    // Validation
    public abstract ValidationResult Validate();
    
    // Change tracking
    protected bool _isModified = false;
    public bool IsModified() => _isModified;
    public void SetModified() => _isModified = true;
    public void ClearModified() => _isModified = false;
}
```

**Messaging Capabilities**:
- Does not directly implement message handling
- Entity state changes may trigger events in services that handle these entities

**Required Implementations**:
1. Entity identification
2. Validation logic

**Inheritance Hierarchy**:
```
AbstractEntity
├── AbstractFlowEntity
├── AbstractProcessingChainEntity
├── AbstractSourceEntity
├── AbstractDestinationEntity
├── AbstractSourceAssignmentEntity
├── AbstractDestinationAssignmentEntity
├── AbstractScheduledFlowEntity
└── AbstractTaskSchedulerEntity
```

### 10.4.2 AbstractFlowEntity

```csharp
/*
 * Abstract base class for flow entities
 */
public abstract class AbstractFlowEntity : AbstractEntity, IFlowEntity
{
    // Flow properties
    public string FlowId { get; set; }
    public string Description { get; set; }
    public string ImporterServiceId { get; set; }
    public string ImporterServiceVersion { get; set; }
    public List<ProcessingChainReference> ProcessingChains { get; set; } = new List<ProcessingChainReference>();
    public List<ExporterReference> Exporters { get; set; } = new List<ExporterReference>();
    public Dictionary<string, ConnectionDefinition> Connections { get; set; } = new Dictionary<string, ConnectionDefinition>();
    
    // IEntity implementation
    public override string GetEntityId() => FlowId;
    public override string GetEntityType() => "FlowEntity";
    
    // Flow-specific validation
    public abstract override ValidationResult Validate();
}
```

**Messaging Capabilities**:
- Does not directly implement message handling
- Flow entity state changes may trigger events in services that handle these entities

**Required Implementations**:
- Validation logic

**Inheritance Hierarchy**:
```
AbstractFlowEntity
└── FlowEntity
```

## 10.5 Strategy Abstract Classes

### 10.5.1 AbstractMergeStrategy

```csharp
/*
 * Abstract base class for merge strategies
 */
public abstract class AbstractMergeStrategy : IMergeStrategy
{
    // Strategy properties
    public abstract string Name { get; }
    
    // Strategy configuration
    protected MergeStrategyConfiguration _configuration;
    
    // Initialization and validation
    public abstract void Initialize(MergeStrategyConfiguration configuration);
    public abstract ValidationResult ValidateConfiguration(Dictionary<string, object> parameters);
    
    // Merge operation
    public abstract MergeResult Merge(Dictionary<string, DataPackage> branchOutputs, MergeContext context);
}
```

**Messaging Capabilities**:
- Does not directly implement message handling
- Used by services that implement messaging (particularly exporters)

**Required Implementations**:
1. Strategy identification
2. Configuration validation
3. Merge operation logic

**Inheritance Hierarchy**:
```
AbstractMergeStrategy
├── LastWriteWinsMergeStrategy
├── PriorityBasedMergeStrategy
├── FieldLevelMergeStrategy
└── CustomMergeStrategy
```

### 10.5.2 AbstractRecoveryStrategy

```csharp
/*
 * Abstract base class for recovery strategies
 */
public abstract class AbstractRecoveryStrategy : IRecoveryStrategy
{
    // Strategy identification
    public abstract string Name { get; }
    
    // Strategy configuration
    protected RecoveryStrategyConfiguration _configuration;
    
    // Initialization and validation
    public abstract void Initialize(RecoveryStrategyConfiguration configuration);
    public abstract ValidationResult ValidateConfiguration(Dictionary<string, object> parameters);
    
    // Recovery operation
    public abstract RecoveryResult Recover(RecoveryContext context);
    
    // Applicability check
    public abstract bool IsApplicable(ErrorContext errorContext);
}
```

**Messaging Capabilities**:
- Does not directly implement message handling
- May publish recovery events through the recovery framework

**Required Implementations**:
1. Strategy identification
2. Configuration validation
3. Recovery operation logic
4. Applicability check

**Inheritance Hierarchy**:
```
AbstractRecoveryStrategy
├── RetryRecoveryStrategy
├── CompensationRecoveryStrategy
├── FallbackRecoveryStrategy
├── CircuitBreakerRecoveryStrategy
└── CustomRecoveryStrategy
```

## 10.6 UML Class Diagrams

### 10.6.1 Service Abstract Classes Hierarchy

```
┌───────────────────┐
│AbstractServiceBase│
└─────────┬─────────┘
          │
          ├─────────────────────────┬────────────────────────┬─────────────────────────┐
          │                         │                        │                         │
┌─────────▼─────────┐    ┌──────────▼───────────┐   ┌────────▼────────────┐   ┌────────▼────────────┐
│AbstractImporterService│   │AbstractProcessorService│   │AbstractExporterService│   │AbstractManagerService│
└─────────┬─────────┘    └──────────┬───────────┐   └────────┬────────────┘   └─────────────────────┘
          │                         │           │            │                          │
┌─────────┼─────────┐    ┌──────────┼──┐   ┌───┼────┐   ┌───┼───┐              ┌───────┼───────┐
│         │         │    │         │  │   │   │    │   │   │   │              │       │       │
▼         ▼         ▼    ▼         ▼  ▼   ▼   ▼    ▼   ▼   ▼   ▼              ▼       ▼       ▼
REST     SFTP    Custom  JSON   Validation  Mapping  REST SFTP Custom    Importer Processor Exporter
Importer Importer Importer Processor Processor Processor Exporter Exporter Exporter Manager Manager Manager
```

### 10.6.2 Protocol Abstract Classes Hierarchy

```
┌─────────────────┐          ┌─────────────────────┐
│ AbstractProtocol │          │AbstractProtocolHandler│
└────────┬────────┘          └───────────┬───────────┘
         │                               │
         ├──────────┬─────────┐          ├──────────┬─────────┐
         │          │         │          │          │         │
┌────────▼───┐ ┌────▼────┐ ┌──▼───┐ ┌────▼────┐ ┌───▼────┐ ┌──▼───┐
│RESTProtocol│ │SFTPProtocol│ │Custom│ │RESTHandler│ │SFTPHandler│ │Custom│
└────────────┘ └──────────┘ └──────┘ └───────────┘ └──────────┘ └──────┘
```

### 10.6.3 Entity Abstract Classes Hierarchy

```
┌──────────────┐
│AbstractEntity │
└───────┬──────┘
        │
        ├───────────────┬──────────────┬─────────────────┬──────────────────┬───────────────────┬──────────────────────┐
        │               │              │                 │                  │                   │                      │
┌───────▼──────┐ ┌──────▼───────┐ ┌────▼────────┐ ┌──────▼─────────┐ ┌──────▼───────────┐ ┌────▼─────────────┐ ┌──────▼────────────┐
│AbstractFlowEntity│ │AbstractSourceEntity│ │AbstractDestinationEntity│ │AbstractScheduledFlowEntity│ │AbstractTaskSchedulerEntity│ │AbstractSourceAssignmentEntity│ │AbstractDestinationAssignmentEntity│
└────────────┘ └──────────────┘ └───────────────┘ └──────────────────┘ └─────────────────────┘ └─────────────────────┘ └─────────────────────────┘
```