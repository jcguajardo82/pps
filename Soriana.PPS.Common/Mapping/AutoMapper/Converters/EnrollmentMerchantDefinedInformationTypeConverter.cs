using AutoMapper;
using Soriana.PPS.Common.DTO.Cybersource.DecisionManager;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Enums;
using Soriana.PPS.Common.Mapping.AutoMapper.Converters.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class EnrollmentMerchantDefinedInformationTypeConverter : ITypeConverter<PaymentOrderProcessRequest, List<RiskDecisionsMerchantDefinedInformation>>
    {
        #region Public Methods
        public List<RiskDecisionsMerchantDefinedInformation> Convert(PaymentOrderProcessRequest source, List<RiskDecisionsMerchantDefinedInformation> destination, ResolutionContext context)
        {
            if (source == null) return destination;
            PaymentOrderProcessRequestHelper.IsShippingReferenceNumberValid(source, ServicesEnum.CheckPayerAuthEnrollment.ToString());
            //IList<MerchantDefinedData> merchantDefinedDataList = PaymentOrderProcessRequestHelper.GetMerchantDefinedData(source, ServicesEnum.CheckPayerAuthEnrollment.ToString());
            destination = context.Mapper.Map<IList<RiskDecisionsMerchantDefinedInformation>>(source.MerchantDefinedData).ToList();
            return destination;
        }
        #endregion
    }
}
