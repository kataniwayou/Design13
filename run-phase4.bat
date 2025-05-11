@echo off
echo ===================================================
echo Phase 4 Execution Process
echo ===================================================
echo.

REM Set the base directory
set BASE_DIR=%~dp0
set SRC_DIR=%BASE_DIR%src\Integration
set TEST_DIR=%BASE_DIR%tests

echo Step 1: Running Build Process...
echo ---------------------------------------------------
call build-phase4.bat
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Build process failed.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] Build process completed successfully.
echo.

echo Step 2: Running Unit Tests...
echo ---------------------------------------------------
echo Running ImporterBase Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.ImporterBase.Tests\FlowOrchestrator.ImporterBase.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] ImporterBase Tests failed.
) else (
    echo [SUCCESS] ImporterBase Tests passed.
)
echo.

echo Running ExporterBase Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.ExporterBase.Tests\FlowOrchestrator.ExporterBase.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] ExporterBase Tests failed.
) else (
    echo [SUCCESS] ExporterBase Tests passed.
)
echo.

echo Running FileImporter Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.FileImporter.Tests\FlowOrchestrator.FileImporter.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] FileImporter Tests failed.
) else (
    echo [SUCCESS] FileImporter Tests passed.
)
echo.

echo Running RestImporter Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.RestImporter.Tests\FlowOrchestrator.RestImporter.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] RestImporter Tests failed.
) else (
    echo [SUCCESS] RestImporter Tests passed.
)
echo.

echo Running DatabaseImporter Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.DatabaseImporter.Tests\FlowOrchestrator.DatabaseImporter.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] DatabaseImporter Tests failed.
) else (
    echo [SUCCESS] DatabaseImporter Tests passed.
)
echo.

echo Running MessageQueueImporter Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.MessageQueueImporter.Tests\FlowOrchestrator.MessageQueueImporter.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] MessageQueueImporter Tests failed.
) else (
    echo [SUCCESS] MessageQueueImporter Tests passed.
)
echo.

echo Running FileExporter Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.FileExporter.Tests\FlowOrchestrator.FileExporter.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] FileExporter Tests failed.
) else (
    echo [SUCCESS] FileExporter Tests passed.
)
echo.

echo Running RestExporter Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.RestExporter.Tests\FlowOrchestrator.RestExporter.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] RestExporter Tests failed.
) else (
    echo [SUCCESS] RestExporter Tests passed.
)
echo.

echo Running DatabaseExporter Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.DatabaseExporter.Tests\FlowOrchestrator.DatabaseExporter.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] DatabaseExporter Tests failed.
) else (
    echo [SUCCESS] DatabaseExporter Tests passed.
)
echo.

echo Running MessageQueueExporter Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.MessageQueueExporter.Tests\FlowOrchestrator.MessageQueueExporter.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] MessageQueueExporter Tests failed.
) else (
    echo [SUCCESS] MessageQueueExporter Tests passed.
)
echo.

echo Running ProtocolAdapters Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.ProtocolAdapters.Tests\FlowOrchestrator.ProtocolAdapters.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] ProtocolAdapters Tests failed.
) else (
    echo [SUCCESS] ProtocolAdapters Tests passed.
)
echo.

echo Step 3: Running Integration Tests...
echo ---------------------------------------------------
echo Running IntegrationDomain Tests...
dotnet test "%TEST_DIR%\Integration\FlowOrchestrator.IntegrationDomain.Tests\FlowOrchestrator.IntegrationDomain.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] IntegrationDomain Tests failed.
) else (
    echo [SUCCESS] IntegrationDomain Tests passed.
)
echo.

echo Step 4: Running Verification Process...
echo ---------------------------------------------------
call verify-phase4.bat
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Verification process failed.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] Verification process completed successfully.
echo.

echo ===================================================
echo Phase 4 Execution Process Completed
echo ===================================================
echo.
echo All Phase 4 components have been built, tested, and verified.
