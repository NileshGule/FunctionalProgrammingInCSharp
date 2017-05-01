<Query Kind="Program" />

public delegate decimal MathOperation (decimal left, decimal right);

public static decimal Add(decimal left, decimal right)
{
	return left + right;
}

public static decimal Subtract(decimal left, decimal right)
{
	return left - right;
}

public static decimal Multiply(decimal left, decimal right)
{
	return left * right;
}

public static decimal Divide(decimal left, decimal right)
{
	return left / right;
}

private static MathOperation GetOperation (char oper)
{
	switch (oper)
	{
		case '+' : return Add;
		case '-' : return Subtract;
		case '*' : return Multiply;
		case '/' : return Divide;
	}
	
	throw new NotSupportedException("This operator is not supported");
}

public static decimal Eval(string expr)
{
	var elements = expr.Split(new[] {' '});
	var l = Decimal.Parse(elements[0]);
	var r = Decimal.Parse(elements[1]);
	var op = elements[2][0];
	
	return GetOperation(op)(l,r);
}

void Main()
{
	Eval("1 3 +").Dump();
	Eval("5 2 -").Dump();
	Eval("10 3 *").Dump();
	Eval("12 3 /").Dump();
}