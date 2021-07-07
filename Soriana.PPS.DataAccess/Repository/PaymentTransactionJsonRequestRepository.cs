using AutoMapper;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.Data.DataTables;
using Soriana.PPS.Common.DTO.Filters;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.Common.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Soriana.PPS.DataAccess.Repository
{
    public sealed class PaymentTransactionJsonRequestRepository : RepositoryBase, IPaymentTransactionJsonRequestRepository
    {
        #region Public Fields
        private readonly IRepositoryRead<PaymentTransactionJsonRequest> _RepositoryRead;
        private readonly IRepositoryCreate<PaymentTransactionJsonRequest> _RepositoryCreate;
        private readonly DataTable _PaymentTransactionJsonRequestTable;
        #endregion
        #region Constructors
        public PaymentTransactionJsonRequestRepository(IUnitOfWork unitOfWork,
                                                        IRepositoryCreate<PaymentTransactionJsonRequest> repositoryCreate,
                                                        IRepositoryRead<PaymentTransactionJsonRequest> repositoryRead,
                                                        ICreateTableType createTableType,
                                                        IMapper mapper) : base(unitOfWork: unitOfWork, mapper: mapper)
        {
            _RepositoryCreate = repositoryCreate;
            _RepositoryRead = repositoryRead;
            _PaymentTransactionJsonRequestTable = createTableType.Create<PaymentTransactionJsonRequestTable>();
        }
        #endregion
        #region Public Methods
        #region RepositoryRead
        public async Task<PaymentTransactionJsonRequest> GetByAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetByAsync(searchFilter);
        }

        public async Task<IList<PaymentTransactionJsonRequest>> GetListAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetListAsync(searchFilter);
        }

        public async Task<IList<PaymentTransactionJsonRequest>> GetListPagedAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetListPagedAsync(searchFilter);
        }

        public async Task<int> RecordCountAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.RecordCountAsync(searchFilter);
        }
        #endregion
        #region RepositoryCreate
        public async Task<long?> InsertAsync(PaymentTransactionJsonRequest entity)
        {
            return await _RepositoryCreate.InsertAsync(entity);
        }

        public async Task<IList<long?>> InsertListAsync(IList<PaymentTransactionJsonRequest> listEntities)
        {
            return await _RepositoryCreate.InsertListAsync(listEntities);
        }
        #endregion
        #region RepositoryCreate Extended Methods
        public async Task UpdatePaymentTransactionJsonRequestWithTableAsync(PaymentOrderProcessRequest request)
        {
            if (request == null ||
                request.Shipments == null ||
                request.Shipments.FirstOrDefault() == null ||
                request.Shipments.FirstOrDefault().Items == null ||
                request.Shipments.FirstOrDefault().Items.FirstOrDefault() == null) return;
            PaymentTransactionJsonRequestTableType paymentTransactionJsonRequestTableType = GetEntityFromRequestObject<PaymentOrderProcessRequest, PaymentTransactionJsonRequestTableType>(request);
            paymentTransactionJsonRequestTableType.PaymentTransactionJSONRequest = JsonConvert.SerializeObject(request);
            _PaymentTransactionJsonRequestTable.AddRowFrom(paymentTransactionJsonRequestTableType);
            object inputTables = new { PaymentTransactionJsonRequest = _PaymentTransactionJsonRequestTable };
            SetCommand(DatabaseSchemaConstants.PROCEDURE_NAME_UPDATE_PAYMENT_TRANSACTION_JSON_REQUEST);
            await ExecuteProcedureWithoutScalarResult(inputTables);
            _PaymentTransactionJsonRequestTable.Clear();
        }
        #endregion
        #endregion
    }
}
