using Application.ViewModel.Client;
using AutoMapper;
using Domain.Entity;
namespace CrossCutting.Mapper
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<Client, ClientListVM>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(e => e.Id))
                .ForMember(dest => dest.NomeSobrenome, opt => opt.MapFrom(e => $"{e.Nome} {e.Sobrenome}"))
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(e => e.DataCadastro.ToString("dd/MM/yyyy")));


            CreateMap<Client, ClientDetailsVM>()
                 .ForMember(dest => dest.ClientId, opt => opt.MapFrom(e => e.Id))
                .ForMember(dest => dest.NomeSobrenome, opt => opt.MapFrom(e => $"{e.Nome} {e.Sobrenome}"))
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(e => e.DataCadastro.ToString("dd/MM/yyyy")));

            CreateMap<Client, ClientCreateVM>();

            CreateMap<Client, ClientDeleteVM>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(e => e.Id))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(e => $"{e.Nome} {e.Sobrenome}"));
        }
    }
}
