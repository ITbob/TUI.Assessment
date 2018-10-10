using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TUI.Places.Source;
using TUI.Transportations.Air;

namespace TUI.Ado.Entity.Source.Repositories
{
    internal class FlightRepository : TuiContextRepository<Flight>
    {
        protected override DbSet<Flight> GetDb()
        {
            return this.Context.Flights;
        }

        protected override IQueryable<Flight> GetQueryable()
        {
            return this.Context.Flights.Include(x => x.ArrivalAirport)
                    .Include(x => x.ArrivalAirport.City)
                    .Include(x => x.ArrivalAirport.Location)
                    .Include(x => x.DepartureAirport)
                    .Include(x => x.DepartureAirport.City)
                    .Include(x => x.DepartureAirport.Location)
                    .Include(x => x.Plane)
                    .Include(x => x.Plane.GasKind)
                    .Include(x => x.Plane.Kind);
        }

        public override void Add(Flight element)
        {
            //no other idea
            this.SetForeignField(element);
            base.Add(element);
        }

        public override void SetModified(Flight element)
        {
            this.SetForeignField(element);
            base.SetModified(element);
        }

        private void SetForeignField(Flight element)
        {
            if (this.Context.Airports.Include(c => c.Location).Any(a => a.Id == element.ArrivalId))
            {
                element.ArrivalAirport = this.Context.Airports.Include(c => c.Location).Single(a => a.Id == element.ArrivalId);
            }

            if (this.Context.Airports.Include(c => c.Location).Any(a => a.Id == element.DepartureId))
            {
                element.DepartureAirport = this.Context.Airports.Include(c => c.Location).Single(a => a.Id == element.DepartureId);
            }
        }
    }
}
