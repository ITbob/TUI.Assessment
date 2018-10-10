using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Gasoline.Source;
using TUI.Transportations.Source;

namespace TUI.Gasoline.Calculator.Source
{
    public static class ConsumptionCalculator
    {
        public static double GetAverageCoefficient(this ITransportationDeviceKind device, IGasKind gasKind)
        {
            return Math.Round(device.Weight*
                device.EngineFactor* 
                gasKind.GasConsumptionCoefficient* 
                device.KmsHourSpeedAverage,2);
        }

        public static double GetStartingCoefficient(this ITransportationDeviceKind device)
        {
            return Math.Round(device.Weight
                * device.StartingEffort
                * device.KmsHourSpeedAverage
                * device.EngineFactor
                * device.RequiredStartingDistance,2);
        }

        public static Double GetAverageConsumption(this ITransportationDeviceKind device, IGasKind gasKind, Double KmDistance)
        {
            return device.GetAverageCoefficient(gasKind) * KmDistance;
        }

        public static Double GetStartingConsumption(this ITransportationDeviceKind device, IGasKind gazKind)
        {
            return device.GetStartingCoefficient() * gazKind.GasConsumptionCoefficient;
        }

        public static Double GetTotalConsumption(this ITransportationDeviceKind device, IGasKind gazKind, Double KmDistance)
        {
            var starting = device.GetStartingConsumption(gazKind);
            var average = device.GetAverageConsumption(gazKind, KmDistance);

            return Math.Round(starting + average,2);
        }
    }
}
