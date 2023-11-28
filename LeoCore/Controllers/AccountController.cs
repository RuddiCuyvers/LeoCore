using LeoCore.Models;

using System.Linq;
using System.Threading.Tasks;
using System.Web;

using WGK.Lib.Web.Mvc.Controllers;
using System.Collections.Generic;
using LeoCore.Data;
using System.Security.Claims;
using System;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

using System.Security.Cryptography;


namespace LeoCore.Controllers
{
    [Authorize] 
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            string currentUserId = User.Identity.Name;
            return View();
        }

        //
        // GET: /Account/ExternalLogin
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            var x = WGK.Lib.Web.GoogleAdminApi.GoogleAdminApi.GoogleNaam("christina.mokos@limburg.wgk.be");
            // Request a redirect to the external login provider
            //var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);
           
            var user = new IdentityUser();

           


            //_signInManager.CreateUserPrincipalAsync(user);
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        private readonly SignInManager<IdentityUser> _signInManager;
      
        //
        // GET: /Account/ExternalLoginCallback
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            //  _signInManager = _signInManager<User>
            // string currentUserId = User.Identity.Name;
            //   SignInManager.IsSignedIn(User)

           //  var loginInfo = await _signInManager.GetExternalLoginInfoAsync();


            //if (loginInfo == null)
            //{
            //    return RedirectToAction("Login");
            //}
            //else
            //{
            //variabele om als verschillende emailadres alles te testen default is ingelogde gebruiker (loginInfo.Email)
            string Email = "christina.mokos@limburg.wgk.be"; // loginInfo.Email; //****

                //var x = WGK.Lib.Web.GoogleAdminApi.GoogleAdminApi.GoogleNaam("christina.mokos@limburg.wgk.be");


                var claims = new[] {
                    new Claim(ClaimTypes.Name, WGK.Lib.Web.GoogleAdminApi.GoogleAdminApi.GoogleNaam(Email)), // loginInfo.DefaultUserName),  //****
                    new Claim(ClaimTypes.Email, Email), //loginInfo.Email), //****
                    new Claim(ClaimTypes.GivenName, WGK.Lib.Web.GoogleAdminApi.GoogleAdminApi.GoogleNaam(Email)),
                    new Claim(ClaimTypes.Uri, WGK.Lib.Web.GoogleAdminApi.GoogleAdminApi.GoogleFotoOphalen(Email)),
                    new Claim(ClaimTypes.HomePhone, WGK.Lib.Web.GoogleAdminApi.GoogleAdminApi.GooglePN(Email)),
                    new Claim(ClaimTypes.Locality, WGK.Lib.Web.GoogleAdminApi.GoogleAdminApi.GoogleON(Email)),
                    new Claim(ClaimTypes.Country, WGK.Lib.Web.GoogleAdminApi.GoogleAdminApi.GoogleOR(Email)),
                    new Claim(ClaimTypes.Role, WGK.Lib.Web.GoogleAdminApi.GoogleAdminApi.GoogleOT(Email))

                    //you can add more claims
                };



            //Deze fct maakt de "Groepen" cookie en vult ze in
            //     AddGroepenCookie(Email);


            //var identity = new ClaimsIdentity(claims, "ApplicationCookie");
            // Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);

            //}
            //return this.RedirectToAction("Identification", "Trainings");
            //RedirectToRoute()
            // return Redirect("/Author/Index");

            //using var context = new LeoCore.Data.LeoDBContext();
           
            //int b = context.QUESTIONs.Count();
            //// QUESTION q = new QUESTION();
            //var q = context.QUESTIONs.ToList();

          


            return RedirectToLocal(returnUrl);  //RedirectToLocal is een private fct hier verderop. Indien returnURL is null (=altijd) dan Index Home
                                                // return RedirectToAction("Index", "Home");

        }


        private void AddGroepenCookie(string email)
        {
            var cookieOptions = new CookieOptions();
            List<String> Test = WGK.Lib.Web.GoogleAdminApi.GoogleAdminApi.GoogleGroepenOphalen(email);
            cookieOptions.Expires = DateTime.Now.AddHours(1);
            cookieOptions.Path = "/";

            string test = "";
            foreach (var item in Test)
            {
                test += item + ",";
            }
   
            HttpContext.Response.Cookies.Append("GroepenCookie", test , cookieOptions);
           
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}

        private void AddErrors(Microsoft.AspNetCore.Identity.IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.ToString());
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Identification", "Trainings");
        }

        internal class ChallengeResult : UnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }
            
            public override void ExecuteResult(ActionContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                
                context.HttpContext.ChallengeAsync( LoginProvider, properties);
            }
        }
        #endregion

        ////
        //// POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    return RedirectToLocal(returnUrl);
        //}
        //}
    }
}