using AutoMapper;
using CyberSource.Model;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class DecisionManagerDeviceInformationTypeConverter : ITypeConverter<string, Riskv1decisionsDeviceInformation>
    {
        #region Public Methods
        public Riskv1decisionsDeviceInformation Convert(string source, Riskv1decisionsDeviceInformation destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source))
                return destination;
            destination = new Riskv1decisionsDeviceInformation()
            {
                FingerprintSessionId = source
            };
            return destination;
        }
        #endregion
    }
}
