using Newtonsoft.Json;
using Soriana.PPS.Common.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Soriana.PPS.Common.Extensions
{
    public static class DictionaryExtensions
    {
        #region Public Methods
        public static Dictionary<KT, VT> ToDictionary<KT, VT>(this object objectOrigin)
            where KT : class
            where VT : class
        {
            Dictionary<KT, VT> keyValuePairs = new Dictionary<KT, VT>();
            if (objectOrigin != null)
            {
                foreach (PropertyInfo property in objectOrigin.GetType().GetProperties())
                {
                    IList<string> sourceNames = MappingHelper.GetSourceNames(objectOrigin.GetType(), property.Name);
                    foreach (string sourceName in sourceNames)
                    {
                        if (string.IsNullOrEmpty(sourceName) ||
                            keyValuePairs.Any(kV => kV.Key.ToString().ToLower() == sourceName.ToLower())) continue;
                        bool serializeAsJson = MappingHelper.SerializeAsJson(objectOrigin.GetType(), property.Name);
                        keyValuePairs.Add(sourceName as KT, serializeAsJson ? JsonConvert.SerializeObject(property.GetValue(objectOrigin)) as VT : property.GetValue(objectOrigin) as VT);
                    }
                }
            }
            return keyValuePairs;
        }
        #endregion
    }
}
