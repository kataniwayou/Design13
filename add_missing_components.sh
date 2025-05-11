#!/bin/bash

# Navigate to the project directory
cd /c/Users/Administrator/Documents/Cline/FlowArchitectureSystem/Design13

# Create missing top-level directories
echo "Creating missing top-level directories..."
mkdir -p tools/build tools/deployment tools/development
mkdir -p samples/SimpleFlow samples/BranchedFlow samples/ComplexTransformation
mkdir -p docs/api docs/guides

# Create missing source directories
echo "Creating missing source directories..."
mkdir -p src/Processing
mkdir -p src/Management

# Move projects from Execution to Management
echo "Moving projects from Execution to Management..."
# Note: We'll create new projects in Management and then remove the old ones from Execution later

# Create Processing projects
echo "Creating Processing projects..."
dotnet new classlib -n FlowOrchestrator.ProcessorBase -o src/Processing/FlowOrchestrator.ProcessorBase
dotnet new classlib -n FlowOrchestrator.JsonProcessor -o src/Processing/FlowOrchestrator.JsonProcessor
dotnet new classlib -n FlowOrchestrator.ValidationProcessor -o src/Processing/FlowOrchestrator.ValidationProcessor
dotnet new classlib -n FlowOrchestrator.EnrichmentProcessor -o src/Processing/FlowOrchestrator.EnrichmentProcessor
dotnet new classlib -n FlowOrchestrator.MappingProcessor -o src/Processing/FlowOrchestrator.MappingProcessor

# Create Management projects
echo "Creating Management projects..."
dotnet new webapi -n FlowOrchestrator.ServiceManager -o src/Management/FlowOrchestrator.ServiceManager
dotnet new webapi -n FlowOrchestrator.FlowManager -o src/Management/FlowOrchestrator.FlowManager
dotnet new webapi -n FlowOrchestrator.ConfigurationManager -o src/Management/FlowOrchestrator.ConfigurationManager
dotnet new webapi -n FlowOrchestrator.VersionManager -o src/Management/FlowOrchestrator.VersionManager
dotnet new worker -n FlowOrchestrator.TaskScheduler -o src/Management/FlowOrchestrator.TaskScheduler

# Create missing Execution projects
echo "Creating missing Execution projects..."
dotnet new worker -n FlowOrchestrator.DatabaseExporter -o src/Execution/FlowOrchestrator.DatabaseExporter
dotnet new worker -n FlowOrchestrator.MessageQueueExporter -o src/Execution/FlowOrchestrator.MessageQueueExporter
dotnet new classlib -n FlowOrchestrator.ProtocolAdapters -o src/Execution/FlowOrchestrator.ProtocolAdapters

# Create missing test projects
echo "Creating missing test projects..."
dotnet new xunit -n FlowOrchestrator.ImporterBase.Tests -o tests/Unit/FlowOrchestrator.ImporterBase.Tests
dotnet new xunit -n FlowOrchestrator.IntegrationDomain.Tests -o tests/Integration/FlowOrchestrator.IntegrationDomain.Tests
dotnet new xunit -n FlowOrchestrator.Reliability.Tests -o tests/System/FlowOrchestrator.Reliability.Tests

# Add projects to solution
echo "Adding projects to solution..."

# Add Processing projects to solution
dotnet sln add src/Processing/FlowOrchestrator.ProcessorBase/FlowOrchestrator.ProcessorBase.csproj
dotnet sln add src/Processing/FlowOrchestrator.JsonProcessor/FlowOrchestrator.JsonProcessor.csproj
dotnet sln add src/Processing/FlowOrchestrator.ValidationProcessor/FlowOrchestrator.ValidationProcessor.csproj
dotnet sln add src/Processing/FlowOrchestrator.EnrichmentProcessor/FlowOrchestrator.EnrichmentProcessor.csproj
dotnet sln add src/Processing/FlowOrchestrator.MappingProcessor/FlowOrchestrator.MappingProcessor.csproj

# Add Management projects to solution
dotnet sln add src/Management/FlowOrchestrator.ServiceManager/FlowOrchestrator.ServiceManager.csproj
dotnet sln add src/Management/FlowOrchestrator.FlowManager/FlowOrchestrator.FlowManager.csproj
dotnet sln add src/Management/FlowOrchestrator.ConfigurationManager/FlowOrchestrator.ConfigurationManager.csproj
dotnet sln add src/Management/FlowOrchestrator.VersionManager/FlowOrchestrator.VersionManager.csproj
dotnet sln add src/Management/FlowOrchestrator.TaskScheduler/FlowOrchestrator.TaskScheduler.csproj

# Add missing Execution projects to solution
dotnet sln add src/Execution/FlowOrchestrator.DatabaseExporter/FlowOrchestrator.DatabaseExporter.csproj
dotnet sln add src/Execution/FlowOrchestrator.MessageQueueExporter/FlowOrchestrator.MessageQueueExporter.csproj
dotnet sln add src/Execution/FlowOrchestrator.ProtocolAdapters/FlowOrchestrator.ProtocolAdapters.csproj

# Add missing test projects to solution
dotnet sln add tests/Unit/FlowOrchestrator.ImporterBase.Tests/FlowOrchestrator.ImporterBase.Tests.csproj
dotnet sln add tests/Integration/FlowOrchestrator.IntegrationDomain.Tests/FlowOrchestrator.IntegrationDomain.Tests.csproj
dotnet sln add tests/System/FlowOrchestrator.Reliability.Tests/FlowOrchestrator.Reliability.Tests.csproj

# Remove duplicate projects from solution (the ones we moved from Execution to Management)
echo "Removing duplicate projects from solution..."
dotnet sln remove src/Execution/FlowOrchestrator.ServiceManager/FlowOrchestrator.ServiceManager.csproj
dotnet sln remove src/Execution/FlowOrchestrator.FlowManager/FlowOrchestrator.FlowManager.csproj
dotnet sln remove src/Execution/FlowOrchestrator.ConfigurationManager/FlowOrchestrator.ConfigurationManager.csproj
dotnet sln remove src/Execution/FlowOrchestrator.VersionManager/FlowOrchestrator.VersionManager.csproj
dotnet sln remove src/Execution/FlowOrchestrator.TaskScheduler/FlowOrchestrator.TaskScheduler.csproj

# Create placeholder files in the tools and samples directories
echo "Creating placeholder files in tools and samples directories..."

# Tools placeholders
echo "# Build Scripts and Tools" > tools/build/README.md
echo "This directory contains build scripts and tools for the FlowOrchestrator system." >> tools/build/README.md

echo "# Deployment Scripts and Tools" > tools/deployment/README.md
echo "This directory contains deployment scripts and tools for the FlowOrchestrator system." >> tools/deployment/README.md

echo "# Development Tools and Utilities" > tools/development/README.md
echo "This directory contains development tools and utilities for the FlowOrchestrator system." >> tools/development/README.md

# Samples placeholders
echo "# Simple Flow Example" > samples/SimpleFlow/README.md
echo "This directory contains a simple flow example for the FlowOrchestrator system." >> samples/SimpleFlow/README.md

echo "# Branched Flow Example" > samples/BranchedFlow/README.md
echo "This directory contains a branched flow example for the FlowOrchestrator system." >> samples/BranchedFlow/README.md

echo "# Complex Transformation Example" > samples/ComplexTransformation/README.md
echo "This directory contains a complex transformation example for the FlowOrchestrator system." >> samples/ComplexTransformation/README.md

# Docs placeholders
echo "# API Documentation" > docs/api/README.md
echo "This directory contains API documentation for the FlowOrchestrator system." >> docs/api/README.md

echo "# Implementation Guides" > docs/guides/README.md
echo "This directory contains implementation guides for the FlowOrchestrator system." >> docs/guides/README.md

echo "Missing components added successfully!"
