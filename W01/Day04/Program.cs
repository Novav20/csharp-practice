Console.WriteLine("Day 04 — Collection Expressions & List Patterns");

PipelineMonitor pipelineMonitor = new();

double[] calibration = [0.0, 0.0, 0.0];
double[] spikeDangerSequence = [0.2, 0.1, 11.0, 167.2];
double[] sustainedOverpressureSequence = [101.0, 9.0, 88.8, 110.2];
double[] severeDepressurizationSequence = [100.0, 50.0, 0.96, 0.83];
double[] normalSequence = [10.0, 20.0, 30.0];
double[] emptySequence = [];
double[] wakeUpSequence = [999.9, 101.0, 9.0, 88.8, 110.2];

ReviewWindow(calibration);
ReviewWindow(spikeDangerSequence);
ReviewWindow(sustainedOverpressureSequence);
ReviewWindow(severeDepressurizationSequence);
ReviewWindow(normalSequence);
ReviewWindow(emptySequence);
ReviewWindow(wakeUpSequence);


void ReviewWindow(ReadOnlySpan<double> window)
{
    var result = pipelineMonitor.AnalyzeWindow(window);

    if (result is { IsSuccess: true, Value: var status })
    {
        Console.WriteLine($"window: [{string.Join(", ", window.ToArray())}] has status {status}");
    }
    else if (result is { IsFailure: true })
    {
        Console.WriteLine($"ERROR: {result.Error}");
    }
}