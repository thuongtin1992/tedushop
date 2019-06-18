using AutoMapper;
using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeduShop.Common;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class ContactController : Controller
    {
        IContactDetailService _contactDetailService;
        IFeedbackService _feedbackService;

        public ContactController(IContactDetailService contactDetailService, IFeedbackService feedbackService)
        {
            this._contactDetailService = contactDetailService;
            this._feedbackService = feedbackService;
        }
        
        public ActionResult Index()
        {
            FeedbackViewModel viewModel = new FeedbackViewModel();
            viewModel.ContactDetail = GetDetail();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaValidation("CaptchaCode", "contactCaptcha", "Mã xác nhận không đúng")]
        public ActionResult SendFeedback(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                Feedback model = new Feedback();
                model.UpdateFeedback(feedbackViewModel);
                model.CreatedDate = DateTime.Now;
                model.Status = true;
                _feedbackService.Create(model);
                _feedbackService.Save();

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/contact_template.html"));
                content = content.Replace("{{Name}}", model.FullName);
                content = content.Replace("{{Email}}", model.Email);
                content = content.Replace("{{Message}}", model.Message);
                content = content.Replace("{{CreatedDate}}", model.CreatedDate.ToString());
                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ website", content);

                ViewData["SuccessMsg"] = "Cảm ơn bạn đã gửi phản hồi cho chúng tôi!";
                feedbackViewModel.FullName = "";
                feedbackViewModel.Email = "";
                feedbackViewModel.Message = "";
            }
            feedbackViewModel.ContactDetail = GetDetail();
            return View("Index", feedbackViewModel);
        }

        private ContactDetailViewModel GetDetail()
        {
            var model = _contactDetailService.GetDefaultContact();
            var contactViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return contactViewModel;
        }
    }
}