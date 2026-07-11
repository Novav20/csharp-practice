using System.Runtime.InteropServices;

public static class WaveformProcessor
{
    public static async Task<VibrationSnapshot> ProcessWaveformAsync(ReadOnlyMemory<byte> rawPayload, string assetId)
    {
        await Task.Delay(1);
        ReadOnlySpan<float> waveformSpan = MemoryMarshal.Cast<byte, float>(rawPayload.Span);
        return new VibrationSnapshot(assetId, ComputePeakValue(waveformSpan), ComputeRms(waveformSpan));
    }
    private static float ComputePeakValue(ReadOnlySpan<float> waveform)
    {
        var peak = Math.Abs(waveform[0]);
        foreach (var point in waveform)
        {
            peak = Math.Abs(point) > Math.Abs(peak) ? Math.Abs(point) : Math.Abs(peak);
        }
        return peak;
    }
    private static float ComputeRms(ReadOnlySpan<float> waveform)
    {
        double squaredSum = 0.0;
        int count = 0;
        foreach (var point in waveform)
        {
            squaredSum += Math.Pow(point, 2);
            count++;
        }
        return (float)Math.Sqrt(squaredSum / count);
    }
}