using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.NUnit2;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Data.Entity;
using TUI.Ado.Entity.Source;
using TUI.Ado.Entity.Source.Factory;
using TUI.Ado.Entity.Source.Unit;
using TUI.Data.Access.Source.Unit;
using TUI.Places.Source;
using TUI.Sandbox.Controllers;
using TUI.Transportations.Air;

namespace TUI.Sandbox.Test.Source.Scenarios
{
    partial class HomeControllerFeatures 
    {
        private String _connection;
        private HomeController _controller;

        private IUnit<City> _cityUnit;
        private IUnit<Airport> _airportUnit;
        private IUnit<Flight> _flightUnit;


        [SetUp]
        public void SetUp()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<TuiContext>());

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
            this._connection = ConfigurationManager.ConnectionStrings["TUITest"].ToString();
            var tui = new TuiContext(this._connection);
            tui.Database.Delete();

            this._cityUnit = new TuiContextUnit<City>(this._connection, RepoFactory.GetTuiContextRepo<City>());
            this._airportUnit = new TuiContextUnit<Airport>(this._connection, RepoFactory.GetTuiContextRepo<Airport>());
            this._flightUnit = new TuiContextUnit<Flight>(this._connection, RepoFactory.GetTuiContextRepo<Flight>());

            this._controller = new HomeController(this._airportUnit, this._flightUnit);
        }

        [Test]
        [Scenario]
        [Label("Check to find airport from string")]
        public void Find_airport_from_partial_name_and_home_page()
        {
            Runner.RunScenario(
                given => An_empty_city_List(),
                given => An_empty_airport_List(),
                given => Add_Pudong_and_CDG_aiports(),
                then => Get_CDC_airport_from_pa_value(),
                then => Get_pudong_airport_from_sh_value(),
                and => Get_nothing_from_fwggw_value()
            );
        }

        [Test]
        [Scenario]
        [Label("Check to find airport from string")]
        public void Find_an_flight_from_two_airports()
        {
            Runner.RunScenario(
                given => An_empty_city_List(),
                given => An_empty_airport_List(),
                given => An_empty_flight_List(),
                then => Add_a_flight(),
                then => Dont_get_flight_because_dont_find_airport(),
                then => Dont_get_flight_because_wrong_date(),
                then => Get_flight_without_date(),
                and => Get_flight_because_with_correct_date()
            );
        }
    }
}
