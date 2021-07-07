using AutoMapper;
using Soriana.PPS.Common.DTO.Common;
using Soriana.PPS.Common.DTO.Salesforce;
using Soriana.PPS.Common.Mapping.AutoMapper.Converters;
using Soriana.PPS.Common.Security.JWT;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Profiles
{
    public sealed class JsonWebTokenProfile : Profile
    {
        #region Constructors
        public JsonWebTokenProfile()
        {
            CreateMap<PaymentOrderProcessRequest, PayloadJWTCardinalRequest>()
                .ConvertUsing<PayloadJWTTypeConverter>();
            CreateMap<PaymentOrderProcessRequest, JsonWebTokenRequest>()
                .ForMember(d => d.Payload, s => s.MapFrom(o => o))
                .ForMember(d => d.ReferenceId, s => s.MapFrom(o => o.TransactionReferenceID));
        }
        #endregion
    }
}
