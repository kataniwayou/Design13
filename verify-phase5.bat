@echo off
echo ===================================================
echo Verifying Phase 5: Processing Domain Components
echo ===================================================

echo.
echo Verifying FlowOrchestrator.ProcessorBase...
if not exist src\Processing\FlowOrchestrator.ProcessorBase\Class1.cs (
    echo Error: FlowOrchestrator.ProcessorBase\Class1.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.ProcessorBase\IProcessor.cs (
    echo Error: FlowOrchestrator.ProcessorBase\IProcessor.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.ProcessorBase\ProcessingContext.cs (
    echo Error: FlowOrchestrator.ProcessorBase\ProcessingContext.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.ProcessorBase\ProcessingResult.cs (
    echo Error: FlowOrchestrator.ProcessorBase\ProcessingResult.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.ProcessorBase\ProcessorCapabilities.cs (
    echo Error: FlowOrchestrator.ProcessorBase\ProcessorCapabilities.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.ProcessorBase\ProcessorStatus.cs (
    echo Error: FlowOrchestrator.ProcessorBase\ProcessorStatus.cs not found
    exit /b 1
)
echo FlowOrchestrator.ProcessorBase verified successfully!

echo.
echo Verifying FlowOrchestrator.TransformationEngine...
if not exist src\Processing\FlowOrchestrator.TransformationEngine\Class1.cs (
    echo Error: FlowOrchestrator.TransformationEngine\Class1.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.TransformationEngine\ITransformationEngine.cs (
    echo Error: FlowOrchestrator.TransformationEngine\ITransformationEngine.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.TransformationEngine\TransformationRule.cs (
    echo Error: FlowOrchestrator.TransformationEngine\TransformationRule.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.TransformationEngine\MappingDefinition.cs (
    echo Error: FlowOrchestrator.TransformationEngine\MappingDefinition.cs not found
    exit /b 1
)
echo FlowOrchestrator.TransformationEngine verified successfully!

echo.
echo Verifying FlowOrchestrator.JsonProcessor...
if not exist src\Processing\FlowOrchestrator.JsonProcessor\Class1.cs (
    echo Error: FlowOrchestrator.JsonProcessor\Class1.cs not found
    exit /b 1
)
echo FlowOrchestrator.JsonProcessor verified successfully!

echo.
echo Verifying FlowOrchestrator.ValidationProcessor...
if not exist src\Processing\FlowOrchestrator.ValidationProcessor\Class1.cs (
    echo Error: FlowOrchestrator.ValidationProcessor\Class1.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.ValidationProcessor\ValidationRule.cs (
    echo Error: FlowOrchestrator.ValidationProcessor\ValidationRule.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.ValidationProcessor\ValidationSeverity.cs (
    echo Error: FlowOrchestrator.ValidationProcessor\ValidationSeverity.cs not found
    exit /b 1
)
echo FlowOrchestrator.ValidationProcessor verified successfully!

echo.
echo Verifying FlowOrchestrator.EnrichmentProcessor...
if not exist src\Processing\FlowOrchestrator.EnrichmentProcessor\Class1.cs (
    echo Error: FlowOrchestrator.EnrichmentProcessor\Class1.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.EnrichmentProcessor\EnrichmentRule.cs (
    echo Error: FlowOrchestrator.EnrichmentProcessor\EnrichmentRule.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.EnrichmentProcessor\IEnrichmentSource.cs (
    echo Error: FlowOrchestrator.EnrichmentProcessor\IEnrichmentSource.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.EnrichmentProcessor\IEnrichmentSourceProvider.cs (
    echo Error: FlowOrchestrator.EnrichmentProcessor\IEnrichmentSourceProvider.cs not found
    exit /b 1
)
echo FlowOrchestrator.EnrichmentProcessor verified successfully!

echo.
echo Verifying FlowOrchestrator.MappingProcessor...
if not exist src\Processing\FlowOrchestrator.MappingProcessor\Class1.cs (
    echo Error: FlowOrchestrator.MappingProcessor\Class1.cs not found
    exit /b 1
)
if not exist src\Processing\FlowOrchestrator.MappingProcessor\MappingDefinition.cs (
    echo Error: FlowOrchestrator.MappingProcessor\MappingDefinition.cs not found
    exit /b 1
)
echo FlowOrchestrator.MappingProcessor verified successfully!

echo.
echo All Phase 5 components verified successfully!
echo ===================================================
