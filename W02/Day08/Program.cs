Console.WriteLine("Day 08's Challenge: Zero-Allocation MQTT Payload Parser");

ReadOnlySpan<byte> validPayload = "PUMP-205,88.5,3.1,1680000000"u8;
ReadOnlySpan<byte> invalidPayload = "PUMP-205,ERR,3.1,1680000000"u8;
ReadOnlySpan<byte> invalidPayloadFormat = "PUMP-205,88.5,3.1"u8;

PrintTelemetryResult(validPayload);
PrintTelemetryResult(invalidPayload);
PrintTelemetryResult(invalidPayloadFormat);



static void PrintTelemetryResult(ReadOnlySpan<byte> payload)
{
    var result = TelemetryParser.Parse(payload);
    if (result is { IsSuccess: true })
    {
        Console.WriteLine(result.Value);
    }
    else
    {
        Console.WriteLine(result.Error);

    }
}