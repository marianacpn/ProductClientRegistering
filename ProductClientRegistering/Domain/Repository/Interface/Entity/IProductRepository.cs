using Domain.Entity;
using Domain.Repository.Interface.Core;
using System.Collections.Generic;

namespace Domain.Repository.Interface.Entity
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IEnumerable<Product> GetAvailableProducts();
        Product GetProductByIdWithTracking(int productId);
    }
}
