Console.WriteLine("Day 09's Challenge: Work Order Routing");

CentrifugalPump multiStageCentrifugalPump = new("CP-101", "ZONE-B", 505.1, 10);
CentrifugalPump singleStageCentrifugalPump = new("CP-101", "ZONE-B", 416.7, 1);
GasCompressor compressorA = new("GC-101", "ZONE-A", 450, 2);
GasCompressor compressorB = new("GC-102", "ZONE-B", 450, 4);
StaticEquipment compressorTank = new("CT-101", "ZONE-A", 100);


PrintRouteAssigment(multiStageCentrifugalPump);
PrintRouteAssigment(singleStageCentrifugalPump);
PrintRouteAssigment(compressorA);
PrintRouteAssigment(compressorB);
PrintRouteAssigment(compressorTank);


static void PrintRouteAssigment(Asset asset)
{
    var result = WorkOrderRouter.Route(asset);
    if (result is { IsSuccess: true, Value: var value })
    {
        Console.WriteLine(value);
    }
}