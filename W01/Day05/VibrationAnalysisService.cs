using Microsoft.Extensions.Logging;

file record InternalVibrationMetrics
{
    public required double Peak { get; init; }
    public required double Rms { get; init; }
}

public class VibrationAnalysisService(
    ILogger<VibrationAnalysisService> logger,
    IAssetContext context
)
{
    public async Task<Result<string, string>> Evaluate(double[] readings, string assetId)
    {
        if (readings.Length == 0)
        {
            return Result<string, string>.Failure("readings window cannot be empty.");
        }
        var metrics = new InternalVibrationMetrics
        {
            Peak = GetPeakValue(readings),
            Rms = GetRmsValue(readings)
        };
        if (metrics.Peak > 10.0 || metrics.Rms > 7.0)
        {
            await context.FlagForMaintenanceAsync(assetId);
            logger.LogWarning("A Maintenance Flag was triggered for asset with ID: {assetId}", assetId);
            return Result<string, string>.Failure("Maintenance required: Peak or RMS exceeded limits.");
        }
        return Result<string, string>.Success("Asset operating within normal vibration parameters.");
    }

    private double GetPeakValue(double[] readings)
    {
        double max = readings[0];
        foreach (var reading in readings)
        {
            if (reading > max)
            {
                max = reading;
            }
        }
        return max;
    }
    private double GetRmsValue(double[] readings)
    {
        double readingSquared;
        double sum = 0.0;
        foreach (var reading in readings)
        {
            readingSquared = reading * reading;
            sum += readingSquared;
        }
        return Math.Sqrt(sum / readings.Length);
    }
}