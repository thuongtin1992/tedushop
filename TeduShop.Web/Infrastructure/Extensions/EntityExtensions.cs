using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryViewModel)
        {
            postCategory.ID = postCategoryViewModel.ID;
            postCategory.Name = postCategoryViewModel.Name;
            postCategory.Alias = postCategoryViewModel.Alias;
            postCategory.Description = postCategoryViewModel.Description;
            postCategory.DisplayOrder = postCategoryViewModel.DisplayOrder;
            postCategory.Image = postCategoryViewModel.Image;
            postCategory.ParentID = postCategoryViewModel.ParentID;
            postCategory.HomeFlag = postCategoryViewModel.HomeFlag;
            postCategory.Status = postCategoryViewModel.Status;
            postCategory.MetaKeyword = postCategoryViewModel.MetaKeyword;
            postCategory.MetaDescription = postCategoryViewModel.MetaDescription;
            postCategory.CreatedBy = postCategoryViewModel.CreatedBy;
            postCategory.CreatedDate = postCategoryViewModel.CreatedDate;
            postCategory.UpdatedBy = postCategoryViewModel.UpdatedBy;
            postCategory.UpdatedDate = postCategoryViewModel.UpdatedDate;
        }

        public static void UpdatePost(this Post post, PostViewModel postViewModel)
        {
            post.ID = postViewModel.ID;
            post.Name = postViewModel.Name;
            post.Alias = postViewModel.Alias;
            post.Description = postViewModel.Description;
            post.DisplayOrder = postViewModel.DisplayOrder;
            post.Image = postViewModel.Image;
            post.Content = postViewModel.Content;
            post.CategoryID = postViewModel.CategoryID;
            post.HomeFlag = postViewModel.HomeFlag;
            post.HotFlag = postViewModel.HotFlag;
            post.ViewCount = postViewModel.ViewCount;
            post.Status = postViewModel.Status;
            post.MetaKeyword = postViewModel.MetaKeyword;
            post.MetaDescription = postViewModel.MetaDescription;
            post.CreatedBy = postViewModel.CreatedBy;
            post.CreatedDate = postViewModel.CreatedDate;
            post.UpdatedBy = postViewModel.UpdatedBy;
            post.UpdatedDate = postViewModel.UpdatedDate;
        }

        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryViewModel)
        {
            productCategory.ID = productCategoryViewModel.ID;
            productCategory.Name = productCategoryViewModel.Name;
            productCategory.Description = productCategoryViewModel.Description;
            productCategory.Alias = productCategoryViewModel.Alias;
            productCategory.ParentID = productCategoryViewModel.ParentID;
            productCategory.DisplayOrder = productCategoryViewModel.DisplayOrder;
            productCategory.Image = productCategoryViewModel.Image;
            productCategory.HomeFlag = productCategoryViewModel.HomeFlag;
            productCategory.CreatedDate = productCategoryViewModel.CreatedDate;
            productCategory.CreatedBy = productCategoryViewModel.CreatedBy;
            productCategory.UpdatedDate = productCategoryViewModel.UpdatedDate;
            productCategory.UpdatedBy = productCategoryViewModel.UpdatedBy;
            productCategory.MetaKeyword = productCategoryViewModel.MetaKeyword;
            productCategory.MetaDescription = productCategoryViewModel.MetaDescription;
            productCategory.Status = productCategoryViewModel.Status;
        }

        public static void UpdateProduct(this Product product, ProductViewModel productViewModel)
        {
            product.ID = productViewModel.ID;
            product.Name = productViewModel.Name;
            product.Description = productViewModel.Description;
            product.Alias = productViewModel.Alias;
            product.CategoryID = productViewModel.CategoryID;
            product.Content = productViewModel.Content;
            product.Image = productViewModel.Image;
            product.MoreImages = productViewModel.MoreImages;
            product.Price = productViewModel.Price;
            product.PromotionPrice = productViewModel.PromotionPrice;
            product.Warranty = productViewModel.Warranty;
            product.HomeFlag = productViewModel.HomeFlag;
            product.ViewCount = productViewModel.ViewCount;
            product.HotFlag = productViewModel.HotFlag;
            product.CreatedDate = productViewModel.CreatedDate;
            product.CreatedBy = productViewModel.CreatedBy;
            product.UpdatedDate = productViewModel.UpdatedDate;
            product.UpdatedBy = productViewModel.UpdatedBy;
            product.MetaKeyword = productViewModel.MetaKeyword;
            product.MetaDescription = productViewModel.MetaDescription;
            product.Status = productViewModel.Status;
            product.Tags = productViewModel.Tags;
        }
    }
}