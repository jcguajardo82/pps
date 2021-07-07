using AutoMapper;
using CyberSource.Model;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class PaymentsDeviceInformationTypeConverter : ITypeConverter<string, Ptsv2paymentsDeviceInformation>
    {
        #region Public Methods
        public Ptsv2paymentsDeviceInformation Convert(string source, Ptsv2paymentsDeviceInformation destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source))
                return destination;
            destination = new Ptsv2paymentsDeviceInformation()
            {
                FingerprintSessionId = source
            };
            return destination;
        }
        #endregion
    }
}
