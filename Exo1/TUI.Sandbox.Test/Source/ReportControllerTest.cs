using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TUI.Ado.Entity.Source.Factory;
using TUI.Ado.Entity.Source.Unit;
using TUI.Report.Source;
using TUI.Sandbox.Controllers;

namespace TUI.Sandbox.Test.Source
{
    [TestFixture]
    class ReportControllerTest
    {
        private ReportController _controller;

        [TestFixtureSetUp]
        public void Setup()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
            var connection = ConfigurationManager.ConnectionStrings["TUITest"].ToString();

            var reportUnit = new TuiContextUnit<HistoryLine>(connection, RepoFactory.GetTuiContextRepo<HistoryLine>());

            this._controller = new ReportController(reportUnit);
        }

        [Test]
        public void Should_Get_Report_Page_Index()
        {
            ViewResult result = this._controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
