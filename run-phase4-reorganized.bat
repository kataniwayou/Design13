@echo off
echo ===================================================
echo FlowOrchestrator Phase 4 Incremental Build and Verification
echo Integration Domain Components (Reorganized)
echo ===================================================
echo.

echo Running build process...
call build-phase4-reorganized.bat
if %ERRORLEVEL% neq 0 (
    echo Build process failed
    exit /b %ERRORLEVEL%
)
echo Build process completed successfully.

echo.
echo Running verification process...
call verify-phase4-reorganized.bat
if %ERRORLEVEL% neq 0 (
    echo Verification process failed
    exit /b %ERRORLEVEL%
)
echo Verification process completed successfully.

echo.
echo ===================================================
echo Phase 4 Incremental Build and Verification Completed Successfully
echo All Integration Domain Components (Reorganized) are ready
echo ===================================================
