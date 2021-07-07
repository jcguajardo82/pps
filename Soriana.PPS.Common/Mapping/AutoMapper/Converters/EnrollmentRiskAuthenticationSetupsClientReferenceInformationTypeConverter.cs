using AutoMapper;
using CyberSource.Model;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class EnrollmentRiskAuthenticationSetupsClientReferenceInformationTypeConverter : ITypeConverter<string, Riskv1authenticationsetupsClientReferenceInformation>
    {
        #region Public Methods
        public Riskv1authenticationsetupsClientReferenceInformation Convert(string source, Riskv1authenticationsetupsClientReferenceInformation destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source)) return destination;
            destination = new Riskv1authenticationsetupsClientReferenceInformation(Code: source) { };
            return destination;
        }
        #endregion
    }
}
