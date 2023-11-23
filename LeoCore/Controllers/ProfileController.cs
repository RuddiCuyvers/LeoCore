using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WGK.Lib.Web.Mvc.Attributes;
using WGK.Lib.Web.Mvc.Controllers;

namespace LeoCore.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ProfileController()
        {
                
        }

        [HttpGet]
        //[Authorize]
        [NoCaching]
        public ViewResult Profile()
        {
            return this.View("~/Views/Profile/Profile.cshtml");
        }
    }
}