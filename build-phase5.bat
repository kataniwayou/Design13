@echo off
echo ===================================================
echo Building Phase 5: Processing Domain Components
echo ===================================================

echo.
echo Building FlowOrchestrator.ProcessorBase...
dotnet build src/Processing/FlowOrchestrator.ProcessorBase/FlowOrchestrator.ProcessorBase.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.ProcessorBase
    exit /b %ERRORLEVEL%
)

echo.
echo Building FlowOrchestrator.TransformationEngine...
dotnet build src/Processing/FlowOrchestrator.TransformationEngine/FlowOrchestrator.TransformationEngine.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.TransformationEngine
    exit /b %ERRORLEVEL%
)

echo.
echo Building FlowOrchestrator.JsonProcessor...
dotnet build src/Processing/FlowOrchestrator.JsonProcessor/FlowOrchestrator.JsonProcessor.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.JsonProcessor
    exit /b %ERRORLEVEL%
)

echo.
echo Building FlowOrchestrator.ValidationProcessor...
dotnet build src/Processing/FlowOrchestrator.ValidationProcessor/FlowOrchestrator.ValidationProcessor.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.ValidationProcessor
    exit /b %ERRORLEVEL%
)

echo.
echo Building FlowOrchestrator.EnrichmentProcessor...
dotnet build src/Processing/FlowOrchestrator.EnrichmentProcessor/FlowOrchestrator.EnrichmentProcessor.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.EnrichmentProcessor
    exit /b %ERRORLEVEL%
)

echo.
echo Building FlowOrchestrator.MappingProcessor...
dotnet build src/Processing/FlowOrchestrator.MappingProcessor/FlowOrchestrator.MappingProcessor.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.MappingProcessor
    exit /b %ERRORLEVEL%
)

echo.
echo All Phase 5 components built successfully!
echo ===================================================
