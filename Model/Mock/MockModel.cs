using System;
using System.Dynamic;
using PivotTablePOC.Model;

namespace PivotTablePOC.Model.Mock
{
    public class MockModel
    {
        public MockModel(string fileContent, IReadOnlyDictionary<string, Type> metaInfo)
        {
            var rows = fileContent.Split('\n');

            var list = new List<DynamicModel>();
            string[] properties = metaInfo.Keys.ToArray();

            foreach(var row in rows.Skip(1))
            {
                var obj = new DynamicModel(metaInfo);
                var values = row.Split(',');

                if (values.Length < properties.Length) continue;

                for(int i = 0; i < properties.Length; i++)
                {
                    obj.SetValue(properties[i], values[i]);
                }

                list.Add(obj);
            }

            Data = list;

            PivotModel = new PivotModel();
        }

        public IEnumerable<DynamicModel> Data { get; set; }

        public PivotModel PivotModel { get; set; }

        public PivotTable BuildPivotTable()
        {
            var pivotTable = new PivotTable(Data, PivotModel.RowDim, PivotModel.ColumnDim, PivotModel.Measures);

            pivotTable.BuildHeaders();

            return pivotTable;
        }
    }
}

