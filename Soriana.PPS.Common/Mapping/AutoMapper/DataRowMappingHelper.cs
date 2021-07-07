using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Soriana.PPS.Common.Mapping.AutoMapper
{
    public static class DataRowMappingHelper
    {
        public static DataRow MapDataRowFrom<T>(T entity, DataRow row)
        {
            if (row == null) return row;
            IList<string> propertyNames = new List<string>();
            foreach (DataColumn column in row.Table.Columns)
            {
                foreach (PropertyInfo property in entity.GetType().GetProperties())
                {
                    IList<string> sourceNames = MappingHelper.GetSourceNames(typeof(T), property.Name);
                    if (sourceNames == null ||
                        !sourceNames.Any(s => s == column.ColumnName) ||
                        propertyNames.Any(pN => pN == property.Name)) continue;
                    object propertyValue = property.GetValue(entity);
                    if (Nullable.GetUnderlyingType(property.PropertyType) != null && propertyValue == null)
                        row[column.ColumnName] = DBNull.Value;
                    else
                        row[column.ColumnName] = propertyValue;
                    propertyNames.Add(property.Name);
                    break;
                }
            }
            return row;
        }

        public static T MapFromDataRow<T>(DataRow row)
        {
            T newEntity = Activator.CreateInstance<T>();
            if (row == null) return newEntity;
            IList<string> propertyNames = new List<string>();
            foreach (DataColumn column in row.Table.Columns)
            {
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    IList<string> sourceNames = MappingHelper.GetSourceNames(typeof(T), property.Name);
                    if (sourceNames == null ||
                        !sourceNames.Any(s => s == column.ColumnName) ||
                        propertyNames.Any(pN => pN == property.Name)) continue;
                    property.SetValue(newEntity, row[column.ColumnName]);
                    propertyNames.Add(property.Name);
                }
            }
            return newEntity;
        }
    }
}
