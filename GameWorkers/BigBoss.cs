using System;
using System.Collections.Generic;
using System.Text;

namespace GameWorkers
{
    class BigBoss : Boss, IManage
    {
        public BigBoss(string name, bool mood, Point position, decimal salary = 10000, bool alive = true) : base (name, mood, position, salary, alive)
        {

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
            if (imngbl is Worker)
            {
                (imngbl as Worker).DoWork();
            }
            else if (imngbl is Boss)
            {
                (imngbl as Boss).DoWork();
            }
        }
    }
}
