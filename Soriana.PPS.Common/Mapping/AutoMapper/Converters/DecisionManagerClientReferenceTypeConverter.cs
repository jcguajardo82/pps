using AutoMapper;
using CyberSource.Model;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Converters
{
    public sealed class DecisionManagerClientReferenceTypeConverter : ITypeConverter<string, Riskv1decisionsClientReferenceInformation>
    {
        #region Public Methods
        public Riskv1decisionsClientReferenceInformation Convert(string source, Riskv1decisionsClientReferenceInformation destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source)) return destination;
            destination = new Riskv1decisionsClientReferenceInformation
            {
                Code = source
            };
            return destination;
        }
        #endregion
    }
}
