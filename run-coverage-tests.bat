@echo off
echo ===================================================
echo Running Coverage Tests for Flow Orchestrator
echo ===================================================

echo Creating coverage results directory...
if not exist "coverage" mkdir coverage

echo Running Core Domain tests with coverage...
dotnet test tests/FlowOrchestrator.UnitTests/FlowOrchestrator.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=../../coverage/core.cobertura.xml /p:Include=\"[FlowOrchestrator.Domain]*,[FlowOrchestrator.Abstractions]*,[FlowOrchestrator.Common]*\" --no-restore

echo Running Management tests with coverage...
dotnet test tests/FlowOrchestrator.UnitTests/FlowOrchestrator.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=../../coverage/management.cobertura.xml /p:Include=\"[FlowOrchestrator.FlowManager]*,[FlowOrchestrator.VersionManager]*\" --no-restore

echo Running Integration tests with coverage...
dotnet test tests/FlowOrchestrator.IntegrationTests/FlowOrchestrator.IntegrationTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=../../coverage/integration.cobertura.xml --no-restore

echo Running System tests with coverage...
dotnet test tests/FlowOrchestrator.SystemTests/FlowOrchestrator.SystemTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=../../coverage/system.cobertura.xml --no-restore

echo Generating coverage report...
reportgenerator -reports:coverage/*.xml -targetdir:coverage/report -reporttypes:Html

echo ===================================================
echo Coverage tests completed!
echo Coverage report generated in coverage/report
echo ===================================================

echo Opening coverage report...
start coverage/report/index.html
