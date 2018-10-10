
using TUI.Ado.Entity.Source.Repositories;
using TUI.Ado.Entity.Source.Session;
using TUI.Data.Access.Source.Session;
using TUI.Data.Access.Source.Unit;
using TUI.Places.Source;

namespace TUI.Ado.Entity.Source.Unit
{
    public class AirportUnit : IUnit<Airport>
    {
        public ISession<Airport> GetSession()
        {
            return new TuiContextStringLessSession<Airport>(new AirportRepository());
        }
    }
}
