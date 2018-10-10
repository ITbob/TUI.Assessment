using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Report.Source;

namespace TUI.Data.Access.Source
{
    public struct OperationInfo
    {
        public OperationType Operation { get; set; }
        public Object obj { get; set; }
    }
}
