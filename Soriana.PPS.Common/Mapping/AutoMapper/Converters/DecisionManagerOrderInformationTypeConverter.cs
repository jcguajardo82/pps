using AutoMapper;
using CyberSource.Model;
using Microsoft.Extensions.Configuration;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Cybersource.DecisionManager;
using Soriana.PPS.Common.DTO.Salesforce;
using System.Collections.Generic;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class DecisionManagerOrderInformationTypeConverter : ITypeConverter<PaymentOrderProcessRequest, Riskv1decisionsOrderInformation>
    {
        #region Private Fields
        private IRuntimeMapper _Mapper;
        private readonly IConfiguration _Configuration;
        #endregion
        #region Constructors
        public DecisionManagerOrderInformationTypeConverter(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        #endregion
        #region Public Methods
        public Riskv1decisionsOrderInformation Convert(PaymentOrderProcessRequest source, Riskv1decisionsOrderInformation destination, ResolutionContext context)
        {
            if (source == null) return destination;
            _Mapper = context.Mapper;
            destination = new Riskv1decisionsOrderInformation()
            {
                AmountDetails = new RiskDecisionsOrderInformationAmountDetails()
                ///TODO: Pendiente validar como debera implementarse el tipo de moneda
                { Currency = _Configuration[ConfigurationConstants.CURRENCY_TYPE] },
                LineItems = GetLineItems(source.Shipments)
            };
            return destination;
        }
        #endregion
        #region Private Methods
        private List<CyberSource.Model.Riskv1decisionsOrderInformationLineItems> GetLineItems(IList<Shipment> shipments)
        {
            List<CyberSource.Model.Riskv1decisionsOrderInformationLineItems> lineItems = new List<CyberSource.Model.Riskv1decisionsOrderInformationLineItems>();
            if (shipments == null) return lineItems;
            foreach (Shipment shipment in shipments)
            {
                if (shipment == null) continue;
                foreach (Item item in shipment.Items)
                {
                    RiskDecisionsOrderInformationLineItems lineItem = _Mapper.Map<RiskDecisionsOrderInformationLineItems>(item);
                    lineItems.Add(lineItem);
                }
            }
            return lineItems;
        }
        #endregion
    }
}
