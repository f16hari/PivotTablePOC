using System;

namespace PivotTablePOC.Model
{
    public class PivotModel
    {
        public List<string> RowDim { get; set; } = new List<string>();

        public List<string> ColumnDim { get; set; } = new List<string>();

        public List<string> Measures { get; set; } = new List<string>();
    }
}
