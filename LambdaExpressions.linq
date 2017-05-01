<Query Kind="Program" />

private static Func<decimal, decimal,decimal> GetOperation (char oper)
{
	switch (oper)
	{
		case '+' : return (l, r) => l + r;
		case '-' : return (l, r) => l - r;
		case '*' : return (l, r) => l * r;
		case '/' : return (l, r) => l / r;
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