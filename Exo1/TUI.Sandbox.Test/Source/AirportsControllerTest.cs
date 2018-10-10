using NUnit.Framework;
using TUI.Sandbox.Controllers;
using System.Web.Mvc;
using System.Configuration;
using System;
using TUI.Places.Source;
using TUI.Ado.Entity.Source.Unit;
using TUI.Ado.Entity.Source;
using TUI.Ado.Entity.Source.Factory;

namespace TUI.Sandbox.Test.Source
{
    [TestFixture]
    class AirportsControllerTest
    {
        private AirportsController _controller;
        [TestFixtureSetUp]
        public void Setup()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
            var connection = ConfigurationManager.ConnectionStrings["TUITest"].ToString();
            var tui = new TuiContext(connection);
            tui.Database.Delete();

            var cityUnit = new TuiContextUnit<City>(connection, RepoFactory.GetTuiContextRepo<City>());
            var airportUnit = new TuiContextUnit<Airport>(connection, RepoFactory.GetTuiContextRepo<Airport>());

            this._controller =
                new AirportsController(airportUnit, cityUnit);
        }

        [Test]
        public void Should_Get_Airport_Index_Page()
        {
            ViewResult result = this._controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void Should_Get_Airport_Create_Page()
        {
            ViewResult result = this._controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void Should_Get_Airport_Details_Page()
        {
            RedirectToRouteResult routeResult = this._controller.Details(1) as RedirectToRouteResult;
            var value = routeResult.RouteValues["Notification"];
            Assert.AreEqual("Sorry, Airport 1 is not found.", value);
        }

        [Test]
        public void Should_Get_Airport_Edit_Page()
        {
            RedirectToRouteResult routeResult = this._controller.Edit(1) as RedirectToRouteResult;
            var value = routeResult.RouteValues["Notification"];
            Assert.AreEqual("Sorry, Airport 1 is not found.", value);
        }

        [Test]
        public void Should_Get_Airport_Delete_Page()
        {
            RedirectToRouteResult routeResult = this._controller.Delete(1) as RedirectToRouteResult;
            var value = routeResult.RouteValues["Notification"];
            Assert.AreEqual("Sorry, Airport 1 is not found.", value);
        }
    }
}
