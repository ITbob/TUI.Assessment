using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TUI.Transportations.Air.Source;

namespace TUI.Ado.Entity.Source.Repositories
{
    internal class PlaneRepository : TuiContextRepository<Plane>
    {

        protected override DbSet<Plane> GetDb()
        {
            return this.Context.Planes;
        }

        protected override IQueryable<Plane> GetQueryable()
        {
            return this.Context.Planes.Include(p => p.GasKind).Include(p => p.Kind);
        }
    }
}
