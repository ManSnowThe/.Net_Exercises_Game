using System;
using System.Threading;

namespace GameWorkers
{
    class Program
    {
        static void Main(string[] args)
        {
            GameZoneForConsole s = new GameZoneForConsole(10, 10);
            s.SpawnObject(new Worker("Bob", true, new Point(4, 4), 1000, true));
            s.SpawnObject(new Worker("John", true, new Point(6, 4), 1000, true));
            s.SpawnObject(new Worker("Jill", true, new Point(2, 2), 1000, true));
            s.SpawnObject(new Boss("Michael", true, new Point(8, 2)));
            s.SpawnObject(new Boss("Leon", true, new Point(9, 9)));
            s.SpawnObject(new BigBoss("Bigboss", true, new Point(7,7)));

            s.SpawnCustomer(new Customer("Billy", new Point(5, 5)));

            s.SpawnOther(new Work(new Point(8, 8)));
            do
            {
                s.MoveItem();
                s.MoveCustomer(s.customers[0]);
                foreach (Employee em in s.empl)
                {
                    s.MoveObject(em);
                    //s.MoveObject(s.empl[1]);
                    //s.MoveObject(s.empl[2]);
                }
                Thread.Sleep(100);
                s.Show();
            }
            while (true);
            {
            }
        }
    }
}
