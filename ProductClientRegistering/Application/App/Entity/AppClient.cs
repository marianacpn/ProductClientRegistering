using Application.App.Core;
using Application.ViewModel.Client;
using AutoMapper;
using System.Collections.Generic;
using Domain.Repository.Interface.Entity;

namespace Application.App.Entity
{
    public class AppClient : AppBase, Interface.IAppClient
    {
        private readonly IClientRepository _clientRepository;
        public AppClient(IClientRepository clientRepository,
            IMapper mapper) : base(mapper)
        {
            _clientRepository = clientRepository;
        }

        public IEnumerable<ClientListVM> GetAllClients() => _mapper.Map<IEnumerable<ClientListVM>>(_clientRepository.GetAllActiveClients());
    }
}
