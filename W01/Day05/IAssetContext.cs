public interface IAssetContext
{
    public Task<bool> FlagForMaintenanceAsync(string assetId);
}