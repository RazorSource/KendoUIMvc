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
        /// Constructor.  This constructor should be used when data is filtered down to a single
        /// page at the datasource level.  This method will result in the best application performance,
        /// especially when dealing with larger sets of data.
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
        /// Constructor.  This constructor creates a page of data across the entire set of models.  This constructor
        /// can be used if paging is desired, but paging cannot occur at the datasource level.
        /// </summary>
        /// <param name="allModels">All of items in the entire set of data.</param>
        /// <param name="take">The total number of items on a page.</param>
        /// <param name="skip">The number of records to skip.</param>
        public PageOfData(IList<TModel> allModels, int? take, int? skip)
        {
            // Get the total count
            this.total = allModels.Count;

            // Apply skip filter
            if (skip != null)
            {
                data = allModels.Skip(skip.Value).ToList();
            }
            else
            {
                data = allModels.ToList();
            }

            // Apply take filter
            if (take != null)
            {
                data = data.Take(take.Value).ToList();
            }
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
