using Domain.Core;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Domain.Entity
{
    [Serializable]
    public class Client : Base
    {
        public Client(string nome, string sobrenome, string email, DateTime dataCadastro, bool ativo)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            DataCadastro = dataCadastro;
            Ativo = ativo;
            
            _products = new List<Product>();
        }

        protected Client()
        {
            _products = new List<Product>();
        }

        public string Nome { get; private set; }

        public string Sobrenome { get; private set; }

        public string Email { get; private set; }

        public DateTime DataCadastro { get; private set; }

        public bool Ativo { get; private set; }

        public int Status { get; private set; }


        private readonly List<Product> _products;

        public ICollection<Product> Products => _products;
    }
}
