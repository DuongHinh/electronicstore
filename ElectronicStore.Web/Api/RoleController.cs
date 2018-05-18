using ElectronicStore.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectronicStore.Service;
using ElectronicStore.Web.Models;
using ElectronicStore.Data.Entities;
using ElectronicStore.Fulcrum.Exceptions;
using System.Web.Script.Serialization;

namespace ElectronicStore.Web.Api
{
    [RoutePrefix("api/role")]
    [Authorize]
    public class RoleController : BaseApiController
    {
        private ILogErrorService logErrorService;
        private IRoleService roleService;
        public RoleController(ILogErrorService logErrorService, IRoleService roleService) : base(logErrorService)
        {
            this.logErrorService = logErrorService;
            this.roleService = roleService;
        }

        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, string keyword, int skip, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = this.roleService.GetAll();

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    model = model.Where(x => x.Name.Contains(keyword));
                }

                var viewModel = model.Select( r => new RoleViewModel() {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                });

                var results = viewModel.Skip(skip).Take(pageSize);

                var responseData = new Pagination<RoleViewModel>()
                {
                    Skip = skip,
                    PageSize = pageSize,
                    Results = results,
                    TotalResults = model.Count(),
                };

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }
        [Route("getlistall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var models = this.roleService.GetAll();

                var viewModels = models.Select(r => new RoleViewModel() {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                });

                var response = request.CreateResponse(HttpStatusCode.OK, viewModels);

                return response;
            });
        }
        [Route("detail/{id}")]
        [HttpGet]
        public HttpResponseMessage Details(HttpRequestMessage request, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " not has value.");
            }
            Role role = this.roleService.GetById(id);
            if (role == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Role not found");
            }
            return request.CreateResponse(HttpStatusCode.OK, role);
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Create(HttpRequestMessage request, RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var newRole = new Role() {
                    Id = Guid.NewGuid().ToString(),
                    Name = roleViewModel.Name,
                    Description = roleViewModel.Description
                };
                
                try
                {
                    this.roleService.Add(newRole);
                    this.roleService.Save();
                    return request.CreateResponse(HttpStatusCode.OK, roleViewModel);
                }
                catch (DuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = this.roleService.GetById(roleViewModel.Id);
                try
                {
                    role = new Role() {
                        Id = roleViewModel.Id,
                        Name = roleViewModel.Name,
                        Description = roleViewModel.Description
                    };
                    this.roleService.Update(role);
                    this.roleService.Save();
                    return request.CreateResponse(HttpStatusCode.OK, role);
                }
                catch (DuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
        {
            this.roleService.Delete(id);
            this.roleService.Save();
            return request.CreateResponse(HttpStatusCode.OK, id);
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedList)
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
                    var listItem = new JavaScriptSerializer().Deserialize<List<string>>(checkedList);
                    foreach (var item in listItem)
                    {
                        this.roleService.Delete(item);
                    }

                    this.roleService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }
    }
}
