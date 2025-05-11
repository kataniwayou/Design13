@echo off
echo ===================================================
echo Running Phase 7: Observability Domain Components
echo ===================================================
echo.

echo Step 1: Building Phase 7 components...
call build-phase7.bat
if %ERRORLEVEL% neq 0 (
    echo Error building Phase 7 components
    exit /b %ERRORLEVEL%
)
echo.

echo Step 2: Verifying Phase 7 components...
call verify-phase7.bat
if %ERRORLEVEL% neq 0 (
    echo Error verifying Phase 7 components
    exit /b %ERRORLEVEL%
)
echo.

echo Step 3: Building full solution...
dotnet build FlowOrchestrator.sln
if %ERRORLEVEL% neq 0 (
    echo Error building full solution
    exit /b %ERRORLEVEL%
)
echo.

echo Phase 7 completed successfully!
echo ===================================================
