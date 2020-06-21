using Domain.Entity;
using Domain.Repository.Interface.Entity;
using Persistence.EFCore.Core;

namespace Persistence.EFCore.Entity.ProductFeature
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ProductClientContext context) : base(context)
        {
        }
    }
}
