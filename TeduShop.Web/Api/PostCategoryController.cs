using System.Collections.Generic;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Model.Models;
using AutoMapper;
using TeduShop.Web.Models;
using TeduShop.Web.Infrastructure.Extensions;

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
                var listCategory = _postCategoryService.GetAll();
                var listPostCategoryVM = Mapper.Map<List<PostCategoryViewModel>>(listCategory);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPostCategoryVM);
                return response;
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
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
                    PostCategory newPostCategory = new PostCategory();
                    newPostCategory.UpdatePostCategory(postCategoryVM);

                    var category = _postCategoryService.Add(newPostCategory);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, category);
                }
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
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
                    var postCategoryDb = _postCategoryService.GetById(postCategoryVM.ID);
                    postCategoryDb.UpdatePostCategory(postCategoryVM);

                    _postCategoryService.Update(postCategoryDb);
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