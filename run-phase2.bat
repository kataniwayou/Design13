@echo off
echo ===================================================
echo Phase 2 Incremental Build and Verification Process
echo ===================================================

set START_TIME=%TIME%
set SOLUTION_DIR=%~dp0
set BUILD_LOG_DIR=%SOLUTION_DIR%build-logs
set PHASE=phase2
set REPORT_FILE=%SOLUTION_DIR%phase2-build-report.md

if not exist "%BUILD_LOG_DIR%" mkdir "%BUILD_LOG_DIR%"

echo # Phase 2 Build Report > %REPORT_FILE%
echo. >> %REPORT_FILE%
echo ## Build Information >> %REPORT_FILE%
echo. >> %REPORT_FILE%
echo - **Date:** %DATE% >> %REPORT_FILE%
echo - **Start Time:** %START_TIME% >> %REPORT_FILE%
echo. >> %REPORT_FILE%
echo ## Build Steps >> %REPORT_FILE%
echo. >> %REPORT_FILE%

echo.
echo Step 1: Running Incremental Build Process...
echo ---------------------------------------------------
call build-phase2.bat
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Incremental build process failed.
    echo - **Incremental Build:** ❌ Failed >> %REPORT_FILE%
    goto :end
) else (
    echo [SUCCESS] Incremental build process completed successfully.
    echo - **Incremental Build:** ✅ Succeeded >> %REPORT_FILE%
)

echo.
echo Step 2: Running Verification Process...
echo ---------------------------------------------------
call verify-phase2.bat
if %ERRORLEVEL% neq 0 (
    echo [ERROR] Verification process failed.
    echo - **Verification:** ❌ Failed >> %REPORT_FILE%
    goto :end
) else (
    echo [SUCCESS] Verification process completed successfully.
    echo - **Verification:** ✅ Succeeded >> %REPORT_FILE%
)

:end
set END_TIME=%TIME%
echo. >> %REPORT_FILE%
echo - **End Time:** %END_TIME% >> %REPORT_FILE%
echo. >> %REPORT_FILE%

echo ## Component Status >> %REPORT_FILE%
echo. >> %REPORT_FILE%
echo | Component | Status | >> %REPORT_FILE%
echo | --------- | ------ | >> %REPORT_FILE%

if exist "src\Infrastructure\FlowOrchestrator.Data.MongoDB\bin\Debug\net9.0\FlowOrchestrator.Data.MongoDB.dll" (
    echo | MongoDB Data Store | ✅ Built | >> %REPORT_FILE%
) else (
    echo | MongoDB Data Store | ❌ Failed | >> %REPORT_FILE%
)

if exist "src\Infrastructure\FlowOrchestrator.Data.Hazelcast\bin\Debug\net9.0\FlowOrchestrator.Data.Hazelcast.dll" (
    echo | Hazelcast Data Store | ✅ Built | >> %REPORT_FILE%
) else (
    echo | Hazelcast Data Store | ❌ Failed | >> %REPORT_FILE%
)

if exist "src\Infrastructure\FlowOrchestrator.Messaging.MassTransit\bin\Debug\net9.0\FlowOrchestrator.Messaging.MassTransit.dll" (
    echo | MassTransit Messaging | ✅ Built | >> %REPORT_FILE%
) else (
    echo | MassTransit Messaging | ❌ Failed | >> %REPORT_FILE%
)

if exist "src\Infrastructure\FlowOrchestrator.Telemetry.OpenTelemetry\bin\Debug\net9.0\FlowOrchestrator.Telemetry.OpenTelemetry.dll" (
    echo | OpenTelemetry Telemetry | ✅ Built | >> %REPORT_FILE%
) else (
    echo | OpenTelemetry Telemetry | ❌ Failed | >> %REPORT_FILE%
)

echo. >> %REPORT_FILE%
echo ## Test Status >> %REPORT_FILE%
echo. >> %REPORT_FILE%
echo | Test | Status | >> %REPORT_FILE%
echo | ---- | ------ | >> %REPORT_FILE%

if exist "%BUILD_LOG_DIR%\%PHASE%-mongodb-test-results.log" (
    findstr /C:"Passed!" "%BUILD_LOG_DIR%\%PHASE%-mongodb-test-results.log" > nul
    if %ERRORLEVEL% equ 0 (
        echo | MongoDB Tests | ✅ Passed | >> %REPORT_FILE%
    ) else (
        echo | MongoDB Tests | ❌ Failed | >> %REPORT_FILE%
    )
) else (
    echo | MongoDB Tests | ⚠️ Not Run | >> %REPORT_FILE%
)

if exist "%BUILD_LOG_DIR%\%PHASE%-hazelcast-test-results.log" (
    findstr /C:"Passed!" "%BUILD_LOG_DIR%\%PHASE%-hazelcast-test-results.log" > nul
    if %ERRORLEVEL% equ 0 (
        echo | Hazelcast Tests | ✅ Passed | >> %REPORT_FILE%
    ) else (
        echo | Hazelcast Tests | ❌ Failed | >> %REPORT_FILE%
    )
) else (
    echo | Hazelcast Tests | ⚠️ Not Run | >> %REPORT_FILE%
)

if exist "%BUILD_LOG_DIR%\%PHASE%-masstransit-test-results.log" (
    findstr /C:"Passed!" "%BUILD_LOG_DIR%\%PHASE%-masstransit-test-results.log" > nul
    if %ERRORLEVEL% equ 0 (
        echo | MassTransit Tests | ✅ Passed | >> %REPORT_FILE%
    ) else (
        echo | MassTransit Tests | ❌ Failed | >> %REPORT_FILE%
    )
) else (
    echo | MassTransit Tests | ⚠️ Not Run | >> %REPORT_FILE%
)

if exist "%BUILD_LOG_DIR%\%PHASE%-opentelemetry-test-results.log" (
    findstr /C:"Passed!" "%BUILD_LOG_DIR%\%PHASE%-opentelemetry-test-results.log" > nul
    if %ERRORLEVEL% equ 0 (
        echo | OpenTelemetry Tests | ✅ Passed | >> %REPORT_FILE%
    ) else (
        echo | OpenTelemetry Tests | ❌ Failed | >> %REPORT_FILE%
    )
) else (
    echo | OpenTelemetry Tests | ⚠️ Not Run | >> %REPORT_FILE%
)

echo.
echo ===================================================
echo Phase 2 Process Completed
echo ===================================================
echo.
echo Build and verification report saved to: %REPORT_FILE%
echo.
