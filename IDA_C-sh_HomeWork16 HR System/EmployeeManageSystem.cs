using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
                "4) Увольнение сотрудника по его ID",
                "5) Подсчет общей суммы зарплаты всех сотрудников",
                "6) Подсчет средней зарплаты среди всех сотрудников",
                "7) Поиск сотрудников с зарплатой выше заданного значения",
                "\n0 - Exit"
            };
        List<Employee> employees_list = new List<Employee>();

        ///// TESTING METHODS /////

        internal List<Employee> JUSTFORTESTINGACCESS_employees_list() { return employees_list; }

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
        // Стандартный вариант
        public void DismissalEmployee_standart(Employee employee) { employees_list.Remove(employee); }
        // Вариант с использованием события
        public void DismissalEmployee(Employee employee) { employee.YouAreHired(); }
        public void DismissalEmployee(string[] employee_name)
        {
            // foreach (Employee employee in employees_list.Where(x => x.FirstName == employee_name[0] && x.LastName == employee_name[1])) 
            // foreach (Employee employee in employees_list.Where(x => x.FirstName == employee_name[0]).Where( x => x.LastName == employee_name[1]))
            // foreach (Employee employee in employees_list.Where(x => x.FirstName == name).Where(x => x.LastName == surname))
            if (employee_name == null) { throw new Exception("empty name"); }
            foreach (Employee employee in employees_list.Where(x => x.FirstName == employee_name[0]).Where(x => x.LastName == employee_name[1]))
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
            try
            {
                switch (ServiceFunction.Get_Int(0, app_menu_list.Count))
                {
                    case 1: AddEmployee(Employee.CreateNew()); break;
                    case 2: ShowEmployeeList(); break;
                    case 3: DismissalEmployee(GetEmployeeByName()); break;
                    case 4: DismissalEmployee(GetEmployeeByID()); break;
                    case 5: Console.WriteLine("Salary summary: {0}", SalarySummary()); break;
                    case 6: Console.WriteLine("Salary average: {0}", SalaryAverage()); break;
                    case 7: SalaryAboveValue(); break;
                    case 0: return false;
                }
            }
            catch ( Exception exception)
            { Console.WriteLine(exception.Message); }
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
        Employee GetEmployeeByName()
        {
            string[] employee_name = new string[2];
            Console.Write("FirstName: ");
            employee_name[0] = Console.ReadLine();
            Console.Write("LastName: ");
            employee_name[1] = Console.ReadLine();
            foreach (Employee employee in employees_list.Where(x => x.FirstName == employee_name[0]).Where(x => x.LastName == employee_name[1]))
                return employee;
            throw new Exception("employee not found");
        }

        Employee GetEmployeeByID() 
        {
            Console.Write("ID: ");
            //int ID = Int32.Parse(Console.ReadLine());
            int ID = ServiceFunction.Get_Int(
                employees_list.Min(x => x.EmployeeID), // lower
                employees_list.Max(x => x.EmployeeID), // higher
                $"ID in range [{employees_list.Min(x => x.EmployeeID)}..{employees_list.Max(x => x.EmployeeID)}]");

            foreach (Employee item in employees_list.Where(x => x.EmployeeID == ID))
                return item;
            throw new Exception("ID not found");   
            
        }
        decimal GetSalaryValue()
        {
            Console.Write("Value of salary to search above: ");
            return Decimal.Parse(Console.ReadLine());
        }

        ///// SUBSCRIBES /////
        
        public void EmployeeHiredSubscibe(Employee employee){DismissalEmployee_standart(employee);}


    }//c
}//n
