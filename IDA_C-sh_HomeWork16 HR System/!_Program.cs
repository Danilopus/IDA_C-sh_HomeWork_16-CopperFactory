﻿// HomeWork template 1.4 // date: 17.10.2023

using IDA_C_sh_HomeWork16_HR_System;
using Service;
using System;
using System.Linq.Expressions;
using System.Text;


// HomeWork 16 : [{HR System}] --------------------------------

namespace IDA_C_sh_HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MainMenu.MainMenu mainMenu = new MainMenu.MainMenu();

            do
            {
                Console.Clear();
                mainMenu.Show_menu();
                if (mainMenu.User_Choice_Handle() == 0) break;
                //Console.ReadKey();
            } while (true);
        }

        public static void Task_1(string work_name)
        /* Задание 2:
        
        Разработайте простую систему управления сотрудниками для небольшой компании. 
        
        Система должна поддерживать следующие возможности:

        1)Создание нового сотрудника с указанием имени, должности и зарплаты.
        2)Просмотр списка всех сотрудников.
        3)Увольнение сотрудника по его имени.
        4)Подсчет общей суммы зарплаты всех сотрудников.
        5)Подсчет средней зарплаты среди всех сотрудников.
        6)Поиск сотрудников с зарплатой выше заданного значения.
        
        Требования:

        Реализуйте необходимые классы (например, Employee, EmployeeManagementSystem и т.д.) с использованием основ ООП.
        Используйте делегаты или события для обработки увольнения сотрудника.
        Используйте лямбды для выполнения поиска сотрудников по критерию зарплаты.
        Реализуйте пользовательский интерфейс (консольный или графический) для взаимодействия с системой управления сотрудниками.
        Представьте ваше решение в виде полноценного проекта, включая все необходимые классы, интерфейсы, методы 
        и демонстрацию работы системы с примерами использования всех функций.*/
        
        { Console.WriteLine("\n***\t{0}\n\n", work_name); 
        
        EmployeeManageSystem manageSystem = new EmployeeManageSystem();

        foreach (Employee employee in Employee.GetEmployeeTeam(20))
                manageSystem.AddEmployee(employee);

            manageSystem.Menu();


        }


    }// class Program
}// namespace