using System.Buffers.Text;
using System.Text;
public static class TelemetryParser
{
    public static Result<TelemetryPacket, string> Parse(ReadOnlySpan<byte> payload)
    {
        // 1. Find the absolute index of every comma
        int firstComma = payload.IndexOf((byte)',');
        if (firstComma == -1) return Result<TelemetryPacket, string>.Failure("Invalid payload format.");

        int secondComma = firstComma + 1 + payload[(firstComma + 1)..].IndexOf((byte)',');
        if (secondComma == firstComma) return Result<TelemetryPacket, string>.Failure("Invalid payload format.");

        int thirdComma = secondComma + 1 + payload[(secondComma + 1)..].IndexOf((byte)',');
        if (thirdComma == secondComma) return Result<TelemetryPacket, string>.Failure("Invalid payload format.");

        // 2. Use Range Operators (..) to slice. No length math required!
        ReadOnlySpan<byte> assetIdSpan = payload[..firstComma];
        ReadOnlySpan<byte> tempSpan = payload[(firstComma + 1)..secondComma];
        ReadOnlySpan<byte> vibSpan = payload[(secondComma + 1)..thirdComma];
        ReadOnlySpan<byte> timestampSpan = payload[(thirdComma + 1)..];

        // 3. Parse the numbers
        if (!Utf8Parser.TryParse(tempSpan, out double temperature, out _))
            return Result<TelemetryPacket, string>.Failure("Invalid temperature format.");

        if (!Utf8Parser.TryParse(vibSpan, out double vibration, out _))
            return Result<TelemetryPacket, string>.Failure("Invalid vibration format.");

        if (!Utf8Parser.TryParse(timestampSpan, out long timestamp, out _))
            return Result<TelemetryPacket, string>.Failure("Invalid timestamp format.");

        // 4. Construct the domain object
        var assetId = Encoding.UTF8.GetString(assetIdSpan);
        return Result<TelemetryPacket, string>.Success(new TelemetryPacket(assetId, temperature, vibration, timestamp));
    }
}