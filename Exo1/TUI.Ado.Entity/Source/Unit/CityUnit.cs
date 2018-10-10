using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Ado.Entity.Source.Repositories;
using TUI.Ado.Entity.Source.Session;
using TUI.Data.Access.Source.Repositories;
using TUI.Data.Access.Source.Session;
using TUI.Data.Access.Source.Unit;
using TUI.Places.Source;

namespace TUI.Ado.Entity.Source.Unit
{
    public class CityUnit : IUnit<City>
    {
        public ISession<City> GetSession()
        {
            return new TuiContextStringLessSession<City>(new CityRepository());
        }
    }
}
