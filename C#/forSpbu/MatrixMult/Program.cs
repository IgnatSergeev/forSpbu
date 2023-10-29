using MatrixMult;

if (args.Length != 3)
{
    Console.WriteLine("Wrong number of arguments(should be mode -s(single) or -m(multi) and two file paths)");
    return;
}

var mode = args[0];
var fstFilePath = args[1];
var secFilePath = args[2];
if (string.IsNullOrEmpty(fstFilePath) || string.IsNullOrEmpty(secFilePath))
{
    Console.WriteLine("Wrong(null or empty) paths");
    return;
}
if (mode != "-m" && mode != "-s")
{
    Console.WriteLine("Incorrect mode - s(single) or m(multi)");
    return;
}

try
{
    var fstMatrix = new Matrix(File.ReadAllLines(fstFilePath));
    var secMatrix = new Matrix(File.ReadAllLines(secFilePath));

    var result = new Matrix();
    switch (mode)
    {
        case "-m":
        {
            result = MatrixMultiplier.MultiThreadedMultiply(fstMatrix, secMatrix);
            break;
        }
        case "-s":
        {
            result = MatrixMultiplier.SingleThreadedMultiply(fstMatrix, secMatrix);
            break;
        }
    }

    var resultLines = result.GetStringRepresentation();
    File.WriteAllLines("result.txt", resultLines);
    Console.WriteLine("Multiplication successful, result matrix has been written to result.txt");
}
catch (IOException)
{
    Console.WriteLine("File not found");
}
catch (MatrixCreationException)
{
    Console.WriteLine("Matrix creation error(given incorrect matrices)");
}
catch (MatrixMultiplierException)
{
    Console.WriteLine("Multiplication error(given incompatible matrices)");
}
