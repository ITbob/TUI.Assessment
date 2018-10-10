using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUI.Ado.Entity.Source
{
    internal class TuiTestInitializer : DropCreateDatabaseAlways<TuiContext>
    {
    }
}
