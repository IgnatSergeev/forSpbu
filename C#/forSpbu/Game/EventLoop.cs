namespace Game;

public class EventLoop
{
    public event EventHandler<EventArgs> LeftHandler = (_, _) => { };
    public event EventHandler<EventArgs> RightHandler = (_, _) => { };
    public event EventHandler<EventArgs> UpHandler = (_, _) => { };
    public event EventHandler<EventArgs> DownHandler = (_, _) => { };
    public event EventHandler<EventArgs> EndHandler = (_, _) => { };
    public event EventHandler<EventArgs> StartHandler = (_, _) => { };
    public event EventHandler<EventArgs> LoopStartHandler = (_, _) => { };

    public void Run()
    {
        var endCondition = false;
        StartHandler(this, EventArgs.Empty);
        while (true)
        {
            if (endCondition)
            {
                break;
            }
            
            LoopStartHandler(this, EventArgs.Empty);
            
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.LeftArrow:
                    LeftHandler(this, EventArgs.Empty);
                    break;
                case ConsoleKey.RightArrow:
                    RightHandler(this, EventArgs.Empty);
                    break;
                case ConsoleKey.UpArrow:
                    UpHandler(this, EventArgs.Empty);
                    break;
                case ConsoleKey.DownArrow:
                    DownHandler(this, EventArgs.Empty);
                    break;
                case ConsoleKey.Enter:
                    EndHandler(this, EventArgs.Empty);
                    endCondition = true;
                    break;
            }
        }
    }
}