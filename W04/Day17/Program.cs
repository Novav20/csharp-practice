using System.Diagnostics;

Console.WriteLine("Day 17's Challenge: The Client-Side Evaluation Trap");

MockAssetDatabase context = new();
WorkOrderRepository repository = new();

var stopwatch = Stopwatch.StartNew();

foreach (var workOrder in repository.GetOpenHighPriorityBad(context.WorkOrders))
{
    Console.WriteLine(workOrder);    
}
stopwatch.Stop();
Console.WriteLine($"IEnumerable\n -- Elapsed: {stopwatch.ElapsedMilliseconds} ms");

var query = repository.GetOpenHighPriorityGood(context.WorkOrders);
Console.WriteLine(query.Expression);

stopwatch.Restart();
foreach (var workOrder in query)
{
    Console.WriteLine(workOrder);    
}
stopwatch.Stop();
Console.WriteLine($"IQueryable\n -- Elapsed: {stopwatch.ElapsedMilliseconds} ms");