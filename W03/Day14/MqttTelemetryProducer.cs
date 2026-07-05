using System.Threading.Channels;

public class MqttTelemetryProducer
{
    public async Task ProduceAsync(ChannelWriter<TelemetryPayload> writer, int messageCount, CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(Random.Shared.Next(1, 10), ct);
            var assetId = "PUMP-" + Random.Shared.Next(100, 104);
            var temperature = 90.0 + 20.0 * Random.Shared.NextDouble();
            var vibration = 4.0 * (1 + Random.Shared.NextDouble());
            long timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            await writer.WriteAsync(new TelemetryPayload(assetId, temperature, vibration, timestamp), ct);
            Console.WriteLine($"Produced: {assetId} - Temp: {temperature} °C");
        }
    }
}