using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TUI.Ado.Entity.Source;
using TUI.Ado.Entity.Source.Factory;
using TUI.Ado.Entity.Source.Unit;
using TUI.Places.Source;
using TUI.Sandbox;
using TUI.Sandbox.Controllers;
using TUI.Transportations.Air;

namespace TUI.Sandbox.Test.Source
{
    [TestFixture]
    public class HomeControllerTest
    {

        private HomeController _controller;

        [TestFixtureSetUp]
        public void Setup()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
            var connection = ConfigurationManager.ConnectionStrings["TUITest"].ToString();
            var tui = new TuiContext(connection);
            tui.Database.Delete();

            var airportUnit = new TuiContextUnit<Airport>(connection, RepoFactory.GetTuiContextRepo<Airport>());
            var flightUnit = new TuiContextUnit<Flight>(connection, RepoFactory.GetTuiContextRepo<Flight>());
            this._controller = new HomeController(airportUnit, flightUnit);

        }

        [Test]
        public void Should_Get_Home_Index_Page()
        {
            ViewResult result = this._controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void Should_Get_Error_Page()
        {
            RedirectToRouteResult routeResult = this._controller.GetFlights(new Models.FlightSearch()) as RedirectToRouteResult;
            var value = routeResult.RouteValues["Notification"];
            Assert.AreEqual("Sorry, departure is not found.", value);
        }
    }
}
