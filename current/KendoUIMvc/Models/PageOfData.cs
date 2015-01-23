using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KendoUIMvc.Models
{
    /// <summary>
    /// Represents the javascript serializable object used for a page of data.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class PageOfData<TModel>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PageOfData()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">A list of items that represent one page of data.</param>
        /// <param name="total">The total number of records across all pages.</param>
        public PageOfData(List<TModel> data, int total)
        {
            this.data = data;
            this.total = total;
        }

        /// <summary>
        /// Constructs a PageOfData with a single item.  This constructor can be used to return the same
        /// data structure sent by an original page of data.  This is useful for client side observable model
        /// binding, which expects similar data structure for create and update methods.
        /// </summary>
        /// <param name="model">Model instance.</param>
        public PageOfData(TModel model)
        {
            this.data = new List<TModel>();
            data.Add(model);
            this.total = 1;
        }

        /// <summary>
        /// A list of items that represent one page of data.
        /// </summary>
        public List<TModel> data { get; set; }

        /// <summary>
        /// The total number of records across all pages.
        /// </summary>
        public int total { get; set; }
    }
}
