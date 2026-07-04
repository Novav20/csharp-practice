public class ProvisioningService
{
    public Result<ProvisioningReport, string> ProvisionAsset(Asset asset)
    {
        // 1. Validate first (Guard clauses)
        if (string.IsNullOrEmpty(asset.SerialNumber) || !asset.SerialNumber.StartsWith("O&G-"))
        {
            return Result<ProvisioningReport, string>.Failure("Invalid serial number format.");
        }
        if (string.IsNullOrEmpty(asset.LocationCode))
        {
            return Result<ProvisioningReport, string>.Failure("LocationCode is required for provisioning.");
        }

        // 2. Process second (Safe from null/empty issues here)
        Criticality criticality = asset switch
        {
            // We use 'string loc' to bind the value directly for the guard clause
            { AssetType: "Pump-Motor", LocationCode: string loc } when loc.StartsWith("FL-PS") => Criticality.High,
            { AssetType: "Pump-Motor" } => Criticality.Medium,
            _ => Criticality.Low
        };

        var report = new ProvisioningReport(asset, criticality);
        return Result<ProvisioningReport, string>.Success(report);
    }
}