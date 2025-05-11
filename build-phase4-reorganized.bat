@echo off
echo ===================================================
echo FlowOrchestrator Phase 4 Incremental Build Process
echo Integration Domain Components (Reorganized)
echo ===================================================
echo.

echo Step 1: Building Base Components...
dotnet build src/Integration/FlowOrchestrator.ImporterBase/FlowOrchestrator.ImporterBase.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building ImporterBase
    exit /b %ERRORLEVEL%
)
echo ImporterBase built successfully.

dotnet build src/Integration/FlowOrchestrator.ExporterBase/FlowOrchestrator.ExporterBase.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building ExporterBase
    exit /b %ERRORLEVEL%
)
echo ExporterBase built successfully.

echo.
echo Step 2: Building Importer Components...
dotnet build src/Integration/FlowOrchestrator.FileImporter/FlowOrchestrator.FileImporter.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FileImporter
    exit /b %ERRORLEVEL%
)
echo FileImporter built successfully.

dotnet build src/Integration/FlowOrchestrator.RestImporter/FlowOrchestrator.RestImporter.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building RestImporter
    exit /b %ERRORLEVEL%
)
echo RestImporter built successfully.

dotnet build src/Integration/FlowOrchestrator.DatabaseImporter/FlowOrchestrator.DatabaseImporter.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building DatabaseImporter
    exit /b %ERRORLEVEL%
)
echo DatabaseImporter built successfully.

dotnet build src/Integration/FlowOrchestrator.MessageQueueImporter/FlowOrchestrator.MessageQueueImporter.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building MessageQueueImporter
    exit /b %ERRORLEVEL%
)
echo MessageQueueImporter built successfully.

echo.
echo Step 3: Building Exporter Components...
dotnet build src/Integration/FlowOrchestrator.FileExporter/FlowOrchestrator.FileExporter.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FileExporter
    exit /b %ERRORLEVEL%
)
echo FileExporter built successfully.

dotnet build src/Integration/FlowOrchestrator.RestExporter/FlowOrchestrator.RestExporter.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building RestExporter
    exit /b %ERRORLEVEL%
)
echo RestExporter built successfully.

dotnet build src/Integration/FlowOrchestrator.DatabaseExporter/FlowOrchestrator.DatabaseExporter.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building DatabaseExporter
    exit /b %ERRORLEVEL%
)
echo DatabaseExporter built successfully.

dotnet build src/Integration/FlowOrchestrator.MessageQueueExporter/FlowOrchestrator.MessageQueueExporter.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building MessageQueueExporter
    exit /b %ERRORLEVEL%
)
echo MessageQueueExporter built successfully.

echo.
echo Step 4: Building Protocol Adapters...
dotnet build src/Integration/FlowOrchestrator.ProtocolAdapters/FlowOrchestrator.ProtocolAdapters.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building ProtocolAdapters
    exit /b %ERRORLEVEL%
)
echo ProtocolAdapters built successfully.

echo.
echo Step 5: Building Integration Tests...
dotnet build tests/Integration/FlowOrchestrator.IntegrationDomain.Tests/FlowOrchestrator.IntegrationDomain.Tests.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building IntegrationDomain.Tests
    exit /b %ERRORLEVEL%
)
echo IntegrationDomain.Tests built successfully.

echo.
echo Step 6: Building Full Solution...
dotnet build
if %ERRORLEVEL% neq 0 (
    echo Error building full solution
    exit /b %ERRORLEVEL%
)
echo Full solution built successfully.

echo.
echo ===================================================
echo Phase 4 Incremental Build Process Completed Successfully
echo ===================================================
