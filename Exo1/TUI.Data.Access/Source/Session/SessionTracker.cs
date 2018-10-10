using System;
using System.Collections.Generic;
using TUI.Data.Access.Source.Repositories;
using TUI.Data.Access.Source.Unit;
using TUI.Model.Shared.Source;
using TUI.Report.Source;

namespace TUI.Data.Access.Source.Session
{
    internal class SessionTracker<T>
                where T : class, IIdContainer
    {
        private readonly IUnit<HistoryLine> _recordingUnit;
        private readonly IList<HistoryLine> _lastRecords;
        private readonly IRepository<T> _repo;
        private readonly ISession<T> _session;

        public SessionTracker(ISession<T> session, IUnit<HistoryLine> recordingUnit)
        {
            this._session = session;
            this._repo = session.GetRepository();
            this._lastRecords = new List<HistoryLine>();
            this._recordingUnit = recordingUnit;

            this._repo.Operated += this.OnOperated;
            this._session.Completed += this.OnCompleted;
            this._session.Disposed += this.OnDisposed;
        }

        private void OnDisposed(Object obj, EventArgs e)
        {
            //unsubscribe for GC
            this._repo.Operated -= this.OnOperated;
            this._session.Completed -= this.OnCompleted;
            this._session.Disposed -= this.OnDisposed;
        }

        private void OnCompleted(Object obj, EventArgs e)
        {
            using(var session = this._recordingUnit.GetSession())
            {
                session.GetRepository().AddRange(this._lastRecords);
                this._lastRecords.Clear();
                session.Complete();
            }
        }

        private void OnOperated(Object obj, OperationInfo info)
        {
            this._lastRecords.Add(new HistoryLine()
            {
                Operation = info.Operation,
                Description = info.obj.ToString(),
                Datetime = DateTime.Now,
                DateType = typeof(T).Name
            });
        }
    }
}
