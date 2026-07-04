public abstract class BaseTelemetryProfile
{
    public required string AssetId {get; init;}
    public required TimeSpan SamplingRate {get; init;}
    // public required string LocationCode {get; init;}
}