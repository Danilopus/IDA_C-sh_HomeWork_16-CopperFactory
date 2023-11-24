using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;

namespace IDA_C_sh_HomeWork16_HR_System
{
    internal class EmployeeManageSystem
    {
         ///// PROPS /////
        
        List<string> app_menu_list = new List<string>()
            {
                "1) Создание нового сотрудника с указанием имени, должности и зарплаты",
                "2) Просмотр списка всех сотрудников",
                "3) Увольнение сотрудника по его имени",
                "4) Подсчет общей суммы зарплаты всех сотрудников",
                "5) Подсчет средней зарплаты среди всех сотрудников",
                "6) Поиск сотрудников с зарплатой выше заданного значения",
                "\n0 - Exit"
            };
        List<Employee> employees_list = new List<Employee>();

        ///// PUBLIC METHODS /////
        
        public void Menu() 
        { 
            do 
            { 
                Console.Clear();
                ShowMainMenu(); 
            } while (UserChoiceHandle()); 
        }
        public void AddEmployee(Employee employee) { employees_list.Add(employee); }
        public void ShowEmployeeList() { foreach (var employee in employees_list) Console.WriteLine(employee); }
        public void DismissalEmployee(Employee employee) { employees_list.Remove(employee); }
        public void DismissalEmployee(string[] employee_name)
        {
            foreach (Employee employee in employees_list.Where(x => x.FirstName == employee_name[0] && x.LastName == employee_name[1]))
                employees_list.Remove(employee);
        }
        public decimal SalarySummary() { return employees_list.Sum(x => x.Salary); }
        public decimal SalaryAverage() { return employees_list.Average(x => x.Salary); }
        public void SalaryAboveValue() 
        {
            decimal value = GetSalaryValue();
            Console.WriteLine("\nSalary above {0}:\n", value);
            foreach (Employee employee in employees_list.Where(x => x.Salary > value))
                Console.WriteLine(employee.ToString().PadLeft(30) + " Salary: " + employee.Salary); 
         }

        ///// METHODS /////

        void ShowMainMenu()
        {
            Console.WriteLine(" *** HR System *** \n");
            foreach (var menu_item in app_menu_list)
                Console.WriteLine(menu_item);
        } 
        bool UserChoiceHandle()
        {
            switch (ServiceFunction.Get_Int(0, app_menu_list.Count))
            {
                case 1: AddEmployee(Employee.CreateNew()); break;
                case 2: ShowEmployeeList(); break;
                case 3: DismissalEmployee(GetEmployeeName()); break;
                case 4: Console.WriteLine("Salary summary: {0}", SalarySummary()); break;
                case 5: Console.WriteLine("Salary average: {0}", SalaryAverage()); break;
                case 6: SalaryAboveValue(); break;
                case 0: return false;
            }  
            Console.ReadKey();
            return true;
        }
        string[] GetEmployeeName()
        {
            string[] employee_name = new string[2];
            Console.Write("FirstName: ");
            employee_name[0] = Console.ReadLine();
            Console.Write("LastName: ");
            employee_name[1] = Console.ReadLine();
            return employee_name;
        }
        decimal GetSalaryValue()
        {
            Console.Write("Value of salary to search above: ");
            return Decimal.Parse(Console.ReadLine());
        }


    }//c
}//n
