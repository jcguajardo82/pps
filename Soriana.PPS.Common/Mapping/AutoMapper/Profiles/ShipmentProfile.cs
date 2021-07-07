using AutoMapper;
using Soriana.PPS.Common.DTO.Salesforce;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Profiles
{
    public sealed class ShipmentProfile : Profile
    {
        #region Constructors
        public ShipmentProfile()
        {
            //Shipment
            CreateMap<Shipment, ShipmentResponse>()
                .ForMember(d => d.ShippingReferenceNumber, s => s.MapFrom(o => o.ShippingReferenceNumber));
        }
        #endregion
    }
}
