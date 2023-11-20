using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace IDA_C_sh_HomeWork_16_CopperFactory
{
    internal class CopperSmeltingControlSystem
    {
        /// PROPS ///////////////////

        public List<CopperFurnace> FurnacesAtControl_list {  get; set; } = new List<CopperFurnace>();
        public void AddFurnaceToControlSystem(CopperFurnace furnace)
        {
            FurnacesAtControl_list.Add(furnace);
        }
        public double AlarmTemperature { get; private set; } = 1100;
        public bool IsRunning { private set; get; } = false; //указывает, запущена ли система контроля переплавки меди.
        Thread TemperatureMonitoringThread;

        /// METHODS ///////////////////

        public bool Start() // запускает систему контроля.
        {
            // Запускаем в отдельном потоке зацикленный метод опроса и контроля температуры
            int MonitoringTimeStep = 100;
            TemperatureMonitoringThread = new Thread(TemperatureMonitoring);
            TemperatureMonitoringThread.Start(MonitoringTimeStep);

            return IsRunning = true;
        }
        public bool Stop() // останавливает систему контроля
        {  return IsRunning = false; }
        public void TemperatureMonitoring(object? timestep)
        {
            int request_number = 0;
            while (true)
            {
                Thread.Sleep(Convert.ToInt32(timestep));
                Console.Clear();
                Console.WriteLine("monitor request " + (request_number++) + "\n");
                foreach (CopperFurnace furnace in FurnacesAtControl_list)
                {
                    double current_temperature = furnace.temperatureSensor.GetTemperature();
                    Console.Write(furnace + "\tT = " + string.Format("{0:f2}" + "\tstate: {1}", current_temperature, (furnace.IsRunning ? "running" : "stopped")));
                    if (current_temperature >= AlarmTemperature) TemperatureExceededThreshold(furnace);
                    Console.WriteLine();
                }
            }
        }

        /// EVENTS ///////////////////

        // Это событие создано чтобы:
        // 1) Оповещать о перегреве печи
        // 2) Отключать перегретую печь
        public event Action<CopperFurnace> TemperatureExceededThreshold = delegate { }; // возникает, если температура переплавки превысила установленный порог.
    }
}
