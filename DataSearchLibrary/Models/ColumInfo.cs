using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSearchLibrary.Models
{
    public class ColumnInfo
    {
        public ColumnInfo(string ColumnName, string ColumnValue, string LogicalOperator,string Comparator)
        {
            this.ColumnName = ColumnName;
            this.ColumnValue = ColumnValue;
            this.LogicalOperator = LogicalOperator;
            this.Comparator = Comparator;
        }

        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public string LogicalOperator { get; set; }
        public string Comparator { get; set; }
    }
}
