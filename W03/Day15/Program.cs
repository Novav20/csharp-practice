using System.Runtime.InteropServices;
using System.Diagnostics;

Console.WriteLine("Day 15's Challenge: Zero-Copy Waveform Processor & LINQ Benchmark");
float[] waveform = [3.2f, -7.1f, 2.5f, 5.8f, -1.3f];
ReadOnlyMemory<byte> waveformMemory = MemoryMarshal.AsBytes<float>(waveform.AsSpan()).ToArray();

var snapshot = await WaveformProcessor.ProcessWaveformAsync(waveformMemory, "VT-101");
Console.WriteLine($"Vibration Snapshot for Asset {snapshot.AssetId}\n Peak Amplitude: {snapshot.PeakAmplitude}; RMS Value: {snapshot.RmsValue}");


// Test 2: LINQ Benchmark
// 1. Materialize the data ONCE before the stopwatch
double[] benchmarkData = [.. TemperatureGenerator()];

var stopwatch = Stopwatch.StartNew();
TelemetryAggregator.AverageWithLinq(benchmarkData);
stopwatch.Stop();
Console.WriteLine($"Average with LINQ\n -- Elapsed: {stopwatch.ElapsedMilliseconds} ms");

stopwatch.Restart(); // Use Restart instead of Start/Stop
TelemetryAggregator.AverageWithLoop(benchmarkData);
stopwatch.Stop();
Console.WriteLine($"Average with Loop\n -- Elapsed: {stopwatch.ElapsedMilliseconds} ms");


static IEnumerable<double> TemperatureGenerator(int count = 1_000_000)
{
    for (int i = 0; i < count; i++)
    {
        yield return 90.0 + 20.0 * Random.Shared.NextDouble();
    }
}