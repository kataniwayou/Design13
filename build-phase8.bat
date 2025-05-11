@echo off
echo ===================================================
echo Building Phase 8: Testing and Integration Components
echo ===================================================
echo.

echo Creating test projects...

echo Creating FlowOrchestrator.UnitTests project...
if not exist tests\FlowOrchestrator.UnitTests (
    mkdir tests\FlowOrchestrator.UnitTests
    dotnet new xunit -o tests\FlowOrchestrator.UnitTests
    dotnet add tests\FlowOrchestrator.UnitTests\FlowOrchestrator.UnitTests.csproj reference src\Core\FlowOrchestrator.Abstractions\FlowOrchestrator.Abstractions.csproj
    dotnet add tests\FlowOrchestrator.UnitTests\FlowOrchestrator.UnitTests.csproj reference src\Core\FlowOrchestrator.Common\FlowOrchestrator.Common.csproj
    dotnet add tests\FlowOrchestrator.UnitTests\FlowOrchestrator.UnitTests.csproj reference src\Core\FlowOrchestrator.Domain\FlowOrchestrator.Domain.csproj
    dotnet add tests\FlowOrchestrator.UnitTests\FlowOrchestrator.UnitTests.csproj package Moq
    dotnet add tests\FlowOrchestrator.UnitTests\FlowOrchestrator.UnitTests.csproj package FluentAssertions
)
echo FlowOrchestrator.UnitTests project created!
echo.

echo Creating FlowOrchestrator.IntegrationTests project...
if not exist tests\FlowOrchestrator.IntegrationTests (
    mkdir tests\FlowOrchestrator.IntegrationTests
    dotnet new xunit -o tests\FlowOrchestrator.IntegrationTests
    dotnet add tests\FlowOrchestrator.IntegrationTests\FlowOrchestrator.IntegrationTests.csproj reference src\Core\FlowOrchestrator.Abstractions\FlowOrchestrator.Abstractions.csproj
    dotnet add tests\FlowOrchestrator.IntegrationTests\FlowOrchestrator.IntegrationTests.csproj reference src\Core\FlowOrchestrator.Common\FlowOrchestrator.Common.csproj
    dotnet add tests\FlowOrchestrator.IntegrationTests\FlowOrchestrator.IntegrationTests.csproj reference src\Core\FlowOrchestrator.Domain\FlowOrchestrator.Domain.csproj
    dotnet add tests\FlowOrchestrator.IntegrationTests\FlowOrchestrator.IntegrationTests.csproj package Microsoft.AspNetCore.Mvc.Testing
    dotnet add tests\FlowOrchestrator.IntegrationTests\FlowOrchestrator.IntegrationTests.csproj package Microsoft.EntityFrameworkCore.InMemory
    dotnet add tests\FlowOrchestrator.IntegrationTests\FlowOrchestrator.IntegrationTests.csproj package FluentAssertions
)
echo FlowOrchestrator.IntegrationTests project created!
echo.

echo Creating FlowOrchestrator.SystemTests project...
if not exist tests\FlowOrchestrator.SystemTests (
    mkdir tests\FlowOrchestrator.SystemTests
    dotnet new xunit -o tests\FlowOrchestrator.SystemTests
    dotnet add tests\FlowOrchestrator.SystemTests\FlowOrchestrator.SystemTests.csproj reference src\Core\FlowOrchestrator.Abstractions\FlowOrchestrator.Abstractions.csproj
    dotnet add tests\FlowOrchestrator.SystemTests\FlowOrchestrator.SystemTests.csproj reference src\Core\FlowOrchestrator.Common\FlowOrchestrator.Common.csproj
    dotnet add tests\FlowOrchestrator.SystemTests\FlowOrchestrator.SystemTests.csproj reference src\Core\FlowOrchestrator.Domain\FlowOrchestrator.Domain.csproj
    dotnet add tests\FlowOrchestrator.SystemTests\FlowOrchestrator.SystemTests.csproj package Microsoft.AspNetCore.Mvc.Testing
    dotnet add tests\FlowOrchestrator.SystemTests\FlowOrchestrator.SystemTests.csproj package Testcontainers
    dotnet add tests\FlowOrchestrator.SystemTests\FlowOrchestrator.SystemTests.csproj package FluentAssertions
)
echo FlowOrchestrator.SystemTests project created!
echo.

echo Creating FlowOrchestrator.SampleFlows project...
if not exist samples\FlowOrchestrator.SampleFlows (
    mkdir samples\FlowOrchestrator.SampleFlows
    dotnet new console -o samples\FlowOrchestrator.SampleFlows
    dotnet add samples\FlowOrchestrator.SampleFlows\FlowOrchestrator.SampleFlows.csproj reference src\Core\FlowOrchestrator.Abstractions\FlowOrchestrator.Abstractions.csproj
    dotnet add samples\FlowOrchestrator.SampleFlows\FlowOrchestrator.SampleFlows.csproj reference src\Core\FlowOrchestrator.Common\FlowOrchestrator.Common.csproj
    dotnet add samples\FlowOrchestrator.SampleFlows\FlowOrchestrator.SampleFlows.csproj reference src\Core\FlowOrchestrator.Domain\FlowOrchestrator.Domain.csproj
    dotnet add samples\FlowOrchestrator.SampleFlows\FlowOrchestrator.SampleFlows.csproj reference src\Execution\FlowOrchestrator.Orchestrator\FlowOrchestrator.Orchestrator.csproj
)
echo FlowOrchestrator.SampleFlows project created!
echo.

echo Building test projects...

echo Building FlowOrchestrator.UnitTests...
dotnet build tests\FlowOrchestrator.UnitTests\FlowOrchestrator.UnitTests.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.UnitTests
    exit /b %ERRORLEVEL%
)
echo FlowOrchestrator.UnitTests built successfully!
echo.

echo Building FlowOrchestrator.IntegrationTests...
dotnet build tests\FlowOrchestrator.IntegrationTests\FlowOrchestrator.IntegrationTests.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.IntegrationTests
    exit /b %ERRORLEVEL%
)
echo FlowOrchestrator.IntegrationTests built successfully!
echo.

echo Building FlowOrchestrator.SystemTests...
dotnet build tests\FlowOrchestrator.SystemTests\FlowOrchestrator.SystemTests.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.SystemTests
    exit /b %ERRORLEVEL%
)
echo FlowOrchestrator.SystemTests built successfully!
echo.

echo Building FlowOrchestrator.SampleFlows...
dotnet build samples\FlowOrchestrator.SampleFlows\FlowOrchestrator.SampleFlows.csproj
if %ERRORLEVEL% neq 0 (
    echo Error building FlowOrchestrator.SampleFlows
    exit /b %ERRORLEVEL%
)
echo FlowOrchestrator.SampleFlows built successfully!
echo.

echo All Phase 8 components built successfully!
echo ===================================================
