using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoUIMVCTest.Areas.Demo.Controllers
{
    public class DemoHomeController : Controller
    {
        // GET: Demo/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}