using Application.ViewModel.Client;
using System.Collections.Generic;

namespace Application.App.Interface
{
    public interface IAppClient
    {
        IEnumerable<ClientListVM> GetAllClients();
        void CreateClient(ClientCreateVM clientVM);
        void DeleteClientById(int clientId);
        ClientDetailsVM GetClientDetailsById(int clientId);
    }
}
