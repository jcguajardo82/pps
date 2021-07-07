using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;

using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.DTO.ClosureOrder;
using AutoMapper;

namespace Soriana.PPS.DataAccess.Repository
{
    public class ClosurePaymentRepository : RepositoryBase, IClosurePaymentRepository
    {
        #region Private Fields
        private readonly IRepositoryCreate<ClosureOrderGroceyRequest> _RepositoryCreate;
        #endregion

        #region Constructor
        public ClosurePaymentRepository(IUnitOfWork unitOfWork,
                                        IRepositoryCreate<ClosureOrderGroceyRequest> repositoryCreate,
                                        IMapper mapper) : base(unitOfWork: unitOfWork, mapper: mapper )
        {
            _RepositoryCreate = repositoryCreate;
        }
        #endregion

        #region Repository Write
        public async Task<long?> InsertAsync(ClosureOrderGroceyRequest entity)
        {
            return await _RepositoryCreate.InsertAsync(entity);
        }

        public async Task<IList<long?>> InsertListAsync(IList<ClosureOrderGroceyRequest> listEntities)
        {
            return await _RepositoryCreate.InsertListAsync(listEntities);
        }
        #endregion

        #region Public Methods
        public async Task<IList<GetTransactionByOrder>> GetTransactionbyOrder(string OrderID)
        {
            try
            {
                SetParameters(new { OrderReferenceNumber = OrderID });
                SetCommand(DatabaseSchemaConstants.PROCEDURE_NAME_GET_TRANSACTION);

                var result = await ExecuteProcedureWithSingleResult<GetTransactionByOrder>(Parameters);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IList<GetJsonOrder>> GetJsonbyOrder(string PaymentTransactionID)
        {
            SetParameters(new { PaymentTransactionID = PaymentTransactionID });
            SetCommand(DatabaseSchemaConstants.PROCEDURE_NAME_GET_REQUEST_PAYMENT);

            var result = await ExecuteProcedureWithSingleResult<GetJsonOrder>(Parameters);

            return result;
        }

        public async Task<IList<CobroXMLResponse>> GetOrder(string OrderId)
        {
            SetParameters(new { OrderID = OrderId });
            SetCommand(DatabaseSchemaConstants.PROCEDURE_NAME_GET_ORDER);

            var Result = await ExecuteProcedureWithSingleResult<CobroXMLResponse>(Parameters);

            return Result;
        }
        #endregion
    }
}
