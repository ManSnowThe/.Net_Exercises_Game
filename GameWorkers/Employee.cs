using System;
using System.Collections.Generic;
using System.Text;

namespace GameWorkers
{
    abstract class Employee : IMoveable
    {
        public Employee(string name, bool mood, Point position, decimal salary, bool alive)
        {
            Salary = salary;
            Name = name;
            Mood = mood;
            IsAlive = alive;
            Pos = position;
        }

        public decimal Salary { get; set; }
        public string Name { get; set; }
        public bool Mood { get; set; }
        public Point Pos { get; set; }
        public string Say(string WhatToSay)
        {
            return Name + " (" + GetType().Name + ") says:" + WhatToSay;
        }
        abstract public void Talk(Employee ee);

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
    }
}
