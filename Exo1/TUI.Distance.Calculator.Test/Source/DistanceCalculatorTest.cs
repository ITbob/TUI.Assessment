using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Distance.Calculator.Source;
using TUI.Places.Source;

namespace TUI.Distance.Calculator.Test.Source
{
    [TestFixture]
    class DistanceCalculatorTest
    {
        [TestCase(-34.598326, -58.375275, 31.1443439, 121.80827299999999, 19631)]
        public void Should_Get_Corrrect_Distance(Double departureLatitude, 
            Double departureLongitude,
            Double arrivalLatitude, 
            Double arrivalLongitude,
            Int32 expectedResult)
        {
            var departureLocation = new Location()
            {
                Latitude = departureLatitude,
                Longitude = departureLongitude
            };

            var arrivalLocation = new Location()
            {
                Latitude = arrivalLatitude,
                Longitude = arrivalLongitude
            };

            var result = departureLocation.GetDistance(arrivalLocation);

            Assert.AreEqual(expectedResult, Convert.ToInt32(result));
        }
    }
}
