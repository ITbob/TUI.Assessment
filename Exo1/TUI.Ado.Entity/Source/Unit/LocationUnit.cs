using TUI.Ado.Entity.Source.Repositories;
using TUI.Ado.Entity.Source.Session;
using TUI.Data.Access.Source.Session;
using TUI.Data.Access.Source.Unit;
using TUI.Places.Source;

namespace TUI.Ado.Entity.Source.Unit
{
    public class LocationUnit : IUnit<Location>
    {
        public ISession<Location> GetSession()
        {
            return new TuiContextStringLessSession<Location>(new LocationRepository());
        }
    }
}
