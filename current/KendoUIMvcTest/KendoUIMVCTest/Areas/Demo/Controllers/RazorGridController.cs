﻿using KendoUIMVCTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using KendoUIMvc.Models;

namespace KendoUIMVCTest.Areas.Demo.Controllers
{
    public class RazorGridController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData(int? take, int? skip)
        {
            // Uncomment to delay to view progress functionality.
            //System.Threading.Thread.Sleep(2000);

            List<DemoModel> demoModels = new List<DemoModel>();
            demoModels.Add(new DemoModel());
            demoModels.Add(new DemoModel() { Id = 1, BirthDate = new DateTime(2015, 1, 19), FavoriteDay = 5 });
            demoModels.Add(new DemoModel() { Id = 2, BirthDate = new DateTime(2015, 1, 20), FavoriteDay = 4 });
            demoModels.Add(new DemoModel() { Id = 3, BirthDate = new DateTime(2015, 1, 21), FavoriteDay = 3 });
            demoModels.Add(new DemoModel() { Id = 4, BirthDate = new DateTime(2015, 1, 22), FavoriteDay = 2 });
            demoModels.Add(new DemoModel() { Id = 5, BirthDate = new DateTime(2015, 1, 23), FavoriteDay = 1 });
            demoModels.Add(new DemoModel() { Id = 6, BirthDate = new DateTime(2015, 1, 24), FavoriteDay = 7 });
            demoModels.Add(new DemoModel() { Id = 7, BirthDate = new DateTime(2015, 1, 25), FavoriteDay = 6 });
            demoModels.Add(new DemoModel() { Id = 8, BirthDate = new DateTime(2015, 1, 26), FavoriteDay = 5 });
           
            PageOfData<DemoModel> pageOfDemoModels = new PageOfData<DemoModel>(FilterDemoModels(demoModels, take, skip), demoModels.Count);

            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(pageOfDemoModels), 
                    ContentType = "application/json", ContentEncoding = Encoding.UTF8
            };
        }

        /// <summary>
        /// Applies simple server side filtering to get a page of data for the demo.  If larger sets of data are used, this should be applied at the datasource level.
        /// This particular implementation should only be used for small implementations.
        /// </summary>
        /// <param name="allModels"></param>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        private List<DemoModel> FilterDemoModels(List<DemoModel> allModels, int? take, int? skip)
        {
            List<DemoModel> filteredModels;

            // Apply skip filter
            if (skip != null)
            {
                filteredModels = allModels.Skip(skip.Value).ToList();
            }
            else
            {
                filteredModels = allModels;
            }

            // Apply take filter
            if (take != null)
            {
                filteredModels = filteredModels.Take(take.Value).ToList();
            }

            return filteredModels;
        }
    }
}