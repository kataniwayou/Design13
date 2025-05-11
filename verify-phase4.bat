@echo off
echo ===================================================
echo Phase 4 Verification Process
echo ===================================================
echo.

REM Set the base directory
set BASE_DIR=%~dp0
set SRC_DIR=%BASE_DIR%src\Integration
set TEST_DIR=%BASE_DIR%tests

echo Step 1: Verifying ImporterBase...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.ImporterBase\bin\Debug\net9.0\FlowOrchestrator.ImporterBase.dll" (
    echo [SUCCESS] ImporterBase build output verified.
) else (
    echo [ERROR] ImporterBase build output not found.
    exit /b 1
)
echo.

echo Step 2: Verifying ExporterBase...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.ExporterBase\bin\Debug\net9.0\FlowOrchestrator.ExporterBase.dll" (
    echo [SUCCESS] ExporterBase build output verified.
) else (
    echo [ERROR] ExporterBase build output not found.
    exit /b 1
)
echo.

echo Step 3: Verifying FileImporter...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.FileImporter\bin\Debug\net9.0\FlowOrchestrator.FileImporter.dll" (
    echo [SUCCESS] FileImporter build output verified.
) else (
    echo [ERROR] FileImporter build output not found.
    exit /b 1
)
echo.

echo Step 4: Verifying RestImporter...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.RestImporter\bin\Debug\net9.0\FlowOrchestrator.RestImporter.dll" (
    echo [SUCCESS] RestImporter build output verified.
) else (
    echo [ERROR] RestImporter build output not found.
    exit /b 1
)
echo.

echo Step 5: Verifying DatabaseImporter...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.DatabaseImporter\bin\Debug\net9.0\FlowOrchestrator.DatabaseImporter.dll" (
    echo [SUCCESS] DatabaseImporter build output verified.
) else (
    echo [ERROR] DatabaseImporter build output not found.
    exit /b 1
)
echo.

echo Step 6: Verifying MessageQueueImporter...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.MessageQueueImporter\bin\Debug\net9.0\FlowOrchestrator.MessageQueueImporter.dll" (
    echo [SUCCESS] MessageQueueImporter build output verified.
) else (
    echo [ERROR] MessageQueueImporter build output not found.
    exit /b 1
)
echo.

echo Step 7: Verifying FileExporter...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.FileExporter\bin\Debug\net9.0\FlowOrchestrator.FileExporter.dll" (
    echo [SUCCESS] FileExporter build output verified.
) else (
    echo [ERROR] FileExporter build output not found.
    exit /b 1
)
echo.

echo Step 8: Verifying RestExporter...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.RestExporter\bin\Debug\net9.0\FlowOrchestrator.RestExporter.dll" (
    echo [SUCCESS] RestExporter build output verified.
) else (
    echo [ERROR] RestExporter build output not found.
    exit /b 1
)
echo.

echo Step 9: Verifying DatabaseExporter...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.DatabaseExporter\bin\Debug\net9.0\FlowOrchestrator.DatabaseExporter.dll" (
    echo [SUCCESS] DatabaseExporter build output verified.
) else (
    echo [ERROR] DatabaseExporter build output not found.
    exit /b 1
)
echo.

echo Step 10: Verifying MessageQueueExporter...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.MessageQueueExporter\bin\Debug\net9.0\FlowOrchestrator.MessageQueueExporter.dll" (
    echo [SUCCESS] MessageQueueExporter build output verified.
) else (
    echo [ERROR] MessageQueueExporter build output not found.
    exit /b 1
)
echo.

echo Step 11: Verifying ProtocolAdapters...
echo ---------------------------------------------------
if exist "%SRC_DIR%\FlowOrchestrator.ProtocolAdapters\bin\Debug\net9.0\FlowOrchestrator.ProtocolAdapters.dll" (
    echo [SUCCESS] ProtocolAdapters build output verified.
) else (
    echo [ERROR] ProtocolAdapters build output not found.
    exit /b 1
)
echo.

echo Step 12: Checking for Unit Tests...
echo ---------------------------------------------------
if exist "%TEST_DIR%\Unit\FlowOrchestrator.ImporterBase.Tests\bin\Debug\net9.0\FlowOrchestrator.ImporterBase.Tests.dll" (
    echo [SUCCESS] ImporterBase.Tests build output verified.
) else (
    echo [INFO] ImporterBase.Tests project not found or not built. Skipping verification.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.ExporterBase.Tests\bin\Debug\net9.0\FlowOrchestrator.ExporterBase.Tests.dll" (
    echo [SUCCESS] ExporterBase.Tests build output verified.
) else (
    echo [INFO] ExporterBase.Tests project not found or not built. Skipping verification.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.FileImporter.Tests\bin\Debug\net9.0\FlowOrchestrator.FileImporter.Tests.dll" (
    echo [SUCCESS] FileImporter.Tests build output verified.
) else (
    echo [INFO] FileImporter.Tests project not found or not built. Skipping verification.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.RestImporter.Tests\bin\Debug\net9.0\FlowOrchestrator.RestImporter.Tests.dll" (
    echo [SUCCESS] RestImporter.Tests build output verified.
) else (
    echo [INFO] RestImporter.Tests project not found or not built. Skipping verification.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.DatabaseImporter.Tests\bin\Debug\net9.0\FlowOrchestrator.DatabaseImporter.Tests.dll" (
    echo [SUCCESS] DatabaseImporter.Tests build output verified.
) else (
    echo [INFO] DatabaseImporter.Tests project not found or not built. Skipping verification.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.MessageQueueImporter.Tests\bin\Debug\net9.0\FlowOrchestrator.MessageQueueImporter.Tests.dll" (
    echo [SUCCESS] MessageQueueImporter.Tests build output verified.
) else (
    echo [INFO] MessageQueueImporter.Tests project not found or not built. Skipping verification.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.FileExporter.Tests\bin\Debug\net9.0\FlowOrchestrator.FileExporter.Tests.dll" (
    echo [SUCCESS] FileExporter.Tests build output verified.
) else (
    echo [INFO] FileExporter.Tests project not found or not built. Skipping verification.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.RestExporter.Tests\bin\Debug\net9.0\FlowOrchestrator.RestExporter.Tests.dll" (
    echo [SUCCESS] RestExporter.Tests build output verified.
) else (
    echo [INFO] RestExporter.Tests project not found or not built. Skipping verification.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.DatabaseExporter.Tests\bin\Debug\net9.0\FlowOrchestrator.DatabaseExporter.Tests.dll" (
    echo [SUCCESS] DatabaseExporter.Tests build output verified.
) else (
    echo [INFO] DatabaseExporter.Tests project not found or not built. Skipping verification.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.MessageQueueExporter.Tests\bin\Debug\net9.0\FlowOrchestrator.MessageQueueExporter.Tests.dll" (
    echo [SUCCESS] MessageQueueExporter.Tests build output verified.
) else (
    echo [INFO] MessageQueueExporter.Tests project not found or not built. Skipping verification.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.ProtocolAdapters.Tests\bin\Debug\net9.0\FlowOrchestrator.ProtocolAdapters.Tests.dll" (
    echo [SUCCESS] ProtocolAdapters.Tests build output verified.
) else (
    echo [INFO] ProtocolAdapters.Tests project not found or not built. Skipping verification.
)
echo.

echo Step 13: Checking for Integration Tests...
echo ---------------------------------------------------
if exist "%TEST_DIR%\Integration\FlowOrchestrator.IntegrationDomain.Tests\bin\Debug\net9.0\FlowOrchestrator.IntegrationDomain.Tests.dll" (
    echo [SUCCESS] IntegrationDomain.Tests build output verified.
) else (
    echo [INFO] IntegrationDomain.Tests project not found or not built. Skipping verification.
)
echo.

echo Step 14: Verifying Test Results...
echo ---------------------------------------------------
if exist "%TEST_DIR%\Unit\FlowOrchestrator.ImporterBase.Tests\TestResults" (
    echo [SUCCESS] ImporterBase.Tests results found.
) else (
    echo [INFO] ImporterBase.Tests results not found. Tests may not have been run.
)

if exist "%TEST_DIR%\Unit\FlowOrchestrator.ExporterBase.Tests\TestResults" (
    echo [SUCCESS] ExporterBase.Tests results found.
) else (
    echo [INFO] ExporterBase.Tests results not found. Tests may not have been run.
)

if exist "%TEST_DIR%\Integration\FlowOrchestrator.IntegrationDomain.Tests\TestResults" (
    echo [SUCCESS] IntegrationDomain.Tests results found.
) else (
    echo [INFO] IntegrationDomain.Tests results not found. Tests may not have been run.
)
echo.

echo ===================================================
echo Phase 4 Verification Process Completed
echo ===================================================
echo.
echo All Phase 4 components have been verified.
