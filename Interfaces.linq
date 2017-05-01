<Query Kind="Program" />

public interface IMathOperation
{
	decimal Compute (decimal left, decimal right);
}

public class AddOperation : IMathOperation
{
	decimal IMathOperation.Compute(decimal left, decimal right)
	{
		return left + right;
	}
}

public class SubtractOperation : IMathOperation
{
	decimal IMathOperation.Compute(decimal left, decimal right)
	{
		return left - right;
	}
}

public class MultiplyOperation : IMathOperation
{
	decimal IMathOperation.Compute(decimal left, decimal right)
	{
		return left * right;
	}
}

public class DivideOperation : IMathOperation
{
	decimal IMathOperation.Compute(decimal left, decimal right)
	{
		return left / right;
	}
}

private static IMathOperation GetOperation (char oper)
{
	switch (oper)
	{
		case '+' : return new AddOperation();
		case '-' : return new SubtractOperation();
		case '*' : return new MultiplyOperation();
		case '/' : return new DivideOperation();
	}
	
	throw new NotSupportedException("This operator is not supported");
}

public static decimal Eval(string expr)
{
	var elements = expr.Split(new[] {' '});
	var l = Decimal.Parse(elements[0]);
	var r = Decimal.Parse(elements[1]);
	var op = elements[2][0];
	
	return GetOperation(op).Compute(l,r);
}

void Main()
{
	Eval("1 3 +").Dump();
	Eval("5 2 -").Dump();
	Eval("10 3 *").Dump();
	Eval("12 3 /").Dump();
}