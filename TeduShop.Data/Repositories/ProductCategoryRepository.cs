using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TeduShop.Data.Repositories
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);
    }

    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbFactory dbFactory) : base(dbFactory){ }

        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return DbContext.ProductCategories.Where(m => m.Alias == alias);
        }
    }
}