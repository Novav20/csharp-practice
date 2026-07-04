using System.Runtime.CompilerServices;

public class SimulatedMqttBroker
{
    public async IAsyncEnumerable<TelemetryPayload> StreamTelemetryAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(500, cancellationToken);
            var assetId = "TT-" + Random.Shared.NextInt64(100,104);
            var temperature = 90.0 + (Random.Shared.NextDouble() * 20.0); // Entre 90 y 110
            yield return new TelemetryPayload(assetId, Math.Round(temperature, 2));
        }
    }
}