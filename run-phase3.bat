@echo off
echo ===================================================
echo Phase 3 Execution Process
echo ===================================================
echo.

REM Set the base directory
set BASE_DIR=%~dp0
set SRC_DIR=%BASE_DIR%src\Execution
set TEST_DIR=%BASE_DIR%tests

echo Step 1: Running Build Process...
echo ---------------------------------------------------
call build-phase3.bat
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Build process failed.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] Build process completed successfully.
echo.

echo Step 2: Running Unit Tests...
echo ---------------------------------------------------
echo Running Orchestrator Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.Orchestrator.Tests\FlowOrchestrator.Orchestrator.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] Orchestrator Tests failed.
) else (
    echo [SUCCESS] Orchestrator Tests passed.
)
echo.

echo Running MemoryManager Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.MemoryManager.Tests\FlowOrchestrator.MemoryManager.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] MemoryManager Tests failed.
) else (
    echo [SUCCESS] MemoryManager Tests passed.
)
echo.

echo Running BranchController Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.BranchController.Tests\FlowOrchestrator.BranchController.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] BranchController Tests failed.
) else (
    echo [SUCCESS] BranchController Tests passed.
)
echo.

echo Running Recovery Tests...
dotnet test "%TEST_DIR%\Unit\FlowOrchestrator.Recovery.Tests\FlowOrchestrator.Recovery.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] Recovery Tests failed.
) else (
    echo [SUCCESS] Recovery Tests passed.
)
echo.

echo Step 3: Running Integration Tests...
echo ---------------------------------------------------
echo Running ExecutionDomain Integration Tests...
dotnet test "%TEST_DIR%\Integration\FlowOrchestrator.ExecutionDomain.Tests\FlowOrchestrator.ExecutionDomain.Tests.csproj" --logger "trx;LogFileName=TestResults.trx"
if %ERRORLEVEL% neq 0 (
    echo [WARNING] ExecutionDomain Integration Tests failed.
) else (
    echo [SUCCESS] ExecutionDomain Integration Tests passed.
)
echo.

echo Step 4: Running Verification Process...
echo ---------------------------------------------------
call verify-phase3.bat
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Verification process failed.
    exit /b %ERRORLEVEL%
)
echo [SUCCESS] Verification process completed successfully.
echo.

echo ===================================================
echo Phase 3 Execution Process Completed
echo ===================================================
echo.
echo All Phase 3 components have been built, tested, and verified.
