using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Places.Source;

namespace TUI.Places.Air.Source
{
    public static class AirportFactory
    {

        private static Dictionary<AirportType, Func<Airport>> _aiports
            = new Dictionary<AirportType, Func<Airport>>
            {
                        {AirportType.Orly, () => GetOrly()},
                        {AirportType.CharlesDeGaule, () => GetCDG()},
                        {AirportType.JFK, () => GetJfk()},
                        {AirportType.berlin, () => GetBerlin()},
                        {AirportType.Newwark, () => GetNewyark()},
                        {AirportType.Beijing, () => GetBeijing()},
                        {AirportType.BuenosAires, () => GetBuenosAires()},
                        {AirportType.Pudong, () => GetPudong()},
                        {AirportType.Hongqiao, () => GetHongqiao()},
                        {AirportType.Incheon, () => GetIncheon()},
                        {AirportType.Melbourne, () => GetMelbourne()},
                        {AirportType.Gimpo, () => GetGimpo()},
                        {AirportType.Netaji, () => GetCalcutta()},
            };

        public static Airport GetAirport(AirportType type)
        {
            return _aiports[type]();
        }

        private static Airport GetOrly()
        {
            return new Airport(48.856614, 2.3522219, @"Orly Airport", CityFactory.GetCity(CityType.Paris));
        }

        private static Airport GetCDG()
        {
            return new Airport(49.0096906, 2.547924500000022, @"Charles de Gaulle Airport", CityFactory.GetCity(CityType.Paris));
        }

        private static Airport GetJfk()
        {
            return new Airport(40.7127753, -74.0059728, @"JFK Airport", CityFactory.GetCity(CityType.NewYork));
        }

        private static Airport GetBerlin()
        {
            return new Airport(52.52000659999999, 13.404953999999975, @"Berlin Airport", CityFactory.GetCity(CityType.Berlin));
        }

        private static Airport GetNewyark()
        {
            return new Airport(40.4325, 13.404953999999975, @"Newwark Airport", CityFactory.GetCity(CityType.NewJersey));
        }

        private static Airport GetBuenosAires()
        {
            return new Airport(-34.598326, -58.375275, @"Buenos Aires Airport", CityFactory.GetCity(CityType.BuenosAires));
        }

        private static Airport GetPudong()
        {
            return new Airport(31.1443439, 121.80827299999999, @"Pudong Airport", CityFactory.GetCity(CityType.Shanghai));
        }

        private static Airport GetHongqiao()
        {
            return new Airport(31.192209, 121.334297, @"Hongqiao Airport", CityFactory.GetCity(CityType.Shanghai));
        }

        private static Airport GetCalcutta()
        {
            return new Airport(22.6520429, 88.4463299, @"Netaji Subhas Chandra Bose International Airport", CityFactory.GetCity(CityType.Calcutta));
        }

        private static Airport GetMelbourne()
        {
            return new Airport(-37.669012, 144.841027, @"Melbourne Airport", CityFactory.GetCity(CityType.Melbourne));
        }

        private static Airport GetBeijing()
        {
            return new Airport(40.0798573, 116.60311209999998, @"Beijing Capital International  Airport", CityFactory.GetCity(CityType.Beijing));
        }

        private static Airport GetIncheon()
        {
            return new Airport(37.460191, 126.440696, @"Incheon Airport", CityFactory.GetCity(CityType.Seoul));
        }

        private static Airport GetGimpo()
        {
            return new Airport(-37.814107, 144.96328, @"Gimpo Airport", CityFactory.GetCity(CityType.Seoul));
        }
    }

    public enum AirportType
    {
        Orly,
        CharlesDeGaule,
        JFK,
        berlin,
        Newwark,
        BuenosAires,
        Pudong,
        Hongqiao,
        Netaji,
        Melbourne,
        Beijing,
        Incheon,
        Gimpo
    }
}
