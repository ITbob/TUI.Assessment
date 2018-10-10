using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TUI.Sandbox.Controllers;

namespace TUI.Sandbox.Test.Source
{
    [TestFixture]
    class NotificationControllerTest
    {
        [Test]
        public void Should_Get_Notification_Index_Page()
        {
            NotificationController controller = new NotificationController();
            ViewResult result = controller.Index("test") as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
