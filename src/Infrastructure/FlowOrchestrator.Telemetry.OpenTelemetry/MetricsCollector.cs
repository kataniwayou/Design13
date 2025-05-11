using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using FlowOrchestrator.Common.Configuration;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Telemetry.OpenTelemetry
{
    /// <summary>
    /// Collector for metrics in the FlowOrchestrator system.
    /// </summary>
    public class MetricsCollector
    {
        private readonly ConfigurationParameters _configuration;
        private readonly ILogger<MetricsCollector> _logger;
        private readonly Meter _meter;
        private readonly Dictionary<string, Counter<long>> _counters = new Dictionary<string, Counter<long>>();
        private readonly Dictionary<string, Histogram<double>> _histograms = new Dictionary<string, Histogram<double>>();
        private readonly Dictionary<string, ObservableGauge<double>> _gauges = new Dictionary<string, ObservableGauge<double>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MetricsCollector"/> class.
        /// </summary>
        /// <param name="configuration">The configuration parameters.</param>
        /// <param name="logger">The logger.</param>
        public MetricsCollector(ConfigurationParameters configuration, ILogger<MetricsCollector> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
            var serviceName = _configuration.GetParameter<string>("ServiceName") ?? "FlowOrchestrator";
            var serviceVersion = _configuration.GetParameter<string>("ServiceVersion") ?? "1.0.0";
            
            _meter = new Meter(serviceName, serviceVersion);
            
            _logger.LogInformation("Metrics collector initialized for service {ServiceName} version {ServiceVersion}", 
                serviceName, serviceVersion);
        }

        /// <summary>
        /// Creates a counter with the specified name and description.
        /// </summary>
        /// <param name="name">The name of the counter.</param>
        /// <param name="description">The description of the counter.</param>
        /// <param name="unit">The unit of the counter.</param>
        /// <returns>The created counter.</returns>
        public Counter<long> CreateCounter(string name, string description, string unit = "")
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Counter name cannot be null or empty.", nameof(name));
            
            try
            {
                _logger.LogDebug("Creating counter {CounterName}", name);
                
                if (_counters.TryGetValue(name, out var existingCounter))
                {
                    _logger.LogDebug("Counter {CounterName} already exists", name);
                    return existingCounter;
                }
                
                var counter = _meter.CreateCounter<long>(name, unit, description);
                _counters[name] = counter;
                
                _logger.LogDebug("Counter {CounterName} created successfully", name);
                
                return counter;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating counter {CounterName}", name);
                throw;
            }
        }

        /// <summary>
        /// Creates a histogram with the specified name and description.
        /// </summary>
        /// <param name="name">The name of the histogram.</param>
        /// <param name="description">The description of the histogram.</param>
        /// <param name="unit">The unit of the histogram.</param>
        /// <returns>The created histogram.</returns>
        public Histogram<double> CreateHistogram(string name, string description, string unit = "")
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Histogram name cannot be null or empty.", nameof(name));
            
            try
            {
                _logger.LogDebug("Creating histogram {HistogramName}", name);
                
                if (_histograms.TryGetValue(name, out var existingHistogram))
                {
                    _logger.LogDebug("Histogram {HistogramName} already exists", name);
                    return existingHistogram;
                }
                
                var histogram = _meter.CreateHistogram<double>(name, unit, description);
                _histograms[name] = histogram;
                
                _logger.LogDebug("Histogram {HistogramName} created successfully", name);
                
                return histogram;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating histogram {HistogramName}", name);
                throw;
            }
        }

        /// <summary>
        /// Creates a gauge with the specified name and description.
        /// </summary>
        /// <param name="name">The name of the gauge.</param>
        /// <param name="description">The description of the gauge.</param>
        /// <param name="observeValue">The function to observe the gauge value.</param>
        /// <param name="unit">The unit of the gauge.</param>
        /// <returns>The created gauge.</returns>
        public ObservableGauge<double> CreateGauge(string name, string description, Func<double> observeValue, string unit = "")
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Gauge name cannot be null or empty.", nameof(name));
            
            if (observeValue == null)
                throw new ArgumentNullException(nameof(observeValue));
            
            try
            {
                _logger.LogDebug("Creating gauge {GaugeName}", name);
                
                if (_gauges.TryGetValue(name, out var existingGauge))
                {
                    _logger.LogDebug("Gauge {GaugeName} already exists", name);
                    return existingGauge;
                }
                
                var gauge = _meter.CreateObservableGauge(name, observeValue, unit, description);
                _gauges[name] = gauge;
                
                _logger.LogDebug("Gauge {GaugeName} created successfully", name);
                
                return gauge;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating gauge {GaugeName}", name);
                throw;
            }
        }

        /// <summary>
        /// Increments a counter by the specified amount.
        /// </summary>
        /// <param name="name">The name of the counter.</param>
        /// <param name="amount">The amount to increment by.</param>
        /// <param name="tags">Optional tags for the counter.</param>
        public void IncrementCounter(string name, long amount = 1, params KeyValuePair<string, object>[] tags)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Counter name cannot be null or empty.", nameof(name));
            
            try
            {
                _logger.LogDebug("Incrementing counter {CounterName} by {Amount}", name, amount);
                
                if (!_counters.TryGetValue(name, out var counter))
                {
                    _logger.LogWarning("Counter {CounterName} not found", name);
                    return;
                }
                
                counter.Add(amount, tags);
                
                _logger.LogDebug("Counter {CounterName} incremented successfully", name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error incrementing counter {CounterName}", name);
                throw;
            }
        }

        /// <summary>
        /// Records a value in a histogram.
        /// </summary>
        /// <param name="name">The name of the histogram.</param>
        /// <param name="value">The value to record.</param>
        /// <param name="tags">Optional tags for the histogram.</param>
        public void RecordHistogram(string name, double value, params KeyValuePair<string, object>[] tags)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Histogram name cannot be null or empty.", nameof(name));
            
            try
            {
                _logger.LogDebug("Recording value {Value} in histogram {HistogramName}", value, name);
                
                if (!_histograms.TryGetValue(name, out var histogram))
                {
                    _logger.LogWarning("Histogram {HistogramName} not found", name);
                    return;
                }
                
                histogram.Record(value, tags);
                
                _logger.LogDebug("Value {Value} recorded successfully in histogram {HistogramName}", value, name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recording value in histogram {HistogramName}", name);
                throw;
            }
        }

        /// <summary>
        /// Disposes the metrics collector.
        /// </summary>
        public void Dispose()
        {
            _meter?.Dispose();
        }
    }
}
