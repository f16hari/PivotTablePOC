using System;

namespace PivotTablePOC.Model
{
    public class PivotDimension
    {
        public PivotDimension(string property)
        {
            Property = property;
        }

        public PivotDimension(string property, PivotDimension parentDim) : this(property)
        {
            ParentDimension = parentDim;
            parentDim.ChildDimension = this;
        }

        public PivotDimension? ParentDimension { get; set; }

        public PivotDimension? ChildDimension { get; set; }

        public string Property { get; set; }


        public static List<PivotDimension> BuildDimensions(IReadOnlyList<string> properties)
        {
            var list = new List<PivotDimension>();

            PivotDimension? prevDim = default;
            foreach (string property in properties)
            {
                var dim = prevDim == null ? new PivotDimension(property) : new PivotDimension(property, prevDim);
                list.Add(dim);

                prevDim = dim;
            }

            return list;
        }
    }
}

