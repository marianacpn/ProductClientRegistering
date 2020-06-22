using Application.App.Interface;
using Domain.Entity;
using Domain.Repository.Interface.Entity;
using Shared.Enums;

namespace Application.App.Validation
{
    public class ClientValidation : IClientValidation
    {
        private readonly IClientRepository _clientRepository;
        public ClientValidation(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public void ValidateClientStatus(int clientId)
        {
            Client client = _clientRepository.GetClientToValidateById(clientId);

            if (client.Products.Count >= 2000)
            {
                if (client.Products.Count >= 2000)
                    client.ChangeClientStatus((int)ClientStatusEnum.gold);
                if (client.Products.Count >= 5000)
                    client.ChangeClientStatus((int)ClientStatusEnum.platinum);
                if (client.Products.Count >= 10000)
                    client.ChangeClientStatus((int)ClientStatusEnum.diamond);

                _clientRepository.SaveChanges();
            }
        }
    }
}
