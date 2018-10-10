using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Report.Source;

namespace TUI.Ado.Entity.Source.Repositories
{
    internal class HistoryLineRepository : TuiContextRepository<HistoryLine>
    {
        protected override DbSet<HistoryLine> GetDb()
        {
            return this.Context.History;
        }

        protected override IQueryable<HistoryLine> GetQueryable()
        {
            return this.Context.History;
        }
    }
}
