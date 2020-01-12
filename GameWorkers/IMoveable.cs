using System;
using System.Collections.Generic;
using System.Text;

namespace GameWorkers
{
    interface IMoveable
    {
        void Move(Point p, int dir);
        bool IsAlive { get; set; }
    }
}
