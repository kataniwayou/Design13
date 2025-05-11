#!/bin/bash

# Navigate to the project directory
cd /c/Users/Administrator/Documents/Cline/FlowArchitectureSystem/Design13

# Create solution file if it doesn't exist
if [ ! -f "FlowOrchestrator.sln" ]; then
    dotnet new sln -n FlowOrchestrator
    echo "Created solution file: FlowOrchestrator.sln"
fi

# Create directory structure
mkdir -p src/Core src/Execution src/Observability src/Infrastructure tests/Unit tests/Integration tests/System docs/architecture
echo "Created directory structure"

# Create Core projects
echo "Creating Core projects..."
dotnet new classlib -n FlowOrchestrator.Common -o src/Core/FlowOrchestrator.Common
dotnet new classlib -n FlowOrchestrator.Abstractions -o src/Core/FlowOrchestrator.Abstractions
dotnet new classlib -n FlowOrchestrator.Domain -o src/Core/FlowOrchestrator.Domain
dotnet new classlib -n FlowOrchestrator.Infrastructure.Common -o src/Core/FlowOrchestrator.Infrastructure.Common
dotnet new classlib -n FlowOrchestrator.Security.Common -o src/Core/FlowOrchestrator.Security.Common

# Add Core projects to solution
echo "Adding Core projects to solution..."
dotnet sln add src/Core/FlowOrchestrator.Common/FlowOrchestrator.Common.csproj
dotnet sln add src/Core/FlowOrchestrator.Abstractions/FlowOrchestrator.Abstractions.csproj
dotnet sln add src/Core/FlowOrchestrator.Domain/FlowOrchestrator.Domain.csproj
dotnet sln add src/Core/FlowOrchestrator.Infrastructure.Common/FlowOrchestrator.Infrastructure.Common.csproj
dotnet sln add src/Core/FlowOrchestrator.Security.Common/FlowOrchestrator.Security.Common.csproj

# Create Execution projects
echo "Creating Execution projects..."
dotnet new webapi -n FlowOrchestrator.Orchestrator -o src/Execution/FlowOrchestrator.Orchestrator
dotnet new worker -n FlowOrchestrator.MemoryManager -o src/Execution/FlowOrchestrator.MemoryManager
dotnet new worker -n FlowOrchestrator.BranchController -o src/Execution/FlowOrchestrator.BranchController
dotnet new webapi -n FlowOrchestrator.ServiceManager -o src/Execution/FlowOrchestrator.ServiceManager
dotnet new webapi -n FlowOrchestrator.FlowManager -o src/Execution/FlowOrchestrator.FlowManager
dotnet new webapi -n FlowOrchestrator.ConfigurationManager -o src/Execution/FlowOrchestrator.ConfigurationManager
dotnet new webapi -n FlowOrchestrator.VersionManager -o src/Execution/FlowOrchestrator.VersionManager
dotnet new worker -n FlowOrchestrator.TaskScheduler -o src/Execution/FlowOrchestrator.TaskScheduler

# Add Execution projects to solution
echo "Adding Execution projects to solution..."
dotnet sln add src/Execution/FlowOrchestrator.Orchestrator/FlowOrchestrator.Orchestrator.csproj
dotnet sln add src/Execution/FlowOrchestrator.MemoryManager/FlowOrchestrator.MemoryManager.csproj
dotnet sln add src/Execution/FlowOrchestrator.BranchController/FlowOrchestrator.BranchController.csproj
dotnet sln add src/Execution/FlowOrchestrator.ServiceManager/FlowOrchestrator.ServiceManager.csproj
dotnet sln add src/Execution/FlowOrchestrator.FlowManager/FlowOrchestrator.FlowManager.csproj
dotnet sln add src/Execution/FlowOrchestrator.ConfigurationManager/FlowOrchestrator.ConfigurationManager.csproj
dotnet sln add src/Execution/FlowOrchestrator.VersionManager/FlowOrchestrator.VersionManager.csproj
dotnet sln add src/Execution/FlowOrchestrator.TaskScheduler/FlowOrchestrator.TaskScheduler.csproj

# Create Observability projects
echo "Creating Observability projects..."
dotnet new webapi -n FlowOrchestrator.StatisticsService -o src/Observability/FlowOrchestrator.StatisticsService
dotnet new webapi -n FlowOrchestrator.MonitoringFramework -o src/Observability/FlowOrchestrator.MonitoringFramework
dotnet new worker -n FlowOrchestrator.AlertingSystem -o src/Observability/FlowOrchestrator.AlertingSystem
dotnet new webapi -n FlowOrchestrator.AnalyticsEngine -o src/Observability/FlowOrchestrator.AnalyticsEngine

# Add Observability projects to solution
echo "Adding Observability projects to solution..."
dotnet sln add src/Observability/FlowOrchestrator.StatisticsService/FlowOrchestrator.StatisticsService.csproj
dotnet sln add src/Observability/FlowOrchestrator.MonitoringFramework/FlowOrchestrator.MonitoringFramework.csproj
dotnet sln add src/Observability/FlowOrchestrator.AlertingSystem/FlowOrchestrator.AlertingSystem.csproj
dotnet sln add src/Observability/FlowOrchestrator.AnalyticsEngine/FlowOrchestrator.AnalyticsEngine.csproj

# Create Infrastructure projects
echo "Creating Infrastructure projects..."
dotnet new classlib -n FlowOrchestrator.Data.MongoDB -o src/Infrastructure/FlowOrchestrator.Data.MongoDB
dotnet new classlib -n FlowOrchestrator.Data.Hazelcast -o src/Infrastructure/FlowOrchestrator.Data.Hazelcast
dotnet new classlib -n FlowOrchestrator.Messaging.MassTransit -o src/Infrastructure/FlowOrchestrator.Messaging.MassTransit
dotnet new classlib -n FlowOrchestrator.Scheduling.Quartz -o src/Infrastructure/FlowOrchestrator.Scheduling.Quartz
dotnet new classlib -n FlowOrchestrator.Telemetry.OpenTelemetry -o src/Infrastructure/FlowOrchestrator.Telemetry.OpenTelemetry

# Add Infrastructure projects to solution
echo "Adding Infrastructure projects to solution..."
dotnet sln add src/Infrastructure/FlowOrchestrator.Data.MongoDB/FlowOrchestrator.Data.MongoDB.csproj
dotnet sln add src/Infrastructure/FlowOrchestrator.Data.Hazelcast/FlowOrchestrator.Data.Hazelcast.csproj
dotnet sln add src/Infrastructure/FlowOrchestrator.Messaging.MassTransit/FlowOrchestrator.Messaging.MassTransit.csproj
dotnet sln add src/Infrastructure/FlowOrchestrator.Scheduling.Quartz/FlowOrchestrator.Scheduling.Quartz.csproj
dotnet sln add src/Infrastructure/FlowOrchestrator.Telemetry.OpenTelemetry/FlowOrchestrator.Telemetry.OpenTelemetry.csproj

# Create Unit Test projects
echo "Creating Unit Test projects..."
dotnet new xunit -n FlowOrchestrator.Common.Tests -o tests/Unit/FlowOrchestrator.Common.Tests
dotnet new xunit -n FlowOrchestrator.Orchestrator.Tests -o tests/Unit/FlowOrchestrator.Orchestrator.Tests
dotnet new xunit -n FlowOrchestrator.Domain.Tests -o tests/Unit/FlowOrchestrator.Domain.Tests

# Add Unit Test projects to solution
echo "Adding Unit Test projects to solution..."
dotnet sln add tests/Unit/FlowOrchestrator.Common.Tests/FlowOrchestrator.Common.Tests.csproj
dotnet sln add tests/Unit/FlowOrchestrator.Orchestrator.Tests/FlowOrchestrator.Orchestrator.Tests.csproj
dotnet sln add tests/Unit/FlowOrchestrator.Domain.Tests/FlowOrchestrator.Domain.Tests.csproj

# Create Integration Test projects
echo "Creating Integration Test projects..."
dotnet new xunit -n FlowOrchestrator.ExecutionDomain.Tests -o tests/Integration/FlowOrchestrator.ExecutionDomain.Tests
dotnet new xunit -n FlowOrchestrator.Infrastructure.Tests -o tests/Integration/FlowOrchestrator.Infrastructure.Tests

# Add Integration Test projects to solution
echo "Adding Integration Test projects to solution..."
dotnet sln add tests/Integration/FlowOrchestrator.ExecutionDomain.Tests/FlowOrchestrator.ExecutionDomain.Tests.csproj
dotnet sln add tests/Integration/FlowOrchestrator.Infrastructure.Tests/FlowOrchestrator.Infrastructure.Tests.csproj

# Create System Test projects
echo "Creating System Test projects..."
dotnet new xunit -n FlowOrchestrator.EndToEnd.Tests -o tests/System/FlowOrchestrator.EndToEnd.Tests
dotnet new xunit -n FlowOrchestrator.Performance.Tests -o tests/System/FlowOrchestrator.Performance.Tests

# Add System Test projects to solution
echo "Adding System Test projects to solution..."
dotnet sln add tests/System/FlowOrchestrator.EndToEnd.Tests/FlowOrchestrator.EndToEnd.Tests.csproj
dotnet sln add tests/System/FlowOrchestrator.Performance.Tests/FlowOrchestrator.Performance.Tests.csproj

echo "Solution structure creation completed!"
