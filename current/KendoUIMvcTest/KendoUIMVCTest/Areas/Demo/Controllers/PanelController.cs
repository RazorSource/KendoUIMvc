using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoUIMVCTest.Models;

namespace KendoUIMVCTest.Areas.Demo.Controllers
{
    public class PanelController : Controller
    {
        // GET: Demo/Panel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WithForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WithForm(DemoModel demoModel)
        {
            return View();
        }
    }
}