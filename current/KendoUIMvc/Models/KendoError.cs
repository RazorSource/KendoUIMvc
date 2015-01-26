using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoUIMvc.Models
{
    /// <summary>
    /// Used to throw custom errors within the Kendo framework.
    /// </summary>
    public class KendoError
    {
        public KendoError()
        {
        }

        public KendoError(string errorThrown)
        {
            //this.errors.Add(errorThrown);
            this.errors = errorThrown;

            this.errorThrown = "custom error";
            this.status = "customerror";

        }

        public string errorThrown;
    //    public List<string> errors = new List<string>();
        public string errors;
        public string sender;
        public string status;
        public string xhr;
    }
}
