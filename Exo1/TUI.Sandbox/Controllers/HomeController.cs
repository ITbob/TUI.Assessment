using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using TUI.Data.Access.Source.Unit;
using TUI.Places.Source;
using TUI.Sandbox.Models;
using TUI.Transportations.Air;

namespace TUI.Sandbox.Controllers
{
    public class HomeController : BasicController
    {
        private readonly IUnit<Airport> _airportUnit;
        private readonly IUnit<Flight> _flightUnit;

        public HomeController(IUnit<Airport> airportUnit, IUnit<Flight> flightUnit)
        {
            _airportUnit = airportUnit;
            _flightUnit = flightUnit;
        }

        private Boolean HasAirport(String airportName)
        {
            using (var session = this._airportUnit.GetSession())
            {
                var airportRepo = session.GetRepository();
                return airportRepo.Find(airport => airport.Name == airportName).Any();
            }
        }

        public ActionResult Index()
        {
            return View(new FlightSearch());
        }

        [HttpPost]
        public ActionResult GetAirports(String value)
        {
            var airportTextInfo = value;

            if (String.IsNullOrEmpty(airportTextInfo))
            {
                return Json(new List<Airport>(), JsonRequestBehavior.AllowGet);
            }

            using (var session = this._airportUnit.GetSession())
            {
                var airportRepo = session.GetRepository();

                var matchedAirport = airportRepo.Find(airport =>
                    airport.Name.ToLower().Contains(airportTextInfo.ToLower())
                    || (airport.City != null && airport.City.Name.ToLower().Contains(airportTextInfo.ToLower()))
                    ).Select(x => // circular references that json cannot serialize
                        new
                        {
                            Id = x.Id,
                            Name = x.Name,
                            City = x.City.Name
                        });

                if (this.IsFound(airportTextInfo, matchedAirport))
                {
                    return Json(new List<Airport>(), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(matchedAirport.ToList(), JsonRequestBehavior.AllowGet);
                }
            }
        }

        private Boolean IsFound(String value, IEnumerable<dynamic> airports)
        {
            if (!airports.Any())
            {
                return false;
            }
            else
            {
                var count = airports.Count();
                var airport = airports.First().Name.ToLower();

                return count == 1
                    && (value.ToLower() == airports.First().City.ToLower()
                            || value.ToLower() == airport.ToLower());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetFlights([Bind(Include = "DepartureAirport,ArrivalAirport,beginningDate,endingDate")] FlightSearch search)
        {
            if (!HasAirport(search.DepartureAirport))
            {
                return GetNotFound("departure");
            }

            if (!HasAirport(search.ArrivalAirport))
            {
                return GetNotFound("arrival");
            }

            Airport departureAirport = null;
            Airport arrivalAirport = null;

            using (var session = this._airportUnit.GetSession())
            {
                departureAirport = session.GetRepository().Find((a) => a.Name == search.DepartureAirport).First();
                arrivalAirport = session.GetRepository().Find((a) => a.Name == search.ArrivalAirport).First();
            }

            using (var session = this._flightUnit.GetSession())
            {
                var flightRepo = session.GetRepository();

                if (search.BeginningDate != DateTime.MinValue && search.EndingDate != DateTime.MinValue)
                {
                    ViewBag.flights = flightRepo.Find(flight =>
                        flight.DepartureAirport.Id == departureAirport.Id
                        && flight.ArrivalAirport.Id == arrivalAirport.Id
                        && search.BeginningDate <= flight.StartDate
                        && flight.StartDate <= search.EndingDate);
                }
                else
                {
                    foreach (var flight in flightRepo.GetAll())
                    {
                        Debug.WriteLine(flight.Description);
                    }

                    ViewBag.flights = flightRepo.Find(flight => flight.DepartureAirport.Id == departureAirport.Id
                        && flight.ArrivalAirport.Id == arrivalAirport.Id);
                }
            }

            ViewBag.previousResearch = search;

            return View("FlightDetails");
        }

    }
}