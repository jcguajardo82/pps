using System;
using System.Data;

namespace Soriana.PPS.Common.Data
{
    public class CreateTableTypeBase : ICreateTableType
    {
        #region Public Methods
        public DataTable Create<T>()
        {
            object tableType = Activator.CreateInstance(typeof(T));
            return (tableType == null) ? null : (tableType as ICreateTable).CreateTable();
        }
        #endregion
    }
}
