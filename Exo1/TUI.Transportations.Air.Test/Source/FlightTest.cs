using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Gasoline.Source;
using TUI.Places.Air.Source;
using TUI.Transportations.Air.Source;

namespace TUI.Transportations.Air.Test.Source
{
    [TestFixture]
    class FlightTest
    {

        private Flight _flight;

        [OneTimeSetUp]
        public void Setup()
        {
            var parisAirport = AirportFactory.GetAirport(AirportType.CharlesDeGaule);
            var newYorkAirport = AirportFactory.GetAirport(AirportType.JFK);

            var airbusKind = PlaneKindFactory.GetPlane(PlaneType.airbus);
            var gasKind = GasFactory.GetJetFuel();

            this._flight = new Flight()
            {
                DepartureAirport = parisAirport,
                ArrivalAirport = newYorkAirport,
                StartDate = new DateTime(2001, 6, 6),
                Plane = new Plane() { Kind = airbusKind, GasKind = gasKind },
            };
        }

        [Test]
        public void Should_Get_5844km_when_flight_between_cdg_jfk()
        {
            var distance = this._flight.GetDistanceKm();
            Assert.AreEqual(5844, Math.Round(distance,0));
        }

        [Test]
        public void Should_Get_961liters_when_flight_between_cdg_jfk()
        {
            var liters = this._flight.GetConsumption();
            Assert.AreEqual(961, Math.Round(liters));
        }


        [Test]
        public void Should_Get_12490euros_when_flight_between_cdg_jfk()
        {
            var price = this._flight.GetPrice();
            Assert.AreEqual(12490, Math.Round(price));
        }


        [Test]
        public void Should_Get_6hours_29minutes_3seconds_when_flight_between_cdg_jfk()
        {
            var duration = this._flight.GetDuration();
            Assert.AreEqual(new TimeSpan(0,6,29,36), 
                new TimeSpan(duration.Days,
                duration.Hours, 
                duration.Minutes, 
                duration.Seconds));
        }

        [Test]
        public void Should_Get_endtime_when_flight_between_cdg_jfk()
        {
            var date = this._flight.GetEndDateWithoutUtc();
            Assert.AreEqual(new DateTime(2001,06,06,6,29,36),
                new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second));
        }
    }
}
