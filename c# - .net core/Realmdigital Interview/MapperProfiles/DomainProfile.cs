using AutoMapper;
using Realmdigital_Interview.Entities;

namespace Realmdigital_Interview.MapperProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile(){


        CreateMap<ApiResponsePrice, DtoApiResponsePrice>()
            .ForMember(x=>x.Currency, o=>o.MapFrom(s=>s.CurrencyCode))
            .ForMember(x=>x.Price, o=>o.MapFrom(s=>s.SellingPrice));
            
        CreateMap<ApiResponseProduct, DtoApiResponseProduct>()
          .ForMember(d=>d.Id, o=>o.MapFrom(s => s.BarCode))
          .ForMember(d=>d.Name, o => o.MapFrom(s=>s.ItemName))        
           .ForMember(d=>d.Prices, o=> o.MapFrom(s=>s.PriceRecords));
        }}
}