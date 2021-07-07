using AutoMapper;
using CyberSource.Model;
using Soriana.PPS.Common.DTO.Cybersource.DecisionManager;
using Soriana.PPS.Common.DTO.Cybersource.PayerAuthentication;
using Soriana.PPS.Common.DTO.Cybersource.Payments;
using Soriana.PPS.Common.DTO.Cybersource.TokenManagement;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Profiles
{
    public sealed class DTOProfile : Profile
    {
        #region Constructors
        public DTOProfile()
        {
            CreateMap<RiskV1DecisionsPost201Response, DecisionManager201Response>();
            CreateMap<RiskV1AuthenticationsPost201Response, Enrollment201Response>();
            CreateMap<RiskV1AuthenticationResultsPost201Response, ValidateAuthentication201Response>();
            CreateMap<PtsV2PaymentsCapturesPost201Response, CapturesPayments201Response>();
            CreateMap<PtsV2PaymentsPost201Response, Payments201Response>();
            CreateMap<TmsV2CustomersResponse, TmsCustomersResponse>();
            CreateMap<RiskV1DecisionsPost400Response, DecisionManager400Response>();
            CreateMap<RiskV1AuthenticationsPost400Response, Enrollment400Response>();
            CreateMap<RiskV1AuthenticationsPost400Response, ValidateAuthentication400Response>();
            CreateMap<PtsV2PaymentsCapturesPost400Response, CapturesPayments400Response>();
            CreateMap<PtsV2PaymentsPost400Response, Payments400Response>();
        }
        #endregion
    }
}
