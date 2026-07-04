Console.WriteLine("Day 12's Challenge: Dynamic Alert Rule Engine");

Func<SensorReading, bool> isOverheating = reading =>
{
    Console.WriteLine($"[EVAL] Checking temperature for {reading.Timestamp}");
    return reading.Temperature > 95.0;
};

Func<SensorReading, bool> isShaking = reading =>
{
    Console.WriteLine($"[EVAL] Checking vibration for {reading.Timestamp}");
    return reading.Vibration > 7.0;
};

Func<SensorReading, bool> criticalFailure = isOverheating.Or(isShaking);

SensorReading[] readings = [
    new SensorReading(101.2, 6.4, 730540000),
    new SensorReading(86.7, 9.1, 730550000),
    new SensorReading(96.7, 9.2, 730560000),
    new SensorReading(80.3, 6.6, 730570000)
];

PrintCriticalReadings(readings);

void PrintCriticalReadings(SensorReading[] readings)
{
    foreach (var reading in readings.Where(criticalFailure))
    {
        Console.WriteLine($"Reading {reading.Timestamp}. Temp: {reading.Temperature}; Vib: {reading.Vibration}");
    }

}