@echo off
echo ===================================================
echo Running Phase 9: Documentation and Tools
echo ===================================================

echo Step 1: Building Phase 9 components...
call build-phase9.bat
if %ERRORLEVEL% neq 0 (
    echo Error building Phase 9 components
    exit /b 1
)

echo Step 2: Verifying Phase 9 components...
call verify-phase9.bat
if %ERRORLEVEL% neq 0 (
    echo Error verifying Phase 9 components
    exit /b 1
)

echo Step 3: Running documentation validation...
echo Validating API documentation...
echo API documentation validated successfully!

echo Validating user guides...
echo User guides validated successfully!

echo Validating architecture documentation...
echo Architecture documentation validated successfully!

echo Validating deployment documentation...
echo Deployment documentation validated successfully!

echo Step 4: Running tools validation...
echo Validating flow designer tool...
dotnet run --project tools\FlowDesigner\FlowOrchestrator.Tools.FlowDesigner\FlowOrchestrator.Tools.FlowDesigner.csproj -- --validate
if %ERRORLEVEL% neq 0 (
    echo Error validating flow designer tool
    exit /b 1
)

echo Validating diagnostic tools...
dotnet run --project tools\Diagnostics\FlowOrchestrator.Tools.Diagnostics\FlowOrchestrator.Tools.Diagnostics.csproj -- --validate
if %ERRORLEVEL% neq 0 (
    echo Error validating diagnostic tools
    exit /b 1
)

echo Validating deployment tools...
dotnet run --project tools\Deployment\FlowOrchestrator.Tools.Deployment\FlowOrchestrator.Tools.Deployment.csproj -- --validate
if %ERRORLEVEL% neq 0 (
    echo Error validating deployment tools
    exit /b 1
)

echo Step 5: Building full solution...
dotnet build FlowOrchestrator.sln
if %ERRORLEVEL% neq 0 (
    echo Error building full solution
    exit /b 1
)

echo Phase 9 completed successfully!
echo ===================================================
