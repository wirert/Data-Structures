using System;

namespace Exam.Expressionist
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var expressionist = new Expressionist();

            expressionist.AddExpression(new Expression("1", "+", ExpressionType.Operator, new Expression("2", "2", ExpressionType.Value, null, null), new Expression("3", "4", ExpressionType.Value, null, null)));

            Console.WriteLine(expressionist.Evaluate());
        }
    }
}
