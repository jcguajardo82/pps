using System.Data;

namespace Soriana.PPS.Common.Data
{
    public interface ICreateTableType
    {
        #region Public Methods
        DataTable Create<T>();
        #endregion
    }
}
