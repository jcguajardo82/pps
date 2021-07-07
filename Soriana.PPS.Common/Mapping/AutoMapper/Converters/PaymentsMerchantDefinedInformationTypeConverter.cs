using AutoMapper;
using Soriana.PPS.Common.DTO.Cybersource.Payments;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Mapping.AutoMapper.Converters.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class PaymentsMerchantDefinedInformationTypeConverter : ITypeConverter<PaymentOrderProcessRequest, List<PtsPaymentsMerchantDefinedInformation>>
    {
        #region Private Fields
        private readonly string _ServiceName;
        #endregion
        #region Constructors
        public PaymentsMerchantDefinedInformationTypeConverter(ServicesEnum servicesEnum)
        {
            _ServiceName = servicesEnum.ToString();
        }
        #endregion
        #region Public Methods
        public List<PtsPaymentsMerchantDefinedInformation> Convert(PaymentOrderProcessRequest source, List<PtsPaymentsMerchantDefinedInformation> destination, ResolutionContext context)
        {
            if (source == null) return destination;
            PaymentOrderProcessRequestHelper.IsShippingReferenceNumberValid(source, _ServiceName); //ServicesEnum.CapturePayment.ToString()
            //IList<MerchantDefinedData> merchantDefinedDataList = PaymentOrderProcessRequestHelper.GetMerchantDefinedData(source, _ServiceName); //ServicesEnum.CapturePayment.ToString()
            destination = context.Mapper.Map<IList<PtsPaymentsMerchantDefinedInformation>>(source.MerchantDefinedData).ToList();
            return destination;
        }
        #endregion
    }
}
