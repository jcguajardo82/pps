using AutoMapper;
using CyberSource.Model;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class PaymentsPaymentInformationTypeConverter : ITypeConverter<string, Ptsv2paymentsPaymentInformation>
    {
        #region Public Methods
        public Ptsv2paymentsPaymentInformation Convert(string source, Ptsv2paymentsPaymentInformation destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source))
                return destination;
            string mappingSource = (source.Contains(CharactersConstants.HYPHEN_CHAR)) ? source.Split(CharactersConstants.HYPHEN_CHAR)[1] : string.Empty;
            destination = new Ptsv2paymentsPaymentInformation
            {
                Customer = new Ptsv2paymentsPaymentInformationCustomer() { CustomerId = mappingSource }
            };
            return destination;
        }
        #endregion
    }
}
