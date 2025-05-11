@echo off
echo ===================================================
echo Verifying Phase 8: Testing and Integration Components
echo ===================================================
echo.

echo Verifying FlowOrchestrator.UnitTests...
if not exist tests\FlowOrchestrator.UnitTests\bin\Debug\net9.0\FlowOrchestrator.UnitTests.dll (
    echo Error: FlowOrchestrator.UnitTests.dll not found
    exit /b 1
)
echo FlowOrchestrator.UnitTests verified successfully!
echo.

echo Verifying FlowOrchestrator.IntegrationTests...
if not exist tests\FlowOrchestrator.IntegrationTests\bin\Debug\net9.0\FlowOrchestrator.IntegrationTests.dll (
    echo Error: FlowOrchestrator.IntegrationTests.dll not found
    exit /b 1
)
echo FlowOrchestrator.IntegrationTests verified successfully!
echo.

echo Verifying FlowOrchestrator.SystemTests...
if not exist tests\FlowOrchestrator.SystemTests\bin\Debug\net9.0\FlowOrchestrator.SystemTests.dll (
    echo Error: FlowOrchestrator.SystemTests.dll not found
    exit /b 1
)
echo FlowOrchestrator.SystemTests verified successfully!
echo.

echo Verifying FlowOrchestrator.SampleFlows...
if not exist samples\FlowOrchestrator.SampleFlows\bin\Debug\net9.0\FlowOrchestrator.SampleFlows.dll (
    echo Error: FlowOrchestrator.SampleFlows.dll not found
    exit /b 1
)
echo FlowOrchestrator.SampleFlows verified successfully!
echo.

echo All Phase 8 components verified successfully!
echo ===================================================
