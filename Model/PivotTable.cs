using System;

namespace PivotTablePOC.Model
{
    public class PivotTable
    {
        public PivotTable(IEnumerable<DynamicModel> source, IReadOnlyList<string> rows, IReadOnlyList<string> columns, IReadOnlyList<string> measures)
        {
            ColumnDim = PivotDimension.BuildDimensions(columns);
            RowDim = PivotDimension.BuildDimensions(rows);
            Measures = PivotMeasure.BuildMeasures(measures);
            Source = source;
        }

        public List<PivotDimension> ColumnDim { get; set; }

        public List<PivotDimension> RowDim { get; set; }

        public List<PivotMeasure> Measures { get; set; }

        public IEnumerable<DynamicModel> Source { get; set; }

        public List<PivotHeader> RowHeaders { get; set; } = new List<PivotHeader>();

        public List<PivotHeader> ColumnHeaders { get; set; } = new List<PivotHeader>();

        public void BuildHeaders()
        {
            PivotHeader.BuildHeader(RowDim.First(), Source, (header) => RowHeaders.Add(header));
            PivotHeader.BuildHeader(ColumnDim.First(), Source, (header) => ColumnHeaders.Add(header));
        }
    }
}

