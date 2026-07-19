using System.Buffers;
using System.Diagnostics;

Console.WriteLine("Day 18's Challenge: High-Throughput Buffer Renting Pipeline");

var stopwatch = Stopwatch.StartNew();
for (int i = 0; i < 10000; i++)
{
    ProcessMessageBad();
}
stopwatch.Stop();
Console.WriteLine($"Heap Allocation Approach: {stopwatch.ElapsedMilliseconds} ms");
stopwatch.Restart();
for (int i = 0; i < 10000; i++)
{
    ProcessMessagePooled();
}
stopwatch.Stop();
Console.WriteLine($"ArrayPool Approach: {stopwatch.ElapsedMilliseconds} ms");
stopwatch.Restart();
for (int i = 0; i < 10000; i++)
{
    await ProcessMessageMemoryPoolAsync();
}
stopwatch.Stop();
Console.WriteLine($"MemoryPool Approach: {stopwatch.ElapsedMilliseconds} ms");
static MqttMessage ProcessMessageBad()
{
    byte[] buffer = new byte[4096]; // Simulate receiving a network packet
    Thread.Sleep(1); // Simulate processing
    return new MqttMessage("pumps/telemetry", 4096);
}

static MqttMessage ProcessMessagePooled()
{
    byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);
    try
    {
        Thread.Sleep(1);
    }
    finally
    {
        ArrayPool<byte>.Shared.Return(buffer, clearArray: false);
    }
    return new MqttMessage("pumps/telemetry", 4096);
}

static async Task<MqttMessage> ProcessMessageMemoryPoolAsync()
{
    using IMemoryOwner<byte> owner = MemoryPool<byte>.Shared.Rent(4096);
    Memory<byte> memory = owner.Memory;
    await Task.Delay(10);
    return new MqttMessage("pumps/telemetry", 4096);
}