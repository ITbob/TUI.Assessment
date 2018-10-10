using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Gasoline.Source;
using TUI.Places.Air.Source;
using TUI.Places.Source;
using TUI.Transportations.Air;
using TUI.Transportations.Air.Source;

namespace TUI.Ado.Entity.Source
{
    public class TuiInitializer:System.Data.Entity.CreateDatabaseIfNotExists<TuiContext> 
    {
        //the database context object as an input parameter
        protected override void Seed(TuiContext context)
        {
            var cities = new List<City>();
            cities.AddRange(new[]
            {
                CityFactory.GetCity(CityType.NewYork),
                CityFactory.GetCity(CityType.Paris),
                CityFactory.GetCity(CityType.Berlin),
                CityFactory.GetCity(CityType.NewJersey),
                CityFactory.GetCity(CityType.Shanghai),
                CityFactory.GetCity(CityType.Beijing),
                CityFactory.GetCity(CityType.Seoul),
                CityFactory.GetCity(CityType.BuenosAires),
                CityFactory.GetCity(CityType.Calcutta),
                CityFactory.GetCity(CityType.Melbourne)
            }
        );

            cities.ForEach(s => context.Cities.Add(s));
    
            var airports = new List<Airport>();
            airports.Add(AirportFactory.GetAirport(AirportType.Orly));
            airports.Add(AirportFactory.GetAirport(AirportType.CharlesDeGaule));
            airports.Add(AirportFactory.GetAirport(AirportType.JFK));
            airports.Add(AirportFactory.GetAirport(AirportType.berlin));
            airports.Add(AirportFactory.GetAirport(AirportType.Newwark));
            airports.Add(AirportFactory.GetAirport(AirportType.BuenosAires));
            airports.Add(AirportFactory.GetAirport(AirportType.Pudong));
            airports.Add(AirportFactory.GetAirport(AirportType.Hongqiao));
            airports.Add(AirportFactory.GetAirport(AirportType.Netaji));
            airports.Add(AirportFactory.GetAirport(AirportType.Melbourne));
            airports.Add(AirportFactory.GetAirport(AirportType.Beijing));
            airports.Add(AirportFactory.GetAirport(AirportType.Incheon));
            airports.Add(AirportFactory.GetAirport(AirportType.Gimpo));

            //overwrite city reference
            foreach (var airport in airports)
            {
                if(cities.Any(c => airport.City.Name == c.Name))
                {
                    airport.City = cities.Find(c => airport.City.Name == c.Name);
                }
            }

            airports.ForEach(s => context.Airports.Add(s));

            var gasKind = GasFactory.GetJetFuel();
            var airbusKind = PlaneKindFactory.GetPlane(PlaneType.airbus);
            var boeingKind = PlaneKindFactory.GetPlane(PlaneType.boeing);

            var flights = new List<Flight>();

            flights.Add(new Flight()
            {
                Plane = new Plane() { Kind = airbusKind, GasKind = gasKind },
                DepartureAirport = airports[0],
                ArrivalAirport = airports[3],
                StartDate = new DateTime(2001, 6, 6, 10, 15, 0)
            });

            flights.Add(new Flight()
            {
                Plane = new Plane() { Kind = boeingKind, GasKind = gasKind },
                DepartureAirport = airports[2],
                ArrivalAirport = airports[7],
                StartDate = new DateTime(2001, 6, 18, 10, 35, 0)
            });

            flights.Add(new Flight()
            {
                Plane = new Plane() { Kind = airbusKind, GasKind = gasKind },
                DepartureAirport = airports[0],
                ArrivalAirport = airports[3],
                StartDate = new DateTime(2001, 6, 9, 13, 15, 0)
            });

            flights.Add(new Flight()
            {
                Plane = new Plane() { Kind = boeingKind, GasKind = gasKind },
                DepartureAirport = airports[2],
                ArrivalAirport = airports[1],
                StartDate = new DateTime(2001, 6,8, 11, 15, 0)
            });

            flights.ForEach(s => context.Flights.Add(s));
            context.SaveChanges();

            context.Users.Add(new Login.source.User() { Name = "bob", Password = "bob" });
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
