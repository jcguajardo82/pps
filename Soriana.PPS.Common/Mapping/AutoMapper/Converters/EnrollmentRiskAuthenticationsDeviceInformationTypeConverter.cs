using AutoMapper;
using CyberSource.Model;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class EnrollmentRiskAuthenticationsDeviceInformationTypeConverter : ITypeConverter<string, Riskv1authenticationsDeviceInformation>
    {
        #region Public Methods
        public Riskv1authenticationsDeviceInformation Convert(string source, Riskv1authenticationsDeviceInformation destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source))
                return destination;
            destination = new Riskv1authenticationsDeviceInformation()
            {
                ///TODO: Validar Device Information para el Enrollment
                IpAddress = source
            };
            return destination;
        }
        #endregion
    }
}
