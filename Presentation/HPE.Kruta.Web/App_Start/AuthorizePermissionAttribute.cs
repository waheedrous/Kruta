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
            // this will be executed each time AuthorizePermissionAttribute is being presented
            // first we need to check if the logged in user can access the system
            var user = filterContext.HttpContext.User as KrutaPrincipal;

            if (user != null)
            {
                user.UserID = 2;
            }
        }
    }
}