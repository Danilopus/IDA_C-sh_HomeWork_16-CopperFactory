using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDA_C_sh_HomeWork_16_CopperFactory
{
    internal class CopperTemperatureSensor : ITemperatureSensor
    {
        public CopperFurnace AttachedFurnance { private set; get; }
        public CopperTemperatureSensor(CopperFurnace furnace)
        {
            AttachedFurnance = furnace;
        }
        public double GetTemperature()
        {
           return AttachedFurnance.CurrentTemperature;
        }
    }
}
