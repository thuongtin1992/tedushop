using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        #region khởi tạo
        IProductService _productService;
        public ProductController(IErrorService errorService, IProductService productSerivce) : base(errorService)
        {
            this._productService = productSerivce;
        }
        #endregion

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetById(id);
                var responseData = Mapper.Map<Product, ProductViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _productService.GetAll(keyword);
                totalRow = model.Count();

                var query = model.OrderByDescending(m => m.CreatedDate)
                            .Skip(page * pageSize)
                            .Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

                var paginationSet = new PaginationSet<ProductViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                int stt = page * pageSize;

                foreach (var item in responseData)
                {
                    item.Index += stt + 1;
                    stt++;
                }

                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getallparents")] // load dữ liệu cho dropdownlist
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductViewModel productViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var newProduct = new Product();

                    newProduct.UpdateProduct(productViewModel);
                    newProduct.CreatedDate = DateTime.Now;

                    _productService.Add(newProduct);
                    _productService.Save();

                    var responseData = Mapper.Map<Product, ProductViewModel>(newProduct);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var dbProduct = _productService.GetById(productViewModel.ID);

                    dbProduct.UpdateProduct(productViewModel);
                    dbProduct.UpdatedDate = DateTime.Now;

                    _productService.Update(dbProduct);
                    _productService.Save();

                    var responseData = Mapper.Map<Product, ProductViewModel>(dbProduct);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var oldProduct = _productService.Delete(id);
                    _productService.Save();

                    var responseData = Mapper.Map<Product, ProductViewModel>(oldProduct);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        [Route("deletemulti")] // xóa nhiều bản ghi
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedProducts)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(checkedProducts);

                    foreach (var item in listProduct)
                    {
                        _productService.Delete(item);
                    }

                    _productService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listProduct.Count());
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }
    }
}
