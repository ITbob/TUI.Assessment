using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUI.Transportations.Source
{
    public interface IStartingDeviceKind
    {
        //required distance to reach average speed
        Double RequiredStartingDistance { get; set; }
        //strating effort may increase if it's a flight or train
        Double StartingEffort { get; set; }
    }
}
