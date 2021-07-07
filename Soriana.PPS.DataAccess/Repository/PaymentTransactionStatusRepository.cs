using AutoMapper;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.Data.DataTables;
using Soriana.PPS.Common.DTO.Filters;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.Common.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Soriana.PPS.DataAccess.Repository
{
    public sealed class PaymentTransactionStatusRepository : RepositoryBase, IPaymentTransactionStatusRepository
    {
        #region Public Fields
        private readonly IRepositoryRead<PaymentTransactionStatus> _RepositoryRead;
        private readonly IRepositoryCreate<PaymentTransactionStatus> _RepositoryCreate;
        private readonly DataTable _PaymentTransactionStatusTable;
        #endregion
        #region Constructors
        public PaymentTransactionStatusRepository(IUnitOfWork unitOfWork,
                                                    IRepositoryCreate<PaymentTransactionStatus> repositoryCreate,
                                                    IRepositoryRead<PaymentTransactionStatus> repositoryRead,
                                                    ICreateTableType createTableType,
                                                    IMapper mapper) : base(unitOfWork: unitOfWork, mapper: mapper)
        {
            _RepositoryCreate = repositoryCreate;
            _RepositoryRead = repositoryRead;
            _PaymentTransactionStatusTable = createTableType.Create<PaymentTransactionStatusTable>();
        }
        #endregion
        #region Public Methods
        #region RepositoryRead 
        public async Task<PaymentTransactionStatus> GetByAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetByAsync(searchFilter);
        }

        public async Task<IList<PaymentTransactionStatus>> GetListAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetListAsync(searchFilter);
        }

        public async Task<IList<PaymentTransactionStatus>> GetListPagedAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetListPagedAsync(searchFilter);
        }

        public async Task<int> RecordCountAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.RecordCountAsync(searchFilter);
        }
        #endregion
        #region RepositoryCreate
        public async Task<long?> InsertAsync(PaymentTransactionStatus entity)
        {
            return await _RepositoryCreate.InsertAsync(entity);
        }

        public async Task<IList<long?>> InsertListAsync(IList<PaymentTransactionStatus> listEntities)
        {
            return await _RepositoryCreate.InsertListAsync(listEntities);
        }
        #endregion
        #region RepositoryCreate Extended Methods
        public async Task InsertPaymentTrasactionStatusWithTableTypeAsync(PaymentOrderProcessRequest request)
        {
            if (request == null ||
                !(request.TransactionReferenceID > 0) ||
                string.IsNullOrEmpty(request.PaymentTransactionService) ||
                string.IsNullOrEmpty(request.TransactionStatus)) return;
            PaymentTransactionStatusTableType paymentTransactionStatusTableType = GetEntityFromRequestObject<PaymentOrderProcessRequest, PaymentTransactionStatusTableType>(request);
            paymentTransactionStatusTableType.IsActive = true;
            _PaymentTransactionStatusTable.AddRowFrom(paymentTransactionStatusTableType);
            object inputTables = new { PaymentTransactionStatus = _PaymentTransactionStatusTable };
            SetCommand(DatabaseSchemaConstants.PROCEDURE_NAME_INSERT_PAYMENT_TRANSACTION_STATUS);
            await ExecuteProcedureWithoutScalarResult(inputTables);
            _PaymentTransactionStatusTable.Clear();
        }
        #endregion
        #endregion
    }
}
