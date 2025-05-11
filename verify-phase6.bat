@echo off
echo ===================================================
echo Verifying Phase 6: Management Domain Components
echo ===================================================

echo.
echo Verifying FlowOrchestrator.ServiceManager...
if not exist src\Management\FlowOrchestrator.ServiceManager\Class1.cs (
    echo Error: FlowOrchestrator.ServiceManager\Class1.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.ServiceManager\IServiceManager.cs (
    echo Error: FlowOrchestrator.ServiceManager\IServiceManager.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.ServiceManager\ServiceRegistration.cs (
    echo Error: FlowOrchestrator.ServiceManager\ServiceRegistration.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.ServiceManager\ServiceDiscovery.cs (
    echo Error: FlowOrchestrator.ServiceManager\ServiceDiscovery.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.ServiceManager\ServiceHealthCheck.cs (
    echo Error: FlowOrchestrator.ServiceManager\ServiceHealthCheck.cs not found
    exit /b 1
)
echo FlowOrchestrator.ServiceManager verified successfully!

echo.
echo Verifying FlowOrchestrator.FlowManager...
if not exist src\Management\FlowOrchestrator.FlowManager\Class1.cs (
    echo Error: FlowOrchestrator.FlowManager\Class1.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.FlowManager\IFlowManager.cs (
    echo Error: FlowOrchestrator.FlowManager\IFlowManager.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.FlowManager\FlowDefinition.cs (
    echo Error: FlowOrchestrator.FlowManager\FlowDefinition.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.FlowManager\FlowVersioning.cs (
    echo Error: FlowOrchestrator.FlowManager\FlowVersioning.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.FlowManager\FlowDeployment.cs (
    echo Error: FlowOrchestrator.FlowManager\FlowDeployment.cs not found
    exit /b 1
)
echo FlowOrchestrator.FlowManager verified successfully!

echo.
echo Verifying FlowOrchestrator.ConfigurationManager...
if not exist src\Management\FlowOrchestrator.ConfigurationManager\Class1.cs (
    echo Error: FlowOrchestrator.ConfigurationManager\Class1.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.ConfigurationManager\IConfigurationManager.cs (
    echo Error: FlowOrchestrator.ConfigurationManager\IConfigurationManager.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.ConfigurationManager\ConfigurationStore.cs (
    echo Error: FlowOrchestrator.ConfigurationManager\ConfigurationStore.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.ConfigurationManager\ConfigurationValidator.cs (
    echo Error: FlowOrchestrator.ConfigurationManager\ConfigurationValidator.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.ConfigurationManager\ParameterSchemaManager.cs (
    echo Error: FlowOrchestrator.ConfigurationManager\ParameterSchemaManager.cs not found
    exit /b 1
)
echo FlowOrchestrator.ConfigurationManager verified successfully!

echo.
echo Verifying FlowOrchestrator.VersionManager...
if not exist src\Management\FlowOrchestrator.VersionManager\Class1.cs (
    echo Error: FlowOrchestrator.VersionManager\Class1.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.VersionManager\IVersionManager.cs (
    echo Error: FlowOrchestrator.VersionManager\IVersionManager.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.VersionManager\VersionCompatibilityMatrix.cs (
    echo Error: FlowOrchestrator.VersionManager\VersionCompatibilityMatrix.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.VersionManager\VersionLifecycleManager.cs (
    echo Error: FlowOrchestrator.VersionManager\VersionLifecycleManager.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.VersionManager\VersionRegistration.cs (
    echo Error: FlowOrchestrator.VersionManager\VersionRegistration.cs not found
    exit /b 1
)
echo FlowOrchestrator.VersionManager verified successfully!

echo.
echo Verifying FlowOrchestrator.TaskScheduler...
if not exist src\Management\FlowOrchestrator.TaskScheduler\Class1.cs (
    echo Error: FlowOrchestrator.TaskScheduler\Class1.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.TaskScheduler\ITaskScheduler.cs (
    echo Error: FlowOrchestrator.TaskScheduler\ITaskScheduler.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.TaskScheduler\ScheduledTask.cs (
    echo Error: FlowOrchestrator.TaskScheduler\ScheduledTask.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.TaskScheduler\TaskExecution.cs (
    echo Error: FlowOrchestrator.TaskScheduler\TaskExecution.cs not found
    exit /b 1
)
if not exist src\Management\FlowOrchestrator.TaskScheduler\RecurringTaskManager.cs (
    echo Error: FlowOrchestrator.TaskScheduler\RecurringTaskManager.cs not found
    exit /b 1
)
echo FlowOrchestrator.TaskScheduler verified successfully!

echo.
echo All Phase 6 components verified successfully!
echo ===================================================
