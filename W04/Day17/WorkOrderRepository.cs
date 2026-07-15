public class WorkOrderRepository
{
    // Bad method: Because the parameter is IEnumerable, this will force local execution.
    public IEnumerable<WorkOrder> GetOpenHighPriorityBad(IEnumerable<WorkOrder> source) =>
        source.Where(wo => wo.Status == WorkOrderStatus.Open && wo.Priority >= 5);

    // Good method: Because the parameter is IQueryable, this will expand the Expression Tree.
    public IQueryable<WorkOrder> GetOpenHighPriorityGood(IQueryable<WorkOrder> source) =>
        source.Where(wo => wo.Status == WorkOrderStatus.Open && wo.Priority >= 5);

}