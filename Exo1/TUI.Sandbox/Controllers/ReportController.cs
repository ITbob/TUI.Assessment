using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TUI.Data.Access.Source;
using TUI.Data.Access.Source.Unit;
using TUI.Report.Source;

namespace TUI.Sandbox.Controllers
{
    public class ReportController : BasicController
    {
        private readonly IUnit<HistoryLine> _historyUnit;

        public ReportController(IUnit<HistoryLine> historyUnit)
        {
            this._historyUnit = historyUnit;
        }

        // GET: Report
        public ActionResult Index()
        {
            using (var session = this._historyUnit.GetSession())
            {
                var repo = session.GetRepository();
                var items = repo.GetAll();
                return View(items);
            }
        }
    }
}