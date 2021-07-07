using AutoMapper;
using CyberSource.Model;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class PaymentsIdCapturesDeviceInformationTypeConverter : ITypeConverter<string, Ptsv2paymentsidcapturesDeviceInformation>
    {
        #region Public Methods
        public Ptsv2paymentsidcapturesDeviceInformation Convert(string source, Ptsv2paymentsidcapturesDeviceInformation destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source))
                return destination;
            ///TODO: Validar como enviar la informacion de Device Information para CapturesPayment
            destination = new Ptsv2paymentsidcapturesDeviceInformation()
            {
                HostName = "",
                IpAddress = "",
                UserAgent = ""
            };
            return destination;
        }
        #endregion
    }
}
