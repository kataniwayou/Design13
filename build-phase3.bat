@echo off
echo ===================================================
echo Phase 3 Build Process
echo ===================================================
echo.

REM Set the base directory
set BASE_DIR=%~dp0
set SRC_DIR=%BASE_DIR%src\Execution
set TEST_DIR=%BASE_DIR%tests

REM Create the Recovery project if it doesn't exist
if not exist "%SRC_DIR%\FlowOrchestrator.Recovery" (
    echo Creating FlowOrchestrator.Recovery project...
    mkdir "%SRC_DIR%\FlowOrchestrator.Recovery"
    dotnet new classlib -o "%SRC_DIR%\FlowOrchestrator.Recovery" --framework net9.0
    echo Created FlowOrchestrator.Recovery project.
    echo.
)

REM Create test projects if they don't exist
if not exist "%TEST_DIR%\Unit\FlowOrchestrator.Orchestrator.Tests" (
    echo Creating FlowOrchestrator.Orchestrator.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.Orchestrator.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.Orchestrator.Tests" --framework net9.0
    echo Created FlowOrchestrator.Orchestrator.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.MemoryManager.Tests" (
    echo Creating FlowOrchestrator.MemoryManager.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.MemoryManager.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.MemoryManager.Tests" --framework net9.0
    echo Created FlowOrchestrator.MemoryManager.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.BranchController.Tests" (
    echo Creating FlowOrchestrator.BranchController.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.BranchController.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.BranchController.Tests" --framework net9.0
    echo Created FlowOrchestrator.BranchController.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Unit\FlowOrchestrator.Recovery.Tests" (
    echo Creating FlowOrchestrator.Recovery.Tests project...
    mkdir "%TEST_DIR%\Unit\FlowOrchestrator.Recovery.Tests"
    dotnet new xunit -o "%TEST_DIR%\Unit\FlowOrchestrator.Recovery.Tests" --framework net9.0
    echo Created FlowOrchestrator.Recovery.Tests project.
    echo.
)

if not exist "%TEST_DIR%\Integration\FlowOrchestrator.ExecutionDomain.Tests" (
    echo Creating FlowOrchestrator.ExecutionDomain.Tests project...
    mkdir "%TEST_DIR%\Integration\FlowOrchestrator.ExecutionDomain.Tests"
    dotnet new xunit -o "%TEST_DIR%\Integration\FlowOrchestrator.ExecutionDomain.Tests" --framework net9.0
    echo Created FlowOrchestrator.ExecutionDomain.Tests project.
    echo.
)

echo Step 1: Building Orchestrator...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\FlowOrchestrator.Orchestrator\FlowOrchestrator.Orchestrator.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build Orchestrator.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] Orchestrator built successfully.
echo.

echo Step 2: Building MemoryManager...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\FlowOrchestrator.MemoryManager\FlowOrchestrator.MemoryManager.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build MemoryManager.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] MemoryManager built successfully.
echo.

echo Step 3: Building BranchController...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\FlowOrchestrator.BranchController\FlowOrchestrator.BranchController.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build BranchController.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] BranchController built successfully.
echo.

echo Step 4: Building Recovery...
echo ---------------------------------------------------
dotnet build "%SRC_DIR%\FlowOrchestrator.Recovery\FlowOrchestrator.Recovery.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build Recovery.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] Recovery built successfully.
echo.

echo Step 5: Building Orchestrator Tests...
echo ---------------------------------------------------
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.Orchestrator.Tests\FlowOrchestrator.Orchestrator.Tests.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build Orchestrator Tests.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] Orchestrator Tests built successfully.
echo.

echo Step 6: Building MemoryManager Tests...
echo ---------------------------------------------------
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.MemoryManager.Tests\FlowOrchestrator.MemoryManager.Tests.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build MemoryManager Tests.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] MemoryManager Tests built successfully.
echo.

echo Step 7: Building BranchController Tests...
echo ---------------------------------------------------
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.BranchController.Tests\FlowOrchestrator.BranchController.Tests.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build BranchController Tests.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] BranchController Tests built successfully.
echo.

echo Step 8: Building Recovery Tests...
echo ---------------------------------------------------
dotnet build "%TEST_DIR%\Unit\FlowOrchestrator.Recovery.Tests\FlowOrchestrator.Recovery.Tests.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build Recovery Tests.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] Recovery Tests built successfully.
echo.

echo Step 9: Building ExecutionDomain Integration Tests...
echo ---------------------------------------------------
dotnet build "%TEST_DIR%\Integration\FlowOrchestrator.ExecutionDomain.Tests\FlowOrchestrator.ExecutionDomain.Tests.csproj"
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Failed to build ExecutionDomain Integration Tests.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] ExecutionDomain Integration Tests built successfully.
echo.

echo ===================================================
echo Phase 3 Build Process Completed Successfully
echo ===================================================
