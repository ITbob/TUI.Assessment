using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUI.Data.Access.Source
{
    public static class DalHelper
    {
        private static IInitialiser _initialiser;

        public static void Set(IInitialiser init)
        {
            if(_initialiser == null)
            {
                _initialiser = init;
            }
        }

        public static void Initalise()
        {
            _initialiser.Initialise();
        }
    }
}
