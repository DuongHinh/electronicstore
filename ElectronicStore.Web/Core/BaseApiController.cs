using ElectronicStore.Data.Entities;
using ElectronicStore.Service;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElectronicStore.Web.Core
{
    public class BaseApiController : ApiController
    {
        private ILogErrorService logErrorService;

        public BaseApiController(ILogErrorService logErrorService)
        {
            this.logErrorService = logErrorService;
        }

        public HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }

                this.WriteLogError(ex);

                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                this.WriteLogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                this.WriteLogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            return response;
        }

        private void WriteLogError(Exception ex)
        {
            LogError error = new LogError();
            error.Date = DateTime.Now;
            error.Message = ex.Message;
            error.StackTrace = ex.StackTrace;
            this.logErrorService.Create(error);
            this.logErrorService.Save();
        }
    }
}