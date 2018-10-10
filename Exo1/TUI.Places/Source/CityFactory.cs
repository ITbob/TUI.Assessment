using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUI.Places.Source
{
    public static class CityFactory
    {
        private static Dictionary<CityType, Func<City>> _cities
            = new Dictionary<CityType, Func<City>>
            {
                {CityType.Paris, () => GetParis()},
                {CityType.Berlin, () => GetBerlin()},
                {CityType.NewYork, () => GetNewYork()},
                {CityType.Shanghai, () => GetShanghai()},
                {CityType.NewJersey, () => GetNewJersey()},
                {CityType.Beijing, () => GetBerlin()},
                {CityType.BuenosAires, () => GetBuenosAires()},
                {CityType.Calcutta, () => GetCalcutta()},
                {CityType.Melbourne, () => GetMelbourne()},
                {CityType.Seoul, () => GetSeoul()},
            };

        public static City GetCity(CityType type)
        {
            return _cities[type]();
        }

        private static City GetParis()
        {
            return new City()
            {
                Name = "Paris",
                Location = new Location()
                {
                    Latitude = 37.566535,
                    Longitude = 126.97796919999996
                }
            };
        }

        private static City GetNewYork()
        {
            return new City()
            {
                Name = "New York",
                Location = new Location()
                {
                    Latitude = 48.856614,
                    Longitude = 2.3522219
                }
            };
        }

        private static City GetBerlin()
        {
            return new City()
            {
                Name = "Berlin",
                Location = new Location()
                {
                    Latitude = 48.856614,
                    Longitude = 2.3522219
                }
            };
        }

        private static City GetNewJersey()
        {
            return new City()
            {
                Name = "New Jersey",
                Location = new Location()
                {
                    Latitude = 48.856614,
                    Longitude = 2.3522219
                }
            };
        }

        private static City GetShanghai()
        {
            return new City()
            {
                Name = "Shanghai",//4
                Location = new Location()
                {
                    Latitude = 31.230416,
                    Longitude = 121.473701
                }
            };
        }

        private static City GetBeijing()
        {
            return new City()
            {
                Name = "Beijing",//5
                Location = new Location()
                {
                    Latitude = 39.904211,
                    Longitude = 116.407395
                }
            };
        }

        private static City GetSeoul()
        {
            return new City()
            {
                Name = "Seoul",//6
                Location = new Location()
                {
                    Latitude = 37.566535,
                    Longitude = 126.97796919999996
                }
            };
        }

        private static City GetBuenosAires()
        {
            return new City()
            {
                Name = "Buenos Aires",//7
                Location = new Location()
                {
                    Latitude = -34.6037232,
                    Longitude = -58.3815931
                }
            };
        }

        private static City GetCalcutta()
        {
            return new City()
            {
                Name = "Calcutta",//8
                Location = new Location()
                {
                    Latitude = 22.572646,
                    Longitude = 88.363895
                }
            };
        }

        private static City GetMelbourne()
        {
            return new City()
            {
                Name = "Melbourne",//9
                Location = new Location()
                {
                    Latitude = -37.814107,
                    Longitude = 144.96328
                }
            };
        }

    }

    public enum CityType
    {
        Paris,
        Berlin,
        NewYork,
        Shanghai,
        Seoul,
        BuenosAires,
        Calcutta,
        Melbourne,
        Beijing,
        NewJersey
    }
}
