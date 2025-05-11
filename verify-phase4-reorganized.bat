@echo off
echo ===================================================
echo FlowOrchestrator Phase 4 Verification Process
echo Integration Domain Components (Reorganized)
echo ===================================================
echo.

echo Step 1: Verifying Directory Structure...
if not exist src\Integration (
    echo Error: Integration directory does not exist
    exit /b 1
)
echo Integration directory exists.

if not exist src\Integration\FlowOrchestrator.ImporterBase (
    echo Error: ImporterBase directory does not exist
    exit /b 1
)
echo ImporterBase directory exists.

if not exist src\Integration\FlowOrchestrator.ExporterBase (
    echo Error: ExporterBase directory does not exist
    exit /b 1
)
echo ExporterBase directory exists.

if not exist src\Integration\FlowOrchestrator.FileImporter (
    echo Error: FileImporter directory does not exist
    exit /b 1
)
echo FileImporter directory exists.

if not exist src\Integration\FlowOrchestrator.RestImporter (
    echo Error: RestImporter directory does not exist
    exit /b 1
)
echo RestImporter directory exists.

if not exist src\Integration\FlowOrchestrator.DatabaseImporter (
    echo Error: DatabaseImporter directory does not exist
    exit /b 1
)
echo DatabaseImporter directory exists.

if not exist src\Integration\FlowOrchestrator.MessageQueueImporter (
    echo Error: MessageQueueImporter directory does not exist
    exit /b 1
)
echo MessageQueueImporter directory exists.

if not exist src\Integration\FlowOrchestrator.FileExporter (
    echo Error: FileExporter directory does not exist
    exit /b 1
)
echo FileExporter directory exists.

if not exist src\Integration\FlowOrchestrator.RestExporter (
    echo Error: RestExporter directory does not exist
    exit /b 1
)
echo RestExporter directory exists.

if not exist src\Integration\FlowOrchestrator.DatabaseExporter (
    echo Error: DatabaseExporter directory does not exist
    exit /b 1
)
echo DatabaseExporter directory exists.

if not exist src\Integration\FlowOrchestrator.MessageQueueExporter (
    echo Error: MessageQueueExporter directory does not exist
    exit /b 1
)
echo MessageQueueExporter directory exists.

if not exist src\Integration\FlowOrchestrator.ProtocolAdapters (
    echo Error: ProtocolAdapters directory does not exist
    exit /b 1
)
echo ProtocolAdapters directory exists.

echo.
echo Step 2: Verifying Build Artifacts...
if not exist src\Integration\FlowOrchestrator.ImporterBase\bin\Debug\net9.0\FlowOrchestrator.ImporterBase.dll (
    echo Error: ImporterBase build artifact does not exist
    exit /b 1
)
echo ImporterBase build artifact exists.

if not exist src\Integration\FlowOrchestrator.ExporterBase\bin\Debug\net9.0\FlowOrchestrator.ExporterBase.dll (
    echo Error: ExporterBase build artifact does not exist
    exit /b 1
)
echo ExporterBase build artifact exists.

if not exist src\Integration\FlowOrchestrator.DatabaseExporter\bin\Debug\net9.0\FlowOrchestrator.DatabaseExporter.dll (
    echo Error: DatabaseExporter build artifact does not exist
    exit /b 1
)
echo DatabaseExporter build artifact exists.

if not exist src\Integration\FlowOrchestrator.MessageQueueExporter\bin\Debug\net9.0\FlowOrchestrator.MessageQueueExporter.dll (
    echo Error: MessageQueueExporter build artifact does not exist
    exit /b 1
)
echo MessageQueueExporter build artifact exists.

if not exist src\Integration\FlowOrchestrator.ProtocolAdapters\bin\Debug\net9.0\FlowOrchestrator.ProtocolAdapters.dll (
    echo Error: ProtocolAdapters build artifact does not exist
    exit /b 1
)
echo ProtocolAdapters build artifact exists.

echo.
echo Step 3: Running Integration Tests...
dotnet test tests/Integration/FlowOrchestrator.IntegrationDomain.Tests/FlowOrchestrator.IntegrationDomain.Tests.csproj
if %ERRORLEVEL% neq 0 (
    echo Error running integration tests
    exit /b %ERRORLEVEL%
)
echo Integration tests passed successfully.

echo.
echo ===================================================
echo Phase 4 Verification Process Completed Successfully
echo ===================================================
