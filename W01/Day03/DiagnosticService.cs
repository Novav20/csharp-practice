public class DiagnosticService
{
    public Result<AssetHealthStatus, string> EvaluateTelemetry(TelemetryPayload payload)
    {
        if (payload is { Temperature: < -273.15 })
        {
            return Result<AssetHealthStatus, string>.Failure("Temperature is below absolute zero.");
        }
        else if (payload is { Vibration: < 0.0 })
        {
            return Result<AssetHealthStatus, string>.Failure("Vibration cannot be negative.");
        }
        var status = (payload.Temperature, payload.Vibration, payload.StateCode) switch
        {
            (_, _, "ERR" or "FAIL") => AssetHealthStatus.Danger,
            ( > 120, _, _) => AssetHealthStatus.Danger,
            (_, > 10.0, _) => AssetHealthStatus.Danger,
            ( > 100, > 8.0, _) => AssetHealthStatus.Critical,
            ( > 100, _, "WARN") => AssetHealthStatus.Critical,
            (_, > 8.0, "WARN") => AssetHealthStatus.Critical,
            ( >= 85 and <= 100, _, _) => AssetHealthStatus.Warning,
            (_, >= 6.0 and <= 8.0, _) => AssetHealthStatus.Warning,
            ( < 0, _, _) => AssetHealthStatus.Warning,
            _ => AssetHealthStatus.Optimal,

        };
        return Result<AssetHealthStatus, string>.Success(status);
    }
}