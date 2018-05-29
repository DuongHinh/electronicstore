using ElectronicStore.Service;
using ElectronicStore.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicStore.Web.Controllers
{
    public class AboutController : Controller
    {
        private IAboutService aboutService;
        public AboutController(IAboutService aboutService)
        {
            this.aboutService = aboutService;
        }
        // GET: About
        public ActionResult Index()
        {
            var model = this.aboutService.GetAboutInfo();
            AboutViewModel viewModel = new AboutViewModel();

            if(model != null)
            {
                viewModel = new AboutViewModel()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    Status = model.Status
                };
            }

            return View(viewModel);
        }
    }
}