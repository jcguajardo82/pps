using AutoMapper;
using CyberSource.Model;
using Microsoft.Extensions.Configuration;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication;
using Soriana.PPS.Common.DTO.Salesforce;
using System.Collections.Generic;
using System.Linq;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class ValidateAuthenticationOrderInformationTypeConverter : ITypeConverter<PaymentOrderProcessRequest, Riskv1authenticationresultsOrderInformation>
    {
        #region Private Fields
        private IRuntimeMapper _Mapper;
        private readonly IConfiguration _Configuration;
        #endregion
        #region Constructors
        public ValidateAuthenticationOrderInformationTypeConverter(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        #endregion
        #region Public Methods
        public Riskv1authenticationresultsOrderInformation Convert(PaymentOrderProcessRequest source, Riskv1authenticationresultsOrderInformation destination, ResolutionContext context)
        {
            if (source == null) return destination;
            _Mapper = context.Mapper;
            destination = new Riskv1authenticationresultsOrderInformation(
                AmountDetails: new Riskv1authenticationsOrderInformationAmountDetails
                ///TODO: Pendiente validar como debera implementarse el tipo de moneda
                (Currency: _Configuration[ConfigurationConstants.CURRENCY_TYPE],
                  TotalAmount: System.Convert.ToString(GetTotalAmount(source.Shipments))),
                LineItems: GetLineItems(source.Shipments)
            );
            return destination;
        }
        #endregion
        #region Private Methods
        private double GetTotalAmount(IList<Shipment> shipments)
        {
            if (shipments == null) return 0;
            double totalAmount = (from shipment in shipments
                                  from item in shipment.Items
                                  select item.ShippintItemTotal).Sum();
            return totalAmount;
        }
        private List<CyberSource.Model.Riskv1authenticationresultsOrderInformationLineItems> GetLineItems(IList<Shipment> shipments)
        {
            List<CyberSource.Model.Riskv1authenticationresultsOrderInformationLineItems> lineItems = new List<CyberSource.Model.Riskv1authenticationresultsOrderInformationLineItems>();
            if (shipments == null) return lineItems;
            foreach (Shipment shipment in shipments)
            {
                if (shipment == null) continue;
                foreach (Item item in shipment.Items)
                {
                    if (item == null) continue;
                    RiskAuthenticationResultsOrderInformationLineItems lineItem = _Mapper.Map<RiskAuthenticationResultsOrderInformationLineItems>(item);
                    lineItems.Add(lineItem);
                }
            }
            return lineItems;
        }
        #endregion
    }
}
