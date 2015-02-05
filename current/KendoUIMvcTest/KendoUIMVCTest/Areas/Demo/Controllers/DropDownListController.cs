using KendoUIMVCTest.Dao;
using KendoUIMVCTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoUIMVCTest.Areas.Demo.Controllers
{
    public class DropDownListController : Controller
    {
        public ActionResult Index()
        {
            InitViewBag();
            return View();
        }

        public ActionResult IndexBound()
        {
            InitViewBag();
            return View(new DemoModel());
        }

        private void InitViewBag()
        {
            ViewBag.DaysOfTheWeek = DaysOfTheWeekDao.GetDaysOfTheWeek();
        }
    }
}