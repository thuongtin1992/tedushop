using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        IProductCategoryService _productCategoryService;
        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategorySerivce) : base(errorService)
        {
            this._productCategoryService = productCategorySerivce;
        }

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _productCategoryService.GetAll();
                totalRow = model.Count();

                var query = model.OrderByDescending(m => m.CreatedDate)
                            .Skip(page * pageSize)
                            .Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
    }
}
