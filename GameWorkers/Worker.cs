using System;
using System.Collections.Generic;
using System.Text;

namespace GameWorkers
{
    class Worker : Employee, IManagable
    {
        public Worker(string name, bool mood, Point position, decimal salary = 1000, bool alive = true) : base(name, mood, position, salary, alive)
        {

        }

        public void DoWork()
        {
            this.Salary += 1000;
            Console.WriteLine("Additional to salary: + 1000. Now: ", this.Salary);
        }

        public override void Talk(Employee ee)
        {
            string speach;
            if(ee is Worker)
            {
                speach = $"Hello, {ee.Name}!";
                Console.WriteLine(Say(speach));
            }
            else
            {
                speach = $"Good morning, {ee.Name}!";
                Console.WriteLine(Say(speach));
            }
        }
    }
}
