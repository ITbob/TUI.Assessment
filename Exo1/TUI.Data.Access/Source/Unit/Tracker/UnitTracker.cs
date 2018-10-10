using TUI.Data.Access.Source.Session;
using TUI.Model.Shared.Source;
using TUI.Report.Source;

namespace TUI.Data.Access.Source.Unit.Tracker
{
    public class UnitTracker<T> : IUnit<T>
        where T : class, IIdContainer
    {
        private readonly IUnit<T> _unit;
        private readonly IUnit<HistoryLine> _recordingUnit;

        public UnitTracker(IUnit<T> observedUnit, IUnit<HistoryLine> recordingUnit)
        {
            this._unit = observedUnit;
            this._recordingUnit = recordingUnit;
        }

        public ISession<T> GetSession()
        {
            var session = this._unit.GetSession();
            //create a tracker for each session, when the session is done
            // the tracker will unsubscribe its event
            // then be collected by GC
            var tracker = new SessionTracker<T>(session, this._recordingUnit);
            return session;
        }
    }
}
