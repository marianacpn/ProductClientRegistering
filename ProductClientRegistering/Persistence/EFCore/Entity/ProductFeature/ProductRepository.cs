using Domain.Entity;
using Domain.Repository.Interface.Entity;
using Microsoft.EntityFrameworkCore;
using Persistence.EFCore.Core;
using System.Collections.Generic;

namespace Persistence.EFCore.Entity.ProductFeature
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ProductClientContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetAvailableProducts()
        {
            return ExecuteQueryToList(where: e => e.Disponivel == true,
                                      include: e => e.Include(a => a.Client));
        }

        public Product GetProductByIdWithTracking(int productId)
        {
            return ExecuteQuery(where: e => e.Id == productId,
                                tracking: true);
        }
    }
}
