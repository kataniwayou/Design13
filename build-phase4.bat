@echo off
echo ===================================================
echo Phase 4 Build Process
echo ===================================================
echo.

REM Set the base directory
set BASE_DIR=%~dp0
set SRC_DIR=%BASE_DIR%src
set TEST_DIR=%BASE_DIR%tests

REM Create the Integration directory if it doesn't exist
if not exist "%SRC_DIR%\Integration" (
    echo Creating Integration directory...
    mkdir "%SRC_DIR%\Integration"
    echo Created Integration directory.
    echo.
)

REM Move existing components from Execution to Integration if they exist
if exist "%SRC_DIR%\Execution\FlowOrchestrator.DatabaseExporter" (
    echo Moving DatabaseExporter from Execution to Integration...
    xcopy /E /I /Y "%SRC_DIR%\Execution\FlowOrchestrator.DatabaseExporter" "%SRC_DIR%\Integration\FlowOrchestrator.DatabaseExporter"
    rd /S /Q "%SRC_DIR%\Execution\FlowOrchestrator.DatabaseExporter"
    echo Moved DatabaseExporter.
    echo.
)

if exist "%SRC_DIR%\Execution\FlowOrchestrator.MessageQueueExporter" (
    echo Moving MessageQueueExporter from Execution to Integration...
    xcopy /E /I /Y "%SRC_DIR%\Execution\FlowOrchestrator.MessageQueueExporter" "%SRC_DIR%\Integration\FlowOrchestrator.MessageQueueExporter"
    rd /S /Q "%SRC_DIR%\Execution\FlowOrchestrator.MessageQueueExporter"
    echo Moved MessageQueueExporter.
    echo.
)

if exist "%SRC_DIR%\Execution\FlowOrchestrator.ProtocolAdapters" (
    echo Moving ProtocolAdapters from Execution to Integration...
    xcopy /E /I /Y "%SRC_DIR%\Execution\FlowOrchestrator.ProtocolAdapters" "%SRC_DIR%\Integration\FlowOrchestrator.ProtocolAdapters"
    rd /S /Q "%SRC_DIR%\Execution\FlowOrchestrator.ProtocolAdapters"
    echo Moved ProtocolAdapters.
    echo.
)

REM Create base components if they don't exist
if not exist "%SRC_DIR%\Integration\FlowOrchestrator.ImporterBase" (
    echo Creating ImporterBase project...
    mkdir "%SRC_DIR%\Integration\FlowOrchestrator.ImporterBase"
    dotnet new classlib -o "%SRC_DIR%\Integration\FlowOrchestrator.ImporterBase" --framework net9.0
    echo Created ImporterBase project.
    echo.
)

if not exist "%SRC_DIR%\Integration\FlowOrchestrator.ExporterBase" (
    echo Creating ExporterBase project...
    mkdir "%SRC_DIR%\Integration\FlowOrchestrator.ExporterBase"
    dotnet new classlib -o "%SRC_DIR%\Integration\FlowOrchestrator.ExporterBase" --framework net9.0
    echo Created ExporterBase project.
    echo.
)

REM Create importer components if they don't exist
if not exist "%SRC_DIR%\Integration\FlowOrchestrator.FileImporter" (
    echo Creating FileImporter project...
    mkdir "%SRC_DIR%\Integration\FlowOrchestrator.FileImporter"
    dotnet new classlib -o "%SRC_DIR%\Integration\FlowOrchestrator.FileImporter" --framework net9.0
    echo Created FileImporter project.
    echo.
)

if not exist "%SRC_DIR%\Integration\FlowOrchestrator.RestImporter" (
    echo Creating RestImporter project...
    mkdir "%SRC_DIR%\Integration\FlowOrchestrator.RestImporter"
    dotnet new classlib -o "%SRC_DIR%\Integration\FlowOrchestrator.RestImporter" --framework net9.0
    echo Created RestImporter project.
    echo.
)

if not exist "%SRC_DIR%\Integration\FlowOrchestrator.DatabaseImporter" (
    echo Creating DatabaseImporter project...
    mkdir "%SRC_DIR%\Integration\FlowOrchestrator.DatabaseImporter"
    dotnet new classlib -o "%SRC_DIR%\Integration\FlowOrchestrator.DatabaseImporter" --framework net9.0
    echo Created DatabaseImporter project.
    echo.
)

if not exist "%SRC_DIR%\Integration\FlowOrchestrator.MessageQueueImporter" (
    echo Creating MessageQueueImporter project...
    mkdir "%SRC_DIR%\Integration\FlowOrchestrator.MessageQueueImporter"
    dotnet new classlib -o "%SRC_DIR%\Integration\FlowOrchestrator.MessageQueueImporter" --framework net9.0
    echo Created MessageQueueImporter project.
    echo.
)

REM Create exporter components if they don't exist
if not exist "%SRC_DIR%\Integration\FlowOrchestrator.FileExporter" (
    echo Creating FileExporter project...
    mkdir "%SRC_DIR%\Integration\FlowOrchestrator.FileExporter"
    dotnet new classlib -o "%SRC_DIR%\Integration\FlowOrchestrator.FileExporter" --framework net9.0
    echo Created FileExporter project.
    echo.
)

if not exist "%SRC_DIR%\Integration\FlowOrchestrator.RestExporter" (
    echo Creating RestExporter project...
    mkdir "%SRC_DIR%\Integration\FlowOrchestrator.RestExporter"
    dotnet new classlib -o "%SRC_DIR%\Integration\FlowOrchestrator.RestExporter" --framework net9.0
    echo Created RestExporter project.
    echo.
)

REM Create test projects if they don't exist
if not exist "%TEST_DIR%\Unit\FlowOrchestrator.ImporterBase.Tests" (
    echo Creating ImporterBase.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.ImporterBase.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.ImporterBase.Tests" --framework net9.0
    echo Created ImporterBase.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.ExporterBase.Tests" (
    echo Creating ExporterBase.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.ExporterBase.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.ExporterBase.Tests" --framework net9.0
    echo Created ExporterBase.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.FileImporter.Tests" (
    echo Creating FileImporter.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.FileImporter.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.FileImporter.Tests" --framework net9.0
    echo Created FileImporter.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.RestImporter.Tests" (
    echo Creating RestImporter.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.RestImporter.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.RestImporter.Tests" --framework net9.0
    echo Created RestImporter.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.DatabaseImporter.Tests" (
    echo Creating DatabaseImporter.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.DatabaseImporter.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.DatabaseImporter.Tests" --framework net9.0
    echo Created DatabaseImporter.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.MessageQueueImporter.Tests" (
    echo Creating MessageQueueImporter.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.MessageQueueImporter.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.MessageQueueImporter.Tests" --framework net9.0
    echo Created MessageQueueImporter.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.FileExporter.Tests" (
    echo Creating FileExporter.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.FileExporter.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.FileExporter.Tests" --framework net9.0
    echo Created FileExporter.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.RestExporter.Tests" (
    echo Creating RestExporter.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.RestExporter.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.RestExporter.Tests" --framework net9.0
    echo Created RestExporter.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.DatabaseExporter.Tests" (
    echo Creating DatabaseExporter.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.DatabaseExporter.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.DatabaseExporter.Tests" --framework net9.0
    echo Created DatabaseExporter.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.MessageQueueExporter.Tests" (
    echo Creating MessageQueueExporter.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.MessageQueueExporter.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.MessageQueueExporter.Tests" --framework net9.0
    echo Created MessageQueueExporter.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.ProtocolAdapters.Tests" (
    echo Creating ProtocolAdapters.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.ProtocolAdapters.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.ProtocolAdapters.Tests" --framework net9.0
    echo Created ProtocolAdapters.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Integration\FlowOrchestrator.IntegrationDomain.Tests" (
    echo Creating IntegrationDomain.Tests project...
    mkdir "%TEST_DIR%\Integration\FlowOrchestrator.IntegrationDomain.Tests"
    dotnet new xunit -o "%TEST_DIR%\Integration\FlowOrchestrator.IntegrationDomain.Tests" --framework net9.0
    echo Created IntegrationDomain.Tests project.
    echo.
)

echo Step 1: Building ImporterBase...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\Integration\FlowOrchestrator.ImporterBase\FlowOrchestrator.ImporterBase.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build ImporterBase.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] ImporterBase built successfully.
echo.

echo Step 2: Building ExporterBase...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\Integration\FlowOrchestrator.ExporterBase\FlowOrchestrator.ExporterBase.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build ExporterBase.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] ExporterBase built successfully.
echo.

echo Step 3: Building FileImporter...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\Integration\FlowOrchestrator.FileImporter\FlowOrchestrator.FileImporter.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build FileImporter.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] FileImporter built successfully.
echo.

echo Step 4: Building RestImporter...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\Integration\FlowOrchestrator.RestImporter\FlowOrchestrator.RestImporter.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build RestImporter.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] RestImporter built successfully.
echo.

echo Step 5: Building DatabaseImporter...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\Integration\FlowOrchestrator.DatabaseImporter\FlowOrchestrator.DatabaseImporter.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build DatabaseImporter.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] DatabaseImporter built successfully.
echo.

echo Step 6: Building MessageQueueImporter...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\Integration\FlowOrchestrator.MessageQueueImporter\FlowOrchestrator.MessageQueueImporter.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build MessageQueueImporter.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] MessageQueueImporter built successfully.
echo.

echo Step 7: Building FileExporter...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\Integration\FlowOrchestrator.FileExporter\FlowOrchestrator.FileExporter.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build FileExporter.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] FileExporter built successfully.
echo.

echo Step 8: Building RestExporter...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\Integration\FlowOrchestrator.RestExporter\FlowOrchestrator.RestExporter.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build RestExporter.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] RestExporter built successfully.
echo.

echo Step 9: Building DatabaseExporter...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\Integration\FlowOrchestrator.DatabaseExporter\FlowOrchestrator.DatabaseExporter.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build DatabaseExporter.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] DatabaseExporter built successfully.
echo.

echo Step 10: Building MessageQueueExporter...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\Integration\FlowOrchestrator.MessageQueueExporter\FlowOrchestrator.MessageQueueExporter.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build MessageQueueExporter.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] MessageQueueExporter built successfully.
echo.

echo Step 11: Building ProtocolAdapters...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\Integration\FlowOrchestrator.ProtocolAdapters\FlowOrchestrator.ProtocolAdapters.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build ProtocolAdapters.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] ProtocolAdapters built successfully.
echo.

echo Step 12: Building Unit Tests...
echo ---------------------------------------------------
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.ImporterBase.Tests\FlowOrchestrator.ImporterBase.Tests.csproj"
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.ExporterBase.Tests\FlowOrchestrator.ExporterBase.Tests.csproj"
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.FileImporter.Tests\FlowOrchestrator.FileImporter.Tests.csproj"
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.RestImporter.Tests\FlowOrchestrator.RestImporter.Tests.csproj"
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.DatabaseImporter.Tests\FlowOrchestrator.DatabaseImporter.Tests.csproj"
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.MessageQueueImporter.Tests\FlowOrchestrator.MessageQueueImporter.Tests.csproj"
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.FileExporter.Tests\FlowOrchestrator.FileExporter.Tests.csproj"
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.RestExporter.Tests\FlowOrchestrator.RestExporter.Tests.csproj"
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.DatabaseExporter.Tests\FlowOrchestrator.DatabaseExporter.Tests.csproj"
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.MessageQueueExporter.Tests\FlowOrchestrator.MessageQueueExporter.Tests.csproj"
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.ProtocolAdapters.Tests\FlowOrchestrator.ProtocolAdapters.Tests.csproj"
echo [SUCCESS] Unit Tests built successfully.
echo.

echo Step 13: Building Integration Tests...
echo ---------------------------------------------------
dotnet build "%TEST_DIR%\Integration\FlowOrchestrator.IntegrationDomain.Tests\FlowOrchestrator.IntegrationDomain.Tests.csproj"
echo [SUCCESS] Integration Tests built successfully.
echo.

echo ===================================================
echo Phase 4 Build Process Completed Successfully
echo ===================================================
