using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TUI.Places.Source;
using TUI.Report.Source;

namespace TUI.Ado.Entity.Source.Repositories
{
    internal class AirportRepository : TuiContextRepository<Airport>
    {
        protected override DbSet<Airport> GetDb()
        {
            return this.Context.Airports;
        }

        protected override IQueryable<Airport> GetQueryable()
        {
            return this.Context.Airports.Include(a => a.City.Location)
                .Include(a => a.Location);
        }

        public override void SetModified(Airport element)
        {
            var originalAirport = this.Context.Airports.Include(c => c.Location)
                .Single(c => c.Id == element.Id);

            //no other idea
            element.LocationId = originalAirport.LocationId;
            element.Location.Id = originalAirport.Location.Id;

            this.Context.Entry(originalAirport).CurrentValues.SetValues(element);
            this.Context.Entry(originalAirport.Location).CurrentValues.SetValues(element.Location);

            this.Context.Entry(originalAirport).State = EntityState.Modified;
            this.Context.Entry(originalAirport.Location).State = EntityState.Modified;

            this.OnOperated(new Data.Access.Source.OperationInfo()
            {
                Operation = OperationType.Update,
                obj = element
            });
        }

    }
}
