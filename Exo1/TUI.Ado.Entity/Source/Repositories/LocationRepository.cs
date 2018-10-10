using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TUI.Places.Source;

namespace TUI.Ado.Entity.Source.Repositories
{
    internal class LocationRepository : TuiContextRepository<Location>
    {
        protected override DbSet<Location> GetDb()
        {
            return this.Context.Locations;
        }

        protected override IQueryable<Location> GetQueryable()
        {
            return this.Context.Locations;
        }
    }
}
