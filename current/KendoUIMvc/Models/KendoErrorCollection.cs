using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoUIMvc.Models
{
    public class KendoErrorCollection
    {
        public KendoErrorCollection()
        {
        }

        public KendoErrorCollection(KendoError kendoError)
        {
            this.errors.Add(kendoError);
        }

        public void AddError(KendoError kendoError)
        {
            this.errors.Add(kendoError);
        }

        public List<KendoError> errors = new List<KendoError>();
    }
}
