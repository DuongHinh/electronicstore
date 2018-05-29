using ElectronicStore.Data.Entities;
using ElectronicStore.Service;
using ElectronicStore.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElectronicStore.Web.Api
{
    [RoutePrefix("api/about")]
    [Authorize]
    public class AboutController : BaseApiController
    {
        private ILogErrorService logErrorService;
        private IAboutService aboutService;
        public AboutController(ILogErrorService logErrorService, IAboutService aboutService) : base(logErrorService)
        {
            this.logErrorService = logErrorService;
            this.aboutService = aboutService;
        }

        [Route("getAboutInfo")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var responseData = this.aboutService.GetAboutInfo();

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, About about)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbAbout = this.aboutService.GetById(about.Id);

                    dbAbout.Title = about.Title;
                    dbAbout.Description = about.Description;

                    this.aboutService.Update(dbAbout);
                    this.aboutService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbAbout);
                }

                return response;
            });
        }
    }
}
