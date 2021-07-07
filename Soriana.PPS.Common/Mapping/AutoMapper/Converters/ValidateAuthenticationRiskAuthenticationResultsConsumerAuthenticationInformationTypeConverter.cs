using AutoMapper;
using CyberSource.Model;
using Soriana.PPS.Common.DTO.Salesforce;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class ValidateAuthenticationRiskAuthenticationResultsConsumerAuthenticationInformationTypeConverter : ITypeConverter<PaymentOrderProcessRequest, Riskv1authenticationresultsConsumerAuthenticationInformation>
    {
        #region Public Methods
        public Riskv1authenticationresultsConsumerAuthenticationInformation Convert(PaymentOrderProcessRequest source, Riskv1authenticationresultsConsumerAuthenticationInformation destination, ResolutionContext context)
        {
            if (source == null) return destination;
            destination = new Riskv1authenticationresultsConsumerAuthenticationInformation(
                AuthenticationTransactionId: source.TransactionAuthorizationId,
                SignedPares: string.Empty);
            return destination;
        }
        #endregion
    }
}
