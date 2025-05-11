@echo off
echo ===================================================
echo Verifying Phase 9: Documentation and Tools
echo ===================================================

echo Verifying documentation components...
echo Verifying API documentation...
if not exist "docs\api" (
    echo Error: API documentation directory not found!
    exit /b 1
) else (
    echo API documentation directory verified!
)

echo Verifying user guides...
if not exist "docs\guides" (
    echo Error: User guides directory not found!
    exit /b 1
) else (
    echo User guides directory verified!
)

echo Verifying architecture documentation...
if not exist "docs\architecture" (
    echo Error: Architecture documentation directory not found!
    exit /b 1
) else (
    echo Architecture documentation directory verified!
)

echo Verifying deployment documentation...
if not exist "docs\deployment" (
    echo Error: Deployment documentation directory not found!
    exit /b 1
) else (
    echo Deployment documentation directory verified!
)

echo Verifying tools components...
echo Verifying flow designer tool...
if not exist "tools\FlowDesigner\FlowOrchestrator.Tools.FlowDesigner\bin" (
    echo Error: Flow designer tool build output not found!
    exit /b 1
) else (
    echo Flow designer tool verified!
)

echo Verifying diagnostic tools...
if not exist "tools\Diagnostics\FlowOrchestrator.Tools.Diagnostics\bin" (
    echo Error: Diagnostic tools build output not found!
    exit /b 1
) else (
    echo Diagnostic tools verified!
)

echo Verifying deployment tools...
if not exist "tools\Deployment\FlowOrchestrator.Tools.Deployment\bin" (
    echo Error: Deployment tools build output not found!
    exit /b 1
) else (
    echo Deployment tools verified!
)

echo All Phase 9 components verified successfully!
echo ===================================================
