using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Ado.Entity.Source.Repositories;
using TUI.Data.Access.Source.Repositories;
using TUI.Data.Access.Source.Session;
using TUI.Model.Shared.Source;

namespace TUI.Ado.Entity.Source.Session
{
    public class TuiContextSession<T> : ISession<T>
                where T : class, IIdContainer
    {
        private readonly String _connection;
        private readonly TuiContextRepository<T> _repo;
        private TuiContext _context { get; set; }
        public EventHandler Completed { get; set; }
        public EventHandler Disposed { get; set; }

        public void OnDisposed()
        {
            this.Disposed?.Invoke(this, EventArgs.Empty);
        }
        private void OnCompleted()
        {
            this.Completed?.Invoke(this, EventArgs.Empty);
        }

        public TuiContextSession(String connection, TuiContextRepository<T> repo)
        {
            this._connection = connection;
            this._repo = repo;
        }

        public int Complete()
        {
            var result = this._context.SaveChanges();
            this.OnCompleted();
            return result;
        }

        ~TuiContextSession()
        {
            Disposing(false);
        }

        public void Dispose()
        {
            this.Disposing(true);
            this.OnDisposed();
            GC.SuppressFinalize(this);
        }

        protected virtual void Disposing(bool disposing)
        {
            if (disposing)
            {
                this._context.Dispose();
            }
        }

        public IRepository<T> GetRepository()
        {
            if(this._context == null)
            {
                this._context = new TuiContext(this._connection);
                this._repo.SetContext(this._context);
            }

            return this._repo;
        }
    }
}
