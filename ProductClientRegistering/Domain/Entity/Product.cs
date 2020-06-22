using Domain.Core;
using System;

namespace Domain.Entity
{
    [Serializable]
    public class Product : Base
    {
        public Product(string nome, double valor, bool disponivel, int clientId)
        {
            Nome = nome;
            Valor = valor;
            Disponivel = disponivel;
            ClientId = clientId;
        }
        protected Product()
        {

        }

        public string Nome { get; private set; }

        public double Valor { get; private set; }

        public bool Disponivel { get; private set; }


        public int ClientId { get; private set; }

        public Client Client { get; private set; }


        public void ChangeDisponivel(bool disponivel)
        {
            Disponivel = disponivel;
        }
    }
}
