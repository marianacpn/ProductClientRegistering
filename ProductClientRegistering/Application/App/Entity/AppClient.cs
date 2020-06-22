using Application.App.Core;
using Application.ViewModel.Client;
using AutoMapper;
using System.Collections.Generic;
using Domain.Repository.Interface.Entity;
using Domain.Entity;
using System;
using Shared.Enums;
using Application.ViewModel;

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

        public void CreateClient(ClientCreateVM clientVM)
        {
            Client oldClient = _clientRepository.GetUnactiveClientByEmailWithTracking(clientVM.Email);

            if (oldClient != null)
            {
                oldClient.ChangeActive(true);
            }
            else
            {
                Client client = new Client(clientVM.Nome, clientVM.Sobrenome, clientVM.Email, DateTime.Now.Date, true, (int)ClientStatusEnum.bronze);

                _clientRepository.Add(client);
            }
            _clientRepository.SaveChanges();
        }

        public void DeleteClientById(int clientId)
        {
            Client client = _clientRepository.GetByClientIdWithTracking(clientId);

            client.ChangeActive(false);

            _clientRepository.SaveChanges();
        }

        public IEnumerable<KeyValuePairVM> GetAllActiveClients() => _mapper.Map<IEnumerable<KeyValuePairVM>>(_clientRepository.GetAllActiveClients());

        public IEnumerable<ClientListVM> GetAllClients() => _mapper.Map<IEnumerable<ClientListVM>>(_clientRepository.GetAllActiveClients());

        public ClientDetailsVM GetClientDetailsById(int clientId)
        {
            Client client = _clientRepository.GetClientAndProductsById(clientId);

            if (client.Status == (int)ClientStatusEnum.bronze && DateTime.Now.Year - client.DataCadastro.Year >= 5)
            {
                client.ChangeClientStatus((int)ClientStatusEnum.silver);
                _clientRepository.SaveChanges();
            }

            return _mapper.Map<ClientDetailsVM>(client);
        }
    }
}
