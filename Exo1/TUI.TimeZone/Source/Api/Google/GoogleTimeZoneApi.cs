using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TUI.Places.Source;

namespace TUI.TimeZone.Source.Api.Google
{
    public class GoogleTimeZoneApi: ITimeZoneApi
    {
        private readonly RestClient _client;

        public GoogleTimeZoneApi()
        {
            this._client = new RestClient("https://maps.googleapis.com");
        }

        private static double ToTimestamp(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        private GoogleTimeZone GetTimeZone(Location loc, DateTime utcDate)
        {
            var request = new RestRequest("maps/api/timezone/json", Method.GET);
            request.AddParameter("location", loc.Latitude.ToString().Replace(",",".") 
                + "," + loc.Longitude.ToString().Replace(",", "."));
            request.AddParameter("timestamp", ToTimestamp(utcDate));
            request.AddParameter("sensor", "false");
            //uselese: request.AddParameter("key", "AIzaSyDsxDimKTCdU12yRTz0swC-8eIuUuHHkqg");
            var response = this._client.Execute<GoogleTimeZone>(request);
            return response.Data;
        }

        public Boolean GetLocalTime(Location departure, DateTime utcDate, ref DateTime localTime)
        {
            Thread.Sleep(1000); //add a sleep otherwise google thinks we are spamming
            var data = GetTimeZone(departure, utcDate);
            if (data == null || data.Status != "OK")
            {
                return false;
            }

            localTime = GetLocalDateTime(data, utcDate);
            return true;
        }

        private static DateTime GetLocalDateTime(GoogleTimeZone timezone, DateTime utcDate)
        {
            return utcDate.AddSeconds(timezone.RawOffset + timezone.DstOffset);
        }
    }
}
