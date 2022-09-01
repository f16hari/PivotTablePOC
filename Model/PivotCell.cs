using System;

namespace PivotTablePOC.Model
{
    public class PivotCell
    {
        public PivotCell(PivotMeasure measure)
        {
            Measure = measure;
        }

        public PivotMeasure Measure { get; set; }
    }
}

