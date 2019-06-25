using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Common;

namespace TeduShop.Service
{
    public interface ICommonService
    {
        Footer GetFooter();
        IEnumerable<Slide> GetSlides();
        SystemConfig GetSystemConfig(string code);
    }

    public class CommonService : ICommonService
    {
        IFooterRepository _footerRepository;
        ISlideRepository _slideRepository;
        ISystemConfigRepository _systemConfigRepository;
        IUnitOfWork _unitOfWork;

        public CommonService(IFooterRepository footerRepository, ISlideRepository slideRepository, ISystemConfigRepository systemConfigRepository, IUnitOfWork unitOfWork)
        {
            _footerRepository = footerRepository;
            _slideRepository = slideRepository;
            _systemConfigRepository = systemConfigRepository;
            _unitOfWork = unitOfWork;
        }

        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(m => m.ID == CommonConstants.DefaultFooterId && m.Status == true);
        }

        public IEnumerable<Slide> GetSlides()
        {
            return _slideRepository.GetMulti(m => m.Status == true).OrderBy(m => m.DisplayOrder);
        }

        public SystemConfig GetSystemConfig(string code)
        {
            return _systemConfigRepository.GetSingleByCondition(x => x.Code == code && x.Status);
        }
    }
}
