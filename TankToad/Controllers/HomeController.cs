using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Text.RegularExpressions;
using TankToad.Models;
using TankToad.Class;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Newtonsoft.Json;
using System.Diagnostics;

namespace TankToad.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {
        public ActionResult Index(string args)
        {
            return View();
        }

        public ActionResult DeviceAttributes()
        {
            return View("/DeviceAttributes/Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}