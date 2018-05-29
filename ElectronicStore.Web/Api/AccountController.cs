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
    public class AccountController : BaseApiController
    {
        public AccountController(ILogErrorService logErrorService) : base(logErrorService)
        {
        }
    }
}
