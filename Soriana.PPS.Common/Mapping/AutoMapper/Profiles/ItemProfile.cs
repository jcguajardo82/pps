using AutoMapper;
using Soriana.PPS.Common.DTO.PaymentProcessor;
using Soriana.PPS.Common.Entities;

namespace Soriana.PPS.Common.Mapping.AutoMapper.Profiles
{
    public sealed class ItemProfile : Profile
    {
        #region Constructors
        public ItemProfile()
        {
            //Item
            CreateMap<Item, ItemResult>()
                .ForMember(d => d.Barcode, s => s.MapFrom(o => o.BarCode));
        }
        #endregion
    }
}
