using System.Diagnostics.CodeAnalysis;

public class VibrationProfile : BaseTelemetryProfile
{
    public required int SensorCount { get; init; }

    [SetsRequiredMembers]
    public VibrationProfile(string assetId, int samplingRateSeconds, int sensorCount)
    {
        AssetId = assetId;
        SamplingRate = TimeSpan.FromSeconds(samplingRateSeconds);
        SensorCount = sensorCount;
    }
    public VibrationProfile() { }
    public override string ToString() => $"Asset ID: {AssetId}; Sampling Rate: {SamplingRate}; Sensor Count: {SensorCount}";
}