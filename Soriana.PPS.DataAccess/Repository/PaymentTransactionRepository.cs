using AutoMapper;
using Dapper;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.Data.DataTables;
using Soriana.PPS.Common.DTO.Filters;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.Common.Extensions;
using Soriana.PPS.DataAccess.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Soriana.PPS.DataAccess.Repository
{
    public sealed class PaymentTransactionRepository : RepositoryBase, IPaymentTransactionRepository
    {
        #region Public Fields
        private readonly IRepositoryRead<PaymentTransaction> _RepositoryRead;
        private readonly IRepositoryCreate<PaymentTransaction> _RepositoryCreate;
        private readonly DataTable _PaymentTransactionTable;
        private readonly DataTable _PaymentTransactionShipmentTable;
        private readonly DataTable _PaymentTransactionShipmentItemTable;
        private readonly DataTable _PaymentTransactionJsonRequestTable;
        #endregion
        #region Constructors
        public PaymentTransactionRepository(IUnitOfWork unitOfWork,
                                            IRepositoryCreate<PaymentTransaction> repositoryCreate,
                                            IRepositoryRead<PaymentTransaction> repositoryRead,
                                            ICreateTableType createTableType,
                                            IMapper mapper) : base(unitOfWork: unitOfWork, mapper: mapper)
        {
            _RepositoryCreate = repositoryCreate;
            _RepositoryRead = repositoryRead;
            _PaymentTransactionTable = createTableType.Create<PaymentTransactionTable>();
            _PaymentTransactionShipmentTable = createTableType.Create<PaymentTransactionShipmentTable>();
            _PaymentTransactionShipmentItemTable = createTableType.Create<PaymentTransactionShipmentItemTable>();
            _PaymentTransactionJsonRequestTable = createTableType.Create<PaymentTransactionJsonRequestTable>();
        }
        #endregion
        #region Public Methods
        #region RepositoryRead 
        public async Task<PaymentTransaction> GetByAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetByAsync(searchFilter);
        }

        public async Task<IList<PaymentTransaction>> GetListAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetListAsync(searchFilter);
        }

        public async Task<IList<PaymentTransaction>> GetListPagedAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetListPagedAsync(searchFilter);
        }
        public async Task<int> RecordCountAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.RecordCountAsync(searchFilter);
        }
        #endregion
        #region RepositoryRead Extended Methods
        public async Task<long> GetSequenceAsync()
        {
            ClearCommand();
            SetCommand(string.Format(DatabaseSchemaConstants.SQL_STATEMENT_EXECUTE_PROCEDURE, DatabaseSchemaConstants.PROCEDURE_NAME_GET_PAYMENT_TRANSACTION_SEQUENCE));
            long sequence = await UnitOfWork.Connection.ExecuteScalarAsync<long>(Command);
            ClearCommand();
            return sequence;
        }

        public async Task<PaymentTransaction> GetPaymentTransactionAsync(PaymentProcessTransactionFilter filter)
        {
            return await UnitOfWork.Connection.GetAsync<PaymentTransaction>(new PaymentTransaction { PaymentTransactionID = filter.PaymentProcessTransactionID, IsActive = filter.IsActive }, null, null);
        }

        public async Task<IList<PaymentTransaction>> GetChildrenPaymentTransactionAsync(PaymentProcessTransactionFilter filter)
        {
            IEnumerable<PaymentTransaction> paymentProcessTransactions = await UnitOfWork.Connection.GetListAsync<PaymentTransaction>(new PaymentTransaction { PaymentTransactionID = filter.ParentPaymentProcessTransactionID, IsActive = filter.IsActive }, null, null);
            return paymentProcessTransactions.AsList();
        }
        #endregion
        #region RepositoryCreate
        public async Task<long?> InsertAsync(PaymentTransaction entity)
        {
            return await _RepositoryCreate.InsertAsync(entity);
        }

        public async Task<IList<long?>> InsertListAsync(IList<PaymentTransaction> listEntities)
        {
            return await _RepositoryCreate.InsertListAsync(listEntities);
        }

        public async Task InsertPaymentTransactionWithTableTypeAsync(PaymentOrderProcessRequest request)
        {
            if (request == null ||
                request.Shipments == null ||
                request.Shipments.FirstOrDefault() == null ||
                request.Shipments.FirstOrDefault().Items == null ||
                request.Shipments.FirstOrDefault().Items.FirstOrDefault() == null) return;
            PaymentTransactionTableType paymentTransactionTableType = GetEntityFromRequestObject<PaymentOrderProcessRequest, PaymentTransactionTableType>(request);
            paymentTransactionTableType.IsActive = true;
            _PaymentTransactionTable.AddRowFrom(paymentTransactionTableType);
            PaymentTransactionJsonRequestTableType paymentTransactionJsonRequestTableType = GetEntityFromRequestObject<PaymentOrderProcessRequest, PaymentTransactionJsonRequestTableType>(request);
            paymentTransactionJsonRequestTableType.PaymentTransactionJSONRequest = JsonConvert.SerializeObject(request);
            _PaymentTransactionJsonRequestTable.AddRowFrom(paymentTransactionJsonRequestTableType);
            IList<PaymentTransactionShipmentTableType> paymentTransactionShipmentTableTypes = new List<PaymentTransactionShipmentTableType>();
            IList<PaymentTransactionShipmentItemTableType> paymentTransactionShipmentItemTableTypes = new List<PaymentTransactionShipmentItemTableType>();
            byte shipmentSequenceId = 1;
            foreach (Shipment shipment in request.Shipments)
            {
                PaymentTransactionShipmentTableType paymentTransactionShipmentTableType = GetEntityFromRequestObject<Shipment, PaymentTransactionShipmentTableType>(shipment);
                paymentTransactionShipmentTableType.PaymentOrderID = request.PaymentOrderID;
                paymentTransactionShipmentTableType.PaymentTransactionID = request.TransactionReferenceID;
                paymentTransactionShipmentTableType.OrderReferenceNumber = Convert.ToInt64(request.OrderReferenceNumber);
                paymentTransactionShipmentTableType.CreatedDate = DateTime.Now;
                paymentTransactionShipmentTableType.DeletedDate = null;
                paymentTransactionShipmentTableType.UpdatedDate = null;
                paymentTransactionShipmentTableType.IsActive = true;
                paymentTransactionShipmentTableType.ShipmentIDSequence = shipmentSequenceId;
                byte itemSequenceId = 1;
                foreach (Common.DTO.Salesforce.Item item in shipment.Items)
                {
                    PaymentTransactionShipmentItemTableType paymentTransactionShipmentItemTableType = GetEntityFromRequestObject<Common.DTO.Salesforce.Item, PaymentTransactionShipmentItemTableType>(item);
                    paymentTransactionShipmentItemTableType.PaymentOrderID = paymentTransactionShipmentTableType.PaymentOrderID;
                    paymentTransactionShipmentItemTableType.PaymentTransactionID = paymentTransactionShipmentTableType.PaymentTransactionID;
                    paymentTransactionShipmentItemTableType.OrderReferenceNumber = Convert.ToInt64(request.OrderReferenceNumber);
                    paymentTransactionShipmentItemTableType.CreatedDate = paymentTransactionShipmentTableType.CreatedDate;
                    paymentTransactionShipmentItemTableType.DeletedDate = paymentTransactionShipmentTableType.DeletedDate;
                    paymentTransactionShipmentItemTableType.UpdatedDate = paymentTransactionShipmentTableType.UpdatedDate;
                    paymentTransactionShipmentItemTableType.IsActive = paymentTransactionShipmentTableType.IsActive;
                    paymentTransactionShipmentItemTableType.ShipmentIDSequence = paymentTransactionShipmentTableType.ShipmentIDSequence;
                    paymentTransactionShipmentItemTableType.ItemIDSequence = itemSequenceId;
                    paymentTransactionShipmentItemTableTypes.Add(paymentTransactionShipmentItemTableType);
                    itemSequenceId += 1;
                }
                paymentTransactionShipmentTableTypes.Add(paymentTransactionShipmentTableType);
                shipmentSequenceId += 1;
            }
            foreach (PaymentTransactionShipmentTableType paymentTransactionShipmentTableType in paymentTransactionShipmentTableTypes)
                _PaymentTransactionShipmentTable.AddRowFrom(paymentTransactionShipmentTableType);
            foreach (PaymentTransactionShipmentItemTableType paymentTransactionShipmentItemTableType in paymentTransactionShipmentItemTableTypes)
                _PaymentTransactionShipmentItemTable.AddRowFrom(paymentTransactionShipmentItemTableType);
            object inputTables = new { PaymentTransaction = _PaymentTransactionTable, PaymentTransactionJsonRequest = _PaymentTransactionJsonRequestTable, PaymentTransactionShipment = _PaymentTransactionShipmentTable, PaymentTransactionShipmentItem = _PaymentTransactionShipmentItemTable };
            SetCommand(DatabaseSchemaConstants.PROCEDURE_NAME_INSERT_PAYMENT_TRANSACTION);
            await ExecuteProcedureWithoutScalarResult(inputTables);
            _PaymentTransactionTable.Clear();
            _PaymentTransactionJsonRequestTable.Clear();
            _PaymentTransactionShipmentTable.Clear();
            _PaymentTransactionShipmentItemTable.Clear();
        }
        #endregion
        #endregion
    }
}
