public class SensorBuffer(int capacity = 10)
{
    private readonly SensorReading[] _buffer = new SensorReading[capacity];
    private int _count = 0;

    public void AddReading(in SensorReading reading)
    {
        if (_count >= _buffer.Length)
        {
            throw new InvalidOperationException("Sensor buffer has reached maximum capacity.");
        }
        _buffer[_count++] = reading;
    }
    public ref readonly SensorReading GetLatestReading()
    {
        if (_count == 0)
        {
            throw new InvalidOperationException("Cannot retrieve the latest reading from an empty sensor buffer.");
        }
        return ref _buffer[_count - 1];
    }
}