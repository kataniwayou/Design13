@echo off
echo ===================================================
echo Phase 2 Incremental Build Process
echo ===================================================

set SOLUTION_DIR=%~dp0
set BUILD_LOG_DIR=%SOLUTION_DIR%build-logs
set PHASE=phase2

if not exist "%BUILD_LOG_DIR%" mkdir "%BUILD_LOG_DIR%"

echo.
echo Step 1: Building MongoDB Data Store...
echo ---------------------------------------------------
dotnet build src\Infrastructure\FlowOrchestrator.Data.MongoDB\FlowOrchestrator.Data.MongoDB.csproj > "%BUILD_LOG_DIR%\%PHASE%-mongodb.log" 2>&1
if %ERRORLEVEL% neq 0 (
    echo [ERROR] MongoDB Data Store build failed. See %BUILD_LOG_DIR%\%PHASE%-mongodb.log for details.
    exit /b 1
) else (
    echo [SUCCESS] MongoDB Data Store built successfully.
)

echo.
echo Step 2: Building Hazelcast Data Store...
echo ---------------------------------------------------
dotnet build src\Infrastructure\FlowOrchestrator.Data.Hazelcast\FlowOrchestrator.Data.Hazelcast.csproj > "%BUILD_LOG_DIR%\%PHASE%-hazelcast.log" 2>&1
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Hazelcast Data Store build failed. See %BUILD_LOG_DIR%\%PHASE%-hazelcast.log for details.
    exit /b 1
) else (
    echo [SUCCESS] Hazelcast Data Store built successfully.
)

echo.
echo Step 3: Building MassTransit Messaging...
echo ---------------------------------------------------
dotnet build src\Infrastructure\FlowOrchestrator.Messaging.MassTransit\FlowOrchestrator.Messaging.MassTransit.csproj > "%BUILD_LOG_DIR%\%PHASE%-masstransit.log" 2>&1
if %ERRORLEVEL% neq 0 (
    echo [ERROR] MassTransit Messaging build failed. See %BUILD_LOG_DIR%\%PHASE%-masstransit.log for details.
    exit /b 1
) else (
    echo [SUCCESS] MassTransit Messaging built successfully.
)

echo.
echo Step 4: Building OpenTelemetry Telemetry...
echo ---------------------------------------------------
dotnet build src\Infrastructure\FlowOrchestrator.Telemetry.OpenTelemetry\FlowOrchestrator.Telemetry.OpenTelemetry.csproj > "%BUILD_LOG_DIR%\%PHASE%-opentelemetry.log" 2>&1
if %ERRORLEVEL% neq 0 (
    echo [ERROR] OpenTelemetry Telemetry build failed. See %BUILD_LOG_DIR%\%PHASE%-opentelemetry.log for details.
    exit /b 1
) else (
    echo [SUCCESS] OpenTelemetry Telemetry built successfully.
)

echo.
echo Step 5: Checking for MongoDB Tests...
echo ---------------------------------------------------
if exist tests\Unit\FlowOrchestrator.Data.MongoDB.Tests\FlowOrchestrator.Data.MongoDB.Tests.csproj (
    echo Building MongoDB Tests...
    dotnet build tests\Unit\FlowOrchestrator.Data.MongoDB.Tests\FlowOrchestrator.Data.MongoDB.Tests.csproj > "%BUILD_LOG_DIR%\%PHASE%-mongodb-tests.log" 2>&1
    if %ERRORLEVEL% neq 0 (
        echo [ERROR] MongoDB Tests build failed. See %BUILD_LOG_DIR%\%PHASE%-mongodb-tests.log for details.
        exit /b 1
    ) else (
        echo [SUCCESS] MongoDB Tests built successfully.
    )
) else (
    echo [INFO] MongoDB Tests project not found. Skipping.
)

echo.
echo Step 6: Checking for Hazelcast Tests...
echo ---------------------------------------------------
if exist tests\Unit\FlowOrchestrator.Data.Hazelcast.Tests\FlowOrchestrator.Data.Hazelcast.Tests.csproj (
    echo Building Hazelcast Tests...
    dotnet build tests\Unit\FlowOrchestrator.Data.Hazelcast.Tests\FlowOrchestrator.Data.Hazelcast.Tests.csproj > "%BUILD_LOG_DIR%\%PHASE%-hazelcast-tests.log" 2>&1
    if %ERRORLEVEL% neq 0 (
        echo [ERROR] Hazelcast Tests build failed. See %BUILD_LOG_DIR%\%PHASE%-hazelcast-tests.log for details.
        exit /b 1
    ) else (
        echo [SUCCESS] Hazelcast Tests built successfully.
    )
) else (
    echo [INFO] Hazelcast Tests project not found. Skipping.
)

echo.
echo Step 7: Checking for MassTransit Tests...
echo ---------------------------------------------------
if exist tests\Unit\FlowOrchestrator.Messaging.MassTransit.Tests\FlowOrchestrator.Messaging.MassTransit.Tests.csproj (
    echo Building MassTransit Tests...
    dotnet build tests\Unit\FlowOrchestrator.Messaging.MassTransit.Tests\FlowOrchestrator.Messaging.MassTransit.Tests.csproj > "%BUILD_LOG_DIR%\%PHASE%-masstransit-tests.log" 2>&1
    if %ERRORLEVEL% neq 0 (
        echo [ERROR] MassTransit Tests build failed. See %BUILD_LOG_DIR%\%PHASE%-masstransit-tests.log for details.
        exit /b 1
    ) else (
        echo [SUCCESS] MassTransit Tests built successfully.
    )
) else (
    echo [INFO] MassTransit Tests project not found. Skipping.
)

echo.
echo Step 8: Checking for OpenTelemetry Tests...
echo ---------------------------------------------------
if exist tests\Unit\FlowOrchestrator.Telemetry.OpenTelemetry.Tests\FlowOrchestrator.Telemetry.OpenTelemetry.Tests.csproj (
    echo Building OpenTelemetry Tests...
    dotnet build tests\Unit\FlowOrchestrator.Telemetry.OpenTelemetry.Tests\FlowOrchestrator.Telemetry.OpenTelemetry.Tests.csproj > "%BUILD_LOG_DIR%\%PHASE%-opentelemetry-tests.log" 2>&1
    if %ERRORLEVEL% neq 0 (
        echo [ERROR] OpenTelemetry Tests build failed. See %BUILD_LOG_DIR%\%PHASE%-opentelemetry-tests.log for details.
        exit /b 1
    ) else (
        echo [SUCCESS] OpenTelemetry Tests built successfully.
    )
) else (
    echo [INFO] OpenTelemetry Tests project not found. Skipping.
)

echo.
echo Step 9: Checking for MongoDB Tests to run...
echo ---------------------------------------------------
if exist tests\Unit\FlowOrchestrator.Data.MongoDB.Tests\FlowOrchestrator.Data.MongoDB.Tests.csproj (
    echo Running MongoDB Tests...
    dotnet test tests\Unit\FlowOrchestrator.Data.MongoDB.Tests\FlowOrchestrator.Data.MongoDB.Tests.csproj --no-build > "%BUILD_LOG_DIR%\%PHASE%-mongodb-test-results.log" 2>&1
    if %ERRORLEVEL% neq 0 (
        echo [WARNING] MongoDB Tests failed. See %BUILD_LOG_DIR%\%PHASE%-mongodb-test-results.log for details.
    ) else (
        echo [SUCCESS] MongoDB Tests passed.
    )
) else (
    echo [INFO] MongoDB Tests project not found. Skipping.
)

echo.
echo Step 10: Checking for Hazelcast Tests to run...
echo ---------------------------------------------------
if exist tests\Unit\FlowOrchestrator.Data.Hazelcast.Tests\FlowOrchestrator.Data.Hazelcast.Tests.csproj (
    echo Running Hazelcast Tests...
    dotnet test tests\Unit\FlowOrchestrator.Data.Hazelcast.Tests\FlowOrchestrator.Data.Hazelcast.Tests.csproj --no-build > "%BUILD_LOG_DIR%\%PHASE%-hazelcast-test-results.log" 2>&1
    if %ERRORLEVEL% neq 0 (
        echo [WARNING] Hazelcast Tests failed. See %BUILD_LOG_DIR%\%PHASE%-hazelcast-test-results.log for details.
    ) else (
        echo [SUCCESS] Hazelcast Tests passed.
    )
) else (
    echo [INFO] Hazelcast Tests project not found. Skipping.
)

echo.
echo Step 11: Checking for MassTransit Tests to run...
echo ---------------------------------------------------
if exist tests\Unit\FlowOrchestrator.Messaging.MassTransit.Tests\FlowOrchestrator.Messaging.MassTransit.Tests.csproj (
    echo Running MassTransit Tests...
    dotnet test tests\Unit\FlowOrchestrator.Messaging.MassTransit.Tests\FlowOrchestrator.Messaging.MassTransit.Tests.csproj --no-build > "%BUILD_LOG_DIR%\%PHASE%-masstransit-test-results.log" 2>&1
    if %ERRORLEVEL% neq 0 (
        echo [WARNING] MassTransit Tests failed. See %BUILD_LOG_DIR%\%PHASE%-masstransit-test-results.log for details.
    ) else (
        echo [SUCCESS] MassTransit Tests passed.
    )
) else (
    echo [INFO] MassTransit Tests project not found. Skipping.
)

echo.
echo Step 12: Checking for OpenTelemetry Tests to run...
echo ---------------------------------------------------
if exist tests\Unit\FlowOrchestrator.Telemetry.OpenTelemetry.Tests\FlowOrchestrator.Telemetry.OpenTelemetry.Tests.csproj (
    echo Running OpenTelemetry Tests...
    dotnet test tests\Unit\FlowOrchestrator.Telemetry.OpenTelemetry.Tests\FlowOrchestrator.Telemetry.OpenTelemetry.Tests.csproj --no-build > "%BUILD_LOG_DIR%\%PHASE%-opentelemetry-test-results.log" 2>&1
    if %ERRORLEVEL% neq 0 (
        echo [WARNING] OpenTelemetry Tests failed. See %BUILD_LOG_DIR%\%PHASE%-opentelemetry-test-results.log for details.
    ) else (
        echo [SUCCESS] OpenTelemetry Tests passed.
    )
) else (
    echo [INFO] OpenTelemetry Tests project not found. Skipping.
)

echo.
echo ===================================================
echo Phase 2 Incremental Build Process Completed
echo ===================================================
echo.
echo Build logs are available in: %BUILD_LOG_DIR%
echo.
