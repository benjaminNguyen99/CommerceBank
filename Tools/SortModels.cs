using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public enum SortTransaction { Ascending = 0, Descending = 1 }
    public class SortModels
    {
        private string Up_sort = "fa fa-arrow-up";
        private string Down_sort = "fa fa-arrow-down";
        public string SortedProperty { get; set; }
        public SortTransaction SortedTransaction { get; set; }  
        
        private List<SortableColumn> sortableColumns = new List<SortableColumn>();

        public void AddColumn(string colname, bool isDefaultCol=false)
        {
            SortableColumn tmp = this.sortableColumns.Where(c=>c.ColumnName.ToLower() == colname.ToLower()).SingleOrDefault();
            if (tmp == null)
            {
                sortableColumns.Add(new SortableColumn() { ColumnName = colname });
            }

            if (isDefaultCol == true || sortableColumns.Count==1)
            {
                SortedProperty = colname;
                SortedTransaction = SortTransaction.Ascending;
            }
        }
        public SortableColumn GetColumn (string colname)
        {
            SortableColumn tmp = this.sortableColumns.Where(c => c.ColumnName.ToLower() == colname.ToLower()).SingleOrDefault();
            if (tmp == null)
            {
                sortableColumns.Add(new SortableColumn() { ColumnName = colname });
            }
            return tmp;
        }
        public void ApplySort(string sortExpression)
        {


            //this.GetColumn("location").SortIcon = "";
            //this.GetColumn("location").SortExpression = "location";

            //this.GetColumn("balancetype").SortIcon = "";
            //this.GetColumn("balancetype").SortExpression = "balancetype";

            if (sortExpression == "")
            {
                sortExpression = this.SortedProperty;
            }
            sortExpression = sortExpression.ToLower();

            foreach (SortableColumn sortableColumn in sortableColumns)
            {
                sortableColumn.SortIcon = "";
                sortableColumn.SortExpression = sortableColumn.ColumnName;
                if (sortExpression == sortableColumn.ColumnName.ToLower())
                {
                    this.SortedTransaction = SortTransaction.Ascending;
                    this.SortedProperty = sortableColumn.ColumnName;
                    sortableColumn.SortIcon = Down_sort;
                    sortableColumn.SortExpression = sortableColumn.ColumnName + "_desc";
                }
                if (sortExpression == sortableColumn.ColumnName.ToLower() + "_desc")
                {
                    this.SortedTransaction = SortTransaction.Descending;
                    this.SortedProperty = sortableColumn.ColumnName;
                    sortableColumn.SortIcon = Up_sort;
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
