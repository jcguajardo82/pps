using System;
using System.Collections.Generic;
using System.Linq;

namespace Soriana.PPS.Common.Mapping
{
    public static class MappingHelper
    {
        public static IList<string> GetSourceNames(Type type, string propertyName)
        {
            IList<string> sourceNames = new List<string>();
            object property = type.GetProperty(propertyName).GetCustomAttributes(false).Where(x => x.GetType() == typeof(SourceNamesAttribute)).FirstOrDefault();
            if (property != null)
                sourceNames = ((SourceNamesAttribute)property).ColumnNames;
            return sourceNames;
        }
        public static bool SerializeAsJson(Type type, string propertyName)
        {
            bool serializeAsJson = false;
            object property = type.GetProperty(propertyName).GetCustomAttributes(false).Where(x => x.GetType() == typeof(SourceNamesAttribute)).FirstOrDefault();
            if (property != null)
                serializeAsJson = ((SourceNamesAttribute)property).IsCustomClass && ((SourceNamesAttribute)property).IsCustomClassSerializedAsJson;
            return serializeAsJson;
        }
    }
}
