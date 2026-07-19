public static class TelemetryExtensions
{
    public static IEnumerable<TelemetryReading> WhereActive(this IEnumerable<TelemetryReading> source)
    {
        foreach (var item in source)
        {
            if (item.Value > 0.0)
            {
                yield return item;
            }
        }
    }
    public static IEnumerable<IList<TelemetryReading>> Batch<TelemetryReading>(this IEnumerable<TelemetryReading> source, int batchSize)
    {
        var batch = new List<TelemetryReading>(batchSize);
        foreach (var item in source)
        {
            batch.Add(item);
            if (batch.Count == batchSize)
            {
                yield return batch;
                batch = new List<TelemetryReading>(batchSize);
            }
        }
        if (batch.Count > 0) yield return batch;
    }
}