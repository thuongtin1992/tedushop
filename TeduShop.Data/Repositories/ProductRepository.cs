using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, string sort, out int totalRow);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.ID equals pt.ProductID
                        where pt.TagID == tagId
                        select p;

            switch (sort)
            {
                case "new":
                    query = query.OrderByDescending(m => m.CreatedDate);
                    break;

                case "popular":
                    query = query.OrderByDescending(m => m.ViewCount);
                    break;

                case "discount":
                    query = query.Where(m => m.PromotionPrice.HasValue).OrderByDescending(m => m.PromotionPrice);
                    break;

                case "price":
                    query = query.OrderBy(m => m.Price);
                    break;

                default:
                    query = query.OrderByDescending(m => m.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}