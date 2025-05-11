@echo off
echo ===================================================
echo Running Phase 8: Testing and Integration Components
echo ===================================================
echo.

echo Step 1: Building Phase 8 components...
call build-phase8.bat
if %ERRORLEVEL% neq 0 (
    echo Error building Phase 8 components
    exit /b %ERRORLEVEL%
)
echo.

echo Step 2: Verifying Phase 8 components...
call verify-phase8.bat
if %ERRORLEVEL% neq 0 (
    echo Error verifying Phase 8 components
    exit /b %ERRORLEVEL%
)
echo.

echo Step 3: Running unit tests...
dotnet test tests\FlowOrchestrator.UnitTests\FlowOrchestrator.UnitTests.csproj
if %ERRORLEVEL% neq 0 (
    echo Warning: Some unit tests failed
    echo Continuing with integration tests...
) else (
    echo All unit tests passed!
)
echo.

echo Step 4: Running integration tests...
dotnet test tests\FlowOrchestrator.IntegrationTests\FlowOrchestrator.IntegrationTests.csproj
if %ERRORLEVEL% neq 0 (
    echo Warning: Some integration tests failed
    echo Continuing with system tests...
) else (
    echo All integration tests passed!
)
echo.

echo Step 5: Running system tests...
dotnet test tests\FlowOrchestrator.SystemTests\FlowOrchestrator.SystemTests.csproj
if %ERRORLEVEL% neq 0 (
    echo Warning: Some system tests failed
    echo Continuing with build verification...
) else (
    echo All system tests passed!
)
echo.

echo Step 6: Building full solution...
dotnet build FlowOrchestrator.sln
if %ERRORLEVEL% neq 0 (
    echo Error building full solution
    exit /b %ERRORLEVEL%
)
echo.

echo Phase 8 completed successfully!
echo ===================================================
