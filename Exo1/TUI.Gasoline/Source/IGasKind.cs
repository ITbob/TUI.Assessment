using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUI.Gasoline.Source
{
    public interface IGasKind
    {
        Double GasConsumptionCoefficient { get; }
    }
}
