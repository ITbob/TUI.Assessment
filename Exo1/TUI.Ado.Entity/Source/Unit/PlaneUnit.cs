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
using TUI.Transportations.Air.Source;

namespace TUI.Ado.Entity.Source.Unit
{
    public class PlaneUnit : IUnit<Plane>
    {
        public ISession<Plane> GetSession()
        {
            return new TuiContextStringLessSession<Plane>(new PlaneRepository());
        }
    }
}
