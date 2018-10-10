using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Data.Access.Source.Repositories;

namespace TUI.Data.Access.Source.Session
{
    public interface ISession<T>:IDisposable
        where T : class
    {
        IRepository<T> GetRepository();
        Int32 Complete();
        EventHandler Completed { get; set; }
        EventHandler Disposed { get; set; }
    }
}
