using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Soriana.PPS.Common.Data
{
    public abstract class RepositoryBase : IRepositoryBase
    {
        #region Private Fields
        private string _Command;
        private object _Parameters;
        public readonly IMapper _Mapper;
        #endregion
        #region Public Properties
        protected IUnitOfWork UnitOfWork { get; set; }
        public string Command { get { return _Command; } }
        public object Parameters { get { return _Parameters; } }
        #endregion
        #region Constructors
        public RepositoryBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }
        #endregion
        #region Public Methods
        public virtual T CreateRepository<T, C, R, U, D>(C create, R read, U update, D delete)
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { UnitOfWork, create, read, update, delete });
        }
        public virtual void SetCommand(string commad)
        {
            _Command = commad;
        }

        public virtual void ClearCommand()
        {
            _Command = string.Empty;
        }

        public virtual void SetParameters(object parameters)
        {
            _Parameters = parameters;
        }

        public virtual void ClearParameters()
        {
            _Parameters = null;
        }

        public virtual async Task<IList<R1>> ExecuteProcedureWithSingleResult<R1>(object inputParams)
        {
            SqlMapper.GridReader gridReaderResults = await UnitOfWork.Connection.QueryMultipleAsync(_Command, inputParams, null, null, CommandType.StoredProcedure);
            IEnumerable<R1> resultsR1 = await gridReaderResults.ReadAsync<R1>();
            return resultsR1.AsList();
        }

        public virtual OutputType GetEntityFromRequestObject<InputType, OutputType>(InputType request)
        {
            OutputType returnType = _Mapper.Map<OutputType>(request);
            return returnType;
        }

        public virtual async Task<T> ExecuteProcedureWithScalarResult<T>(object inputTables)
        {
            using IDbTransaction transaction = UnitOfWork.Connection.BeginTransaction();
            T result = await UnitOfWork.Connection.ExecuteScalarAsync<T>(_Command, inputTables, transaction, null, commandType: CommandType.StoredProcedure);
            transaction.Commit();
            ClearCommand();
            return result;
        }

        public virtual async Task ExecuteProcedureWithoutScalarResult(object inputTables)
        {
            using IDbTransaction transaction = UnitOfWork.Connection.BeginTransaction();
            await UnitOfWork.Connection.ExecuteScalarAsync(_Command, inputTables, transaction, null, commandType: CommandType.StoredProcedure);
            transaction.Commit();
            ClearCommand();
        }
        #endregion
    }
}
