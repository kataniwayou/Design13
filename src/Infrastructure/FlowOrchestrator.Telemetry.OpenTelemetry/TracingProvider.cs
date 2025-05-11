using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Telemetry.OpenTelemetry
{
    /// <summary>
    /// Provider for tracing in the FlowOrchestrator system.
    /// </summary>
    public class TracingProvider
    {
        private readonly OpenTelemetryProvider _telemetryProvider;
        private readonly ILogger<TracingProvider> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TracingProvider"/> class.
        /// </summary>
        /// <param name="telemetryProvider">The OpenTelemetry provider.</param>
        /// <param name="logger">The logger.</param>
        public TracingProvider(OpenTelemetryProvider telemetryProvider, ILogger<TracingProvider> logger)
        {
            _telemetryProvider = telemetryProvider ?? throw new ArgumentNullException(nameof(telemetryProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Starts a new trace.
        /// </summary>
        /// <param name="name">The name of the trace.</param>
        /// <returns>The created activity.</returns>
        public Activity? StartTrace(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Trace name cannot be null or empty.", nameof(name));
            
            try
            {
                _logger.LogDebug("Starting trace {TraceName}", name);
                
                var activity = _telemetryProvider.StartActivity(name, ActivityKind.Internal);
                
                if (activity != null)
                {
                    _logger.LogDebug("Trace {TraceName} started with ID {TraceId}", name, activity.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to start trace {TraceName}", name);
                }
                
                return activity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting trace {TraceName}", name);
                throw;
            }
        }

        /// <summary>
        /// Starts a new client trace.
        /// </summary>
        /// <param name="name">The name of the trace.</param>
        /// <returns>The created activity.</returns>
        public Activity? StartClientTrace(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Trace name cannot be null or empty.", nameof(name));
            
            try
            {
                _logger.LogDebug("Starting client trace {TraceName}", name);
                
                var activity = _telemetryProvider.StartActivity(name, ActivityKind.Client);
                
                if (activity != null)
                {
                    _logger.LogDebug("Client trace {TraceName} started with ID {TraceId}", name, activity.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to start client trace {TraceName}", name);
                }
                
                return activity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting client trace {TraceName}", name);
                throw;
            }
        }

        /// <summary>
        /// Starts a new server trace.
        /// </summary>
        /// <param name="name">The name of the trace.</param>
        /// <returns>The created activity.</returns>
        public Activity? StartServerTrace(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Trace name cannot be null or empty.", nameof(name));
            
            try
            {
                _logger.LogDebug("Starting server trace {TraceName}", name);
                
                var activity = _telemetryProvider.StartActivity(name, ActivityKind.Server);
                
                if (activity != null)
                {
                    _logger.LogDebug("Server trace {TraceName} started with ID {TraceId}", name, activity.Id);
                }
                else
                {
                    _logger.LogWarning("Failed to start server trace {TraceName}", name);
                }
                
                return activity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error starting server trace {TraceName}", name);
                throw;
            }
        }

        /// <summary>
        /// Adds an event to the current activity.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="tags">Optional tags for the event.</param>
        public void AddEvent(string name, Dictionary<string, object> tags = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Event name cannot be null or empty.", nameof(name));
            
            try
            {
                _logger.LogDebug("Adding event {EventName} to current activity", name);
                
                var activity = Activity.Current;
                if (activity == null)
                {
                    _logger.LogWarning("No current activity to add event {EventName}", name);
                    return;
                }
                
                if (tags != null)
                {
                    var tagList = new ActivityTagsCollection();
                    foreach (var tag in tags)
                    {
                        tagList.Add(tag.Key, tag.Value);
                    }
                    
                    activity.AddEvent(new ActivityEvent(name, tags: tagList));
                }
                else
                {
                    activity.AddEvent(new ActivityEvent(name));
                }
                
                _logger.LogDebug("Event {EventName} added successfully to activity {ActivityId}", name, activity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding event {EventName} to current activity", name);
                throw;
            }
        }

        /// <summary>
        /// Adds a tag to the current activity.
        /// </summary>
        /// <param name="key">The key of the tag.</param>
        /// <param name="value">The value of the tag.</param>
        public void AddTag(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Tag key cannot be null or empty.", nameof(key));
            
            try
            {
                _logger.LogDebug("Adding tag {TagKey} to current activity", key);
                
                var activity = Activity.Current;
                if (activity == null)
                {
                    _logger.LogWarning("No current activity to add tag {TagKey}", key);
                    return;
                }
                
                activity.SetTag(key, value);
                
                _logger.LogDebug("Tag {TagKey} added successfully to activity {ActivityId}", key, activity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding tag {TagKey} to current activity", key);
                throw;
            }
        }

        /// <summary>
        /// Sets the status of the current activity.
        /// </summary>
        /// <param name="status">The status to set.</param>
        /// <param name="description">The description of the status.</param>
        public void SetStatus(ActivityStatusCode status, string description = null)
        {
            try
            {
                _logger.LogDebug("Setting status {Status} on current activity", status);
                
                var activity = Activity.Current;
                if (activity == null)
                {
                    _logger.LogWarning("No current activity to set status {Status}", status);
                    return;
                }
                
                activity.SetStatus(status, description);
                
                _logger.LogDebug("Status {Status} set successfully on activity {ActivityId}", status, activity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting status {Status} on current activity", status);
                throw;
            }
        }
    }
}
