using System.Linq.Expressions;
using System.Threading.Channels;

Console.WriteLine("Day 20 Capstone Challenge: The Zero-Copy Query Engine");
Console.WriteLine("-----------------------------------------------------");

// 1. Setup the Channel
var channel = Channel.CreateUnbounded<VibrationSnapshot>();

// 2. Define and compile the dynamic filter ONCE
Expression<Func<VibrationSnapshot, bool>> expressionRule = s => s.PeakAmplitude > 5.0f;
var compiledRule = expressionRule.Compile();
Console.WriteLine($"[Config] Filter compiled: PeakAmplitude > 5.0");

// 3. Setup Cancellation (Auto-stop after 3 seconds)
using CancellationTokenSource cts = new();
cts.CancelAfter(TimeSpan.FromSeconds(3));

// 4. Instantiate Workers
IngestionWorker worker = new();
FilteringConsumer consumer = new();

Console.WriteLine("[Main] Starting pipeline... Will auto-stop in 3 seconds.\n");

try
{
    // 5. Run Producer and Consumer concurrently
    Task producerTask = worker.RunAsync(channel.Writer, cts.Token);
    Task consumerTask = consumer.RunAsync(channel.Reader, compiledRule, cts.Token);

    // Wait for both to complete (or cancel)
    await Task.WhenAll(producerTask, consumerTask);
}
catch (OperationCanceledException)
{
    Console.WriteLine("\n[Main] Pipeline gracefully shut down via CancellationToken.");
}