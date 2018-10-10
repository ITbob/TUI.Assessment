using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Places.Source;

namespace TUI.Distance.Calculator.Source
{
    public static class DistanceCalculator
    {
        /// <summary>
        /// provide distance between two locations
        /// </summary>
        /// <param name="a">departure</param>
        /// <param name="b">arrival</param>
        /// <returns></returns>
        public static Double GetDistance(this Location a, Location b)
        {
            double degreePi = Math.PI / 180;// 0.017453292519943295
            var v = 0.5 - Math.Cos((b.Latitude - a.Latitude) * degreePi) / 2 +
                    Math.Cos(a.Latitude * degreePi) * Math.Cos(b.Latitude * degreePi) *
                    (1 - Math.Cos((b.Longitude - a.Longitude) * degreePi)) / 2;
            var result = 12742 * Math.Asin(Math.Sqrt(v)); // 2 * R; R = 6371 km
            return Math.Round(result,2);
        }
    }
}
