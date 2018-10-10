using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUI.TimeZone.Source
{
    internal class GoogleTimeZone
    {
        public Double DstOffset { get; set; }
        public Double RawOffset { get; set; }
        public String Status { get; set; }
        public String TimeZoneId { get; set; }
        public String TimeZoneName { get; set; }
    }
}
