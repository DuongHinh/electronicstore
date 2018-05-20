using ElectronicStore.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectronicStore.Service;
using ElectronicStore.Web.App_Start;
using ElectronicStore.Fulcrum.Exceptions;
using ElectronicStore.Data.Entities;
using System.Threading.Tasks;
using ElectronicStore.Web.Models;

namespace ElectronicStore.Web.Api
{
    [RoutePrefix("api/user")]
    [Authorize]
    public class UserController : BaseApiController
    {
        private ILogErrorService logErrorService;
        private IGroupService groupService;
        private IRoleService roleService;
        private ApplicationUserManager userManager;
        public UserController(ILogErrorService logErrorService, IGroupService groupService, IRoleService roleService, ApplicationUserManager userManager)
            : base(logErrorService)
        {
            this.logErrorService = logErrorService;
            this.groupService = groupService;
            this.roleService = roleService;
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("getall")]
        // [Authorize(Roles = "ViewUser")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                var models = this.userManager.Users;

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    models = this.userManager.Users.Where(x => x.UserName.Contains(keyword));
                }

                var viewModels = models.Select(u => new ApplicationUserViewModel()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    BirthDay = u.BirthDay,
                    Active = u.Active,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    UserName = u.UserName,
                    Password = u.PasswordHash
                });

                //var results = viewModels.Skip(skip).Take(pageSize);

                //var responseData = new Pagination<ApplicationUserViewModel>()
                //{
                //    Skip = skip,
                //    PageSize = pageSize,
                //    Results = results,
                //    TotalResults = viewModels.Count(),
                //};

                var response = request.CreateResponse(HttpStatusCode.OK, viewModels);

                return response;
            });
        }

        [Route("getbyid")]
        [HttpGet]
        //[Authorize(Roles = "ViewUser")]
        public HttpResponseMessage GetById(HttpRequestMessage request, string id)
        {
            if (string.IsNullOrEmpty(id))
            {

                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " not value");
            }
            var user = this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "Not Found");
            }
            else
            {
                var userModel = user.Result;
                var userViewModel = new ApplicationUserViewModel() {
                    Id = userModel.Id,
                    FirstName = userModel.FirstName,
                    MiddleName = userModel.MiddleName,
                    LastName = userModel.LastName,
                    BirthDay = userModel.BirthDay,
                    Active = userModel.Active,
                    Email = userModel.Email,
                    PhoneNumber = userModel.PhoneNumber,
                    UserName = userModel.UserName,
                    Address = userModel.Address
                };

                var listGroup = this.groupService.GetListGroupByUserId(userViewModel.Id);

                var listGroupViewModel = listGroup.Select(g => new GroupViewModel {
                    Id = g.Id,
                    Name = g.Name
                });

                foreach (var item in listGroupViewModel)
                {
                    item.Roles = this.roleService.GetListRoleByGroupId(item.Id).Select(r => new RoleViewModel() {
                        Id = r.Id,
                        Name = r.Name,
                        Description = r.Description
                    });
                }

                userViewModel.Groups = listGroupViewModel;
                return request.CreateResponse(HttpStatusCode.OK, userViewModel);
            }

        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "AddUser")]
        public async Task<HttpResponseMessage> Create(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppUser = new ApplicationUser() {
                    Id = applicationUserViewModel.Id,
                    FirstName = applicationUserViewModel.FirstName,
                    MiddleName = applicationUserViewModel.MiddleName,
                    LastName = applicationUserViewModel.LastName,
                    BirthDay = applicationUserViewModel.BirthDay,
                    Email = applicationUserViewModel.Email,
                    PhoneNumber = applicationUserViewModel.PhoneNumber,
                    UserName = applicationUserViewModel.UserName,
                    Address = applicationUserViewModel.Address,
                    Active = true
                };

                try
                {
                    newAppUser.Id = Guid.NewGuid().ToString();
                    var result = await this.userManager.CreateAsync(newAppUser, applicationUserViewModel.Password);
                    if (result.Succeeded)
                    {
                        var listUserGroup = new List<UserGroup>();
                        foreach (var group in applicationUserViewModel.Groups)
                        {
                            listUserGroup.Add(new UserGroup()
                            {
                                GroupId = group.Id,
                                UserId = newAppUser.Id
                            });
                            //add role to user
                            var listRole = this.roleService.GetListRoleByGroupId(group.Id);
                            foreach (var role in listRole)
                            {
                                await this.userManager.RemoveFromRoleAsync(newAppUser.Id, role.Name);
                                await this.userManager.AddToRoleAsync(newAppUser.Id, role.Name);
                            }
                        }
                        this.groupService.AddUserToGroups(listUserGroup, newAppUser.Id);
                        this.groupService.Save();


                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);

                    }
                    else
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                }
                catch (DuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "UpdateUser")]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var oldUser = await this.userManager.FindByIdAsync(applicationUserViewModel.Id);
                try
                {
                    oldUser.FirstName = applicationUserViewModel.FirstName;
                    oldUser.LastName = applicationUserViewModel.LastName;
                    oldUser.MiddleName = applicationUserViewModel.MiddleName;
                    oldUser.BirthDay = applicationUserViewModel.BirthDay;
                    oldUser.PhoneNumber = applicationUserViewModel.PhoneNumber;
                    oldUser.UserName = applicationUserViewModel.UserName;
                    oldUser.Email = applicationUserViewModel.Email;
                    oldUser.Address = applicationUserViewModel.Address;
                    var result = await this.userManager.UpdateAsync(oldUser);
                    if (result.Succeeded)
                    {
                        var listAppUserGroup = new List<UserGroup>();
                        foreach (var group in applicationUserViewModel.Groups)
                        {
                            listAppUserGroup.Add(new UserGroup()
                            {
                                GroupId = group.Id,
                                UserId = applicationUserViewModel.Id
                            });
                            //add role to user
                            var listRole = this.roleService.GetListRoleByGroupId(group.Id);
                            foreach (var role in listRole)
                            {
                                await this.userManager.RemoveFromRoleAsync(oldUser.Id, role.Name);
                                await this.userManager.AddToRoleAsync(oldUser.Id, role.Name);
                            }
                        }
                        this.groupService.AddUserToGroups(listAppUserGroup, applicationUserViewModel.Id);
                        this.groupService.Save();
                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);

                    }
                    else
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                }
                catch (DuplicatedException ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        [Route("delete")]
        // [Authorize(Roles = "DeleteUser")]
        public async Task<HttpResponseMessage> Delete(HttpRequestMessage request, string id)
        {
            var appUser = await this.userManager.FindByIdAsync(id);
            var result = await this.userManager.DeleteAsync(appUser);
            if (result.Succeeded)
                return request.CreateResponse(HttpStatusCode.OK, id);
            else
                return request.CreateErrorResponse(HttpStatusCode.OK, string.Join(",", result.Errors));
        }
    }
}
