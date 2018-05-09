using BotDetect.Web.Mvc;
using ElectronicStore.Data.Entities;
using ElectronicStore.Service;
using ElectronicStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicStore.Web.Controllers
{
    public class ContactController : Controller
    {
        IContactService contactService;
        IFeedbackService feedbackService;
        public ContactController(IContactService contactService, IFeedbackService feedbackService)
        {
            this.contactService = contactService;
            this.feedbackService = feedbackService;
        }
        // GET: Contact
        public ActionResult Index()
        {
            FeedbackViewModel viewModel = new FeedbackViewModel();
            viewModel.ContactInfor = this.GetInforContact();
            return View(viewModel);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "feedbackCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult SendFeedback(FeedbackViewModel feedback)
        {
            if (ModelState.IsValid)
            {
                Feedback newFeedback = new Feedback()
                {
                    Id = feedback.Id,
                    Name = feedback.Name,
                    Email = feedback.Email,
                    PhoneNumber = feedback.PhoneNumber,
                    Message = feedback.Message,
                    CreatedDate = DateTime.Now
                };
                
                this.feedbackService.Create(newFeedback);
                this.feedbackService.Save();

                ViewData["SuccessMessage"] = "Gửi phản hồi thành công";


                //send mail
                //-----------------
                //


                feedback.Name = string.Empty;
                feedback.Message = string.Empty;
                feedback.Email = string.Empty;
                feedback.PhoneNumber = string.Empty;
            }

            feedback.ContactInfor = this.GetInforContact();
            return View("Index", feedback);
        }

        private ContactViewModel GetInforContact()
        {
            var model = this.contactService.GetInforContact();

            if (model == null)
            {
                throw new Exception($"Contact Information Not Found");
            }

            ContactViewModel viewModel = new ContactViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Fax = model.Fax,
                Email = model.Email,
                Address = model.Address,
                Other = model.Other,
                Status = model.Status               
            };
           
            return viewModel;
        }
    }
}