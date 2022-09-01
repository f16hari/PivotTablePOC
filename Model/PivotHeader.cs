using System;

namespace PivotTablePOC.Model
{
    public class PivotHeader
    {
        public PivotHeader(string name, PivotDimension dimension, PivotHeader? parentHeader, IEnumerable<DynamicModel> source, int level)
        {
            Name = name;
            Dimension = dimension;
            ParentHeader = parentHeader;
            Level = level;
        }

        public string Name { get; set; }

        public PivotDimension Dimension { get; set; }

        public PivotHeader? ParentHeader { get; set; }

        public List<PivotHeader> ChildHeaders { get; set; } = new List<PivotHeader>();

        public IEnumerable<DynamicModel> Source { get; set; } = new List<DynamicModel>();

        public List<PivotHeader> Path { get; set; } = new List<PivotHeader>();

        public int Level { get; set; } = 1;

        public static void BuildHeader(PivotDimension dimension, IEnumerable<DynamicModel> source, Action<PivotHeader> traversalCallback, int level = 1, PivotHeader? parentHeader = null, List<PivotHeader>? parentPath = null)
        {
            var groups = source.GroupBy(x => x.GetValue<string>(dimension.Property));

            foreach (var group in groups)
            {
                var header = new PivotHeader(group.Key, dimension, parentHeader, group, level);

                if(parentHeader != null)
                {
                    parentHeader.ChildHeaders.Add(header);
                }

                if(parentPath != null)
                {
                    header.Path.AddRange(parentPath);
                }

                header.Path.Add(header);
                header.Source = group;
                traversalCallback.Invoke(header);

                if (dimension.ChildDimension != null)
                {
                    BuildHeader(dimension.ChildDimension, group, traversalCallback, level + 1, header);
                }
            }
        }
    }
}