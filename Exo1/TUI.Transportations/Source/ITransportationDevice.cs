using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUI.Transportations.Source
{
    public interface ITransportationDeviceKind: IStartingDeviceKind
    {
        Double KmsHourSpeedAverage { get; set; }
        Double Weight { get; set; }
        Double EngineFactor { get; set; }
    }
}
