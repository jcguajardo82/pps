using System.Data;

namespace Soriana.PPS.Common.Data
{
    public interface ICreateTable
    {
        #region Public Methods
        DataTable CreateTable();
        #endregion
    }
}
