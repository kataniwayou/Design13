@echo off
echo ===================================================
echo Verifying Phase 7: Observability Domain Components
echo ===================================================
echo.

echo Verifying FlowOrchestrator.StatisticsService...
if not exist src\Observability\FlowOrchestrator.StatisticsService\bin\Debug\net9.0\FlowOrchestrator.StatisticsService.dll (
    echo Error: FlowOrchestrator.StatisticsService.dll not found
    exit /b 1
)
echo FlowOrchestrator.StatisticsService verified successfully!
echo.

echo Verifying FlowOrchestrator.MonitoringFramework...
if not exist src\Observability\FlowOrchestrator.MonitoringFramework\bin\Debug\net9.0\FlowOrchestrator.MonitoringFramework.dll (
    echo Error: FlowOrchestrator.MonitoringFramework.dll not found
    exit /b 1
)
echo FlowOrchestrator.MonitoringFramework verified successfully!
echo.

echo Verifying FlowOrchestrator.AlertingSystem...
if not exist src\Observability\FlowOrchestrator.AlertingSystem\bin\Debug\net9.0\FlowOrchestrator.AlertingSystem.dll (
    echo Error: FlowOrchestrator.AlertingSystem.dll not found
    exit /b 1
)
echo FlowOrchestrator.AlertingSystem verified successfully!
echo.

echo Verifying FlowOrchestrator.AnalyticsEngine...
if not exist src\Observability\FlowOrchestrator.AnalyticsEngine\bin\Debug\net9.0\FlowOrchestrator.AnalyticsEngine.dll (
    echo Error: FlowOrchestrator.AnalyticsEngine.dll not found
    exit /b 1
)
echo FlowOrchestrator.AnalyticsEngine verified successfully!
echo.

echo All Phase 7 components verified successfully!
echo ===================================================
