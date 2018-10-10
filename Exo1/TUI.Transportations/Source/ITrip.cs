using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Places.Source;

namespace TUI.Transportations
{
    /// <summary>
    /// Itrip may be implemented by all kinds of trips (flight, train....)
    /// </summary>
    public interface ITrip
    {
        Double GetDistanceKm();
        Location Departure { get; }
        Location Arrival { get; }

        DateTime StartDate { get; set; }

        TimeSpan GetDuration();
        DateTime GetEndDate();
    }
}
