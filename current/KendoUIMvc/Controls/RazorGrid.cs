using KendoUIMvc.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KendoUIMvc.Controls
{
    public class RazorGrid<TModel>
    {
        protected HtmlHelper<TModel> htmlHelper;
        protected string name;
        protected IList<Column> columns = new List<Column>();

        public RazorGrid(HtmlHelper<TModel> htmlHelper, string name)
        {
            this.htmlHelper = htmlHelper;
            this.name = name;
        }

        public RazorGrid<TModel> AddColumn(string name, string label)
        {
            columns.Add(new Column(name, label));

            return this;
        }

        public MvcHtmlString Render()
        {
            StringBuilder html = new StringBuilder();
            string dataTemplateName = name + "_layoutTemplate";
            string dataSourceName = name + "_dataSource";

            html.Append(@"
            <table id=""" + this.name + @""">
                <thead>");

            // Render column headers.
            foreach (Column nextColumn in columns)
            {
                html.Append(@"
                    <tr>
                        <th>" + nextColumn.Label + @"</th>
                    </tr>");
            }

            html.Append(@"
                </thead>
                <tbody data-bind=""source: " + dataSourceName + @""" data-template=""" + dataTemplateName + @""">
                </tbody>
            </table>
            <script type=""text/x-kendo-template"" id=""" + dataTemplateName + @""">
                <tr>");

            // Render column data templates
            foreach (Column nextColumn in columns)
            {
                html.Append(@"
                    <td>#: " + nextColumn.Property + @" #</td>");
            }

            html.Append(@"
                </tr>
            </script>
            <script type=""text/javascript"">
                $(document).ready(function() {

        //$('#" + this.name + @" tbody').html('Grid data');

                    var dataSource = new kendo.data.DataSource({
                        //data: movies,
                        transport: {
                            read: {
                                url: '/RazorGrid/GetData',
                                dataType: 'json'
                            }
                        },
schema: {
      model: {
        fields: {
          FavoriteDay: { type: 'string' },
          BirthDate: { type: 'string' }
        }
      }},
                        change: function() { // subscribe to the CHANGE event of the data source
                            $('#" + this.name + @" tbody').html(kendo.render(" + dataTemplateName + @", this.view())); // populate the table
                        }
                    });

                    dataSource.read();
                });
            </script>");


            return new MvcHtmlString(html.ToString());
        }

        public override string ToString()
        {
            MvcHtmlHelper.WriteUnencodedContent(this.htmlHelper, Render().ToString());

            return "";
        }

        /// <summary>
        /// Represents a column in the grid.
        /// </summary>
        public class Column
        {
            public Column()
            {
            }

            public Column(string property, string label)
            {
                this.Property = property;
                this.Label = label;
            }

            public string Property { get; set; }
            public string Label { get; set; }
        }
    }
}
