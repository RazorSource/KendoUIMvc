﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoUIMVCTest.Models;

namespace KendoUIMVCTest.Areas.Demo.Controllers
{
    public class WindowController : Controller
    {
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

        public ActionResult WithFormCustomized()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WithFormCustomized(DemoModel demoModel)
        {
            return View();
        }
    }
}