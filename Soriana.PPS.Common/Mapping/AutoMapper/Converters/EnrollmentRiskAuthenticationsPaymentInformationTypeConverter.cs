using AutoMapper;
using CyberSource.Model;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class EnrollmentRiskAuthenticationsPaymentInformationTypeConverter : ITypeConverter<string, Riskv1authenticationsPaymentInformation>
    {
        #region Public Methods
        public Riskv1authenticationsPaymentInformation Convert(string source, Riskv1authenticationsPaymentInformation destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source))
                return destination;
            string mappingSource = (source.Contains(CharactersConstants.HYPHEN_CHAR)) ? source.Split(CharactersConstants.HYPHEN_CHAR)[1] : string.Empty;
            destination = new Riskv1authenticationsPaymentInformation()
            {
                Customer = new Ptsv2paymentsPaymentInformationCustomer() { CustomerId = mappingSource }
            };
            return destination;
        }
        #endregion
    }
}
