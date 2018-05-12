using ElectronicStore.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ElectronicStore.Service;

namespace ElectronicStore.Web.Api
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : BaseApiController
    {
        ILogErrorService logErrorService;
        public HomeController(ILogErrorService logErrorService) : base(logErrorService)
        {
            this.logErrorService = logErrorService;
        }

        [HttpGet]
        [Route("redirectRequest")]
        public string RedirectRequest()
        {
            return "Welcome to Electronic store";
        }
    }
}