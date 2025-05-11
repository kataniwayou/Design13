# FlowOrchestrator Getting Started Tutorial

## Introduction

This tutorial will guide you through the process of creating and executing a simple data flow using the FlowOrchestrator system. By the end of this tutorial, you will have a working flow that reads data from a file, transforms it, and writes it to another file.

## Prerequisites

Before you begin, make sure you have:

- Installed the FlowOrchestrator system
- Basic understanding of C# and .NET
- A text editor or IDE

## Step 1: Create a New Project

First, let's create a new console application project:

```bash
dotnet new console -n FlowOrchestratorDemo
cd FlowOrchestratorDemo
```

Add references to the FlowOrchestrator packages:

```bash
dotnet add package FlowOrchestrator.Abstractions
dotnet add package FlowOrchestrator.Domain
dotnet add package FlowOrchestrator.Common
dotnet add package FlowOrchestrator.FlowManager
dotnet add package FlowOrchestrator.Orchestrator
```

## Step 2: Create a Sample Data File

Create a file named `data.json` in the project directory with the following content:

```json
{
  "customers": [
    {
      "id": 1,
      "name": "John Doe",
      "email": "john.doe@example.com",
      "age": 30
    },
    {
      "id": 2,
      "name": "Jane Smith",
      "email": "jane.smith@example.com",
      "age": 25
    },
    {
      "id": 3,
      "name": "Bob Johnson",
      "email": "bob.johnson@example.com",
      "age": 35
    }
  ]
}
```

## Step 3: Create the Program

Replace the content of `Program.cs` with the following code:

```csharp
using FlowOrchestrator.Abstractions;
using FlowOrchestrator.Domain.Models;
using FlowOrchestrator.FlowManager;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowOrchestratorDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("FlowOrchestrator Demo");
            Console.WriteLine("=====================");

            // Set up dependency injection
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            // Create a flow definition
            var flowDefinition = CreateFlowDefinition();

            // Save the flow definition
            var flowManager = serviceProvider.GetRequiredService<IFlowManager>();
            await flowManager.SaveFlowDefinitionAsync(flowDefinition);

            // Execute the flow
            var orchestrator = serviceProvider.GetRequiredService<IFlowOrchestrator>();
            var result = await orchestrator.ExecuteFlowAsync(flowDefinition.FlowId);

            // Display the results
            Console.WriteLine($"Flow execution completed with status: {(result.Success ? "Success" : "Failure")}");
            Console.WriteLine($"Execution ID: {result.ExecutionId}");
            Console.WriteLine($"Start time: {result.StartTime}");
            Console.WriteLine($"End time: {result.EndTime}");
            Console.WriteLine($"Duration: {result.EndTime - result.StartTime}");
            Console.WriteLine($"Completed components: {string.Join(", ", result.CompletedComponents)}");

            if (result.Errors.Count > 0)
            {
                Console.WriteLine("Errors:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"- {error}");
                }
            }

            Console.WriteLine("Done!");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // Register FlowOrchestrator services
            services.AddFlowOrchestrator();
        }

        static FlowDefinition CreateFlowDefinition()
        {
            return new FlowDefinition
            {
                FlowId = "demo-flow",
                Name = "Demo Flow",
                Description = "A simple demo flow",
                Version = "1.0.0",
                Components = new List<FlowComponent>
                {
                    new FlowComponent
                    {
                        ComponentId = "source",
                        Name = "File Source",
                        ComponentType = "FileImporter",
                        Configuration = new Dictionary<string, object>
                        {
                            { "filePath", "data.json" },
                            { "format", "json" }
                        }
                    },
                    new FlowComponent
                    {
                        ComponentId = "transform",
                        Name = "JSON Transformer",
                        ComponentType = "JsonProcessor",
                        Configuration = new Dictionary<string, object>
                        {
                            { "operations", new[] 
                                { 
                                    "customers[*].name => UPPER(name)",
                                    "customers[*].email => LOWER(email)",
                                    "customers[*].age => age + 1"
                                } 
                            }
                        }
                    },
                    new FlowComponent
                    {
                        ComponentId = "destination",
                        Name = "File Destination",
                        ComponentType = "FileExporter",
                        Configuration = new Dictionary<string, object>
                        {
                            { "filePath", "output.json" },
                            { "format", "json" },
                            { "overwriteExisting", true }
                        }
                    }
                },
                Connections = new List<FlowConnection>
                {
                    new FlowConnection
                    {
                        ConnectionId = "conn1",
                        SourceComponentId = "source",
                        TargetComponentId = "transform"
                    },
                    new FlowConnection
                    {
                        ConnectionId = "conn2",
                        SourceComponentId = "transform",
                        TargetComponentId = "destination"
                    }
                }
            };
        }
    }
}
```

## Step 4: Run the Application

Build and run the application:

```bash
dotnet build
dotnet run
```

You should see output similar to the following:

```
FlowOrchestrator Demo
=====================
Flow execution completed with status: Success
Execution ID: 12345678-1234-1234-1234-123456789012
Start time: 2023-06-08 10:00:00
End time: 2023-06-08 10:00:01
Duration: 00:00:01
Completed components: source, transform, destination
Done!
```

## Step 5: Examine the Output

Check the output file `output.json`. It should contain the transformed data:

```json
{
  "customers": [
    {
      "id": 1,
      "name": "JOHN DOE",
      "email": "john.doe@example.com",
      "age": 31
    },
    {
      "id": 2,
      "name": "JANE SMITH",
      "email": "jane.smith@example.com",
      "age": 26
    },
    {
      "id": 3,
      "name": "BOB JOHNSON",
      "email": "bob.johnson@example.com",
      "age": 36
    }
  ]
}
```

## Next Steps

Now that you have created and executed a simple flow, you can:

1. Explore more component types
2. Create more complex flows
3. Implement custom components
4. Explore advanced features like error handling and monitoring

## Conclusion

In this tutorial, you learned how to:

1. Create a new project using FlowOrchestrator
2. Define a flow with source, transformation, and destination components
3. Execute the flow
4. Examine the results

Congratulations! You have successfully created and executed your first flow using the FlowOrchestrator system.
