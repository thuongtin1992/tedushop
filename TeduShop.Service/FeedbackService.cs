using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IFeedbackService
    {
        Feedback Create(Feedback feedback);

        void Update(Feedback feedback);

        Feedback Delete(int id);

        Feedback GetById(int id);

        IEnumerable<Feedback> GetAll();

        IEnumerable<Feedback> GetAll(string keyword);

        void Save();
    }

    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository _feedbackRepository;
        private IUnitOfWork _unitOfWork;

        public FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            _feedbackRepository = feedbackRepository;
            _unitOfWork = unitOfWork;
        }

        public Feedback Create(Feedback feedback)
        {
            return _feedbackRepository.Add(feedback);
        }

        public Feedback Delete(int id)
        {
            return _feedbackRepository.Delete(id);
        }

        public IEnumerable<Feedback> GetAll()
        {
            return _feedbackRepository.GetAll();
        }

        public IEnumerable<Feedback> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _feedbackRepository.GetMulti(m => m.FirstName.Contains(keyword) || m.LastName.Contains(keyword) || m.FullName.Contains(keyword) || m.Message.Contains(keyword));
            else
                return _feedbackRepository.GetAll();
        }

        public Feedback GetById(int id)
        {
            return _feedbackRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Feedback feedback)
        {
            _feedbackRepository.Update(feedback);
        }
    }
}
