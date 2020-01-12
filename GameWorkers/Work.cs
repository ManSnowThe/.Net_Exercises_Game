using System;
using System.Collections.Generic;
using System.Text;

namespace GameWorkers
{
    class Work : IMoveable
    {
        public Work(Point position, bool alive = false)
        {
            IsAlive = alive;
            Pos = position;
        }
        public bool IsAlive { get; set; }
        public void Move(Point p, int n)
        {
            Pos = p;
            int nothing = n;
        }
        public Point Pos { get; set; }
    }
}
