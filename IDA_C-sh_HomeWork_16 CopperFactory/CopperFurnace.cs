using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDA_C_sh_HomeWork_16_CopperFactory
{
    internal class CopperFurnace
    {
        /// CTOR ///////////////////
       
        public CopperFurnace()
        { 
            temperatureSensor = new CopperTemperatureSensor(this);
            ID = ID_counter++;
        }


        /// PROPS ///////////////////
        public int ID { private set; get; }
        static int ID_counter = 1;
        const double COPPER_MELT_TEMPERATURE = 1083;
        public bool IsRunning { private set; get; } = false; //указывает, запущена ли печь.
        internal CopperTemperatureSensor temperatureSensor { private set; get; }
        public double TargetFurnanceTemperature { private set; get; } = 0;
        public double CurrentTemperature { private set; get; } = 0;
        Thread FurnaceThread;

        /// METHODS ///////////////////

        internal bool MeltCopper() 
        // запускает процесс переплавки меди в печи.
        // Метод запуска печи возращает экземпляр CopperSmeltingControlSystem в состоянии IsRunnig = true
        {
            if (IsRunning) throw new Exception(this + " is already running");
            IsRunning = true;

            TargetFurnanceTemperature = COPPER_MELT_TEMPERATURE;
            
            // Запускаем в отдельном потоке (потому что там есть sleep) метод имитации нагрева печи
            FurnaceThread = new Thread(FurnaceHeating);
            FurnaceThread.Start();

            return true;   
         }        
        void FurnaceHeating()
        // Метод имитирует разогрев и поддержание заданной температуры печи
        {                       
            
            int TimeStep = 500; // ms
            int TemperatureStep = 50; // degrees

            while(IsRunning)
            {
                if (CurrentTemperature < 0.9 * TargetFurnanceTemperature) CurrentTemperature += ServiceFunction.Get_Random(TemperatureStep);
                else CurrentTemperature = TargetFurnanceTemperature - ServiceFunction.Get_Random(-TemperatureStep / 2, TemperatureStep / 2);
                Thread.Sleep(TimeStep);
            }
        }
        internal void Stop()
        {
            IsRunning = false;

         /* CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            cancelTokenSource.Cancel();
            //  проверяем статус потока
            Console.WriteLine($"FurnaceThread Status: {FurnaceThread.IsAlive}");
            cancelTokenSource.Dispose(); // освобождаем ресурсы
            //FurnaceThread.Abort();*/
        }

        /// OVERRIDES ///////////////////
        public override string ToString()   {return "furnace_" + ID;}

    }//c
}//n
