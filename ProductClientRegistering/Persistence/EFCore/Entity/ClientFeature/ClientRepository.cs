using Domain.Entity;
using Domain.Repository.Interface.Entity;
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
    }
}
