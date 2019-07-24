using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSearchLibrary.Models
{
    public class ColumnInfo
    {
        public ColumnInfo(string ColumnName, string ColumnValue, string ColumnOperator)
        {
            this.ColumnName = ColumnName;
            this.ColumnValue = ColumnValue;
            this.ColumnOperator = ColumnOperator;
        }

        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public string ColumnOperator { get; set; }
    }
}
