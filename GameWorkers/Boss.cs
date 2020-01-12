using System;
using System.Collections.Generic;
using System.Text;

namespace GameWorkers
{
    class Boss : Employee, IManagable, IManage
    {
        public Boss(string name, bool mood, Point position, decimal salary = 5000, bool alive = true) : base(name, mood, position, salary, alive)
        {

        }

        public void DoWork()
        {
            Console.WriteLine("Work");
        }

        public override void Talk(Employee ee)
        {
            string speach;
            if (ee is Boss || ee is Worker)
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
        public void Manage(IManagable imngbl)
        {
            if(imngbl is Worker)
            {
                (imngbl as Worker).DoWork();
            }
        }
    }
}
