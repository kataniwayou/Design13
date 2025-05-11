using FlowOrchestrator.Domain.Models;
using FlowOrchestrator.FlowManager;
using System.Text.Json;

namespace FlowOrchestrator.SampleFlows;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("FlowOrchestrator Sample Flows");
        Console.WriteLine("============================");
        Console.WriteLine();

        if (args.Length > 0 && args[0] == "list")
        {
            ListAvailableSampleFlows();
            return;
        }

        string flowName = args.Length > 0 ? args[0] : "data-transformation";

        Console.WriteLine($"Executing sample flow: {flowName}");
        Console.WriteLine();

        try
        {
            await ExecuteSampleFlowAsync(flowName);
            Console.WriteLine("Flow execution completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error executing flow: {ex.Message}");
        }
    }

    private static void ListAvailableSampleFlows()
    {
        Console.WriteLine("Available Sample Flows:");
        Console.WriteLine("1. data-transformation - Data Transformation Flow");
        Console.WriteLine("2. data-validation - Data Validation Flow");
        Console.WriteLine("3. data-enrichment - Data Enrichment Flow");
        Console.WriteLine("4. data-mapping - Data Mapping Flow");
        Console.WriteLine();
        Console.WriteLine("Usage: FlowOrchestrator.SampleFlows <flow-name>");
    }

    private static async Task ExecuteSampleFlowAsync(string flowName)
    {
        FlowDefinition flowDefinition;

        switch (flowName.ToLower())
        {
            case "data-transformation":
                flowDefinition = CreateDataTransformationFlow();
                break;
            case "data-validation":
                flowDefinition = CreateDataValidationFlow();
                break;
            case "data-enrichment":
                flowDefinition = CreateDataEnrichmentFlow();
                break;
            case "data-mapping":
                flowDefinition = CreateDataMappingFlow();
                break;
            default:
                throw new ArgumentException($"Unknown flow: {flowName}");
        }

        // In a real implementation, this would use the orchestrator to execute the flow
        Console.WriteLine($"Flow Definition: {JsonSerializer.Serialize(flowDefinition, new JsonSerializerOptions { WriteIndented = true })}");

        // Simulate flow execution
        Console.WriteLine("Executing flow components:");
        foreach (var component in flowDefinition.Components)
        {
            Console.WriteLine($"- Executing component: {component.Name} ({component.ComponentType})");
            await Task.Delay(500); // Simulate processing time
        }

        Console.WriteLine("Flow execution completed.");
    }

    private static FlowDefinition CreateDataTransformationFlow()
    {
        return new FlowDefinition
        {
            FlowId = "data-transformation-flow",
            Name = "Data Transformation Flow",
            Description = "A sample flow demonstrating data transformation",
            Version = "1.0.0",
            Components = new List<FlowComponent>
            {
                new FlowComponent
                {
                    ComponentId = "source",
                    Name = "JSON Source",
                    ComponentType = "JsonProcessor",
                    Configuration = new Dictionary<string, object>
                    {
                        { "data", "{ \"name\": \"John Doe\", \"age\": 30, \"email\": \"john.doe@example.com\" }" }
                    }
                },
                new FlowComponent
                {
                    ComponentId = "transform",
                    Name = "Transform Data",
                    ComponentType = "TransformationEngine",
                    Configuration = new Dictionary<string, object>
                    {
                        { "transformations", new[]
                            {
                                "name => UPPER(name)",
                                "age => age + 1",
                                "email => LOWER(email)"
                            }
                        }
                    }
                },
                new FlowComponent
                {
                    ComponentId = "destination",
                    Name = "Console Output",
                    ComponentType = "ConsoleOutput",
                    Configuration = new Dictionary<string, object>()
                }
            },
            Connections = new List<FlowConnection>
            {
                new FlowConnection { ConnectionId = "conn1", SourceComponentId = "source", TargetComponentId = "transform" },
                new FlowConnection { ConnectionId = "conn2", SourceComponentId = "transform", TargetComponentId = "destination" }
            }
        };
    }

    private static FlowDefinition CreateDataValidationFlow()
    {
        return new FlowDefinition
        {
            FlowId = "data-validation-flow",
            Name = "Data Validation Flow",
            Description = "A sample flow demonstrating data validation",
            Version = "1.0.0",
            Components = new List<FlowComponent>
            {
                new FlowComponent
                {
                    ComponentId = "source",
                    Name = "JSON Source",
                    ComponentType = "JsonProcessor",
                    Configuration = new Dictionary<string, object>
                    {
                        { "data", "{ \"name\": \"John Doe\", \"age\": 30, \"email\": \"john.doe@example.com\" }" }
                    }
                },
                new FlowComponent
                {
                    ComponentId = "validate",
                    Name = "Validate Data",
                    ComponentType = "ValidationProcessor",
                    Configuration = new Dictionary<string, object>
                    {
                        { "rules", new[]
                            {
                                "name => REQUIRED",
                                "age => RANGE(18, 65)",
                                "email => EMAIL"
                            }
                        }
                    }
                },
                new FlowComponent
                {
                    ComponentId = "destination",
                    Name = "Console Output",
                    ComponentType = "ConsoleOutput",
                    Configuration = new Dictionary<string, object>()
                }
            },
            Connections = new List<FlowConnection>
            {
                new FlowConnection { ConnectionId = "conn1", SourceComponentId = "source", TargetComponentId = "validate" },
                new FlowConnection { ConnectionId = "conn2", SourceComponentId = "validate", TargetComponentId = "destination" }
            }
        };
    }

    private static FlowDefinition CreateDataEnrichmentFlow()
    {
        return new FlowDefinition
        {
            FlowId = "data-enrichment-flow",
            Name = "Data Enrichment Flow",
            Description = "A sample flow demonstrating data enrichment",
            Version = "1.0.0",
            Components = new List<FlowComponent>
            {
                new FlowComponent
                {
                    ComponentId = "source",
                    Name = "JSON Source",
                    ComponentType = "JsonProcessor",
                    Configuration = new Dictionary<string, object>
                    {
                        { "data", "{ \"userId\": \"12345\", \"name\": \"John Doe\" }" }
                    }
                },
                new FlowComponent
                {
                    ComponentId = "enrich",
                    Name = "Enrich Data",
                    ComponentType = "EnrichmentProcessor",
                    Configuration = new Dictionary<string, object>
                    {
                        { "enrichments", new[]
                            {
                                "userDetails => LOOKUP(userId, 'user-database')",
                                "location => GEOIP(userDetails.ipAddress)",
                                "timestamp => NOW()"
                            }
                        }
                    }
                },
                new FlowComponent
                {
                    ComponentId = "destination",
                    Name = "Console Output",
                    ComponentType = "ConsoleOutput",
                    Configuration = new Dictionary<string, object>()
                }
            },
            Connections = new List<FlowConnection>
            {
                new FlowConnection { ConnectionId = "conn1", SourceComponentId = "source", TargetComponentId = "enrich" },
                new FlowConnection { ConnectionId = "conn2", SourceComponentId = "enrich", TargetComponentId = "destination" }
            }
        };
    }

    private static FlowDefinition CreateDataMappingFlow()
    {
        return new FlowDefinition
        {
            FlowId = "data-mapping-flow",
            Name = "Data Mapping Flow",
            Description = "A sample flow demonstrating data mapping",
            Version = "1.0.0",
            Components = new List<FlowComponent>
            {
                new FlowComponent
                {
                    ComponentId = "source",
                    Name = "JSON Source",
                    ComponentType = "JsonProcessor",
                    Configuration = new Dictionary<string, object>
                    {
                        { "data", "{ \"firstName\": \"John\", \"lastName\": \"Doe\", \"emailAddress\": \"john.doe@example.com\" }" }
                    }
                },
                new FlowComponent
                {
                    ComponentId = "map",
                    Name = "Map Data",
                    ComponentType = "MappingProcessor",
                    Configuration = new Dictionary<string, object>
                    {
                        { "mappings", new Dictionary<string, string>
                            {
                                { "name", "CONCAT(firstName, ' ', lastName)" },
                                { "email", "emailAddress" },
                                { "username", "SUBSTRING_BEFORE(emailAddress, '@')" }
                            }
                        }
                    }
                },
                new FlowComponent
                {
                    ComponentId = "destination",
                    Name = "Console Output",
                    ComponentType = "ConsoleOutput",
                    Configuration = new Dictionary<string, object>()
                }
            },
            Connections = new List<FlowConnection>
            {
                new FlowConnection { ConnectionId = "conn1", SourceComponentId = "source", TargetComponentId = "map" },
                new FlowConnection { ConnectionId = "conn2", SourceComponentId = "map", TargetComponentId = "destination" }
            }
        };
    }
}
