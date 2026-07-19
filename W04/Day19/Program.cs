Console.WriteLine("Day 19's Challenge: Custom Telemetry Stream Operators");
var batches = GenerateReadings().WhereActive().Batch(3);
int batchNumber = 1;
foreach (var batch in batches)
{
    Console.WriteLine($"Bath {batchNumber}:");
    foreach (var reading in batch)
    {
        Console.WriteLine(reading);
    }
    batchNumber++;
}



static IEnumerable<TelemetryReading> GenerateReadings()
{
    // 10 lecturas con valores positivos
    for (int i = 0; i < 10; i++)
    {
        var assetId = "PUMP-" + Random.Shared.NextInt64(100, 104);
        var vibration = 1.0 + 4.0 * Random.Shared.NextDouble(); // > 1.0
        var timestamp = DateTime.UtcNow;
        yield return new TelemetryReading(assetId, Math.Round(vibration,2), timestamp);
    }

    // 2 lecturas con valor <= 0 para probar el filtro
    for (int i = 0; i < 2; i++)
    {
        var assetId = "PUMP-" + Random.Shared.NextInt64(100, 104);
        var vibration = -1.0 * Random.Shared.NextDouble(); // entre 0 y -1
        var timestamp = DateTime.UtcNow;
        yield return new TelemetryReading(assetId, Math.Round(vibration,2), timestamp);
    }
}