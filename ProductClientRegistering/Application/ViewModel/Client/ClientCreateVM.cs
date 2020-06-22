using FluentValidation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

    public class ClientCreateVMValidator : AbstractValidator<ClientCreateVM>
    {
        public ClientCreateVMValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("E-mail é requerido")
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex).WithMessage("E-mail em formato incorreto");
        }
    }
}
