using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TUI.Report.Source;

namespace TUI.Data.Access.Source.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(Int32 i);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, Boolean>> predicate);

        void Add(T element);
        void AddRange(IEnumerable<T> element);

        void Remove(T element);
        void RemoveRange(IEnumerable<T> element);

        void SetModified(T element);
        EventHandler<OperationInfo> Operated { get; set; }
    }
}
