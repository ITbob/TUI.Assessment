using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUI.Shared.Exceptions
{
    public class InvalidParameterException:Exception
    {
        public InvalidParameterException(Object obj):base($"{obj} ({obj.GetType()}) is an invalid parameter")
        {

        }
    }
}
