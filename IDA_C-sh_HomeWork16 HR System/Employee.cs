using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManger;
using Service;

namespace IDA_C_sh_HomeWork16_HR_System
{
    internal class Employee
    {
        ///// CTOR /////

        public Employee() { EmployeeID = ID_counter++; }
        static public Employee[] GetEmployeeTeam(int employee_qua)
        {
            string[] females_names;
            string[] males_names;
            string[] males_surnames;
            FileManager.RandomNameSurnameLoader(out males_surnames, out males_names, out females_names);

            Random random = new Random();
            List<Employee> list = new List<Employee>();

            for (int i = 0; i < employee_qua; i++)
            {
                bool gender = false; // female
                if (random.Next(10) > 5) gender = true; // man
                Employee employee = new Employee();
                if (gender)
                {
                    employee.FirstName = males_names[random.Next(males_names.Length)];
                    employee.LastName = males_surnames[random.Next(males_surnames.Length)];
                }
                else
                {
                    employee.FirstName = females_names[random.Next(females_names.Length)];
                    employee.LastName = (males_surnames[random.Next(males_names.Length)] + "а");
                }
                string[] positions = new string[] { "Director", "Head", "Ordinary" };
                switch (random.Next(10))
                {
                    case 0: 
                        employee.Position = positions[0];
                        employee.Salary = (decimal)ServiceFunction.Get_Random(1e5, 1.5e5);
                        break;
                    case 1: case 2: 
                        employee.Position = positions[1];
                        employee.Salary = (decimal)ServiceFunction.Get_Random(0.5e5, 1e5);
                        break;
                    default: 
                        employee.Position = positions[2];
                        employee.Salary = (decimal)ServiceFunction.Get_Random(1e4, 4e4);
                        break;
                }
                employee.Salary = Math.Round(employee.Salary, 0);
                list.Add(employee);
            }

            return list.ToArray(); 
        }
        static public Employee CreateNew()
        {
            Employee employee = new Employee();
            Console.Write("FirstName: ");
            employee.FirstName = Console.ReadLine();
            Console.Write("LastName: ");
            employee.LastName = Console.ReadLine();
            Console.Write("Position: ");
            employee.Position = Console.ReadLine();
            Console.Write("Salary: ");
            employee.Salary = Decimal.Parse(Console.ReadLine());
            return employee;
        }

        ///// PROPS /////

        static int ID_counter = 1;
        public int EmployeeID { get; set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Position { get; private set; }
        public decimal Salary { get; private set; }

        ///// PUBLIC METHODS /////

        public override string ToString()
        {
            return FirstName + " " + LastName + " ID_" + EmployeeID;
        }
    }
}
