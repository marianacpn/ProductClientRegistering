using System.ComponentModel;
namespace Application.ViewModel.Client
{
    public class ClientCreateVM
    {
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Sobrenome")]
        public string Sobrenome { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }
    }
}
