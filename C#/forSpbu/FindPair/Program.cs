namespace FindPare;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Incorrect number of program args");
            return;
        }
        if (!int.TryParse(args[0], out var fieldSize))
        {
            Console.WriteLine("Error in parsing fieldSize, incorrect program parameter");
            return;
        }

        if (fieldSize <= 0 || fieldSize % 2 == 1)
        {
            Console.WriteLine("Incorrect program parameter, should be > 0 and % 2 == 0");
            return;
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new GameForm(fieldSize));
    }
}