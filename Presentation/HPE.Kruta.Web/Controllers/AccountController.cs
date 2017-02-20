using HPE.Kruta.Domain.User;
using HPE.Kruta.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserManager userManager = new UserManager();
            var user = userManager.VerifyUser(model.Username, model.Password);

            if (user != null)
            {
                var ident = new ClaimsIdentity(
                  new[] {
                      // adding following 2 claim just for supporting default antiforgery provider
                      new Claim(ClaimTypes.NameIdentifier, model.Username),
                      new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                      new Claim(ClaimTypes.Name,model.Username),
                      // optionally you could add roles if any
                      //new Claim(ClaimTypes.Role, "RoleName"),
                      //new Claim(ClaimTypes.Role, "AnotherRole"),
                  },
                  DefaultAuthenticationTypes.ApplicationCookie);
                ident.AddClaim(new Claim(ClaimTypes.GivenName, user.EmployeeName));
                ident.AddClaim(new Claim(ClaimTypes.Sid, user.EmployeeID.ToString()));

                HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, ident);
                return RedirectToLocal(returnUrl);
            }
            // invalid username or password
            //ModelState.AddModelError("", "invalid username or password");
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}