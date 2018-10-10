using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TUI.Ado.Entity.Source.Factory;
using TUI.Ado.Entity.Source.Unit;
using TUI.Places.Source;
using TUI.Sandbox.Controllers;
using TUI.Transportations.Air;
using TUI.Transportations.Air.Source;

namespace TUI.Sandbox.Test.Source
{
    [TestFixture]
    class FlightsControllerTest
    {
        private FlightsController _controller;

        [TestFixtureSetUp]
        public void Setup()
        {

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
            var connection = ConfigurationManager.ConnectionStrings["TUITest"].ToString();

            var flightUnit = new TuiContextUnit<Flight>(connection, RepoFactory.GetTuiContextRepo<Flight>());
            var planeUnit = new TuiContextUnit<Plane>(connection, RepoFactory.GetTuiContextRepo<Plane>());
            var airportUnit = new TuiContextUnit<Airport>(connection, RepoFactory.GetTuiContextRepo<Airport>());

            this._controller =
                new FlightsController(flightUnit, airportUnit, planeUnit);
        }

        [Test]
        public void Should_Get_Flight_Index_Page()
        {
            ViewResult result = this._controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void Should_Get_Flight_Create_Page()
        {
            ViewResult result = this._controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }


        [Test]
        public void Should_Get_Flight_Details_Page()
        {
            RedirectToRouteResult routeResult = this._controller.Details(1) as RedirectToRouteResult;
            var value = routeResult.RouteValues["Notification"];
            Assert.AreEqual("Sorry, Flight 1 is not found.", value);
        }

        [Test]
        public void Should_Get_Flight_Edit_Page()
        {
            RedirectToRouteResult routeResult = this._controller.Edit(1) as RedirectToRouteResult;
            var value = routeResult.RouteValues["Notification"];
            Assert.AreEqual("Sorry, Flight 1 is not found.", value);
        }

        [Test]
        public void Should_Get_Flight_Delete_Page()
        {
            RedirectToRouteResult routeResult = this._controller.Delete(1) as RedirectToRouteResult;
            var value = routeResult.RouteValues["Notification"];
            Assert.AreEqual("Sorry, Flight 1 is not found.", value);
        }
    }
}
