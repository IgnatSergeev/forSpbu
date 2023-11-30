
class Program
{
    static public void Main()
    {
        var players = 2;
        Table table = new Table(players);
        var philosophers = new Philosophers.Philosopher[players];
        for (int i = 0; i < players; i++) {
            philosophers[i] = new Philosophers.Philosopher(table, i);
        }
    }
}
