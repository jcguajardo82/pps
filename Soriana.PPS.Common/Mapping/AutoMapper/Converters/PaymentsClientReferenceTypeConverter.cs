using AutoMapper;
using CyberSource.Model;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class PaymentsClientReferenceTypeConverter : ITypeConverter<string, Ptsv2paymentsClientReferenceInformation>
    {
        #region Public Methods
        public Ptsv2paymentsClientReferenceInformation Convert(string source, Ptsv2paymentsClientReferenceInformation destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source)) return destination;
            destination = new Ptsv2paymentsClientReferenceInformation
            {
                Code = source
            };
            return destination;
        }
        #endregion
    }
}
