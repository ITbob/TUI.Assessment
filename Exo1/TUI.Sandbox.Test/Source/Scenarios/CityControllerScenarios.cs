using LightBDD.Framework;
using LightBDD.Framework.Scenarios.Extended;
using LightBDD.NUnit2;
using NUnit.Framework;
using System;
using System.Configuration;
using TUI.Ado.Entity.Source;
using TUI.Ado.Entity.Source.Factory;
using TUI.Ado.Entity.Source.Unit;
using TUI.Data.Access.Source.Unit;
using TUI.Places.Source;
using TUI.Sandbox.Controllers;

[assembly: LightBddScopeAttribute]
namespace TUI.Sandbox.Test.Source.Scenarios
{
    [TestFixture]
    [FeatureDescription(@"city controller")]
    [Label("test city controller features")]
    public partial class CityControllerFeatures
    {
        private String _connection;
        private IUnit<City> _cityUnit;
        private CitiesController _controller;
        private City _model;

        [SetUp]
        public void SetUp()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
            this._connection = ConfigurationManager.ConnectionStrings["TUITest"].ToString();
            var tui = new TuiContext(this._connection);
            tui.Database.Delete();

            this._cityUnit = new TuiContextUnit<City>(this._connection, RepoFactory.GetTuiContextRepo<City>());
            var cityUnit = new TuiContextUnit<City>(this._connection, RepoFactory.GetTuiContextRepo<City>());
            var locationUnit = new TuiContextUnit<Location>(this._connection, RepoFactory.GetTuiContextRepo<Location>());

            this._controller = new CitiesController(cityUnit, locationUnit);
        }

        [Test]
        [Scenario]
        [Label("Add added city from details page")]
        public void Check_added_city_from_details_page()
        {
            Runner.RunScenario(
                given => An_empty_city_List(),
                and => Add_element_from_create_page(),
                then => Check_details_of_the_added_city()
                );
        }

        [Test]
        [Scenario]
        [Label("Check a undefined city")]
        public void Check_undefined_city_from_details_page()
        {
            Runner.RunScenario(
                given => An_empty_city_List(),
                then => Check_details_of_the_undefined_city()
                );
        }

        [Test]
        [Scenario]
        [Label("Check two added city from list page")]
        public void Add_two_cities_then_check_list_page()
        {
            Runner.RunScenario(
                given => An_empty_city_List(),
                and => Add_element_from_create_page(),
                and => Add_element_from_create_page(),
                then => Check_list_page_has_two_cities()
                );
        }

        [Test]
        [Scenario]
        [Label("Check two added cities then one deleted city from list page")]
        public void Add_two_cities_then_delete_one_city_then_check_list_page()
        {
            Runner.RunScenario(
                given => An_empty_city_List(),
                and => Add_element_from_create_page(),
                and => Add_element_from_create_page(),
                then => Check_list_page_has_two_cities(),
                and => Delete_first_element(),
                then => Check_list_page_has_one_city()
                );
        }

        [Test]
        [Scenario]
        [Label("add element then edit element then check element")]
        public void Add_a_city_then_edit_it_then_check_change()
        {
            Runner.RunScenario(
                given => An_empty_city_List(),
                and => Add_element_from_create_page(),
                and => Edit_added_element(),
                then => Check_details_of_the_edited_city()
                );
        }
    }
}
