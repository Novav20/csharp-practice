public class Asset
{
    public required string SerialNumber { get; init; }
    public required string AssetType { get; init; }
    public string? LocationCode { get; set; }
}