using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class BaseController : Controller
    {
        public int LoggedInUserId
        {
            get
            {
                var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid);
                var userIdClaim = claim == null ? null : claim.Value;

                int userId = 0;
                int.TryParse(userIdClaim, out userId);

                return userId;
            }
        }
    }
}