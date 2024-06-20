namespace ConsoleNetChat;

/// <summary>
/// Represents asynchronous console
/// </summary>
public static class AsyncConsole
{
    private static readonly Mutex ConsoleMutex = new ();
    
    /// <summary>
    /// Async version of Console.WriteLine
    /// </summary>
    /// <param name="message">Message to write</param>
    public static void WriteLine(string message)
    {
        ConsoleMutex.WaitOne();
        Console.WriteLine(message);
        ConsoleMutex.ReleaseMutex();
    }
    
    /// <summary>
    /// Async version of Console.Write
    /// </summary>
    /// <param name="message">Message to write</param>
    public static void Write(string message)
    {
        ConsoleMutex.WaitOne();
        Console.Write(message);
        ConsoleMutex.ReleaseMutex();
    }
}