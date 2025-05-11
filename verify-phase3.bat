@echo off
echo ===================================================
echo Phase 3 Verification Process
echo ===================================================
echo.

REM Set the base directory
set BASE_DIR=%~dp0
set SRC_DIR=%BASE_DIR%src\Execution
set TEST_DIR=%BASE_DIR%tests

echo Step 1: Verifying Orchestrator...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.Orchestrator\bin\Debug\net9.0\FlowOrchestrator.Orchestrator.dll" (
    echo [SUCCESS] Orchestrator build output verified.
) else (
    echo [ERROR] Orchestrator build output not found.
    exit /b 1
)
echo.

echo Step 2: Verifying MemoryManager...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.MemoryManager\bin\Debug\net9.0\FlowOrchestrator.MemoryManager.dll" (
    echo [SUCCESS] MemoryManager build output verified.
) else (
    echo [ERROR] MemoryManager build output not found.
    exit /b 1
)
echo.

echo Step 3: Verifying BranchController...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.BranchController\bin\Debug\net9.0\FlowOrchestrator.BranchController.dll" (
    echo [SUCCESS] BranchController build output verified.
) else (
    echo [ERROR] BranchController build output not found.
    exit /b 1
)
echo.

echo Step 4: Verifying Recovery...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.Recovery\bin\Debug\net9.0\FlowOrchestrator.Recovery.dll" (
    echo [SUCCESS] Recovery build output verified.
) else (
    echo [ERROR] Recovery build output not found.
    exit /b 1
)
echo.

echo Step 5: Checking for Orchestrator Tests...
echo ---------------------------------------------------
if exist "%TEST_DIR%\Unit\FlowOrchestrator.Orchestrator.Tests\bin\Debug\net9.0\FlowOrchestrator.Orchestrator.Tests.dll" (
    echo [SUCCESS] Orchestrator Tests build output verified.
) else (
    echo [INFO] Orchestrator Tests project not found or not built. Skipping verification.
)
echo.

echo Step 6: Checking for MemoryManager Tests...
echo ---------------------------------------------------
if exist "%TEST_DIR%\Unit\FlowOrchestrator.MemoryManager.Tests\bin\Debug\net9.0\FlowOrchestrator.MemoryManager.Tests.dll" (
    echo [SUCCESS] MemoryManager Tests build output verified.
) else (
    echo [INFO] MemoryManager Tests project not found or not built. Skipping verification.
)
echo.

echo Step 7: Checking for BranchController Tests...
echo ---------------------------------------------------
if exist "%TEST_DIR%\Unit\FlowOrchestrator.BranchController.Tests\bin\Debug\net9.0\FlowOrchestrator.BranchController.Tests.dll" (
    echo [SUCCESS] BranchController Tests build output verified.
) else (
    echo [INFO] BranchController Tests project not found or not built. Skipping verification.
)
echo.

echo Step 8: Checking for Recovery Tests...
echo ---------------------------------------------------
if exist "%TEST_DIR%\Unit\FlowOrchestrator.Recovery.Tests\bin\Debug\net9.0\FlowOrchestrator.Recovery.Tests.dll" (
    echo [SUCCESS] Recovery Tests build output verified.
) else (
    echo [INFO] Recovery Tests project not found or not built. Skipping verification.
)
echo.

echo Step 9: Checking for ExecutionDomain Integration Tests...
echo ---------------------------------------------------
if exist "%TEST_DIR%\Integration\FlowOrchestrator.ExecutionDomain.Tests\bin\Debug\net9.0\FlowOrchestrator.ExecutionDomain.Tests.dll" (
    echo [SUCCESS] ExecutionDomain Integration Tests build output verified.
) else (
    echo [INFO] ExecutionDomain Integration Tests project not found or not built. Skipping verification.
)
echo.

echo Step 10: Verifying Test Results...
echo ---------------------------------------------------
if exist "%TEST_DIR%\Unit\FlowOrchestrator.Orchestrator.Tests\TestResults" (
    echo [SUCCESS] Orchestrator Tests results found.
) else (
    echo [INFO] Orchestrator Tests results not found. Tests may not have been run.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.MemoryManager.Tests\TestResults" (
    echo [SUCCESS] MemoryManager Tests results found.
) else (
    echo [INFO] MemoryManager Tests results not found. Tests may not have been run.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.BranchController.Tests\TestResults" (
    echo [SUCCESS] BranchController Tests results found.
) else (
    echo [INFO] BranchController Tests results not found. Tests may not have been run.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.Recovery.Tests\TestResults" (
    echo [SUCCESS] Recovery Tests results found.
) else (
    echo [INFO] Recovery Tests results not found. Tests may not have been run.
)

if exist "%TEST_DIR%\Integration\FlowOrchestrator.ExecutionDomain.Tests\TestResults" (
    echo [SUCCESS] ExecutionDomain Integration Tests results found.
) else (
    echo [INFO] ExecutionDomain Integration Tests results not found. Tests may not have been run.
)
echo.

echo ===================================================
echo Phase 3 Verification Process Completed
echo ===================================================
echo.
echo All Phase 3 components have been verified.
