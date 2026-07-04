Console.WriteLine("Day 11's Challenge: Type-Safe Telemetry Batch Processor");

TelemetryBatch<VibrationData> vibrationBatch = new();
vibrationBatch.Add(new VibrationData { Value = 4.1, Timestamp = 16800000, Axis = "X" });
vibrationBatch.Add(new VibrationData { Value = 5.2, Timestamp = 16890000, Axis = "X" });

TelemetryBatch<TemperatureData> temperatureBatch = new();
temperatureBatch.Add(new TemperatureData { Value = 86.4, Timestamp = 16800000, Unit = "°C" });
temperatureBatch.Add(new TemperatureData { Value = 89.9, Timestamp = 16890000, Unit = "°C" });


Console.WriteLine($"Vibration Batch average value: {vibrationBatch.GetAverageValue()}");
Console.WriteLine($"Temperature Batch average value: {temperatureBatch.GetAverageValue()}");