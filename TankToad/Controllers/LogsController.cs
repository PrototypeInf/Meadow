using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TankToad.Controllers
{
    [Authorize(Roles = "admin")]
    public class LogsController : Controller
    {
        // GET: Logs
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SMS()
        {
            return View("~/Views/Logs/SMS/Index.cshtml");
        }
        public ActionResult DeviceAttributesLog()
        {
            return View("~/Views/Logs/DeviceAttributesLog/Index.cshtml");
        }
        public ActionResult Data()
        {
            return View("~/Views/Logs/Data/Index.cshtml");
        }
        public ActionResult Diagnostics()
        {
            return View("~/Views/Logs/Diagnostics/Index.cshtml");
        }
    }
}