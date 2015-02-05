using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KendoUIMVCTest.Dao
{
    /// <summary>
    /// Simple mock DAO class used for days of the week functionality.
    /// </summary>
    public class DaysOfTheWeekDao
    {
        public static IList<SelectListItem> GetDaysOfTheWeek()
        {
            List<SelectListItem> daysOfTheWeek = new List<SelectListItem>();
            daysOfTheWeek.Add(new SelectListItem() { Text = "Sunday", Value = "1" });
            daysOfTheWeek.Add(new SelectListItem() { Text = "Monday", Value = "2" });
            daysOfTheWeek.Add(new SelectListItem() { Text = "Tuesday", Value = "3" });
            daysOfTheWeek.Add(new SelectListItem() { Text = "Wednesday", Value = "4" });
            daysOfTheWeek.Add(new SelectListItem() { Text = "Thursday", Value = "5" });
            daysOfTheWeek.Add(new SelectListItem() { Text = "Friday", Value = "6" });
            daysOfTheWeek.Add(new SelectListItem() { Text = "Saturday", Value = "7" });

            return daysOfTheWeek;
        }
    }
}