using System;
using System.Data;

namespace Soriana.PPS.Common.Data
{
    public abstract class DatabaseBase : IDatabaseBase, IDisposable
    {
        #region Public Properties
        public IDbConnection Connection { get; set; }
        protected readonly IUnitOfWork _UnitOfWork;
        #endregion
        #region Constructors
        public DatabaseBase(IDbConnection connection, IUnitOfWork unitOfWork)
        {
            Connection = connection;
            Connection.Open();
            _UnitOfWork = unitOfWork;
        }
        ~DatabaseBase()
        {
            Dispose();
        }
        #endregion
        #region Public Virtual Methods
        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Close();
                Connection.Dispose();
            }
            if (_UnitOfWork != null)
                _UnitOfWork.Dispose();
        }
        #endregion
    }
}
