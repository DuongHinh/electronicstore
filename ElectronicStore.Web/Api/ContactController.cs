using ElectronicStore.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectronicStore.Service;
using ElectronicStore.Data.Entities;

namespace ElectronicStore.Web.Api
{
    [RoutePrefix("api/contact")]
    [Authorize]
    public class ContactController : BaseApiController
    {
        private ILogErrorService logErrorService;
        private IContactService contactService;
        public ContactController(ILogErrorService logErrorService, IContactService contactService) : base(logErrorService)
        {
            this.logErrorService = logErrorService;
            this.contactService = contactService;
        }

        [Route("getContactInfo")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var responseData = this.contactService.GetInforContact();
                
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("update")]
        [HttpPost]
        public HttpResponseMessage Update(HttpRequestMessage request, Contact contact)
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
                    var dbContact = this.contactService.GetById(contact.Id);
                    dbContact.Name = contact.Name;
                    dbContact.Email = contact.Email;
                    dbContact.Address = contact.Address;
                    dbContact.Fax = contact.Fax;
                    dbContact.PhoneNumber = contact.PhoneNumber;
                    dbContact.Status = contact.Status;
                    dbContact.Other = contact.Other;

                    this.contactService.Update(dbContact);
                    this.contactService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbContact);
                }

                return response;
            });
        }
    }
}
