public class AssetConfigurationService
{
    private readonly Dictionary<string, PumpConfig> _cache = new()
    {
        ["PUMP-101"] = new PumpConfig("PUMP-101", 150.0)
    };
    public ValueTask<PumpConfig> GetConfigAsync(string assetId)
    {
        // 1. Synchronous path: Cache hit. ZERO allocation.
        if (_cache.TryGetValue(assetId, out var cachedConfig))
        {
            return new ValueTask<PumpConfig>(cachedConfig);
        }
        // 2. Asynchronous path: Cache miss. Wraps the async Task.
        return new ValueTask<PumpConfig>(SimulatedDatabaseLookupAsync(assetId));

    }
    private async Task<PumpConfig> SimulatedDatabaseLookupAsync(string assetId)
    {
        await Task.Delay(100);
        var config = new PumpConfig(assetId, 90.0);
        _cache[assetId] = config;
        return config;
    }
}