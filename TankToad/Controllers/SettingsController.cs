using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TankToad.Controllers
{
    [Authorize(Roles = "admin")]
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DeviceList()
        {
            return View("~/Views/Settings/DeviceList/Index.cshtml");
        }
        public ActionResult DeviceSpecificConstants()
        {
            return View("~/Views/Settings/DeviceSpecificConstants/Index.cshtml");
        }
    }
}