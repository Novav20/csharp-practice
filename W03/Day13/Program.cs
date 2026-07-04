Console.WriteLine("Day 13's Challenge: Hot-Path Cache & Live Telemetry Streaming");

AssetConfigurationService configurationService = new();

// Await to extract the PumpConfig from the ValueTask
var config1 = await configurationService.GetConfigAsync("PUMP-101");
Console.WriteLine($"Cache Hit: {config1.AssetId} - {config1.MaxPressure}");

var config2 = await configurationService.GetConfigAsync("PUMP-999");
Console.WriteLine($"Cache Miss: {config2.AssetId} - {config2.MaxPressure}");

SimulatedMqttBroker broker = new();
using CancellationTokenSource cts = new();
cts.CancelAfter(TimeSpan.FromSeconds(2));

try
{
    await foreach (var payload in broker.StreamTelemetryAsync(cts.Token))
    {
        Console.WriteLine($"Telemetry: {payload.AssetId} - {payload.Temperature}°C");
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("Telemetry stream gracefully shut down.");
}