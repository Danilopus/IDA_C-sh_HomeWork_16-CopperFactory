// HomeWork template 1.4 // date: 17.10.2023

using IDA_C_sh_HomeWork_16_CopperFactory;
using Service;
using System;
using System.Linq.Expressions;
using System.Text;

/// QUESTIONS ///
/// 1. 

// HomeWork 16 : [{CopperFactory}] --------------------------------

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
                Console.ReadKey();
            } while (true);
            // Console.ReadKey();
        }

        public static void Task_1(string work_name)
        /* Задание 1: Система контроля переплавки меди
        
        Вам необходимо разработать систему контроля переплавки меди на медеплавильном заводе. 
        Система должна позволять контролировать температуру плавления меди, 
        автоматически регулировать процесс переплавки и предоставлять информацию о текущем состоянии переплавки.

        1.Требования:
        
            1.1) Создайте класс CopperSmeltingControlSystem, который будет представлять систему контроля переплавки меди. 
        Класс должен иметь следующие основные свойства и методы:

        Свойство IsRunning (тип bool): указывает, запущена ли система контроля переплавки меди.
        Метод Start(): запускает систему контроля.
        Метод Stop(): останавливает систему контроля.
        Событие TemperatureExceededThreshold: возникает, если температура переплавки превысила установленный порог.
        
            1.2) Создайте класс CopperFurnace, представляющий медеплавильную печь. 
        Класс должен иметь следующие основные свойства и методы:

        Свойство Temperature (тип double): текущая температура печи.
        Метод MeltCopper(): запускает процесс переплавки меди в печи.
        
            1.3) Создайте интерфейс ITemperatureSensor, который будет представлять датчик температуры. 
        Интерфейс должен объявлять следующий метод:

        Метод GetTemperature(): возвращает текущую температуру.
        
            1.4) Создайте класс CopperTemperatureSensor, который реализует интерфейс ITemperatureSensor. 
        Класс должен предоставлять возможность получить текущую температуру измерительного прибора.

        2. Задачи:

        2.1) Создайте классы в соответствии с требованиями, описанными выше.

        2.2) Реализуйте логику класса CopperSmeltingControlSystem, чтобы система запускалась 
        и останавливалась при вызове соответствующих методов. 
        Когда система запущена, она должна периодически получать текущую температуру измерительного 
        прибора CopperTemperatureSensor и проверять, не превысила ли температура установленный порог. 
        Если температура превысила пороговое значение, необходимо возбудить событие TemperatureExceededThreshold.

        2.3) Реализуйте логику класса CopperFurnace для запуска и остановки процесса переплавки меди. 
        При запуске процесса переплавки методом MeltCopper() должна автоматически 
        устанавливаться температура плавления меди и включаться контроль.

        2.4) Реализуйте класс CopperTemperatureSensor и метод GetTemperature(), 
        чтобы он возвращал случайное значение температуры в пределах допустимого диапазона.

        Замечания:
        Для генерации случайного значения температуры в классе CopperTemperatureSensor используйте класс Random из стандартной библиотеки C#.

        Для возбуждения и обработки событий используйте делегаты и события.*/
        {
            Console.WriteLine("\n***\t{0}\n\n", work_name);

            CopperSmeltingControlSystem copperSmeltingControlSystem_1 = new CopperSmeltingControlSystem();

            // Создадим 3 печи
            for (int i = 0; i < 3; i++) { copperSmeltingControlSystem_1.FurnacesAtControl_list.Add(new CopperFurnace()); }


            // Запускаем все печи из списка
            foreach (CopperFurnace furnace in copperSmeltingControlSystem_1.FurnacesAtControl_list)
                furnace.MeltCopper();

            //Запускаем систему контроля
            copperSmeltingControlSystem_1.Start();

            // Создадим метод, который включим в событие TemperatureExceededThreshold
            void TemperatureExceeded_handler (CopperFurnace furnace)
            {
                furnace.Stop();
                //Console.WriteLine(furnace + " is overheated");
                Console.Write("\tOVERHEAT");
            }

            // Теперь подпишем созданный метод TemperatureExceeded_handler на событие TemperatureExceededThreshold
            copperSmeltingControlSystem_1.TemperatureExceededThreshold += TemperatureExceeded_handler;

        }


    }// class Program
}// namespace