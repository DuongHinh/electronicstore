using BotDetect.Web.Mvc;
using ElectronicStore.Data.Entities;
using ElectronicStore.Web.App_Start;
using ElectronicStore.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ElectronicStore.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager signInManager;
        private ApplicationUserManager userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                this.signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                this.userManager = value;
            }
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "signupCaptcha", "Mã xác nhận không đúng. Thử lại!")]
        public async Task<ActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userByEmail = await this.userManager.FindByEmailAsync(model.Email);
                if (userByEmail != null)
                {
                    ModelState.AddModelError("email", "Email đã tồn tại");
                    return View(model);
                }
                var userByUserName = await this.userManager.FindByNameAsync(model.UserName);
                if (userByUserName != null)
                {
                    ModelState.AddModelError("username", "Tài khoản đã tồn tại");
                    return View(model);
                }
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    Active = true
                };

                await this.userManager.CreateAsync(user, model.Password);

                var userCreate = await this.userManager.FindByEmailAsync(model.Email);
                if (userCreate != null)
                    await this.userManager.AddToRolesAsync(userCreate.Id, new string[] { "User" });

                //string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/newuser.html"));
                //content = content.Replace("{{UserName}}", adminUser.FullName);
                //content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink") + "dang-nhap.html");

                //MailHelper.SendMail(adminUser.Email, "Đăng ký thành công", content);

                ViewData["SuccessMessage"] = "Đăng ký thành công";
            }

            return View();
        }


        [HttpGet]
        public ActionResult SignIn(string redirectUrl)
        {
            ViewBag.RedirectUrl = redirectUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInViewModel model, string redirectUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = this.userManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
                    authManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = this.userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();

                    authManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(redirectUrl))
                    {
                        return Redirect(redirectUrl);
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu không đúng.");
                }
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return Redirect("/");
        }
    }
}