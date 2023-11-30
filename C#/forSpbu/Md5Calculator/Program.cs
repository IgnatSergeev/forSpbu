using Md5Calculator;

if (args.Length != 1 || string.IsNullOrEmpty(args[0]))
{
    Console.WriteLine("Should be 1 argument with path");
    return;
}

try
{
    var hash = await Md5Calculator.Md5Calculator.ComputeAsync(".");
    Console.WriteLine(Convert.ToBase64String(hash));
}
catch (CalculatorException e)
{
    Console.WriteLine(e.Message);
    return;
}
