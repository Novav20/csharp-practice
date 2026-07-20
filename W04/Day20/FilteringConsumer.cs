using System.Threading.Channels;

public class FilteringConsumer
{
    public async Task RunAsync(ChannelReader<VibrationSnapshot> reader, Func<VibrationSnapshot,bool> filter, CancellationToken ct)
    {
        await foreach (var snapshot in reader.ReadAllAsync(ct).Where(filter))
        {
            Console.WriteLine($"Persisted: {snapshot.AssetId} | Peak: {snapshot.PeakAmplitude}");
        }
    }
}