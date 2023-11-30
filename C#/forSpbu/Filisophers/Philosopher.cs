
namespace Philosophers;

class Philosopher
{
    public Philosopher(Table table, int id) {
        table = table;
        id = id;
        thread = new Thread(this.Live);
        thread.Start();
    }

    public enum State
    {
        Eating,
        Thinking
    }

    private void Think() {
        Thread.Sleep(1000);
    }

    private void Eat() {
        Thread.Sleep(1000);
    }

    public void Live()
    {
        while (true) {
            if (!table.GiveFork()) {
                state = State.Thinking;
                Console.WriteLine($"{id}: Thinking, forks = {forks}");
                Think();
                continue;
            }
            forks++;
            if (forks == 2) {
                state = State.Eating;
                Eat();
                Console.WriteLine($"{id}: Eating");
                table.GetFork();
                table.GetFork();
                continue;
            }
        }

    }

    private Thread thread;
    private Table table;
    private int id;
    private int forks = 0;
    private State state = State.Thinking;
}