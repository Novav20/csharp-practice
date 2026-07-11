public static class TelemetryAggregator
{
    public static double AverageWithLinq(double[] readings) => readings.Average();
    public static double AverageWithLoop(double[] readings)
    {
        int count = 0;
        double sum = 0.0;
        foreach (var reading in readings)
        {
            sum += reading;
            count++;
        }
        return count > 0 ? sum / count : 0.0;
    }

}