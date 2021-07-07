using AutoMapper;
using Newtonsoft.Json;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.Data.DataTables;
using Soriana.PPS.Common.DTO.Filters;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Soriana.PPS.DataAccess.Repository
{
    public sealed class PaymentOrderRepository : RepositoryBase, IPaymentOrderRepository
    {
        #region Public Fields
        private readonly IRepositoryRead<PaymentOrder> _RepositoryRead;
        private readonly IRepositoryCreate<PaymentOrder> _RepositoryCreate;
        private readonly DataTable _PaymentOrderTable;
        private readonly DataTable _PaymentOrderShipmentTable;
        private readonly DataTable _PaymentOrderShipmentItemTable;
        private readonly DataTable _PaymentOrderJsonRequestTable;
        #endregion
        #region Constructors
        public PaymentOrderRepository(IUnitOfWork unitOfWork,
                                        IRepositoryCreate<PaymentOrder> repositoryCreate,
                                        IRepositoryRead<PaymentOrder> repositoryRead,
                                        ICreateTableType createTableType,
                                        IMapper mapper) : base(unitOfWork: unitOfWork, mapper: mapper)
        {
            _RepositoryCreate = repositoryCreate;
            _RepositoryRead = repositoryRead;
            _PaymentOrderTable = createTableType.Create<PaymentOrderTable>();
            _PaymentOrderShipmentTable = createTableType.Create<PaymentOrderShipmentTable>();
            _PaymentOrderShipmentItemTable = createTableType.Create<PaymentOrderShipmentItemTable>();
            _PaymentOrderJsonRequestTable = createTableType.Create<PaymentOrderJsonRequestTable>();
        }
        #endregion
        #region Public Methods
        #region RepositoryRead 
        public async Task<PaymentOrder> GetByAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetByAsync(searchFilter);
        }

        public async Task<IList<PaymentOrder>> GetListAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetListAsync(searchFilter);
        }

        public async Task<IList<PaymentOrder>> GetListPagedAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.GetListPagedAsync(searchFilter);
        }
        public async Task<int> RecordCountAsync(ISearchFilter searchFilter)
        {
            return await _RepositoryRead.RecordCountAsync(searchFilter);
        }
        #endregion
        #region RepositoryCreate
        public async Task<long?> InsertAsync(PaymentOrder entity)
        {
            return await _RepositoryCreate.InsertAsync(entity);
        }

        public async Task<IList<long?>> InsertListAsync(IList<PaymentOrder> listEntities)
        {
            return await _RepositoryCreate.InsertListAsync(listEntities);
        }

        public async Task InsertPaymentOrderAsync(PaymentOrderProcessRequest request)
        {
            if (request == null ||
                request.Shipments == null ||
                request.Shipments.FirstOrDefault() == null ||
                request.Shipments.FirstOrDefault().Items == null ||
                request.Shipments.FirstOrDefault().Items.FirstOrDefault() == null) return;
            PaymentOrderTableType paymentOrderTableType = GetEntityFromRequestObject<PaymentOrderProcessRequest, PaymentOrderTableType>(request);
            paymentOrderTableType.IsActive = true;
            _PaymentOrderTable.AddRowFrom(paymentOrderTableType);
            PaymentOrderJsonRequestTableType paymentOrderJsonRequestTableType = GetEntityFromRequestObject<PaymentOrderProcessRequest, PaymentOrderJsonRequestTableType>(request);
            paymentOrderJsonRequestTableType.PaymentOrderJSONRequest = JsonConvert.SerializeObject(request);
            _PaymentOrderJsonRequestTable.AddRowFrom(paymentOrderJsonRequestTableType);
            IList<PaymentOrderShipmentTableType> paymentOrderShipmentTableTypes = new List<PaymentOrderShipmentTableType>();
            IList<PaymentOrderShipmentItemTableType> paymentOrderShipmentItemTableTypes = new List<PaymentOrderShipmentItemTableType>();
            byte shipmentSequenceId = 1;
            foreach (Shipment shipment in request.Shipments)
            {
                PaymentOrderShipmentTableType paymentOrderShipmentTableType = GetEntityFromRequestObject<Shipment, PaymentOrderShipmentTableType>(shipment);
                paymentOrderShipmentTableType.OrderReferenceNumber = Convert.ToInt64(request.OrderReferenceNumber);
                paymentOrderShipmentTableType.CreatedDate = DateTime.Now;
                paymentOrderShipmentTableType.DeletedDate = null;
                paymentOrderShipmentTableType.UpdatedDate = null;
                paymentOrderShipmentTableType.IsActive = true;
                paymentOrderShipmentTableType.ShipmentIDSequence = shipmentSequenceId;
                byte itemSequenceId = 1;
                foreach (Common.DTO.Salesforce.Item item in shipment.Items)
                {
                    PaymentOrderShipmentItemTableType paymentOrderShipmentItemTableType = GetEntityFromRequestObject<Common.DTO.Salesforce.Item, PaymentOrderShipmentItemTableType>(item);
                    paymentOrderShipmentItemTableType.OrderReferenceNumber = Convert.ToInt64(request.OrderReferenceNumber);
                    paymentOrderShipmentItemTableType.CreatedDate = paymentOrderShipmentTableType.CreatedDate;
                    paymentOrderShipmentItemTableType.DeletedDate = paymentOrderShipmentTableType.DeletedDate;
                    paymentOrderShipmentItemTableType.UpdatedDate = paymentOrderShipmentTableType.UpdatedDate;
                    paymentOrderShipmentItemTableType.IsActive = paymentOrderShipmentTableType.IsActive;
                    paymentOrderShipmentItemTableType.ShipmentIDSequence = paymentOrderShipmentTableType.ShipmentIDSequence;
                    paymentOrderShipmentItemTableType.ItemIDSequence = itemSequenceId;
                    paymentOrderShipmentItemTableTypes.Add(paymentOrderShipmentItemTableType);
                    itemSequenceId += 1;
                }
                paymentOrderShipmentTableTypes.Add(paymentOrderShipmentTableType);
                shipmentSequenceId += 1;
            }
            foreach (PaymentOrderShipmentTableType paymentOrderShipmentTableType in paymentOrderShipmentTableTypes)
                _PaymentOrderShipmentTable.AddRowFrom(paymentOrderShipmentTableType);
            foreach (PaymentOrderShipmentItemTableType paymentOrderShipmentItemTableType in paymentOrderShipmentItemTableTypes)
                _PaymentOrderShipmentItemTable.AddRowFrom(paymentOrderShipmentItemTableType);
            object inputTables = new { PaymentOrder = _PaymentOrderTable, PaymentOrderJsonRequest = _PaymentOrderJsonRequestTable, PaymentOrderShipment = _PaymentOrderShipmentTable, PaymentOrderShipmentItem = _PaymentOrderShipmentItemTable };
            SetCommand(DatabaseSchemaConstants.PROCEDURE_NAME_INSERT_PAYMENT_ORDER_REQUEST);
            request.PaymentOrderID = await ExecuteProcedureWithScalarResult<long>(inputTables);
            _PaymentOrderTable.Clear();
            _PaymentOrderJsonRequestTable.Clear();
            _PaymentOrderShipmentTable.Clear();
            _PaymentOrderShipmentItemTable.Clear();
        }
        #endregion
        #endregion
    }
}
