using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using TUI.Data.Access.Source.Unit;
using TUI.Places.Source;

namespace TUI.Sandbox.Controllers
{
    public class AirportsController : CrudController<Airport>
    {
        private readonly IUnit<City> _cityUnit;


        public AirportsController(IUnit<Airport> unit, IUnit<City> cityUnit) :base(unit)
        {
            this._cityUnit = cityUnit;
        }

        protected override void SetViewBagDependencies()
        {
            using (var session = this._cityUnit.GetSession())
            {
                ViewBag.Cities = new SelectList(
                    session.GetRepository().GetAll(), "Id", "Name");
            }
        }

        protected override void SetViewBagDependencies(Airport item)
        {
            using (var session = this._cityUnit.GetSession())
            {
                var items = new List<SelectListItem>();
                foreach (var city in session.GetRepository().GetAll())
                {
                    items.Add(new SelectListItem()
                    {
                        Value = city.Id.ToString(),
                        Text = city.Name,
                        Selected = city.Id == item.CityId
                    });
                }

                ViewBag.Cities = items;
            }
        }


        // https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Create([Bind(Include = "Id,Latitude,Longitude,Name,CityId")] Airport item)
        {
            return base.Create(item);
        }

        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Edit([Bind(Include = "Id,Latitude,Longitude,Name,CityId")] Airport item)
        {
            return base.Edit(item);
        }


    }
}