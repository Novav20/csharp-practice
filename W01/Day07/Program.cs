Console.WriteLine("Day 07's Challenge: Zero-Copy Sensor Buffer");

SensorBuffer buffer = new();

SensorReading reading = new(92.5, 4.1, DateTimeOffset.Now.ToUnixTimeMilliseconds());
buffer.AddReading(reading);
reading = new(88.9, 6.0, DateTimeOffset.Now.ToUnixTimeMilliseconds());
buffer.AddReading(reading);

var latestReading = buffer.GetLatestReading();

LogReading(latestReading);

SensorBuffer emptyBuffer = new();

try
{
    emptyBuffer.GetLatestReading();
}
catch (InvalidOperationException ex)
{
    
    Console.WriteLine($"ERROR: {ex.Message}");
}

static void LogReading(in SensorReading reading)
{
    Console.WriteLine($"Temp: {reading.Temperature}, Vib: {reading.Vibration}");
}