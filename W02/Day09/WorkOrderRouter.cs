public static class WorkOrderRouter
{
    public static Result<string, string> Route(Asset asset)
    {
        string targetTeam = asset switch
        {
            CentrifugalPump { Horsepower: > 500.0 } => "HeavyPumpTeam",
            CentrifugalPump => "GeneralPumpTeam",
            GasCompressor { LocationCode: "ZONE-A" } => "CompressorSpecialists",
            RotatingEquipment => "RotatingTeam",
            StaticEquipment => "StaticTeam",
            _ => "GeneralMaintenance"
        };
        return Result<string, string>.Success(targetTeam);
    }
}