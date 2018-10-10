
using TUI.Ado.Entity.Source.Repositories;
using TUI.Ado.Entity.Source.Session;
using TUI.Data.Access.Source.Session;
using TUI.Data.Access.Source.Unit;
using TUI.Transportations.Air;

namespace TUI.Ado.Entity.Source.Unit
{
    public class FlightUnit : IUnit<Flight>
    {
        public ISession<Flight> GetSession()
        {
            return new TuiContextStringLessSession<Flight>(new FlightRepository());
        }
    }
}
