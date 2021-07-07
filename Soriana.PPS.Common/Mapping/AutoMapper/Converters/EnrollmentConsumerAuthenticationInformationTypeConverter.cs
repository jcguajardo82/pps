using AutoMapper;
using CyberSource.Model;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class EnrollmentConsumerAuthenticationInformationTypeConverter : ITypeConverter<long, Riskv1decisionsConsumerAuthenticationInformation>
    {
        #region Public Methods
        public Riskv1decisionsConsumerAuthenticationInformation Convert(long source, Riskv1decisionsConsumerAuthenticationInformation destination, ResolutionContext context)
        {
            if (source == 0) return destination;
            destination = new Riskv1decisionsConsumerAuthenticationInformation
            {
                ReferenceId = source.ToString()
            };
            return destination;
        }
        #endregion
    }
}
