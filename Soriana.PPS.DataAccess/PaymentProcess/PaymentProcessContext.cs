using Soriana.PPS.Common.Data;
using Soriana.PPS.Common.DTO.PaymentProcessor;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Entities;
using Soriana.PPS.DataAccess.Filters;
using Soriana.PPS.DataAccess.Repository;
using Soriana.PPS.Common.DTO.ClosureOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Soriana.PPS.DataAccess.PaymentProcess
{
    public class PaymentProcessContext : DatabaseBase, IPaymentProcessContext
    {
        #region Private Fields
        private readonly IClientHasTokenRepository _ClientHasTokenRepository;

        private readonly IPaymentTransactionRepository _PaymentTransactionRepository;

        private readonly IPaymentTransactionStatusRepository _PaymentTransactionStatusRepository;

        private readonly IItemRepository _ItemRepository;

        private readonly IPaymentOrderRepository _PaymentOrderRepository;

        private readonly IPaymentTransactionJsonRequestRepository _PaymentTransactionJsonRequestRepository;

        private readonly IClosurePaymentRepository _ClosurePaymentRepository;
        #endregion

        #region Constructors
        public PaymentProcessContext(IDbConnection connection,
                                        IUnitOfWork unitOfWork,
                                        IClientHasTokenRepository clientHasTokenRepository,
                                        IPaymentTransactionRepository paymentProcessTransactionRepository,
                                        IItemRepository itemRepository,
                                        IPaymentOrderRepository paymentOrderRepository,
                                        IClosurePaymentRepository closurePaymentRepository,
                                        IPaymentTransactionStatusRepository paymentTransactionStatusRepository,
                                        IPaymentTransactionJsonRequestRepository paymentTransactionJsonRequestRepository
                                    ) : base(connection, unitOfWork: unitOfWork)
        {
            _ClientHasTokenRepository = clientHasTokenRepository;
            _PaymentTransactionRepository = paymentProcessTransactionRepository;
            _PaymentTransactionStatusRepository = paymentTransactionStatusRepository;
            _ItemRepository = itemRepository;
            _PaymentOrderRepository = paymentOrderRepository;
            _PaymentTransactionJsonRequestRepository = paymentTransactionJsonRequestRepository;
            _ClosurePaymentRepository = closurePaymentRepository;
        }
        #endregion

        #region Public Methods PaymentTransaction Entity
        public async Task<long> GetPaymentTransactionIDAsync()
        {
            return await _PaymentTransactionRepository.GetSequenceAsync();
        }

        public async Task InsertPaymentTransactionAsync(PaymentTransaction entity)
        {
            try
            {
                _UnitOfWork.Begin();
                await _PaymentTransactionRepository.InsertAsync(entity);
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<PaymentTransaction> GetPaymentTransactionAsync(PaymentProcessTransactionFilter filter)
        {
            return await _PaymentTransactionRepository.GetPaymentTransactionAsync(filter);
        }

        public async Task<IList<PaymentTransaction>> GetChildrenPaymentTransactionAsync(PaymentProcessTransactionFilter filter)
        {
            return await _PaymentTransactionRepository.GetChildrenPaymentTransactionAsync(filter);
        }
        public async Task InsertPaymentTransactionWithTableTypeAsync(PaymentOrderProcessRequest request)
        {
            await _PaymentTransactionRepository.InsertPaymentTransactionWithTableTypeAsync(request);
        }
        #endregion
        #region Public Methods ClientHasToken Entity
        public async Task InsertClientHasTokenAsync(ClientHasToken entity)
        {
            try
            {
                _UnitOfWork.Begin();
                await _ClientHasTokenRepository.InsertAsync(entity);
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<ClientHasToken> GetClientHasTokenBy(ClientHasTokenFilter filter)
        {
            return await _ClientHasTokenRepository.GetClientHasTokenBy(filter);
        }

        public async Task<IList<ItemResult>> GetGroceryItems()
        {
            return await _ItemRepository.GetGroceryItems();
        }

        public async Task<IList<ItemResult>> GetNonGroceryItems()
        {
            return await _ItemRepository.GetNonGroceryItems();
        }
        #endregion
        #region Public Methods PaymentOrder Entity
        public async Task InsertPaymentOrderAsync(PaymentOrderProcessRequest request)
        {
            await _PaymentOrderRepository.InsertPaymentOrderAsync(request);
        }
        #endregion
        #region Public Methods PaymentTransactionJsonRequest Entity
        public async Task UpdatePaymentTransactionJsonRequestAsync(PaymentOrderProcessRequest request)
        {
            await _PaymentTransactionJsonRequestRepository.UpdatePaymentTransactionJsonRequestWithTableAsync(request);
        }
        #endregion
        #region Public Methods PaymentTransactionStatus Entity
        public async Task InsertPaymentTrasactionStatusWithTableTypeAsync(PaymentOrderProcessRequest request)
        {
            await _PaymentTransactionStatusRepository.InsertPaymentTrasactionStatusWithTableTypeAsync(request);
        }
        #endregion
        public async Task<IList<GetTransactionByOrder>> GetTransactionbyOrder(string OrderID)
        {
            return await _ClosurePaymentRepository.GetTransactionbyOrder(OrderID);
        }

        public async Task<IList<GetJsonOrder>> GetJsonbyOrder(string PaymentTransactionID)
        {
            return await _ClosurePaymentRepository.GetJsonbyOrder(PaymentTransactionID);
        }

        public async Task<IList<CobroXMLResponse>> GetOrder(string OrderId)
        {
            return await _ClosurePaymentRepository.GetOrder(OrderId);
        }
    }
}
