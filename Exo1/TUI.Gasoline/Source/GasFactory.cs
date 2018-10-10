using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUI.Gasoline.Source
{
    public static class GasFactory
    {
        public static GasKind GetJetFuel()
        {
            return new GasKind("JetFuel", 0.00000085, 13);
        }
    }
}
