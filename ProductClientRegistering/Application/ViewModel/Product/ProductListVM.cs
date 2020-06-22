using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel.Product
{
    public class ProductListVM
    {
        public ProductListVM()
        {
            Products = new List<ProductListVM>();
        }
        public int ProductId { get; set; }

        [DisplayName("Produto")]
        public string Nome { get; set; }

        [DisplayName("Valor")]
        public string Valor { get; set; }

        [DisplayName("Cliente")]
        public string ClientName { get; set; }

        public IEnumerable<ProductListVM> Products { get; set; }
    }
}
