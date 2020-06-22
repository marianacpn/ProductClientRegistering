using Application.ViewModel.Product;
using System.Collections.Generic;

namespace Application.App.Interface
{
    public interface IAppProduct
    {
        IEnumerable<ProductListVM> GetAvailableProducts();
        void CreateProduct(ProductCreateVM productVM);
        void DeleteProduct(int productId);
    }
}
