using KendoUIMVCTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoUIMVCTest.Areas.Demo.Controllers
{
    public class DatePickerController : Controller
    {
        // GET: Demo/DatePicker
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexBound()
        {
            return View(new DemoModel());
        }
    }
}