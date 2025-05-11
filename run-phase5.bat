@echo off
echo ===================================================
echo Running Phase 5: Processing Domain Components
echo ===================================================

echo.
echo Step 1: Building Phase 5 components...
call build-phase5.bat
if %ERRORLEVEL% neq 0 (
    echo Error building Phase 5 components
    exit /b %ERRORLEVEL%
)

echo.
echo Step 2: Verifying Phase 5 components...
call verify-phase5.bat
if %ERRORLEVEL% neq 0 (
    echo Error verifying Phase 5 components
    exit /b %ERRORLEVEL%
)

echo.
echo Step 3: Building full solution...
dotnet build
if %ERRORLEVEL% neq 0 (
    echo Error building full solution
    exit /b %ERRORLEVEL%
)

echo.
echo Phase 5 completed successfully!
echo ===================================================
