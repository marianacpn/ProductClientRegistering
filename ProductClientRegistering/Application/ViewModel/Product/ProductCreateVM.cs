using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Application.ViewModel.Product
{
    public class ProductCreateVM
    {
        [DisplayName("Produto")]
        public string Nome { get; set; }

        [DisplayName("Valor")]
        public string Valor { get; set; }

        [DisplayName("Selecione o cliente")]
        public int ClientId { get; set; }
       
        public IEnumerable<KeyValuePairVM> Clients { get; set; }
    }

    public class ProductCreateVMValidator : AbstractValidator<ProductCreateVM>
    {
        public ProductCreateVMValidator()
        {
            RuleFor(e => e.Valor)
                .NotNull().WithMessage("Valor é requerido")
                .Must(BeValidDecimal).WithMessage("Valor deve ser numérico");
        }

        private bool BeValidDecimal(string arg)
        {
            if (arg.Contains("."))
                return false;

            if (!double.TryParse(arg, NumberStyles.Any, new CultureInfo("pt-BR"), out double result))
                return false;

            return true;
        }
    }
}
