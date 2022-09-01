using System;

namespace PivotTablePOC.Model
{
    public class PivotMeasure
    {
        public PivotMeasure(string property)
        {
            Property = property;
        }

        public string Property { get; set; }

        public static List<PivotMeasure> BuildMeasures(IReadOnlyList<string> properties)
        {
            return properties.Select(p => new PivotMeasure(p)).ToList();
        }

        public object GetMeasureValue(PivotHeader rowHeader, PivotHeader columnHeader)
        {
            IEnumerable<DynamicModel> source = rowHeader.Source.ToList();

            foreach(var header in columnHeader.Path)
            {
                var group = source.GroupBy(x => x.GetValue<string>(header.Dimension.Property)).FirstOrDefault(x => x.Key == header.Name);
                if(group == null)
                {
                    source = new List<DynamicModel>();
                }
                else
                {
                    source = group;
                }
            }

            return source.Sum(x => x.GetValue<int>(Property));
        }

        public object GetMeasureValue(PivotHeader header)
        {
            return header.Source.Sum(x => x.GetValue<int>(Property));
        }
    }
}

