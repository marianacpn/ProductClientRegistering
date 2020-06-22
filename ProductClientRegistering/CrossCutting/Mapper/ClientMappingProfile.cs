using Application.ViewModel;
using Application.ViewModel.Client;
using AutoMapper;
using Domain.Entity;
using Shared.Enums;
using Shared.ClassExtensions;

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
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(e => e.DataCadastro.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(e => ((ClientStatusEnum)e.Status).EnumDisplayName()));

            CreateMap<Client, ClientCreateVM>();

            CreateMap<Client, KeyValuePairVM>();

            CreateMap<Client, ClientDeleteVM>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(e => e.Id))
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(e => $"{e.Nome} {e.Sobrenome}"));
        }
    }
}
