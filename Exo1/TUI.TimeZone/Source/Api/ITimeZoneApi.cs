using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Places.Source;

namespace TUI.TimeZone.Source
{
    public interface ITimeZoneApi
    {
        Boolean GetLocalTime(Location departure, DateTime utcDate, ref DateTime localTime);
    }
}
