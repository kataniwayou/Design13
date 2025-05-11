using System.Text;
using System.Text.RegularExpressions;

namespace FlowOrchestrator.Common.Utilities;

/// <summary>
/// Utility class for working with strings.
/// </summary>
public static class StringUtility
{
    /// <summary>
    /// Converts a string to camel case.
    /// </summary>
    /// <param name="input">The string to convert.</param>
    /// <returns>The camel case version of the string.</returns>
    public static string ToCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }
        
        return char.ToLowerInvariant(input[0]) + input[1..];
    }
    
    /// <summary>
    /// Converts a string to pascal case.
    /// </summary>
    /// <param name="input">The string to convert.</param>
    /// <returns>The pascal case version of the string.</returns>
    public static string ToPascalCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }
        
        return char.ToUpperInvariant(input[0]) + input[1..];
    }
    
    /// <summary>
    /// Converts a string to kebab case.
    /// </summary>
    /// <param name="input">The string to convert.</param>
    /// <returns>The kebab case version of the string.</returns>
    public static string ToKebabCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }
        
        // Replace any non-alphanumeric characters with a hyphen
        var result = Regex.Replace(input, @"[^a-zA-Z0-9]", "-");
        
        // Replace any uppercase letters with a hyphen followed by the lowercase letter
        result = Regex.Replace(result, @"([a-z])([A-Z])", "$1-$2");
        
        // Replace any consecutive hyphens with a single hyphen
        result = Regex.Replace(result, @"-+", "-");
        
        // Remove any leading or trailing hyphens
        result = result.Trim('-');
        
        // Convert to lowercase
        return result.ToLowerInvariant();
    }
    
    /// <summary>
    /// Converts a string to snake case.
    /// </summary>
    /// <param name="input">The string to convert.</param>
    /// <returns>The snake case version of the string.</returns>
    public static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }
        
        // Replace any non-alphanumeric characters with an underscore
        var result = Regex.Replace(input, @"[^a-zA-Z0-9]", "_");
        
        // Replace any uppercase letters with an underscore followed by the lowercase letter
        result = Regex.Replace(result, @"([a-z])([A-Z])", "$1_$2");
        
        // Replace any consecutive underscores with a single underscore
        result = Regex.Replace(result, @"_+", "_");
        
        // Remove any leading or trailing underscores
        result = result.Trim('_');
        
        // Convert to lowercase
        return result.ToLowerInvariant();
    }
    
    /// <summary>
    /// Truncates a string to the specified length.
    /// </summary>
    /// <param name="input">The string to truncate.</param>
    /// <param name="maxLength">The maximum length of the string.</param>
    /// <param name="suffix">The suffix to append to the truncated string.</param>
    /// <returns>The truncated string.</returns>
    public static string Truncate(string input, int maxLength, string suffix = "...")
    {
        if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
        {
            return input;
        }
        
        return input[..(maxLength - suffix.Length)] + suffix;
    }
}
