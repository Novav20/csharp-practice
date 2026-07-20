using System.Buffers;
using System.Runtime.InteropServices;
using System.Threading.Channels;

public class IngestionWorker
{
    public async Task RunAsync(ChannelWriter<VibrationSnapshot> writer, CancellationToken ct)
    {
        Console.WriteLine("[Producer] Ingestion worker started.");
        
        while (!ct.IsCancellationRequested)
        {
            // Rent a 32-byte buffer (enough for 8 floats)
            byte[] buffer = ArrayPool<byte>.Shared.Rent(32); 
            
            try
            {
                // 1. Mock the incoming MQTT payload by filling the buffer with random floats
                Span<byte> byteSpan = buffer.AsSpan(0, 32);
                Span<float> floatSpan = MemoryMarshal.Cast<byte, float>(byteSpan);
                
                for (int i = 0; i < floatSpan.Length; i++)
                {
                    // Simulate vibration amplitude between 0.0 and 10.0
                    floatSpan[i] = (float)(Random.Shared.NextDouble() * 10.0);
                }

                // 2. Zero-copy parse the peak amplitude
                float peak = GetPeakAmplitude(floatSpan);
                
                // 3. Push the domain object to the channel
                await writer.WriteAsync(new VibrationSnapshot("PUMP-101", peak), ct);
            }
            finally
            {
                // 4. Return buffer immediately to prevent memory leaks
                ArrayPool<byte>.Shared.Return(buffer, clearArray: false);
            }
            
            // Simulate network latency
            await Task.Delay(50, ct);
        }
        
        Console.WriteLine("[Producer] Ingestion worker stopped.");
    }

    private float GetPeakAmplitude(ReadOnlySpan<float> readings)
    {
        float max = 0.0f;
        foreach (var reading in readings)
        {
            if (reading > max) max = reading;
        }
        return max;
    }
}