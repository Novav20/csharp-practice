Console.WriteLine("------ Day 02:  Modern C# Construction & The Result Pattern ------");

Asset invalidSNAsset = new()
{
    SerialNumber = "SN-P101.1",
    AssetType = "Pump-Motor",
    LocationCode = "FL-PS101"
};

Asset missingLocationAsset = new()
{
    SerialNumber = "O&G-P101.1",
    AssetType = "Pump-Motor"
};

Asset validAsset = new()
{
    SerialNumber = "O&G-P101.1",
    AssetType = "Pump-Motor",
    LocationCode = "FL-PS101"
};

ProvisioningService provisioningService = new();

var result = provisioningService.ProvisionAsset(invalidSNAsset);

if (result.IsFailure)
{
    Console.WriteLine(result.Error);
}
result = provisioningService.ProvisionAsset(missingLocationAsset);

if (result.IsFailure)
{
    Console.WriteLine(result.Error);
}

result = provisioningService.ProvisionAsset(validAsset);

if (result is { IsSuccess: true, Value: var report })
{
    Console.WriteLine($"SUCCESS: {report.Asset.SerialNumber} mapped to {report.Criticality} priority.");
}