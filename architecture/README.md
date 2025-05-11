# FlowOrchestrator Architecture Documentation

This directory contains architecture documentation for the FlowOrchestrator system.

## Documentation Files

- **AbstractClassesUML.puml**: PlantUML diagram of all abstract classes and their relationships
- **AbstractClassRelationships.md**: Textual representation of abstract class relationships
- **AbstractAndCommonComponentsRelationships.md**: Comprehensive view of abstract classes and common components relationships

## UML Diagrams

### Abstract Classes UML Diagram

The file `AbstractClassesUML.puml` contains a PlantUML diagram of all abstract classes in the FlowOrchestrator system, including their relationships and MassTransit integration.

To render this diagram, you have several options:

### Option 1: Use the provided batch file (Windows)

1. Make sure Java is installed on your system
2. Run the `render-uml.bat` file in the root directory
3. The rendered diagrams will be available in the `images` directory

### Option 2: Use PlantUML directly

1. Install PlantUML (https://plantuml.com/starting)
2. Run the following command:
   ```
   java -jar plantuml.jar AbstractClassesUML.puml -o images
   ```
3. The rendered diagram will be available at `images/FlowOrchestrator_Abstract_Classes.png`

### Option 3: Use online PlantUML renderers (Recommended)

1. Copy the content of the `AbstractClassesUML.puml` file
2. Paste it into one of these online renderers:
   - PlantUML Online Server: https://www.plantuml.com/plantuml/
   - PlantText: https://www.planttext.com/

### Option 4: Use the PlantUML extension for Visual Studio Code

1. Install the PlantUML extension for VS Code
2. Open the `AbstractClassesUML.puml` file
3. Use Alt+D to preview the diagram

### Key Components in the Diagram

The UML diagram shows the following key components:

1. **AbstractServiceBase**: The foundation for all service-oriented components
2. **Service-Specific Abstract Classes**: Extensions of AbstractServiceBase for specific service types
3. **AbstractEntity**: The foundation for all domain entities
4. **Entity-Specific Abstract Classes**: Extensions of AbstractEntity for specific entity types
5. **MassTransit Integration**: Consumer interfaces and publishing methods
6. **OpenTelemetry Integration**: Logging, metrics, and tracing capabilities

### Inheritance Hierarchies

The diagram illustrates two main inheritance hierarchies:

1. **Service Hierarchy**:
   - AbstractServiceBase at the root
   - Service-specific abstract classes extending AbstractServiceBase
   - Each service-specific abstract class implementing relevant consumer interfaces

2. **Entity Hierarchy**:
   - AbstractEntity at the root
   - Entity-specific abstract classes extending AbstractEntity
   - Each entity-specific abstract class providing domain-specific functionality

### Infrastructure Integration

The diagram also shows how abstract components integrate with infrastructure components:

1. **MassTransit Integration**: For messaging capabilities
2. **OpenTelemetry Integration**: For observability capabilities

## Additional Documentation

### Local Documentation

- **AbstractClassRelationships.md**: Textual representation of abstract class relationships
  - Provides a hierarchical view of all abstract classes
  - Shows key relationships between components
  - Includes concrete implementations and their relationships to abstract classes

- **AbstractAndCommonComponentsRelationships.md**: Comprehensive view of abstract classes, common components, and concrete implementations
  - Part 1: Detailed abstract class relationships with project locations
  - Part 2: Common components relationship to abstract components
  - Part 3: Concrete implementations and inheritance hierarchies
  - Covers infrastructure integration, configuration, validation, error management, state management, execution, and protocol components
  - Includes complete inheritance hierarchies and cross-cutting concerns
  - Contains references to detailed business descriptions in ComponentBusinessDescriptions.md

- **ComponentBusinessDescriptions.md**: Detailed business descriptions for all components
  - Provides business purpose, responsibilities, relationships, and business rules for each component
  - Organized by component type (core abstract, service-oriented abstract, entity abstract, etc.)
  - Cross-referenced from AbstractAndCommonComponentsRelationships.md for easy navigation

### System Architecture Documentation

For more detailed information about each abstract component, refer to the following documents:

- Abstract Classes Specification: `../SAD/Abstract Classes Specification.md`
- Core Domain Model: `../SAD/Core Domain Model.md`
- Component Architecture: `../SAD/Component Architecture.md`
- System Overview: `../SAD/System Overview.md`
