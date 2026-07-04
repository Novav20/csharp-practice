public class TelemetryBatch<T>(int capacity = 10) where T : struct, ISensorData
{
    private readonly T[] _buffer = new T[capacity];
    private int _count = 0;

    public void Add(T data)
    {
        if (_count >= _buffer.Length) 
            throw new InvalidOperationException("Telemetry batch is at maximum capacity.");
            
        // 2. Use index assignment, not LINQ Append
        _buffer[_count++] = data; 
    }

    public double GetAverageValue()
    {
        if (_count == 0) return 0.0;
        
        double sum = 0.0;
        // 3. Iterate only up to _count, not the whole array length
        for (int i = 0; i < _count; i++)
        {
            sum += _buffer[i].Value;
        }
        return sum / _count;
    }
}