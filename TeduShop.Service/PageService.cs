using System;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IPageService
    {
        Page Add(Page page);

        void Update(Page page);

        Page Delete(int id);

        Page GetById(int id);

        Page GetByAlias(string alias);

        IEnumerable<Page> GetAll();

        IEnumerable<Page> GetAll(string keyword);

        void IncreaseViewCount(string alias);

        void Save();
    }

    public class PageService : IPageService
    {
        private IPageRepository _pageRepository;
        private IUnitOfWork _unitOfWork;

        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
        }

        public Page Add(Page page)
        {
            return _pageRepository.Add(page);
        }

        public Page Delete(int id)
        {
            return _pageRepository.Delete(id);
        }

        public IEnumerable<Page> GetAll()
        {
            return _pageRepository.GetAll();
        }

        public IEnumerable<Page> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _pageRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword) || x.Content.Contains(keyword));
            else
                return _pageRepository.GetAll();
        }

        public Page GetByAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(m => m.Alias == alias && m.Status);
        }

        public Page GetById(int id)
        {
            return _pageRepository.GetSingleById(id);
        }

        public void IncreaseViewCount(string alias)
        {
            var model = GetByAlias(alias);
            if (model.ViewCount.HasValue && model.ViewCount > 0 && model.Status)
                model.ViewCount += 1;
            else
                model.ViewCount = 1;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Page page)
        {
            _pageRepository.Update(page);
        }
    }
}