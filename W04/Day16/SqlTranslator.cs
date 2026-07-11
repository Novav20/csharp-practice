using System.Linq.Expressions;

public static class SqlTranslator
{
    public static string TranslateToSql(Expression<Func<WorkOrder, bool>> expression)
    {
        return "WHERE " + TranslateNode(expression.Body);
    }

    private static string TranslateNode(Expression node)
    {
        if (node is BinaryExpression binary)
        {
            // Handle combined conditions (AND)
            if (binary.NodeType == ExpressionType.AndAlso)
            {
                return $"{TranslateNode(binary.Left)} AND {TranslateNode(binary.Right)}";
            }

            // Handle simple comparisons (==, >=)
            string leftProp = ((MemberExpression)binary.Left).Member.Name;
            string rightVal = FormatValue(((ConstantExpression)binary.Right).Value);

            string op = binary.NodeType switch
            {
                ExpressionType.Equal => "=",
                ExpressionType.GreaterThanOrEqual => ">=",
                _ => "?"
            };

            return $"{leftProp} {op} {rightVal}";
        }

        return "1=1"; // Fallback for the 'true' constant expression
    }

    private static string FormatValue(object? value) => value is string s ? $"'{s}'" : value?.ToString() ?? "NULL";
}