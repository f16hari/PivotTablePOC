using System;

namespace PivotTablePOC.Model
{
    public static class TypeMappings
    {
        public static Dictionary<string, Type> Types = new Dictionary<string, Type>()
        {
            { "int", typeof(int) },
            { "string", typeof(string) }
        };
    }
}

