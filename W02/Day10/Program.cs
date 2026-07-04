Console.WriteLine("Day 10's Challenge: Telemetry Profile Factory");

VibrationProfile profile1 = new("PUMP-101", 5, 3);
VibrationProfile profile2 = new()
{
    AssetId = "PUMP-102",
    SamplingRate = TimeSpan.FromSeconds(10),
    SensorCount = 2
};

BaseTelemetryProfile[] profiles = [profile1, profile2];

DeploymentManifest manifest = new("ZONE-A", profiles);

Console.WriteLine("--- Manifest's Details ---");
Console.WriteLine(manifest.Zone);
foreach (var profile in manifest.Profiles)
{
    Console.WriteLine(profile);
}
