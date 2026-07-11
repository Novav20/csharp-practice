Console.WriteLine("Day 16's Challenge: Dynamic Query Builder & AST Translation!");

List<WorkOrder> backlog = [
    new WorkOrder(1, "PM-101", WorkOrderStatus.Open, 9, DateTime.Today.AddDays(-1)),
    new WorkOrder(1, "SV-122", WorkOrderStatus.InProgress, 10, DateTime.Today.AddDays(-7)),
    new WorkOrder(1, "TT-202", WorkOrderStatus.Open, 2,DateTime.Today.AddDays(-2)),
    new WorkOrder(1, "VT-099", WorkOrderStatus.Completed, 3, DateTime.Today.AddDays(-5)),
    new WorkOrder(1, "PV-112", WorkOrderStatus.Completed, 6, DateTime.Today.AddDays(-7))
];

var buildFilter = WorkOrderFilterBuilder.BuildFilter(WorkOrderStatus.Open, 5);
Console.WriteLine(SqlTranslator.TranslateToSql(buildFilter));

var priorityFilter = WorkOrderFilterBuilder.BuildFilter(null, 8);
var priorityFilterCompiled = priorityFilter.Compile();

var filteredBacklog = backlog.Where(priorityFilterCompiled);

foreach (var wo in filteredBacklog)
{
    Console.WriteLine(wo);
}