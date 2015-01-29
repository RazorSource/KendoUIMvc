using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KendoUIMVCTest.Models
{
    public class DemoModel
    {
        public DemoModel()
        {
            BirthDate = new DateTime(2015, 1, 14);
            FavoriteDay = 6;
        }

        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        public int FavoriteDay { get; set; }
        public bool Agree { get; set; }
        public bool? LikeThis { get; set; }
    }
}