Console.WriteLine("Day 14's Challenge: Edge Gateway Ingestion Pipeline");

var channel = ChannelFactory.CreateIngestionChannel();
using var cts = new CancellationTokenSource();
cts.CancelAfter(TimeSpan.FromSeconds(5));

DatabasePersistenceConsumer consumer = new();
MqttTelemetryProducer producer = new();
try
{
    var consumerTask = consumer.ConsumeAsync(channel.Reader, cts.Token);
    var producerTask = producer.ProduceAsync(channel.Writer, 100, cts.Token);
    await Task.WhenAll(consumerTask, producerTask);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Telemetry stream gracefully shut down.");
}