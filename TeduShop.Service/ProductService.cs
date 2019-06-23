using System;
using System.Collections.Generic;
using System.Linq;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IProductService
    {
        Product Add(Product Product);

        void Update(Product Product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword);

        IEnumerable<Product> GetLatest(int top);

        IEnumerable<Product> GetHotProduct(int top);

        IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<string> GetListProductByName(string name);

        IEnumerable<Product> GetRelatedProducts(int id, int top);

        Product GetById(int id);

        Product GetByIdWeb(int id);

        void IncreaseViewCount(int id);

        IEnumerable<Tag> GetListTagByProductId(int id);

        IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, string sort, out int totalRow);

        Tag GetTag(string tagId);

        void Save();

        bool SellProduct(int productId, int quantity);
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;

        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IProductTagRepository productTagRepository,
            ITagRepository _tagRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._productTagRepository = productTagRepository;
            this._tagRepository = _tagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product Product)
        {
            var product = _productRepository.Add(Product);
            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
            return product;
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product Product)
        {
            _productRepository.Update(Product);
            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == Product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
        }

        public IEnumerable<Product> GetLatest(int top)
        {
            return _productRepository.GetMulti(m => m.Status == true && m.HomeFlag == true).OrderByDescending(m => m.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return _productRepository.GetMulti(m => m.Status == true && m.HomeFlag == true && m.HotFlag == true).OrderByDescending(m => m.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(m => m.Status == true && m.CategoryID == categoryId);

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

        public IEnumerable<string> GetListProductByName(string name)
        {
            return _productRepository
                   .GetMulti(m => m.Status && m.Name.Contains(name) || m.Alias.Contains(name))
                   .Select(m => m.Name);
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(m => m.Status == true && m.Name.Contains(keyword) || m.Alias.Contains(keyword));

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

        public void IncreaseViewCount(int id)
        {
            var model = GetByIdWeb(id);
            if (model.ViewCount.HasValue && model.ViewCount > 0 && model.Status)
                model.ViewCount += 1;
            else
                model.ViewCount = 1;
        }

        public IEnumerable<Product> GetRelatedProducts(int id, int top)
        {
            var product = GetByIdWeb(id);
            return _productRepository
                  .GetMulti(m => m.Status == true && m.ID != id && m.CategoryID == product.CategoryID)
                  .OrderByDescending(m => m.CreatedDate)
                  .Take(top);
        }

        public IEnumerable<Tag> GetListTagByProductId(int id)
        {
            return _productTagRepository.GetMulti(m => m.ProductID == id, new string[] { "Tag" }).Select(m => m.Tag);
        }

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, string sort, out int totalRow)
        {
            var model = _productRepository.GetListProductByTag(tagId, page, pageSize, sort, out totalRow);
            return model;
        }

        public Tag GetTag(string tagId)
        {
            return _tagRepository.GetSingleByCondition(m => m.ID == tagId);
        }

        public Product GetByIdWeb(int id)
        {
            return _productRepository.GetSingleByCondition(m => m.ID == id && m.Status);
        }

        public bool SellProduct(int productId, int quantity)
        {
            var product = GetByIdWeb(productId);

            if (product.Quantity < quantity)
                return false;

            product.Quantity -= quantity;
            return true;
        }
    }
}