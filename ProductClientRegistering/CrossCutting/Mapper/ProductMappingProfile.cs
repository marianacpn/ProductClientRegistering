using Application.ViewModel.Product;
using AutoMapper;
using Domain.Entity;

namespace CrossCutting.Mapper
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductListVM>()
               .ForMember(dest => dest.ProductId, opt => opt.MapFrom(e => e.Id))
               .ForMember(dest => dest.Nome, opt => opt.MapFrom(e => e.Nome))
               .ForMember(dest => dest.Valor, opt => opt.MapFrom(e => $"R$ {e.Valor}"))
               .ForMember(dest => dest.ClientName, opt => opt.MapFrom(e => $"{e.Client.Nome} {e.Client.Sobrenome}"));
        }
    }
}
