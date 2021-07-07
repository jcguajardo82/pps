using System;
using System.Data;

namespace Soriana.PPS.Common.Data
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        #region Public Properties
        private readonly IDbConnection _Connection = null;
        private readonly Guid _Id = Guid.Empty;
        private IDbTransaction _Transaction = null;
        #endregion
        #region Constructor
        public UnitOfWork(IDbConnection connection)
        {
            _Id = Guid.NewGuid();
            _Connection = connection;
        }
        #endregion
        #region Public Methods
        public Guid Id { get { return _Id; } }

        public IDbConnection Connection { get { return _Connection; } }

        public IDbTransaction Transaction { get { return _Transaction; } }

        public void Begin()
        {
            _Transaction = _Connection.BeginTransaction();
        }

        public void Commit()
        {
            _Transaction.Commit();
            Dispose();
        }

        public void Dispose()
        {
            if (_Transaction == null) return;
            _Transaction.Dispose();
            _Transaction = null;
        }

        public void Rollback()
        {
            _Transaction.Rollback();
            Dispose();
        }
        #endregion
    }
}
