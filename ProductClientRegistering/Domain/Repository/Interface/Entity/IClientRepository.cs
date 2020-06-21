using Domain.Entity;
using Domain.Repository.Interface.Core;
using System.Collections.Generic;

namespace Domain.Repository.Interface.Entity
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        IEnumerable<Client> GetAllActiveClients();
        Client GetUnactiveClientByEmail(string email);
        Client GetClientById(int clientId);
        Client GetClientAndProductsById(int clientId);
    }
}
