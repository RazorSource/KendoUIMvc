using KendoUIMVCTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KendoUIMVCTest.Areas.Demo.Controllers
{
    public class RazorGridController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            List<DemoModel> demoModels = new List<DemoModel>();
            demoModels.Add(new DemoModel());
            demoModels.Add(new DemoModel() { BirthDate = new DateTime(2015, 1, 19), FavoriteDay = 6 });

            return new ContentResult { Content = JsonConvert.SerializeObject(demoModels), 
                ContentType = "application/json", ContentEncoding = Encoding.UTF8 };
        }
    }
}