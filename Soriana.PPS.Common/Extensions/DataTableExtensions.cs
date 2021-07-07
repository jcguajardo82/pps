using Soriana.PPS.Common.Mapping.AutoMapper;
using System.Data;

namespace Soriana.PPS.Common.Extensions
{
    public static class DataTableExtensions
    {
        #region Public Methods
        public static void AddRowFrom<Entity>(this DataTable dataTable, Entity entity)
        {
            if (dataTable == null) return;
            dataTable.Rows.Add(DataRowMappingHelper.MapDataRowFrom(entity, dataTable.NewRow()));
        }
        #endregion
    }
}
