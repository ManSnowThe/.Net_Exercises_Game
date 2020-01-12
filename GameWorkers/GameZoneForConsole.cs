using System;
using System.Collections.Generic;
using System.Text;

namespace GameWorkers
{
    class GameZoneForConsole
    {
        public string[,] GameZone { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int Dir { get; set; }

        public List<Employee> empl = new List<Employee>();
        public List<Work> works = new List<Work>();
        public List<Customer> customers = new List<Customer>();

        Random random = new Random();

        public GameZoneForConsole(int rows, int columns)
        {
            Rows = rows + 2;
            Cols = columns + 2;
            BuildZone(Rows, Cols);
            Show();
        }

        #region
        public void BuildZone(int rows, int cols)
        {
            GameZone = new string[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    GameZone[i, j] = ".";
                }
            }

            for (int i = 0; i < rows; i++)
            {
                GameZone[i, 0] = "|";
                GameZone[i, cols - 1] = "|";
            }
            for (int i = 0; i < cols; i++)
            {
                GameZone[0, i] = "|";
                GameZone[rows - 1, i] = "|";
            }
        }

        public void Show()
        {
            Console.Clear();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Console.Write(GameZone[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        #endregion

        //Спавнит живые объекты на поле
        public void SpawnObject(Employee emp)
        {
            if (emp is Worker)
            {
                GameZone[emp.Pos.X, emp.Pos.Y] = "W";
                empl.Add(emp);
            }
            else if (emp is Boss)
            {
                GameZone[emp.Pos.X, emp.Pos.Y] = "b";
                empl.Add(emp);
            }
            else if (emp is BigBoss)
            {
                GameZone[emp.Pos.X, emp.Pos.Y] = "B";
                empl.Add(emp);
            }
        }

        //Спавнит предметы на поле
        public void SpawnOther(Work work)
        {
            if (work is Work)
            {
                GameZone[work.Pos.X, work.Pos.Y] = "w";
                works.Add(work);
            }
        }

        public void SpawnCustomer(Customer customer)
        {
            if (customer is Customer)
            {
                GameZone[customer.Pos.X, customer.Pos.Y] = "C";
                customers.Add(customer);
            }
        }

        //Метод, отвечающий за передвижение живых сущностей
        public void MoveObject(Employee emp)
        {
            Dir = random.Next(4);
            RemoveObject(emp);
            emp.Move(emp.Pos, Dir);

            if (GameZone[emp.Pos.X, emp.Pos.Y] != "|")
            {
                SetObject(emp);
                
                if (CompareObj2(emp))
                {
                    if (emp is Worker)
                    {
                        (emp as Worker).Talk(CompareObj(emp));
                    }
                    else if (emp is Worker && CompareObj(emp) is Boss)
                    {
                        (emp as Worker).DoWork();
                    }
                    else if (emp is Boss)
                    {
                        (emp as Boss).Talk(CompareObj(emp));
                    }
                    else if (emp is Boss && CompareObj(emp) is BigBoss)
                    {
                        (emp as Boss).DoWork();
                    }
                    else if(emp is BigBoss)
                    {
                        (emp as BigBoss).Talk(CompareObj(emp));
                        (emp as BigBoss).Manage(CompareObj(emp) as IManagable);
                    }
                    //ChangeDirection1(emp, Dir, emp.Pos);
                    ChangeDirection2(emp, Dir, emp.Pos);
                }
                else if (MoveItem())
                {
                    if (emp is Worker)
                    {
                        (emp as Worker).DoWork();
                    }
                }
            }
            else
            {
                ChangeDirection2(emp, Dir, emp.Pos);
            }
            //Console.WriteLine(emp.Pos.X + " " + emp.Pos.Y);
        }

        // Метод, который отвечает за сущность "Работа"
        public bool MoveItem()
        {
            Work work;
            work = works[0];

            RemoveItem(work);
            work.Move(work.Pos, 0);

            if (GameZone[work.Pos.X, work.Pos.Y] != "|")
            {
                SetItem(work);
                if (CompareObjectAndItem(work))
                {
                    RemoveItem(work);
                    works.Clear();
                    SpawnOther(new Work(new Point(random.Next(2,9), random.Next(2,9))));
                    return true;
                }
                return false;
            }
            return false;
        }

        //Метод, отвечающий за движение сущности "Клиент"
        public void MoveCustomer(Customer customer)
        {
            Dir = random.Next(4);
            RemoveCustomer(customer);
            customer.Move(customer.Pos, Dir);

            if (GameZone[customer.Pos.X, customer.Pos.Y] != "|")
            {
                SetCustomer(customer);

                if (CompareCustomer2(customer))
                {
                    if(customer is Customer)
                    {
                        (customer as Customer).Manage(CompareCustomer(customer) as IManagable);
                    }
                    ChangeDirection2(customer, Dir, customer.Pos);
                }
            }
            else
            {
                ChangeDirection2(customer, Dir, customer.Pos);
            }
        }

        // Для того, чтобы живой объект не исчезал с карты при передвижении
        public void SetObject(Employee emp)
        {
            if (emp is Worker)
            {
                GameZone[emp.Pos.X, emp.Pos.Y] = "W";
            }
            else if (emp is Boss)
            {
                GameZone[emp.Pos.X, emp.Pos.Y] = "b";
            }
            else if (emp is BigBoss)
            {
                GameZone[emp.Pos.X, emp.Pos.Y] = "B";
            }
        }

        //Метод, отвечающий за отображение сущности "Работа"
        public void SetItem(Work work)
        {
            if (work is Work)
            {
                GameZone[work.Pos.X, work.Pos.Y] = "w";
            }
        }

        public void SetCustomer(Customer customer)
        {
            if (customer is Customer)
            {
                GameZone[customer.Pos.X, customer.Pos.Y] = "C";
            }
        }

        public void RemoveObject(Employee emp)
        {
            GameZone[emp.Pos.X, emp.Pos.Y] = ".";
        }

        public void RemoveItem(Work work)
        {
            GameZone[work.Pos.X, work.Pos.Y] = ".";
        }

        public void RemoveCustomer(Customer customer)
        {
            GameZone[customer.Pos.X, customer.Pos.Y] = ".";
        }

        //Метод, меняющий направление живого объекта, если он столкнулся с препятствием
        public void ChangeDirection2(Employee emp, int dir, Point custPos)
        {
            //Right
            if (dir == 0)
            {
                dir = 1;
                emp.Move(custPos, dir);
                SetObject(emp);
            }
            //Left
            else if (dir == 1)
            {
                dir = 0;
                emp.Move(custPos, dir);
                SetObject(emp);
            }
            //up
            else if (dir == 2)
            {
                dir = 3;
                emp.Move(custPos, dir);
                SetObject(emp);
            }
            //down
            else
            {
                dir = 2;
                emp.Move(custPos, dir);
                SetObject(emp);
            }
        }

        public void ChangeDirection2(Customer customer, int dir, Point empPos)
        {
            //Right
            if (dir == 0)
            {
                dir = 1;
                customer.Move(empPos, dir);
                SetCustomer(customer);
            }
            //Left
            else if (dir == 1)
            {
                dir = 0;
                customer.Move(empPos, dir);
                SetCustomer(customer);
            }
            //up
            else if (dir == 2)
            {
                dir = 3;
                customer.Move(empPos, dir);
                SetCustomer(customer);
            }
            //down
            else
            {
                dir = 2;
                customer.Move(empPos, dir);
                SetCustomer(customer);
            }
        }

        Employee CompareObj(Employee emp)
        {
            foreach (Employee e in empl)
            {
                if (emp.Name != e.Name && emp.Pos == e.Pos)
                {
                    return e;
                }
            }
            return null;
        }

        bool CompareObj2(Employee emp)
        {
            foreach(Employee e in empl)
            {
                if(emp.Name != e.Name && emp.Pos == e.Pos)
                {
                    return true;
                }
            }
            return false;
        }
        
        bool CompareObjectAndItem(Work work)
        {
            foreach(Employee e in empl)
            {
                if(e is Worker && work.Pos == e.Pos)
                {
                    return true;
                }
            }
            return false;
        }

        bool CompareCustomer2(Customer customer)
        {
            foreach (Customer e in customers)
            {
                if (customer.Name != e.Name && customer.Pos == e.Pos)
                {
                    return true;
                }
            }
            return false;
        }

        Customer CompareCustomer(Customer customer)
        {
            foreach (Customer e in customers)
            {
                if (customer.Name != e.Name && customer.Pos == e.Pos)
                {
                    return e;
                }
            }
            return null;
        }
    }
}
