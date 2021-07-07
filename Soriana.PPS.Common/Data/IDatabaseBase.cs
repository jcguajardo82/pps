using System.Data;

namespace Soriana.PPS.Common.Data
{
    public interface IDatabaseBase
    {
        #region Public Properties
        IDbConnection Connection { get; set; }
        #endregion
    }
}
