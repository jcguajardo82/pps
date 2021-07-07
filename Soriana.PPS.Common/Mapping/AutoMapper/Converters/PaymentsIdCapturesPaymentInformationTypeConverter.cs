using AutoMapper;
using CyberSource.Model;
using Soriana.PPS.Common.Constants;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class PaymentsIdCapturesPaymentInformationTypeConverter : ITypeConverter<string, Ptsv2paymentsidcapturesPaymentInformation>
    {
        #region Public Methods
        public Ptsv2paymentsidcapturesPaymentInformation Convert(string source, Ptsv2paymentsidcapturesPaymentInformation destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source))
                return destination;
            string mappingSource = (source.Contains(CharactersConstants.HYPHEN_CHAR)) ? source.Split(CharactersConstants.HYPHEN_CHAR)[1] : string.Empty;
            destination = new Ptsv2paymentsidcapturesPaymentInformation
            {
                Customer = new Ptsv2paymentsPaymentInformationCustomer() { CustomerId = mappingSource }
            };
            return destination;
        }
        #endregion
    }
}
