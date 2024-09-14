using System.Linq.Expressions;

class LinqExpressions
{
    static void Main()
    {
        ParameterExpression a = Expression.Parameter(typeof(int), "a");
        ParameterExpression b = Expression.Parameter(typeof(int), "b");

        BinaryExpression condition = Expression.GreaterThan(Expression.Add(a, b), Expression.Constant(10));

        Expression<Func<int, int, bool>> lambdaExpression = Expression.Lambda<Func<int, int, bool>>(condition, a, b);

        Func<int, int, bool> compiledExpression = lambdaExpression.Compile();
        bool result = compiledExpression(5, 6);

        Console.WriteLine($"Result of the expression: {result}");
    }
}
