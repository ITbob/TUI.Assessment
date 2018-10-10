using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Data.Access.Source.Session;
using TUI.Model.Shared.Source;

namespace TUI.Data.Access.Source.Unit
{
    public interface IUnit<T> where T : class, IIdContainer
    {
        ISession<T> GetSession();
    }
}
