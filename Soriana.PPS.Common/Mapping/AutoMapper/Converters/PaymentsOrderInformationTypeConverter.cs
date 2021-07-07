using AutoMapper;
using CyberSource.Model;
using Microsoft.Extensions.Configuration;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Cybersource.Payments;
using Soriana.PPS.Common.DTO.Salesforce;
using System.Collections.Generic;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class PaymentsOrderInformationTypeConverter : ITypeConverter<PaymentOrderProcessRequest, Ptsv2paymentsOrderInformation>
    {
        #region Private Fields
        private IRuntimeMapper _Mapper;
        private readonly IConfiguration _Configuration;
        #endregion
        #region Constructors
        public PaymentsOrderInformationTypeConverter(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        #endregion
        #region Public Methods
        public Ptsv2paymentsOrderInformation Convert(PaymentOrderProcessRequest source, Ptsv2paymentsOrderInformation destination, ResolutionContext context)
        {
            if (source == null) return destination;
            _Mapper = context.Mapper;
            destination = new Ptsv2paymentsOrderInformation()
            {
                AmountDetails = new Ptsv2paymentsOrderInformationAmountDetails
                ///TODO: Pendiente validar como debera implementarse el tipo de moneda
                { Currency = _Configuration[ConfigurationConstants.CURRENCY_TYPE] },
                LineItems = GetLineItems(source.Shipments)
            };
            return destination;
        }
        #endregion
        #region Private Methods
        private List<CyberSource.Model.Ptsv2paymentsOrderInformationLineItems> GetLineItems(IList<Shipment> shipments)
        {
            List<CyberSource.Model.Ptsv2paymentsOrderInformationLineItems> lineItems = new List<CyberSource.Model.Ptsv2paymentsOrderInformationLineItems>();
            if (shipments == null) return lineItems;
            foreach (Shipment shipment in shipments)
            {
                if (shipment == null) continue;
                foreach (Item item in shipment.Items)
                {
                    PtsPaymentsOrderInformationLineItems lineItem = _Mapper.Map<PtsPaymentsOrderInformationLineItems>(item);
                    lineItems.Add(lineItem);
                }
            }
            return lineItems;
        }
        #endregion
    }
}
