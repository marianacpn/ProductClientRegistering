﻿using System.ComponentModel;

namespace Application.ViewModel.Client
{
    public class ClientDetailsVM
    {
        public int ClientId { get; set; }

        [DisplayName("Cliente")]
        public string NomeSobrenome { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Cadastrado em")]
        public string DataCadastro { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }
    }
}
