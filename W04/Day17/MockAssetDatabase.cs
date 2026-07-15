public class MockAssetDatabase
{
    private readonly List<WorkOrder> _data = [
    new WorkOrder(1, "PM-101", WorkOrderStatus.Open, 9, DateTime.Today.AddDays(-1)),
    new WorkOrder(2, "SV-122", WorkOrderStatus.InProgress, 10, DateTime.Today.AddDays(-7)),
    new WorkOrder(3, "TT-202", WorkOrderStatus.Open, 2,DateTime.Today.AddDays(-2)),
    new WorkOrder(4, "VT-099", WorkOrderStatus.Completed, 3, DateTime.Today.AddDays(-5)),
    new WorkOrder(5, "PV-112", WorkOrderStatus.Open, 6, DateTime.Today.AddDays(-7))
    ];

    public IQueryable<WorkOrder> WorkOrders => _data.AsQueryable();
}