using System.Collections.Generic;
using System.Web.Mvc;
using TUI.Data.Access.Source.Unit;
using TUI.Places.Source;
using TUI.Transportations.Air;
using TUI.Transportations.Air.Source;

namespace TUI.Sandbox.Controllers
{
    public class FlightsController : CrudController<Flight>
    {
        private readonly IUnit<Airport> _airportUnit;
        private readonly IUnit<Plane> _planeUnit;

        public FlightsController(IUnit<Flight> unit
            , IUnit<Airport> airportUnit
            , IUnit<Plane> planeUnit) : base(unit)
        {
            this._airportUnit = airportUnit;
            this._planeUnit = planeUnit;
        }

        protected override void SetViewBagDependencies(Flight item)
        {
            using (var session = this._airportUnit.GetSession())
            {
                var airports = session.GetRepository().GetAll();
                var arrivals = new List<SelectListItem>();
                foreach (var airport in airports)
                {
                    arrivals.Add(new SelectListItem()
                    {
                        Value = airport.Id.ToString(),
                        Text = airport.Description,
                        Selected = airport.Id == item.ArrivalId
                    });
                }

                var departures = new List<SelectListItem>();
                foreach (var airport in airports)
                {
                    departures.Add(new SelectListItem()
                    {
                        Value = airport.Id.ToString(),
                        Text = airport.Description,
                        Selected = airport.Id == item.ArrivalId
                    });
                }

                ViewBag.Departures = arrivals;
                ViewBag.Arrivals = departures;
            }

            using (var session = this._planeUnit.GetSession())
            {
                var planes = new List<SelectListItem>();
                foreach (var plane in session.GetRepository().GetAll())
                {
                    planes.Add(new SelectListItem()
                    {
                        Value = plane.Id.ToString(),
                        Text = plane.Description,
                        Selected = plane.Id == item.PlaneId
                    });
                }
                ViewBag.Planes = planes;
            }
        }

        protected override void SetViewBagDependencies()
        {
            using (var session = this._airportUnit.GetSession())
            {
                var airports = session.GetRepository().GetAll();
                ViewBag.Departures = new SelectList(airports, "Id", "Description");
                ViewBag.Arrivals = new SelectList(airports, "Id", "Description");
            }

            using (var session = this._planeUnit.GetSession())
            {
                ViewBag.Planes = new SelectList(session.GetRepository().GetAll(), "Id", "Description");
            }
        }

        // https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([Bind(Include = "Id,StartDate,PlaneId,DepartureId,ArrivalId")] Flight item)
        {
            return base.Create(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit([Bind(Include = "Id,StartDate,PlaneId,DepartureId,ArrivalId")] Flight item)
        {
            return base.Edit(item);
        }


    }
}