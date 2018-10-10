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
    internal class CityRepository : TuiContextRepository<City>
    {

        protected override DbSet<City> GetDb()
        {
            return this.Context.Cities;
        }

        protected override IQueryable<City> GetQueryable()
        {
            return this.Context.Cities.Include(c => c.Location);
        }

        public override void SetModified(City element)
        {
            var originalCity = this.Context.Cities.Include(c => c.Location)
                .Single(c => c.Id == element.Id);

            //no other idea
            element.LocationId = originalCity.LocationId;
            element.Location.Id = originalCity.Location.Id;

            this.Context.Entry(originalCity).CurrentValues.SetValues(element);
            this.Context.Entry(originalCity.Location).CurrentValues.SetValues(element.Location);

            this.Context.Entry(originalCity).State = EntityState.Modified;
            this.Context.Entry(originalCity.Location).State = EntityState.Modified;

            this.OnOperated(new Data.Access.Source.OperationInfo()
            {
                Operation = OperationType.Update,
                obj = element
            });
        }
    }
}
