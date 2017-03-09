using HPE.Kruta.Common.Enum;
using HPE.Kruta.Domain.Principals;
using System;
using System.Web.Mvc;

namespace HPE.Kruta.Web
{
    /// <summary>
    /// Custom attribute to handle the authorization logic for the logged in user
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizePermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// AuthorizePermissionAttribute default constructor
        /// </summary>
        /// <param name="roles"></param>
        public AuthorizePermissionAttribute(params RolesEnum[] roles)
        {
            Roles = string.Join(",", roles);
        }

        /// <summary>
        /// Handles the authorization routine for the requested action
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User as KrutaPrincipal;

            if (user != null)
            {
                user.UserID = 2;
            }
        }

        /// <summary>
        /// Handle the unauthorized requets, in case the windows authintication failed
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new ViewResult { ViewName = "Unauthorized" };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}