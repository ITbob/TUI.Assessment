using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TUI.Sandbox.Controllers
{
    public class NotificationController : Controller
    {
        public ActionResult Index(String notification)
        {
            ViewBag.Notification = notification;
            return View();
        }

        public ActionResult Error()
        {
            ViewBag.Notification = "Sorry, an error happened.";
            return View("index");
        }

        public ActionResult PageNotFound()
        {
            ViewBag.Notification = "Page not found...";
            return View("index");
        }
    }
}