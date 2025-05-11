@echo off
echo ===================================================
echo Running Phase 6: Management Domain Components
echo ===================================================

echo.
echo Step 1: Building Phase 6 components...
call build-phase6.bat
if %ERRORLEVEL% neq 0 (
    echo Error building Phase 6 components
    exit /b 1
)

echo.
echo Step 2: Verifying Phase 6 components...
call verify-phase6.bat
if %ERRORLEVEL% neq 0 (
    echo Error verifying Phase 6 components
    exit /b 1
)

echo.
echo Step 3: Building full solution...
dotnet build
if %ERRORLEVEL% neq 0 (
    echo Error building full solution
    exit /b 1
)

echo.
echo Phase 6 completed successfully!
echo ===================================================
