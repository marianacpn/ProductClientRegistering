using Domain.Entity;
using Domain.Repository.Interface.Entity;
using Persistence.EFCore.Core;

namespace Persistence.EFCore.Entity.ClientFeature
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(ProductClientContext context) : base(context)
        {
        }
    }
}
