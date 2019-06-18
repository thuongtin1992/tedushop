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
using System;
using System.Linq;
using System.Web.Script.Serialization;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/page")]
    [Authorize]
    public class PageController : ApiControllerBase
    {
        #region khởi tạo
        IPageService _pageService;
        public PageController(IErrorService errorService, IPageService pageSerivce) : base(errorService)
        {
            this._pageService = pageSerivce;
        }
        #endregion

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _pageService.GetById(id);
                var responseData = Mapper.Map<Page, PageViewModel>(model);
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
                var model = _pageService.GetAll(keyword);
                totalRow = model.Count();

                var query = model.OrderByDescending(m => m.CreatedDate)
                            .Skip(page * pageSize)
                            .Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Page>, IEnumerable<PageViewModel>>(query);

                var paginationSet = new PaginationSet<PageViewModel>()
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

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, PageViewModel pageViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var newPage = new Page();
                    newPage.UpdatePage(pageViewModel);
                    newPage.CreatedDate = DateTime.Now;
                    newPage.CreatedBy = User.Identity.Name;

                    _pageService.Add(newPage);
                    _pageService.Save();

                    var responseData = Mapper.Map<Page, PageViewModel>(newPage);
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
        public HttpResponseMessage Update(HttpRequestMessage request, PageViewModel pageViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var dbPage = _pageService.GetById(pageViewModel.ID);

                    dbPage.UpdatePage(pageViewModel);
                    dbPage.UpdatedDate = DateTime.Now;
                    dbPage.UpdatedBy = User.Identity.Name;

                    _pageService.Update(dbPage);
                    _pageService.Save();

                    var responseData = Mapper.Map<Page, PageViewModel>(dbPage);
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
                    var oldPage = _pageService.Delete(id);
                    _pageService.Save();

                    var responseData = Mapper.Map<Page, PageViewModel>(oldPage);
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
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedPages)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var listPage = new JavaScriptSerializer().Deserialize<List<int>>(checkedPages);

                    foreach (var item in listPage)
                    {
                        _pageService.Delete(item);
                    }

                    _pageService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listPage.Count());
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
