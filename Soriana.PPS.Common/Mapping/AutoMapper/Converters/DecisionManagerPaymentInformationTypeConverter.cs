using AutoMapper;
using CyberSource.Model;
using Soriana.PPS.Common.Constants;
using Soriana.PPS.Common.DTO.Cybersource.Payments;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class DecisionManagerPaymentInformationTypeConverter : ITypeConverter<string, Riskv1decisionsPaymentInformation>
    {
        #region Public Methods
        public Riskv1decisionsPaymentInformation Convert(string source, Riskv1decisionsPaymentInformation destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source))
                return destination;
            string mappingSource = (source.Contains(CharactersConstants.HYPHEN_CHAR)) ? source.Split(CharactersConstants.HYPHEN_CHAR)[1] : string.Empty;
            destination = new Riskv1decisionsPaymentInformation
            {
                Customer = new PtsPaymentsPaymentInformationCustomer() { CustomerId = mappingSource }
            };
            return destination;
        }
        #endregion
    }
}
