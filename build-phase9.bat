@echo off
echo ===================================================
echo Building Phase 9: Documentation and Tools
echo ===================================================

echo Creating documentation directory...
if not exist "docs" mkdir docs

echo Creating API documentation...
if not exist "docs\api" mkdir docs\api
echo API documentation directory created!

echo Creating user guides...
if not exist "docs\guides" mkdir docs\guides
echo User guides directory created!

echo Creating architecture documentation...
if not exist "docs\architecture" mkdir docs\architecture
echo Architecture documentation directory created!

echo Creating deployment documentation...
if not exist "docs\deployment" mkdir docs\deployment
echo Deployment documentation directory created!

echo Creating tools directory...
if not exist "tools" mkdir tools

echo Creating flow designer tool...
if not exist "tools\FlowDesigner" mkdir tools\FlowDesigner
echo Flow designer tool directory created!

echo Creating diagnostic tools...
if not exist "tools\Diagnostics" mkdir tools\Diagnostics
echo Diagnostic tools directory created!

echo Creating deployment tools...
if not exist "tools\Deployment" mkdir tools\Deployment
echo Deployment tools directory created!

echo Building documentation components...
echo Generating API documentation...
dotnet build src/Core/FlowOrchestrator.Abstractions/FlowOrchestrator.Abstractions.csproj -c Release
dotnet build src/Core/FlowOrchestrator.Domain/FlowOrchestrator.Domain.csproj -c Release
dotnet build src/Core/FlowOrchestrator.Common/FlowOrchestrator.Common.csproj -c Release
dotnet build src/Management/FlowOrchestrator.FlowManager/FlowOrchestrator.FlowManager.csproj -c Release
dotnet build src/Management/FlowOrchestrator.VersionManager/FlowOrchestrator.VersionManager.csproj -c Release

echo Generating architecture documentation...
copy /Y docs\templates\architecture-template.md docs\architecture\system-architecture.md
copy /Y docs\templates\component-diagram-template.md docs\architecture\component-diagram.md
copy /Y docs\templates\interaction-flow-template.md docs\architecture\interaction-flow.md

echo Generating user guides...
copy /Y docs\templates\user-guide-template.md docs\guides\flow-creation-guide.md
copy /Y docs\templates\configuration-guide-template.md docs\guides\configuration-guide.md
copy /Y docs\templates\tutorial-template.md docs\guides\getting-started.md

echo Generating deployment documentation...
copy /Y docs\templates\deployment-guide-template.md docs\deployment\deployment-guide.md
copy /Y docs\templates\environment-setup-template.md docs\deployment\environment-setup.md
copy /Y docs\templates\configuration-options-template.md docs\deployment\configuration-options.md

echo Building tools components...
echo Building flow designer tool...
dotnet new console -o tools\FlowDesigner\FlowOrchestrator.Tools.FlowDesigner
dotnet add tools\FlowDesigner\FlowOrchestrator.Tools.FlowDesigner\FlowOrchestrator.Tools.FlowDesigner.csproj reference src\Management\FlowOrchestrator.FlowManager\FlowOrchestrator.FlowManager.csproj
dotnet build tools\FlowDesigner\FlowOrchestrator.Tools.FlowDesigner\FlowOrchestrator.Tools.FlowDesigner.csproj

echo Building diagnostic tools...
dotnet new console -o tools\Diagnostics\FlowOrchestrator.Tools.Diagnostics
dotnet add tools\Diagnostics\FlowOrchestrator.Tools.Diagnostics\FlowOrchestrator.Tools.Diagnostics.csproj reference src\Core\FlowOrchestrator.Common\FlowOrchestrator.Common.csproj
dotnet build tools\Diagnostics\FlowOrchestrator.Tools.Diagnostics\FlowOrchestrator.Tools.Diagnostics.csproj

echo Building deployment tools...
dotnet new console -o tools\Deployment\FlowOrchestrator.Tools.Deployment
dotnet add tools\Deployment\FlowOrchestrator.Tools.Deployment\FlowOrchestrator.Tools.Deployment.csproj reference src\Core\FlowOrchestrator.Common\FlowOrchestrator.Common.csproj
dotnet build tools\Deployment\FlowOrchestrator.Tools.Deployment\FlowOrchestrator.Tools.Deployment.csproj

echo All Phase 9 components built successfully!
echo ===================================================
