using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Data.Access.Source;
using TUI.Data.Access.Source.Repositories;
using TUI.Model.Shared.Source;
using TUI.Report.Source;

namespace TUI.Ado.Entity.Source.Repositories
{
    public abstract class TuiContextRepository<T> 
        : IRepository<T> where T : class, IIdContainer
    {
        protected TuiContext Context { get; set; }
        protected abstract DbSet<T> GetDb();
        protected abstract IQueryable<T> GetQueryable();

        public EventHandler<OperationInfo> Operated { get; set; }

        protected void OnOperated(OperationInfo info)
        {
            this.Operated?.Invoke(this, info);
        }

        public virtual void Add(T element)
        {
            this.GetDb().Add(element);
            this.OnOperated(new OperationInfo()
            {
                obj = element,
                Operation = OperationType.Create
            });
        }

        public void AddRange(IEnumerable<T> element)
        {
            this.GetDb().AddRange(element);
            this.OnOperated(new OperationInfo()
            {
                Operation = OperationType.Create
            });
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.GetQueryable().Where(predicate).ToList();
        }

        public T Get(int i)
        {
            var result = this.GetQueryable().Where(f => f.Id == i);

            if (!result.Any())
            {
                return null;
            }


            return result.Single();
        }

        public IEnumerable<T> GetAll()
        {
            var data = this.GetQueryable();
            if (data == null)
            {
                return new List<T>();
            }
            else
            {
                return data.ToList();
            }
        }

        public void Remove(T element)
        {
            this.OnOperated(new OperationInfo() {
                obj = element,
                Operation = OperationType.Remove
            });
            this.GetDb().Remove(element);
        }

        public void RemoveRange(IEnumerable<T> element)
        {
            this.GetDb().RemoveRange(element);
            this.OnOperated(new OperationInfo()
            {
                Operation = OperationType.Remove
            });
        }

        public virtual void SetModified(T element)
        {
            this.Context.Entry(element).State = EntityState.Modified;
            this.OnOperated(new OperationInfo()
            {
                obj= element,
                Operation = OperationType.Update
            });
        }

        public void SetContext(TuiContext context)
        {
            this.Context = context;
        }
    }
}
