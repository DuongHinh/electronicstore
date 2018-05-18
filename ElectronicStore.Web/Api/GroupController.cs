using ElectronicStore.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectronicStore.Service;
using ElectronicStore.Web.App_Start;
using System.Web.Script.Serialization;
using ElectronicStore.Fulcrum.Exceptions;
using System.Threading.Tasks;
using ElectronicStore.Data.Entities;
using ElectronicStore.Web.Models;

namespace ElectronicStore.Web.Api
{
    [RoutePrefix("api/group")]
    [Authorize]
    public class GroupController : BaseApiController
    {
        private ILogErrorService logErrorService;
        private IGroupService groupService;
        private IRoleService roleService;
        private ApplicationUserManager userManager;
        public GroupController(ILogErrorService logErrorService, IGroupService groupService, IRoleService roleService, ApplicationUserManager userManager) 
            : base(logErrorService)
        {
            this.logErrorService = logErrorService;
            this.groupService = groupService;
            this.roleService = roleService;
        }

        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, string keyword, int skip, int pageSize)
        {

            return CreateHttpResponse(request, () =>
            {
                var model = this.groupService.GetAll();
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    model = model.Where(x => x.Name.Contains(keyword));
                }

                var viewModel = model.Select(r => new GroupViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                });

                var results = viewModel.Skip(skip).Take(pageSize);

                var responseData = new Pagination<GroupViewModel>()
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
                var model = this.groupService.GetAll();
                var viewModel = model.Select(r => new GroupViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                });

                var response = request.CreateResponse(HttpStatusCode.OK, viewModel);

                return response;
            });
        }
        [Route("detail/{id:int}")]
        [HttpGet]
        public HttpResponseMessage Details(HttpRequestMessage request, int id)
        {
            if (id == 0)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " is required.");
            }

            Group group = this.groupService.GetById(id);
            var groupViewModel = new GroupViewModel()
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description
            };

            if (group == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Group not Found");
            }
            var listRole = this.roleService.GetListRoleByGroupId(groupViewModel.Id);
            var listRoleViewModel = listRole.Select( g => new RoleViewModel() {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description
            });
            groupViewModel.Roles = listRoleViewModel;

            return request.CreateResponse(HttpStatusCode.OK, groupViewModel);
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Create(HttpRequestMessage request, GroupViewModel groupViewModel)
        {
            if (ModelState.IsValid)
            {
                var newGroup = new Group();
                newGroup.Name = groupViewModel.Name;
                try
                {
                    var appGroup = this.groupService.Add(newGroup);
                    this.groupService.Save();

                    //save group
                    var listRoleGroup = new List<RoleGroup>();
                    foreach (var role in groupViewModel.Roles)
                    {
                        listRoleGroup.Add(new RoleGroup()
                        {
                            GroupId = appGroup.Id,
                            RoleId = role.Id
                        });
                    }
                    this.roleService.AddRolesToGroup(listRoleGroup, appGroup.Id);
                    this.roleService.Save();


                    return request.CreateResponse(HttpStatusCode.OK, groupViewModel);


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
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, GroupViewModel groupViewModel)
        {
            if (ModelState.IsValid)
            {
                var oldGroup = this.groupService.GetById(groupViewModel.Id);
                try
                {
                    oldGroup.Name = groupViewModel.Name;
                    oldGroup.Description = groupViewModel.Description;
                    this.groupService.Update(oldGroup);
                    //this.groupService.Save();

                    //save role group
                    var listRoleGroup = new List<RoleGroup>();
                    foreach (var role in groupViewModel.Roles)
                    {
                        listRoleGroup.Add(new RoleGroup()
                        {
                            GroupId = oldGroup.Id,
                            RoleId = role.Id
                        });
                    }
                    this.roleService.AddRolesToGroup(listRoleGroup, oldGroup.Id);
                    this.roleService.Save();

                    //add role to user
                    var listRole = this.roleService.GetListRoleByGroupId(oldGroup.Id);
                    var listUserInGroup = this.groupService.GetListUserByGroupId(oldGroup.Id);
                    foreach (var user in listUserInGroup)
                    {
                        var listRoleName = listRole.Select(x => x.Name).ToArray();
                        foreach (var roleName in listRoleName)
                        {
                            await this.userManager.RemoveFromRoleAsync(user.Id, roleName);
                            await this.userManager.AddToRoleAsync(user.Id, roleName);
                        }
                    }
                    return request.CreateResponse(HttpStatusCode.OK, groupViewModel);
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
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var appGroup = this.groupService.Delete(id);
            this.groupService.Save();
            return request.CreateResponse(HttpStatusCode.OK, appGroup);
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
                    var listItem = new JavaScriptSerializer().Deserialize<List<int>>(checkedList);
                    foreach (var item in listItem)
                    {
                        this.groupService.Delete(item);
                    }

                    this.groupService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }
    }
}
