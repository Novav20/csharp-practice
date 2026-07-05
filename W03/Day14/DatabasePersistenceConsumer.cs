using System.Threading.Channels;

public class DatabasePersistenceConsumer
{
    public async Task ConsumeAsync(ChannelReader<TelemetryPayload> reader, CancellationToken ct)
    {
        await foreach (var payload in reader.ReadAllAsync(ct))
        {
            await Task.Delay(50, ct);
            Console.WriteLine($"Persisted: {payload.AssetId} - Temp: {payload.Temperature} °C");
        }
    }
}