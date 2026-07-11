using System.Linq.Expressions;

public static class WorkOrderFilterBuilder
{
    public static Expression<Func<WorkOrder, bool>> BuildFilter(WorkOrderStatus? status, int? minPriority)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(WorkOrder), "wo");
        Expression? body = null;

        // 1. Build Status condition
        if (status.HasValue)
        {
            var left = Expression.Property(parameter, nameof(WorkOrder.Status));
            var right = Expression.Constant(status.Value, typeof(WorkOrderStatus));
            body = Expression.Equal(left, right);
        }

        // 2. Build Priority condition
        if (minPriority.HasValue)
        {
            var left = Expression.Property(parameter, nameof(WorkOrder.Priority));
            var right = Expression.Constant(minPriority.Value, typeof(int));
            var priorityExpr = Expression.GreaterThanOrEqual(left, right);

            // 3. Combine if we already have a status condition
            body = body == null ? priorityExpr : Expression.AndAlso(body, priorityExpr);
        }

        // 4. Fallback if neither was provided
        body ??= Expression.Constant(true);

        return Expression.Lambda<Func<WorkOrder, bool>>(body, parameter);
    }
}