using AutoMapper;
using Microsoft.Extensions.Configuration;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Security.JWT;
using System.Linq;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class PayloadJWTTypeConverter : ITypeConverter<PaymentOrderProcessRequest, PayloadJWTCardinalRequest>
    {
        #region Private Fields
        private readonly IConfiguration _Configuration;
        #endregion
        #region Constructors
        public PayloadJWTTypeConverter(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        #endregion
        #region Public Methods
        public PayloadJWTCardinalRequest Convert(PaymentOrderProcessRequest source, PayloadJWTCardinalRequest destination, ResolutionContext context)
        {
            if (source == null) return destination;
            destination = new PayloadJWTCardinalRequest()
            {
                OrdenDetails = new OrdenDetailsJWTCardinalRequest()
                {
                    Amount = (from shipment in source.Shipments
                              from item in shipment.Items
                              select item.ShippintItemTotal).Sum().ToString(),
                    CurrencyCode = _Configuration[ConfigurationConstants.CURRENCY_TYPE],
                    OrderNumber = source.OrderReferenceNumber
                }
            };
            return destination;
        }
        #endregion
    }
}
