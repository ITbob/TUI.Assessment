using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TUI.Places.Source;

namespace TUI.TimeZone.Source
{
    public static class TimeZoneHelper
    {
        public static ITimeZoneApi Api { get; set; }

        public static UtcMessage GetDiff(Location departure, Location arrival, DateTime utcDate)
        {

            var localDepature = DateTime.MinValue;
            if (!Api.GetLocalTime(departure, utcDate, ref localDepature))
            {
                return new UtcMessage() { IsReceived = false };
            }

            var localArrival = DateTime.MinValue;
            if (!Api.GetLocalTime(arrival, utcDate, ref localArrival))
            {
                return new UtcMessage() { IsReceived = false };
            }

            return new UtcMessage() {
                Offset = localDepature.Subtract(localArrival),
                IsReceived = true};
        }
    }
}
