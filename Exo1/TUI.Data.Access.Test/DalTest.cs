using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TUI.Places.Source;
using TUI.Transportations.Air;
using TUI.Transportations.Air.Source;
using System.Configuration;
using TUI.Gasoline.Source;
using TUI.Data.Access.Source.Unit;
using TUI.Places.Air.Source;
using System.Data.Entity;
using TUI.Data.Access.Source.Unit.Tracker;
using TUI.Report.Source;
using TUI.Ado.Entity.Source.Unit;
using TUI.Ado.Entity.Source;
using TUI.Ado.Entity.Source.Factory;

namespace TUI.Data.Access.Test
{
    [TestFixture]
    class DalTest
    {

        private IUnit<Flight> _flightUnit;
        private IUnit<Airport> _airportUnit;
        private IUnit<City> _cityUnit;
        private IUnit<City> _cityUniTracker;
        private IUnit<HistoryLine> _historyLineUnit;
        private String _connection;

        [SetUp]
        public void Setup()
        {
            this._connection = ConfigurationManager.ConnectionStrings["TUITest"].ToString();
            var tui = new TuiContext(this._connection);
            tui.Database.Delete();

            this._flightUnit = new TuiContextUnit<Flight>(this._connection, RepoFactory.GetTuiContextRepo<Flight>());
            this._airportUnit = new TuiContextUnit<Airport>(this._connection, RepoFactory.GetTuiContextRepo<Airport>());
            this._cityUnit = new TuiContextUnit<City>(this._connection, RepoFactory.GetTuiContextRepo<City>());
            this._historyLineUnit = new TuiContextUnit<HistoryLine>(this._connection, RepoFactory.GetTuiContextRepo<HistoryLine>());
            this._cityUniTracker = new UnitTracker<City>(this._cityUnit, this._historyLineUnit);
        }

        [Test]
        public void Should_Flight_list_be_empty()
        {
            using (var session = this._flightUnit.GetSession())
            {
                var flightsInfo = session.GetRepository().GetAll();
                Assert.AreEqual(0, flightsInfo.Count());
            }
        }

        [Test]
        public void Should_Get_Two_Cities()
        {
            using (var session = this._cityUnit.GetSession())
            {
                var citiesRepo = session.GetRepository();
                var citiesCount = citiesRepo.GetAll().Count();
                var paris = CityFactory.GetCity(CityType.Paris);
                var newYork = CityFactory.GetCity(CityType.NewYork);


                citiesRepo.AddRange(new[] { newYork, paris });
                session.Complete();

                Assert.AreEqual(citiesCount+2, citiesRepo.GetAll().Count());
            }
        }

        [Test]
        public void Should_Get_Three_More_Flights()
        {
            using (var flightSession = this._flightUnit.GetSession())
            {
                using (var citySession = this._cityUnit.GetSession())
                {
                    using (var airportSession = this._airportUnit.GetSession())
                    {
                        var fightRepo = flightSession.GetRepository();
                        var flightsInfo = fightRepo.GetAll();

                        var aiportRepo = airportSession.GetRepository();

                        var parisAirport = AirportFactory.GetAirport(AirportType.CharlesDeGaule);
                        var newYorkAirport = AirportFactory.GetAirport(AirportType.JFK);
                        var berlinAirport = AirportFactory.GetAirport(AirportType.berlin);

                        var airports = new[] { parisAirport, newYorkAirport, berlinAirport };

                        var citiesRepo = citySession.GetRepository();
                        citiesRepo.AddRange(airports.Select(ap => ap.City));
                        citySession.Complete();

                        aiportRepo.AddRange(airports);
                        airportSession.Complete();

                        var airbusKind = PlaneKindFactory.GetPlane(PlaneType.airbus);
                        var gasKind = GasFactory.GetJetFuel();


                        var flights = new List<Flight>()
                        {
                            new Flight(){
                                DepartureAirport = parisAirport,
                                ArrivalAirport= newYorkAirport,
                                StartDate =  new DateTime(2001, 6, 6),
                                Plane = new Plane() { Kind = airbusKind, GasKind = gasKind },
                            },
                            new Flight(){
                                DepartureAirport = newYorkAirport,
                                ArrivalAirport = berlinAirport,
                                StartDate = new DateTime(2001, 6, 6),
                                Plane = new Plane() { Kind = airbusKind, GasKind = gasKind },
                            },
                            new Flight(){
                                DepartureAirport = parisAirport,
                                ArrivalAirport = berlinAirport,
                                StartDate = new DateTime(2001, 6, 6),
                                Plane = new Plane() { Kind = airbusKind, GasKind = gasKind },
                            }
                        };

                        fightRepo.AddRange(flights);
                        flightSession.Complete();
                        var result = fightRepo.GetAll();

                        Assert.AreEqual(flightsInfo.Count() + 3, result.Count());
                    }
                }
            }
        }

        [Test]
        public void Should_Get_One_Recorded_city_in_report_history_when_add_city_from_tracking_unit()
        {
            using(var session = this._cityUniTracker.GetSession())
            {
                var repo = session.GetRepository();
                repo.Add(CityFactory.GetCity(CityType.Melbourne));
                session.Complete();
            }

            using (var session = this._historyLineUnit.GetSession())
            {
                var historyLineRepo = session.GetRepository();
                var historyLines = historyLineRepo.GetAll();

                Assert.AreEqual(1, historyLines.Count());
                Assert.AreEqual(typeof(City).Name, historyLines.First().DateType);
                Assert.AreEqual(OperationType.Create, historyLines.First().Operation);
            }
        }

        [Test]
        public void Should_initialise_correctly()
        {
            Database.SetInitializer<TuiContext>(new TuiInitializer());
            var tui = new TuiContext(this._connection);
            tui.Database.Initialize(false);

            Assert.AreEqual(10, tui.Cities.Count());
            Assert.AreEqual(13, tui.Airports.Count());
            Assert.AreEqual(4, tui.Flights.Count());
            Assert.AreEqual(1, tui.Users.Count());
        }


    }
}
