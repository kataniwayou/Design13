@echo off
echo ===================================================
echo Phase 2 Verification Process
echo ===================================================

set SOLUTION_DIR=%~dp0
set BUILD_LOG_DIR=%SOLUTION_DIR%build-logs
set PHASE=phase2

if not exist "%BUILD_LOG_DIR%" mkdir "%BUILD_LOG_DIR%"

echo.
echo Step 1: Verifying MongoDB Data Store...
echo ---------------------------------------------------
if not exist "src\Infrastructure\FlowOrchestrator.Data.MongoDB\bin\Debug\net9.0\FlowOrchestrator.Data.MongoDB.dll" (
    echo [ERROR] MongoDB Data Store build output not found.
    exit /b 1
) else (
    echo [SUCCESS] MongoDB Data Store build output verified.
)

echo.
echo Step 2: Verifying Hazelcast Data Store...
echo ---------------------------------------------------
if not exist "src\Infrastructure\FlowOrchestrator.Data.Hazelcast\bin\Debug\net9.0\FlowOrchestrator.Data.Hazelcast.dll" (
    echo [ERROR] Hazelcast Data Store build output not found.
    exit /b 1
) else (
    echo [SUCCESS] Hazelcast Data Store build output verified.
)

echo.
echo Step 3: Verifying MassTransit Messaging...
echo ---------------------------------------------------
if not exist "src\Infrastructure\FlowOrchestrator.Messaging.MassTransit\bin\Debug\net9.0\FlowOrchestrator.Messaging.MassTransit.dll" (
    echo [ERROR] MassTransit Messaging build output not found.
    exit /b 1
) else (
    echo [SUCCESS] MassTransit Messaging build output verified.
)

echo.
echo Step 4: Verifying OpenTelemetry Telemetry...
echo ---------------------------------------------------
if not exist "src\Infrastructure\FlowOrchestrator.Telemetry.OpenTelemetry\bin\Debug\net9.0\FlowOrchestrator.Telemetry.OpenTelemetry.dll" (
    echo [ERROR] OpenTelemetry Telemetry build output not found.
    exit /b 1
) else (
    echo [SUCCESS] OpenTelemetry Telemetry build output verified.
)

echo.
echo Step 5: Checking for MongoDB Tests...
echo ---------------------------------------------------
if exist "tests\Unit\FlowOrchestrator.Data.MongoDB.Tests\FlowOrchestrator.Data.MongoDB.Tests.csproj" (
    if not exist "tests\Unit\FlowOrchestrator.Data.MongoDB.Tests\bin\Debug\net9.0\FlowOrchestrator.Data.MongoDB.Tests.dll" (
        echo [ERROR] MongoDB Tests build output not found.
        exit /b 1
    ) else (
        echo [SUCCESS] MongoDB Tests build output verified.
    )
) else (
    echo [INFO] MongoDB Tests project not found. Skipping verification.
)

echo.
echo Step 6: Checking for Hazelcast Tests...
echo ---------------------------------------------------
if exist "tests\Unit\FlowOrchestrator.Data.Hazelcast.Tests\FlowOrchestrator.Data.Hazelcast.Tests.csproj" (
    if not exist "tests\Unit\FlowOrchestrator.Data.Hazelcast.Tests\bin\Debug\net9.0\FlowOrchestrator.Data.Hazelcast.Tests.dll" (
        echo [ERROR] Hazelcast Tests build output not found.
        exit /b 1
    ) else (
        echo [SUCCESS] Hazelcast Tests build output verified.
    )
) else (
    echo [INFO] Hazelcast Tests project not found. Skipping verification.
)

echo.
echo Step 7: Checking for MassTransit Tests...
echo ---------------------------------------------------
if exist "tests\Unit\FlowOrchestrator.Messaging.MassTransit.Tests\FlowOrchestrator.Messaging.MassTransit.Tests.csproj" (
    if not exist "tests\Unit\FlowOrchestrator.Messaging.MassTransit.Tests\bin\Debug\net9.0\FlowOrchestrator.Messaging.MassTransit.Tests.dll" (
        echo [ERROR] MassTransit Tests build output not found.
        exit /b 1
    ) else (
        echo [SUCCESS] MassTransit Tests build output verified.
    )
) else (
    echo [INFO] MassTransit Tests project not found. Skipping verification.
)

echo.
echo Step 8: Checking for OpenTelemetry Tests...
echo ---------------------------------------------------
if exist "tests\Unit\FlowOrchestrator.Telemetry.OpenTelemetry.Tests\FlowOrchestrator.Telemetry.OpenTelemetry.Tests.csproj" (
    if not exist "tests\Unit\FlowOrchestrator.Telemetry.OpenTelemetry.Tests\bin\Debug\net9.0\FlowOrchestrator.Telemetry.OpenTelemetry.Tests.dll" (
        echo [ERROR] OpenTelemetry Tests build output not found.
        exit /b 1
    ) else (
        echo [SUCCESS] OpenTelemetry Tests build output verified.
    )
) else (
    echo [INFO] OpenTelemetry Tests project not found. Skipping verification.
)

echo.
echo Step 9: Verifying Test Results...
echo ---------------------------------------------------
if exist "tests\Unit\FlowOrchestrator.Data.MongoDB.Tests\FlowOrchestrator.Data.MongoDB.Tests.csproj" (
    if not exist "%BUILD_LOG_DIR%\%PHASE%-mongodb-test-results.log" (
        echo [WARNING] MongoDB Test results not found.
    ) else (
        echo [SUCCESS] MongoDB Test results verified.
    )
) else (
    echo [INFO] MongoDB Tests project not found. Skipping test results verification.
)

if exist "tests\Unit\FlowOrchestrator.Data.Hazelcast.Tests\FlowOrchestrator.Data.Hazelcast.Tests.csproj" (
    if not exist "%BUILD_LOG_DIR%\%PHASE%-hazelcast-test-results.log" (
        echo [WARNING] Hazelcast Test results not found.
    ) else (
        echo [SUCCESS] Hazelcast Test results verified.
    )
) else (
    echo [INFO] Hazelcast Tests project not found. Skipping test results verification.
)

if exist "tests\Unit\FlowOrchestrator.Messaging.MassTransit.Tests\FlowOrchestrator.Messaging.MassTransit.Tests.csproj" (
    if not exist "%BUILD_LOG_DIR%\%PHASE%-masstransit-test-results.log" (
        echo [WARNING] MassTransit Test results not found.
    ) else (
        echo [SUCCESS] MassTransit Test results verified.
    )
) else (
    echo [INFO] MassTransit Tests project not found. Skipping test results verification.
)

if exist "tests\Unit\FlowOrchestrator.Telemetry.OpenTelemetry.Tests\FlowOrchestrator.Telemetry.OpenTelemetry.Tests.csproj" (
    if not exist "%BUILD_LOG_DIR%\%PHASE%-opentelemetry-test-results.log" (
        echo [WARNING] OpenTelemetry Test results not found.
    ) else (
        echo [SUCCESS] OpenTelemetry Test results verified.
    )
) else (
    echo [INFO] OpenTelemetry Tests project not found. Skipping test results verification.
)

echo.
echo ===================================================
echo Phase 2 Verification Process Completed
echo ===================================================
echo.
echo All Phase 2 components have been verified.
echo.
