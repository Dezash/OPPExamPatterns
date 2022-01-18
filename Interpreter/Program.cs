Expression num1 = new Number(10);
Expression num2 = new Number(20);
Expression addition = new Add(num1, num2);
Console.WriteLine(addition.Execute());

string expressionInput = Console.ReadLine() ?? throw new InvalidOperationException();
string[] tokens = expressionInput.Split(" ");
Expression result = null;
for(int i = 0; i < tokens.Length; i++)
{
    string token = tokens[i];
    if (ExpressionFactory.IsOperator(token))
    {
        if (result == null)
            throw new InvalidOperationException("Unexpected token");

        TerminalExpression nextExpression = ExpressionFactory.GetExpression(tokens[i + 1]);
        i++;

        result = ExpressionFactory.GetExpression(result, token, nextExpression);
    }
    else
    {
        result = ExpressionFactory.GetExpression(token);
    }
}

Console.WriteLine(result?.Execute().ToString() ?? "Could not calculate");

public abstract class Expression
{
    public abstract int Execute();
}

public static class ExpressionFactory
{
    public static TerminalExpression GetExpression(string token)
    {
        return new Number(int.Parse(token));
    }

    public static NonTerminalExpression GetExpression(Expression left, string operatorToken, Expression right)
    {
        return operatorToken switch
        {
            "+" => new Add(left, right),
            "-" => new Subtract(left, right),
            "*" => new Multiply(left, right),
            "/" => new Divide(left, right),
            _ => throw new ArgumentException("Invalid token")
        };
    }

    public static bool IsOperator(string token)
    {
        return token is "+" or "-" or "*" or "/";
    }
}

public abstract class TerminalExpression : Expression
{
}

public abstract class NonTerminalExpression : Expression
{
    protected NonTerminalExpression(Expression left, Expression right)
    {
        Left = left;
        Right = right;
    }

    protected Expression Left { get; set; }
    protected Expression Right { get; set; }
}

public class Subtract : NonTerminalExpression
{
    public override int Execute()
    {
        return Left.Execute() - Right.Execute();
    }

    public Subtract(Expression left, Expression right) : base(left, right)
    {
    }
}

public class Number : TerminalExpression
{
    private readonly int _value;

    public Number(int number)
    {
        _value = number;
    }

    public override int Execute()
    {
        return _value;
    }
}

public class Multiply : NonTerminalExpression
{
    public override int Execute()
    {
        return Left.Execute() * Right.Execute();
    }

    public Multiply(Expression left, Expression right) : base(left, right)
    {
    }
}

public class Divide : NonTerminalExpression
{
    public override int Execute()
    {
        return Left.Execute() / Right.Execute();
    }

    public Divide(Expression left, Expression right) : base(left, right)
    {
    }
}

public class Add : NonTerminalExpression
{
    public override int Execute()
    {
        return Left.Execute() + Right.Execute();
    }

    public Add(Expression left, Expression right) : base(left, right)
    {
    }
}
