public class PipelineMonitor
{
    public Result<PipelineStatus, string> AnalyzeWindow(ReadOnlySpan<double> window)
    {
        if (window.IsEmpty)
        {
            return Result<PipelineStatus, string>.Failure("Telemetry window cannot be empty.");
        }
        if (window[0] == 999.9)
        {
            window = window[1..];
            if (window.IsEmpty)
            {
                return Result<PipelineStatus, string>.Failure("Telemetry window cannot be empty.");
            }
        }
        var status = window switch
        {
            [0.0, 0.0, 0.0] => PipelineStatus.Calibrating,
            [.., < 20.0, > 150.0] => PipelineStatus.SpikeDanger,
            [> 100.0, .., > 100.0] => PipelineStatus.SustainedOverpressure,
            [var initial, .., var current] when current <= 0.1 * initial => PipelineStatus.SevereDepressurization,
            _ => PipelineStatus.Normal
        };

        return Result<PipelineStatus, string>.Success(status);
    }
}