using System.Threading.Channels;

public static class ChannelFactory
{
    public static Channel<TelemetryPayload> CreateIngestionChannel(int capacity = 10_000)
    {
        var options = new BoundedChannelOptions(capacity: capacity)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true,
            SingleWriter = false
        };
        var channel = Channel.CreateBounded<TelemetryPayload>(options);
        return channel;
    }
}
