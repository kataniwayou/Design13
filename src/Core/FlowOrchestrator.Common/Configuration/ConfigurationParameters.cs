namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents a collection of configuration parameters.
/// </summary>
public class ConfigurationParameters
{
    private readonly Dictionary<string, object> _parameters = new();

    /// <summary>
    /// Gets the number of parameters in the collection.
    /// </summary>
    public int Count => _parameters.Count;

    /// <summary>
    /// Gets the parameter keys in the collection.
    /// </summary>
    public IEnumerable<string> Keys => _parameters.Keys;

    /// <summary>
    /// Gets the parameter values in the collection.
    /// </summary>
    public IEnumerable<object> Values => _parameters.Values;

    /// <summary>
    /// Gets or sets the parameter with the specified key.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <returns>The parameter value.</returns>
    public object this[string key]
    {
        get => GetParameter(key);
        set => SetParameter(key, value);
    }

    /// <summary>
    /// Creates a new instance of the ConfigurationParameters class.
    /// </summary>
    public ConfigurationParameters()
    {
    }

    /// <summary>
    /// Creates a new instance of the ConfigurationParameters class with the specified parameters.
    /// </summary>
    /// <param name="parameters">The parameters to initialize the collection with.</param>
    public ConfigurationParameters(IDictionary<string, object> parameters)
    {
        if (parameters == null)
        {
            throw new ArgumentNullException(nameof(parameters));
        }

        foreach (var parameter in parameters)
        {
            _parameters[parameter.Key] = parameter.Value;
        }
    }

    /// <summary>
    /// Gets the parameter with the specified key.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <returns>The parameter value.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the key is not found in the collection.</exception>
    public object GetParameter(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        if (!_parameters.TryGetValue(key, out var value))
        {
            throw new KeyNotFoundException($"Parameter with key '{key}' not found.");
        }

        return value;
    }

    /// <summary>
    /// Gets the parameter with the specified key as the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert the parameter value to.</typeparam>
    /// <param name="key">The parameter key.</param>
    /// <returns>The parameter value converted to the specified type.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the key is not found in the collection.</exception>
    /// <exception cref="InvalidCastException">Thrown when the parameter value cannot be converted to the specified type.</exception>
    public T GetParameter<T>(string key)
    {
        var value = GetParameter(key);

        if (value is T typedValue)
        {
            return typedValue;
        }

        try
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
        catch (Exception ex)
        {
            throw new InvalidCastException($"Cannot convert parameter '{key}' to type '{typeof(T).Name}'.", ex);
        }
    }

    /// <summary>
    /// Tries to get the parameter with the specified key.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">When this method returns, contains the parameter value if the key is found; otherwise, the default value for the type of the value parameter.</param>
    /// <returns>true if the parameter is found; otherwise, false.</returns>
    public bool TryGetParameter(string key, out object? value)
    {
        if (string.IsNullOrEmpty(key))
        {
            value = null;
            return false;
        }

        return _parameters.TryGetValue(key, out value);
    }

    /// <summary>
    /// Tries to get the parameter with the specified key as the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert the parameter value to.</typeparam>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">When this method returns, contains the parameter value converted to the specified type if the key is found and the conversion succeeds; otherwise, the default value for the type of the value parameter.</param>
    /// <returns>true if the parameter is found and the conversion succeeds; otherwise, false.</returns>
    public bool TryGetParameter<T>(string key, out T? value)
    {
        value = default;

        if (!TryGetParameter(key, out var objValue))
        {
            return false;
        }

        if (objValue is T typedValue)
        {
            value = typedValue;
            return true;
        }

        try
        {
            if (objValue != null)
            {
                value = (T)Convert.ChangeType(objValue, typeof(T));
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Sets the parameter with the specified key to the specified value.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">The parameter value.</param>
    public void SetParameter(string key, object value)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }

        _parameters[key] = value;
    }

    /// <summary>
    /// Removes the parameter with the specified key.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <returns>true if the parameter is successfully removed; otherwise, false.</returns>
    public bool RemoveParameter(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        return _parameters.Remove(key);
    }

    /// <summary>
    /// Determines whether the collection contains a parameter with the specified key.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <returns>true if the collection contains a parameter with the specified key; otherwise, false.</returns>
    public bool ContainsParameter(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            return false;
        }

        return _parameters.ContainsKey(key);
    }

    /// <summary>
    /// Clears all parameters from the collection.
    /// </summary>
    public void Clear()
    {
        _parameters.Clear();
    }

    /// <summary>
    /// Returns a dictionary containing all parameters in the collection.
    /// </summary>
    /// <returns>A dictionary containing all parameters in the collection.</returns>
    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>(_parameters);
    }
}
