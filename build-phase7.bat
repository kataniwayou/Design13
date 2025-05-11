@echo off
echo ===================================================
echo Building Phase 7: Observability Domain Components
echo ===================================================
echo.

echo Building FlowOrchestrator.StatisticsService...
dotnet build src/Observability/FlowOrchestrator.StatisticsService/FlowOrchestrator.StatisticsService.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.StatisticsService
    exit /b %ERRORLEVEL%
)
echo FlowOrchestrator.StatisticsService built successfully!
echo.

echo Building FlowOrchestrator.MonitoringFramework...
dotnet build src/Observability/FlowOrchestrator.MonitoringFramework/FlowOrchestrator.MonitoringFramework.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.MonitoringFramework
    exit /b %ERRORLEVEL%
)
echo FlowOrchestrator.MonitoringFramework built successfully!
echo.

echo Building FlowOrchestrator.AlertingSystem...
dotnet build src/Observability/FlowOrchestrator.AlertingSystem/FlowOrchestrator.AlertingSystem.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.AlertingSystem
    exit /b %ERRORLEVEL%
)
echo FlowOrchestrator.AlertingSystem built successfully!
echo.

echo Building FlowOrchestrator.AnalyticsEngine...
dotnet build src/Observability/FlowOrchestrator.AnalyticsEngine/FlowOrchestrator.AnalyticsEngine.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.AnalyticsEngine
    exit /b %ERRORLEVEL%
)
echo FlowOrchestrator.AnalyticsEngine built successfully!
echo.

echo All Phase 7 components built successfully!
echo ===================================================
