# FlowOrchestrator.Common

This project contains common utilities and constants used throughout the FlowOrchestrator system.

## Constants

### FlowOrchestratorConstants

Contains constants used throughout the FlowOrchestrator system:

- `Execution`: Constants related to flow execution
- `Configuration`: Constants related to flow configuration
- `Validation`: Constants related to validation
- `Versioning`: Constants related to versioning

## Utilities

### JsonUtility

Utility class for working with JSON:

- `Serialize<T>`: Serializes an object to a JSON string
- `Deserialize<T>`: Deserializes a JSON string to an object
- `TryDeserialize<T>`: Tries to deserialize a JSON string to an object
- `IsValidJson`: Validates that a string is valid JSON

### IdGenerator

Utility class for generating IDs:

- `GenerateId()`: Generates a new GUID-based ID
- `GenerateId(string prefix)`: Generates a new GUID-based ID with a prefix
- `GenerateFlowId()`: Generates a new GUID-based ID for a flow
- `GenerateFlowStepId()`: Generates a new GUID-based ID for a flow step
- `GenerateFlowBranchId()`: Generates a new GUID-based ID for a flow branch
- `GenerateValidationRuleId()`: Generates a new GUID-based ID for a validation rule
- `GenerateCompensatingActionId()`: Generates a new GUID-based ID for a compensating action

### StringUtility

Utility class for working with strings:

- `ToCamelCase`: Converts a string to camel case
- `ToPascalCase`: Converts a string to pascal case
- `ToKebabCase`: Converts a string to kebab case
- `ToSnakeCase`: Converts a string to snake case
- `Truncate`: Truncates a string to the specified length

### VersionInfo

Represents version information for a component or entity:

- `Major`: Gets the major version number
- `Minor`: Gets the minor version number
- `Patch`: Gets the patch version number
- `PreRelease`: Gets the pre-release version string
- `BuildMetadata`: Gets the build metadata string
- `Parse`: Parses a version string into a VersionInfo object
- `TryParse`: Tries to parse a version string into a VersionInfo object
- `CompareTo`: Compares this version to another version

### VersionRange

Represents a range of versions:

- `MinVersion`: Gets the minimum version in the range
- `MaxVersion`: Gets the maximum version in the range
- `MinInclusive`: Gets whether the minimum version is inclusive
- `MaxInclusive`: Gets whether the maximum version is inclusive
- `Contains`: Checks if a version is within this range
- `Overlaps`: Checks if this range overlaps with another range

### CompatibilityMatrix

Represents a compatibility matrix for components or entities:

- `Entries`: Gets the compatibility entries for all components
- `AddEntry`: Adds a compatibility entry to the matrix
- `IsCompatible`: Checks if a component version is compatible with another component version
- `GetCompatibleDependencyVersions`: Gets the compatible dependency versions for a component version
- `GetCompatibleComponentVersions`: Gets the compatible component versions for a dependency version

### CompatibilityEntry

Represents a compatibility entry in a compatibility matrix:

- `DependencyId`: Gets the dependency component ID
- `MinVersion`: Gets the minimum component version
- `MaxVersion`: Gets the maximum component version
- `MinDependencyVersion`: Gets the minimum dependency version
- `MaxDependencyVersion`: Gets the maximum dependency version
- `Notes`: Gets or sets the compatibility notes

### ErrorClassification

Represents a classification of errors in the system:

- `ErrorCode`: Gets the error code
- `Category`: Gets the error category
- `Severity`: Gets the error severity
- `IsRetryable`: Gets whether the error is retryable
- `MessageTemplate`: Gets the error message template
- `RecommendedAction`: Gets the recommended action to take when this error occurs
- `DocumentationUrl`: Gets the documentation URL for this error
- `FormatMessage`: Formats the error message template with the specified parameters

### ConfigurationParameters

Represents a collection of configuration parameters:

- `Count`: Gets the number of parameters in the collection
- `Keys`: Gets the parameter keys in the collection
- `Values`: Gets the parameter values in the collection
- `GetParameter`: Gets the parameter with the specified key
- `GetParameter<T>`: Gets the parameter with the specified key as the specified type
- `TryGetParameter`: Tries to get the parameter with the specified key
- `TryGetParameter<T>`: Tries to get the parameter with the specified key as the specified type
- `SetParameter`: Sets the parameter with the specified key to the specified value
- `RemoveParameter`: Removes the parameter with the specified key
- `ContainsParameter`: Determines whether the collection contains a parameter with the specified key
- `Clear`: Clears all parameters from the collection
- `ToDictionary`: Returns a dictionary containing all parameters in the collection

### MemoryAddressing

Provides utilities for memory addressing in the system:

- `GenerateMemoryAddress`: Generates a new memory address
- `GenerateMemoryAddress(string prefix)`: Generates a new memory address with the specified prefix
- `GenerateFlowExecutionMemoryAddress`: Generates a new memory address for the specified flow execution
- `GenerateBranchExecutionMemoryAddress`: Generates a new memory address for the specified branch execution
- `GenerateStepExecutionMemoryAddress`: Generates a new memory address for the specified step execution
- `GenerateTemporaryMemoryAddress`: Generates a new temporary memory address
- `GenerateCacheMemoryAddress`: Generates a new cache memory address
- `ParseMemoryAddress`: Parses a memory address into its components
- `IsValidMemoryAddress`: Validates a memory address

### SecurityUtilities

Provides security utilities for the system:

- `GenerateRandomString`: Generates a random string of the specified length
- `GenerateSecureRandomString`: Generates a cryptographically secure random string of the specified length
- `ComputeSha256Hash`: Computes the SHA-256 hash of the specified string
- `ComputeHmacSha256Hash`: Computes the HMAC-SHA-256 hash of the specified string using the specified key
- `EncryptAes`: Encrypts the specified string using AES encryption with the specified key and initialization vector
- `DecryptAes`: Decrypts the specified string using AES encryption with the specified key and initialization vector
- `GenerateEncryptionKey`: Generates a random encryption key
- `GenerateInitializationVector`: Generates a random initialization vector

### LoggingUtilities

Provides logging utilities for the system:

- `FormatLogMessage`: Formats a log message with the specified parameters
- `CreateStructuredLogEntry`: Creates a structured log entry with the specified properties
- `CreateLogContext`: Creates a log context with the specified properties
- `AddToLogContext`: Adds a property to a log context
- `RemoveFromLogContext`: Removes a property from a log context
- `CreateLogScope`: Creates a log scope with the specified properties
- `FormatException`: Formats an exception for logging
- `CreateFlowExecutionLogEntry`: Creates a log entry for a flow execution
- `CreateBranchExecutionLogEntry`: Creates a log entry for a branch execution
- `CreateStepExecutionLogEntry`: Creates a log entry for a step execution

## Exceptions

### FlowOrchestratorException

Base exception class for all FlowOrchestrator exceptions:

- `ErrorCode`: Gets the error code for this exception

### FlowValidationException

Exception thrown when a flow validation error occurs:

- `FlowId`: Gets the flow ID that caused the validation error

### FlowExecutionException

Exception thrown when a flow execution error occurs:

- `FlowId`: Gets the flow ID that caused the execution error
- `StepId`: Gets the step ID that caused the execution error
- `BranchPath`: Gets the branch path that caused the execution error
