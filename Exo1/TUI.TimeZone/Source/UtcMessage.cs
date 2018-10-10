using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUI.TimeZone.Source
{
    public struct UtcMessage
    {
        public TimeSpan Offset { get; set; }
        public Boolean IsReceived { get; set; }
    }
}
