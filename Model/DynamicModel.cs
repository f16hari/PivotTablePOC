using System;
using System.ComponentModel;

namespace PivotTablePOC.Model
{
    public class DynamicModel
    {

        public DynamicModel(IReadOnlyDictionary<string, Type> metaInfo)
        {
            ValuePairs = new Dictionary<string, string>();
            MetaInfo = metaInfo;
        }

        private Dictionary<string, string> ValuePairs { get; set; }
        private IReadOnlyDictionary<string, Type> MetaInfo { get; set; }

        public T GetValue<T>(string property)
        {
            if (MetaInfo.TryGetValue(property, out _))
            {
                return (T)Convert.ChangeType(ValuePairs[property], typeof(T));
            }

            throw new ArgumentOutOfRangeException($"{property} is not found");
        }

        public void SetValue(string property, string value)
        {
            if (!MetaInfo.TryGetValue(property, out var type)) throw new ArgumentOutOfRangeException($"{property} is not found");

            ValuePairs[property] = value;
        }
    }
}

