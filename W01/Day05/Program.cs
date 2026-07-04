using Microsoft.Extensions.Logging.Abstractions;

Console.WriteLine("Day 05's Challenge: Vibration Analysis Service");

// 2. Setup dependencies
var logger = NullLogger<VibrationAnalysisService>.Instance;
var context = new MockAssetContext();

// 3. Instantiate using primary constructor syntax
var service = new VibrationAnalysisService(logger, context);

// 4. Test Cases (using collection expressions)
double[] normalReadings = [4.5, 5.1, 4.8, 5.0];
double[] criticalReadings = [11.2, 8.5, 9.1, 10.5];


Console.WriteLine("--- Testing Normal Asset ---");
var normalResult = await service.Evaluate(normalReadings, "PUMP-104A");
Console.WriteLine(normalResult.IsSuccess ? $"SUCCESS: {normalResult.Value}" : $"FAILURE: {normalResult.Error}");

Console.WriteLine("\n--- Testing Critical Asset ---");
var criticalResult = await service.Evaluate(criticalReadings, "PUMP-104B");
Console.WriteLine(criticalResult.IsSuccess ? $"SUCCESS: {criticalResult.Value}" : $"FAILURE: {criticalResult.Error}");

// 1. Define a simple mock right in this file (hidden from the rest of the app)
file class MockAssetContext : IAssetContext
{
    public Task<bool> FlagForMaintenanceAsync(string assetId)
    {
        Console.WriteLine($"[MOCK] Maintenance work order generated for asset: {assetId}");
        return Task.FromResult(true);
    }
}
