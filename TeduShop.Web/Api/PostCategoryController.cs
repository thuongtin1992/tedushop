using System.Collections.Generic;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Model.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;  

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) 
            : base(errorService)
        {
            this._postCategoryService = postCategoryService;   
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid) // trường hợp có lỗi
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else // trường hợp bình thường ko có lỗi
                {
                    var listCategory = _postCategoryService.GetAll();

                    response = request.CreateResponse(HttpStatusCode.OK, listCategory);
                }
                return response;
            });
        }

        public HttpResponseMessage Post(HttpRequestMessage request, PostCategory postCategory)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid) // trường hợp có lỗi
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else // trường hợp bình thường ko có lỗi
                {
                    var category = _postCategoryService.Add(postCategory);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, category);
                }
                return response;
            });
        }

        public HttpResponseMessage Put(HttpRequestMessage request, PostCategory postCategory)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid) // trường hợp có lỗi
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else // trường hợp bình thường ko có lỗi
                {
                    _postCategoryService.Update(postCategory);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid) // trường hợp có lỗi
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else // trường hợp bình thường ko có lỗi
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }
    }
}