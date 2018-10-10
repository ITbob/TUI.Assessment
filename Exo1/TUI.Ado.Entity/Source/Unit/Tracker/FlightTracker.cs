using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Data.Access.Source.Unit.Tracker;
using TUI.Transportations.Air;

namespace TUI.Ado.Entity.Source.Unit.Tracker
{
    public class FlightTracker : UnitTracker<Flight>
    {
        public FlightTracker() : base(new FlightUnit(), new HistoryUnit())
        {

        }
    }
}
