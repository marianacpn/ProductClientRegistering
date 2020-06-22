using Domain.Entity;
using Domain.Repository.Interface.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.EFCore.Core;
using System.Collections.Generic;

namespace Persistence.EFCore.Entity.ClientFeature
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(ProductClientContext context) : base(context)
        {
        }

        public IEnumerable<Client> GetAllActiveClients()
        {
            return ExecuteQueryToList(where: e => e.Ativo == true);
        }

        public Client GetByClientIdWithTracking(int clientId)
        {
            return ExecuteQuery(where: e => e.Id == clientId,
                               tracking: true);
        }

        public Client GetClientAndProductsById(int clientId)
        {
            return ExecuteQuery(where: e => e.Id == clientId);
        }

        public Client GetClientById(int clientId)
        {
            return ExecuteQuery(where: e => e.Id == clientId);
        }

        public Client GetClientToValidateById(int clientId)
        {
            return ExecuteQuery(where: e => e.Id == clientId, 
                                include: e => e.Include(c => c.Products),
                                tracking: true);
        }

        public Client GetUnactiveClientByEmailWithTracking(string email)
        {
            return ExecuteQuery(where: e => e.Ativo == false && e.Email == email,
                                tracking: true);
        }
    }
}
