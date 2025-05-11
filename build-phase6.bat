@echo off
echo ===================================================
echo Building Phase 6: Management Domain Components
echo ===================================================

echo.
echo Building FlowOrchestrator.ServiceManager...
dotnet build src/Management/FlowOrchestrator.ServiceManager/FlowOrchestrator.ServiceManager.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.ServiceManager
    exit /b 1
)
echo FlowOrchestrator.ServiceManager built successfully!

echo.
echo Building FlowOrchestrator.FlowManager...
dotnet build src/Management/FlowOrchestrator.FlowManager/FlowOrchestrator.FlowManager.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.FlowManager
    exit /b 1
)
echo FlowOrchestrator.FlowManager built successfully!

echo.
echo Building FlowOrchestrator.ConfigurationManager...
dotnet build src/Management/FlowOrchestrator.ConfigurationManager/FlowOrchestrator.ConfigurationManager.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.ConfigurationManager
    exit /b 1
)
echo FlowOrchestrator.ConfigurationManager built successfully!

echo.
echo Building FlowOrchestrator.VersionManager...
dotnet build src/Management/FlowOrchestrator.VersionManager/FlowOrchestrator.VersionManager.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.VersionManager
    exit /b 1
)
echo FlowOrchestrator.VersionManager built successfully!

echo.
echo Building FlowOrchestrator.TaskScheduler...
dotnet build src/Management/FlowOrchestrator.TaskScheduler/FlowOrchestrator.TaskScheduler.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.TaskScheduler
    exit /b 1
)
echo FlowOrchestrator.TaskScheduler built successfully!

echo.
echo All Phase 6 components built successfully!
echo ===================================================
