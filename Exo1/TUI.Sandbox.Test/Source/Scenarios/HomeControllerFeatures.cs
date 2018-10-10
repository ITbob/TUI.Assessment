
using LightBDD.NUnit2;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using TUI.Gasoline.Source;
using TUI.Places.Air.Source;
using TUI.Places.Source;
using TUI.Sandbox.Models;
using TUI.Transportations.Air;
using TUI.Transportations.Air.Source;

namespace TUI.Sandbox.Test.Source.Scenarios
{
    partial class HomeControllerFeatures : FeatureFixture
    {
        private Airport _cdc;
        private Airport _pudong;
        private Flight _flight;

        private void An_empty_city_List()
        {
            using (var session = this._cityUnit.GetSession())
            {
                //Arrange
                var cityRepo = session.GetRepository();

                // Act
                cityRepo.RemoveRange(cityRepo.GetAll());
                session.Complete();
                // Assert
                Assert.AreEqual(0, cityRepo.GetAll().Count());
            }
        }

        private void An_empty_airport_List()
        {
            using (var session = this._airportUnit.GetSession())
            {
                //Arrange
                var airportRepo = session.GetRepository();

                // Act
                airportRepo.RemoveRange(airportRepo.GetAll());

                // Assert
                Assert.AreEqual(0, airportRepo.GetAll().Count());
            }
        }

        private void An_empty_flight_List()
        {
            using (var session = this._flightUnit.GetSession())
            {
                //Arrange
                var flightRepo = session.GetRepository();

                // Act
                flightRepo.RemoveRange(flightRepo.GetAll());

                // Assert
                Assert.AreEqual(0, flightRepo.GetAll().Count());
            }
        }

        private void Add_Pudong_and_CDG_aiports()
        {
            using(var session = this._airportUnit.GetSession())
            {
                var repo = session.GetRepository();

                this._cdc = AirportFactory.GetAirport(AirportType.CharlesDeGaule);
                this._pudong = AirportFactory.GetAirport(AirportType.Pudong);

                repo.Add(this._cdc);
                repo.Add(this._pudong);

                session.Complete();
            }
        }

        private object GetReflectedProperty(object obj, string propertyName)
        {
            PropertyInfo property = obj.GetType().GetProperty(propertyName);

            if (property == null)
            {
                return null;
            }

            return property.GetValue(obj, null);
        }

        private void Get_CDC_airport_from_pa_value()
        {
            //Arrange
            JsonResult result = null;

            // Act
            result = this._controller.GetAirports("pa") as JsonResult;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IList>(result.Data);
            foreach (var data in (IList)result.Data)
            {
                Assert.AreEqual(this.GetReflectedProperty(data,"Id"), this._cdc.Id);
                Assert.AreEqual(this.GetReflectedProperty(data, "Name"), this._cdc.Name);
                Assert.AreEqual(this.GetReflectedProperty(data, "City"), this._cdc.City.Name);
            }

        }

        private void Get_pudong_airport_from_sh_value()
        {
            //Arrange
            JsonResult result = null;

            // Act
            result = this._controller.GetAirports("sh") as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IList>(result.Data);
            foreach (var data in (IList)result.Data)
            {
                Assert.AreEqual(this.GetReflectedProperty(data, "Id"), this._pudong.Id);
                Assert.AreEqual(this.GetReflectedProperty(data, "Name"), this._pudong.Name);
                Assert.AreEqual(this.GetReflectedProperty(data, "City"), this._pudong.City.Name);
            }
        }

        private void Get_nothing_from_fwggw_value()
        {
            //Arrange
            JsonResult result = null;

            // Act
            result = this._controller.GetAirports("fwggw") as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IList>(result.Data);
            Assert.IsEmpty((IList)result.Data);
        }

        private void Add_a_flight()
        {
            using (var session = this._flightUnit.GetSession())
            {
                var repo = session.GetRepository();

                this._cdc = AirportFactory.GetAirport(AirportType.CharlesDeGaule);
                this._pudong = AirportFactory.GetAirport(AirportType.Pudong);

                this._flight = new Flight()
                {
                    DepartureAirport = this._pudong,
                    ArrivalAirport = this._cdc,
                    StartDate = new DateTime(2016, 05, 03, 10, 45, 0),
                    Plane = new Plane()
                    {
                        GasKind = GasFactory.GetJetFuel(),
                        Kind = PlaneKindFactory.GetPlane(PlaneType.airbus)
                    }
                };

                repo.Add(this._flight);

                session.Complete();
            }
        }

        private void Dont_get_flight_because_dont_find_airport()
        {
            var flightInfo = new FlightSearch()
            {
                DepartureAirport = "undefined airport",
                ArrivalAirport = this._flight.ArrivalAirport.Name,
            };

            RedirectToRouteResult routeResult = this._controller.GetFlights(flightInfo) as RedirectToRouteResult;
            var value = routeResult.RouteValues["Notification"];
            Assert.AreEqual("Sorry, departure is not found.", value);
        }

        private void Dont_get_flight_because_wrong_date()
        {
            var flightInfo = new FlightSearch()
            {
                DepartureAirport = this._flight.DepartureAirport.Name,
                ArrivalAirport = this._flight.ArrivalAirport.Name,
                BeginningDate = new DateTime(2010, 10, 10, 10, 10, 10),
                EndingDate = new DateTime(2011, 10, 10, 10, 10, 10),
            };

            ViewResult result = this._controller.GetFlights(flightInfo) as ViewResult;
            var flights = result.ViewBag.flights as IList;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(flights);
            Assert.AreEqual("FlightDetails", result.ViewName);
        }

        private void Get_flight_without_date()
        {
            var flightInfo = new FlightSearch()
            {
                DepartureAirport = this._flight.DepartureAirport.Name,
                ArrivalAirport = this._flight.ArrivalAirport.Name,
            };

            ViewResult result = this._controller.GetFlights(flightInfo) as ViewResult;
            var flights = result.ViewBag.flights as IList;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(flights);
            Assert.AreEqual("FlightDetails", result.ViewName);
        }

        private void Get_flight_because_with_correct_date()
        {
            var flightInfo = new FlightSearch()
            {
                DepartureAirport = this._flight.DepartureAirport.Name,
                ArrivalAirport = this._flight.ArrivalAirport.Name,
                BeginningDate = new DateTime(2015, 10, 10, 10, 10, 10),
                EndingDate = new DateTime(2017, 10, 10, 10, 10, 10),
            };

            ViewResult result = this._controller.GetFlights(flightInfo) as ViewResult;
            var flights = result.ViewBag.flights as IList;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(flights);
            Assert.AreEqual("FlightDetails", result.ViewName);
        }


    }
}
