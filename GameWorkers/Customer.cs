using System;
using System.Collections.Generic;
using System.Text;

namespace GameWorkers
{
    class Customer : IMoveable, IManage
    {
        public Customer(string name, Point position, bool mood = true, bool alive = true)
        {
            Name = name;
            Mood = mood;
            IsAlive = alive;
            Pos = position;
        }
        public string Name { get; set; }
        public bool Mood { get; set; }
        public Point Pos { get; set; }
        public bool IsAlive { get; set; }

        public void Move(Point p, int dir)
        {
            Pos = p;

            //Down (right)
            if (dir == 0)
            {
                Pos = new Point(Pos.X, Pos.Y + 1);
            }
            //Up (Left)
            else if (dir == 1)
            {
                Pos = new Point(Pos.X, Pos.Y - 1);
            }
            //Left (Up)
            else if (dir == 2)
            {
                Pos = new Point(Pos.X - 1, Pos.Y);
            }
            //Right (Down)
            else
            {
                Pos = new Point(Pos.X + 1, Pos.Y);
            }
        }

        public void Manage(IManagable imngbl)
        {
            if (imngbl is Worker)
            {
                (imngbl as Worker).DoWork();
            }
        }
    }
}
