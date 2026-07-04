Console.WriteLine("Day 03: Advanced Pattern Matching (Tuples, Relational, and Logical Operators)");

DiagnosticService diagnosticService = new();

// 1. Define all payloads
TelemetryPayload failPayload = new(90, 6.0, "FAIL");
TelemetryPayload hotAndShakingPayload = new(105, 8.5, null);
TelemetryPayload warnPayload = new(105, 6.0, "WARN");
TelemetryPayload healthyPayload = new(80, 4.0, null);
TelemetryPayload invalidTemperaturePayload = new(-281, 6.0, null);
TelemetryPayload invalidVibrationPayload = new(81, -6.0, null);

// 2. Evaluate and print them using the helper below
ProcessTelemetry(failPayload);
ProcessTelemetry(hotAndShakingPayload);
ProcessTelemetry(warnPayload);
ProcessTelemetry(healthyPayload);
ProcessTelemetry(invalidTemperaturePayload);
ProcessTelemetry(invalidVibrationPayload);

// 3. Local Function: Handles the result processing once
void ProcessTelemetry(TelemetryPayload payload)
{
    var result = diagnosticService.EvaluateTelemetry(payload);

    if (result is { IsSuccess: true, Value: var status })
    {
        Console.WriteLine($"Temperature: {payload.Temperature} °C, Vibration: {payload.Vibration} mm/s. Status: {status.ToString().ToUpper()}");
    }
    else if (result is { IsFailure: true })
    {
        Console.WriteLine($"ERROR for Payload ({payload.Temperature} °C, {payload.Vibration} mm/s): {result.Error}");
    }
}