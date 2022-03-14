using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSwiggy.Tools
{
    public class SortModel
    {
        private string UpIcon = "fa fa-arrow-up";
        private string DownIcon = "fa fa-arrow-down";
        public string SortedProperty { get; set; }
        public string SortedOrder { get; set; }
        public string SortedExpression { get; private set; }
        public List<SortableColumn> sortableColumn = new List<SortableColumn>();

        public void AddColumn(string colname, bool IsDefaultColumn = false)
        {
            SortableColumn tmp = this.sortableColumn.Where(c => c.ColumnName.ToLower() == colname.ToLower()).FirstOrDefault();
            if (tmp == null)
            {
                sortableColumn.Add(new SortableColumn() { ColumnName = colname });
            }
            if (IsDefaultColumn == true || sortableColumn.Count == 1)
            {
                SortedProperty = colname;
                SortedOrder = "Ascending";
            }

        }
        public SortableColumn GetColumn(string colname)
        {
            SortableColumn tmp = this.sortableColumn.Where(c => c.ColumnName.ToLower() == colname.ToLower()).FirstOrDefault();
            if (tmp == null)
            {
                sortableColumn.Add(new SortableColumn() { ColumnName = colname });
            }
            return tmp;
        }
        public void ApplySort(string sortExpression)
        {
            if (sortExpression == null)
                sortExpression = "";

            if (sortExpression == "")
                sortExpression = this.SortedProperty;

            sortExpression = sortExpression.ToLower();
            SortedExpression = sortExpression;
            foreach (SortableColumn sortableColumn in this.sortableColumn)
            {
                sortableColumn.SortIcon = "";
                sortableColumn.SortExpression = sortableColumn.ColumnName;

                if (sortExpression == sortableColumn.ColumnName.ToLower())
                {
                    this.SortedOrder = "Ascending";
                    this.SortedProperty = sortableColumn.ColumnName;
                    sortableColumn.SortIcon = DownIcon;
                    sortableColumn.SortExpression = sortableColumn.ColumnName + "_desc";
                }

                if (sortExpression == sortableColumn.ColumnName.ToLower() + "_desc")
                {
                    this.SortedOrder = "Descending";
                    this.SortedProperty = sortableColumn.ColumnName;
                    sortableColumn.SortIcon = UpIcon;
                    sortableColumn.SortExpression = sortableColumn.ColumnName;
                }
            }
        }
    }
    public class SortableColumn
    {
        public string ColumnName { get; set; }
        public string SortExpression { get; set; }
        public string SortIcon { get; set; }
    }

}
