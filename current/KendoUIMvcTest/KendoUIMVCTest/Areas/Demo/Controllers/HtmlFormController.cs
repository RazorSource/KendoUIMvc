using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoUIMVCTest.Models;

namespace KendoUIMVCTest.Areas.Demo.Controllers
{
    public class HtmlFormController : Controller
    {
        // GET: Demo/HtmlForm
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(DemoModel demoModel)
        {
            return View();
        }

        public ActionResult HorizontalLabels()
        {
            return View();
        }

        public ActionResult TwoColumns()
        {
            return View();
        }

        public ActionResult TwoColumnsHorizontalLabels()
        {
            return View();
        }

        public ActionResult WithPanel()
        {
            return View();
        }
    }
}