# FlowOrchestrator

FlowOrchestrator is a modular service-oriented architecture system designed for orchestrating complex data flows with the following key characteristics:
- Clear separation of concerns across specialized components
- Message-based communication between services
- Shared memory for efficient data transfer
- Centralized orchestration with distributed execution
- Parallel processing through isolated branches
- Comprehensive observability through integrated telemetry
- Version-controlled components for system evolution

## Solution Structure

The FlowOrchestrator system is implemented as a single Visual Studio solution containing multiple projects. This monorepo architecture provides several significant benefits:

1. **Centralized Development**: All code is maintained in a single repository, simplifying version control, continuous integration, and deployment processes.
2. **Simplified Dependencies**: Project references between components are managed directly within the solution, ensuring consistent versioning and eliminating version conflicts.
3. **Shared Code Reuse**: Common libraries, interfaces, and abstract classes can be easily shared across all microservices without duplication.
4. **Coordinated Testing**: Integration and system testing can be performed across all components simultaneously, ensuring proper interaction between services.
5. **Unified Build Process**: The entire system can be built, tested, and deployed from a single pipeline, streamlining DevOps processes.

### Solution Organization

The Visual Studio solution is organized into the following structure:

```
FlowOrchestrator.sln
│
├── src/
│   ├── Core/
│   │   ├── FlowOrchestrator.Common/                   # Common utilities and helpers
│   │   ├── FlowOrchestrator.Abstractions/             # Core interfaces and abstract classes
│   │   ├── FlowOrchestrator.Domain/                   # Domain models and entities
│   │   ├── FlowOrchestrator.Infrastructure.Common/    # Shared infrastructure components
│   │   └── FlowOrchestrator.Security.Common/          # Common security components
│   │
│   ├── Execution/
│   │   ├── FlowOrchestrator.Orchestrator/             # Orchestrator Service
│   │   ├── FlowOrchestrator.MemoryManager/            # Memory Manager
│   │   ├── FlowOrchestrator.BranchController/         # Branch Controller
│   │   ├── FlowOrchestrator.DatabaseExporter/         # Database Exporter Service
│   │   ├── FlowOrchestrator.MessageQueueExporter/     # Message Queue Exporter Service
│   │   └── FlowOrchestrator.ProtocolAdapters/         # Protocol Adapters
│   │
│   ├── Processing/
│   │   ├── FlowOrchestrator.ProcessorBase/            # Processor Service Base
│   │   ├── FlowOrchestrator.JsonProcessor/            # JSON Transformation Processor
│   │   ├── FlowOrchestrator.ValidationProcessor/      # Data Validation Processor
│   │   ├── FlowOrchestrator.EnrichmentProcessor/      # Data Enrichment Processor
│   │   └── FlowOrchestrator.MappingProcessor/         # Mapping Processor
│   │
│   ├── Management/
│   │   ├── FlowOrchestrator.ServiceManager/           # Service Manager
│   │   ├── FlowOrchestrator.FlowManager/              # Flow Manager
│   │   ├── FlowOrchestrator.ConfigurationManager/     # Configuration Manager
│   │   ├── FlowOrchestrator.VersionManager/           # Version Manager
│   │   └── FlowOrchestrator.TaskScheduler/            # Task Scheduler
│   │
│   ├── Observability/
│   │   ├── FlowOrchestrator.StatisticsService/        # Statistics Service
│   │   ├── FlowOrchestrator.MonitoringFramework/      # Monitoring Framework
│   │   ├── FlowOrchestrator.AlertingSystem/           # Alerting System
│   │   └── FlowOrchestrator.AnalyticsEngine/          # Analytics Engine
│   │
│   └── Infrastructure/
│       ├── FlowOrchestrator.Data.MongoDB/             # MongoDB Integration
│       ├── FlowOrchestrator.Data.Hazelcast/           # Hazelcast Integration
│       ├── FlowOrchestrator.Messaging.MassTransit/    # MassTransit Integration
│       ├── FlowOrchestrator.Scheduling.Quartz/        # Quartz.NET Integration
│       └── FlowOrchestrator.Telemetry.OpenTelemetry/  # OpenTelemetry Integration
│
├── tests/
│   ├── Unit/
│   │   ├── FlowOrchestrator.Common.Tests/             # Common components tests
│   │   ├── FlowOrchestrator.Orchestrator.Tests/       # Orchestrator Service tests
│   │   ├── FlowOrchestrator.Domain.Tests/             # Domain models tests
│   │   └── FlowOrchestrator.ImporterBase.Tests/       # Importer Service Base tests
│   │
│   ├── Integration/
│   │   ├── FlowOrchestrator.ExecutionDomain.Tests/    # Execution domain integration tests
│   │   ├── FlowOrchestrator.IntegrationDomain.Tests/  # Integration domain integration tests
│   │   └── FlowOrchestrator.Infrastructure.Tests/     # Infrastructure integration tests
│   │
│   └── System/
│       ├── FlowOrchestrator.EndToEnd.Tests/           # End-to-end system tests
│       ├── FlowOrchestrator.Performance.Tests/        # Performance tests
│       └── FlowOrchestrator.Reliability.Tests/        # Reliability and chaos tests
│
├── docs/
│   ├── architecture/                                  # Architecture documentation
│   ├── api/                                           # API documentation
│   └── guides/                                        # Implementation guides
│
├── tools/
│   ├── build/                                         # Build scripts and tools
│   ├── deployment/                                    # Deployment scripts and tools
│   └── development/                                   # Development tools and utilities
│
└── samples/
    ├── SimpleFlow/                                    # Simple flow example
    ├── BranchedFlow/                                  # Branched flow example
    └── ComplexTransformation/                         # Complex transformation example
```

## Project Types

The solution projects are organized into the following categories:

### Core Projects
Core projects contain the fundamental abstractions, interfaces, and models used throughout the system:

- **Class Libraries**: Fundamental abstractions, interfaces, and domain models
  - `FlowOrchestrator.Abstractions`: Core interfaces and abstract classes for all services
  - `FlowOrchestrator.Domain`: Domain models and entities
  - `FlowOrchestrator.Common`: Shared utilities and helpers

### Domain Service Projects
Domain service projects implement specific microservices organized by their domain:

- **Web API Projects**: Services with external REST API endpoints
  - `FlowOrchestrator.Orchestrator`: Orchestrator Service
  - `FlowOrchestrator.FlowManager`: Flow Manager
  - `FlowOrchestrator.ServiceManager`: Service Manager
  - `FlowOrchestrator.ConfigurationManager`: Configuration Manager
  - `FlowOrchestrator.VersionManager`: Version Manager
  - `FlowOrchestrator.StatisticsService`: Statistics Service
  - `FlowOrchestrator.MonitoringFramework`: Monitoring Framework
  - `FlowOrchestrator.AnalyticsEngine`: Analytics Engine

- **Worker Service Projects**: Background services without external REST APIs
  - `FlowOrchestrator.MemoryManager`: Memory Manager
  - `FlowOrchestrator.BranchController`: Branch Controller
  - `FlowOrchestrator.TaskScheduler`: Task Scheduler
  - `FlowOrchestrator.AlertingSystem`: Alerting System
  - `FlowOrchestrator.DatabaseExporter`: Database Exporter Service
  - `FlowOrchestrator.MessageQueueExporter`: Message Queue Exporter Service

### Processing Projects
Processing projects implement data transformation and validation services:

- **Class Libraries**: Processing components for data transformation
  - `FlowOrchestrator.ProcessorBase`: Base class for all processors
  - `FlowOrchestrator.JsonProcessor`: JSON transformation processor
  - `FlowOrchestrator.ValidationProcessor`: Data validation processor
  - `FlowOrchestrator.EnrichmentProcessor`: Data enrichment processor
  - `FlowOrchestrator.MappingProcessor`: Data mapping processor

### Infrastructure Projects
Infrastructure projects provide integration with external systems and frameworks:

- **Class Libraries**: Integration components for external systems
  - `FlowOrchestrator.Data.MongoDB`: MongoDB Integration
  - `FlowOrchestrator.Data.Hazelcast`: Hazelcast Integration
  - `FlowOrchestrator.Messaging.MassTransit`: MassTransit Integration
  - `FlowOrchestrator.Scheduling.Quartz`: Quartz.NET Integration
  - `FlowOrchestrator.Telemetry.OpenTelemetry`: OpenTelemetry Integration
  - `FlowOrchestrator.ProtocolAdapters`: Protocol adapters for various communication protocols

### Test Projects
Test projects provide comprehensive testing at different levels:

- **Unit Tests**: Testing individual components in isolation
- **Integration Tests**: Testing interactions between components
- **System Tests**: Testing the entire system end-to-end

## Getting Started

To build the solution:

```bash
dotnet build
```

To run tests:

```bash
dotnet test
```

## Project Structure Implementation

The solution structure has been implemented according to the specifications in the System Overview document section 1.5.2. The implementation includes:

1. **Core Projects**: Fundamental abstractions, interfaces, and models
2. **Execution Projects**: Services for flow execution and orchestration
3. **Processing Projects**: Components for data transformation and validation
4. **Management Projects**: Services for system management and configuration
5. **Observability Projects**: Services for monitoring and analytics
6. **Infrastructure Projects**: Integration with external systems and frameworks
7. **Test Projects**: Comprehensive testing at different levels
8. **Documentation**: Architecture documentation, API documentation, and implementation guides
9. **Tools**: Build scripts, deployment tools, and development utilities
10. **Samples**: Example flows demonstrating system capabilities

## Documentation

For more detailed information, refer to the Software Architecture Document (SAD) in the docs directory.
