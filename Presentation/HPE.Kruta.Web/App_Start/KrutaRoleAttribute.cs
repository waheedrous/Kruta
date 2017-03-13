using HPE.Kruta.Common.Enum;
using HPE.Kruta.Domain.Principals;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HPE.Kruta.Web
{
    /// <summary>
    /// Custom attribute to handle the authorization logic for the logged in user
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class KrutaRole : AuthorizeAttribute
    {
        /// <summary>
        /// KrutaRole default constructor
        /// </summary>
        /// <param name="roles"></param>
        public KrutaRole(params RolesEnum[] roles)
        {
            Roles = string.Join(",", roles);
        }

        /// <summary>
        /// Handles the authorization routine for the requested action
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (string.IsNullOrWhiteSpace(Roles))
            {
                SendToUnauthorized(filterContext);
            }

            // this will be executed each time KrutaRole is being presented
            var user = filterContext.HttpContext.User as KrutaPrincipal;
            Dictionary<string, bool> auth = new Dictionary<string, bool>();

            if (user != null)
            {
                var roles = Roles.Split(',');

                foreach (var role in roles)
                {
                    auth.Add(role, user.IsInRole(role));
                }
            }

            if (auth.Values.Any(q => !q))
            {
                // user is NOT allowed
                SendToUnauthorized(filterContext);
            }
        }

        private static void SendToUnauthorized(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = 403;
            filterContext.Result = new ViewResult { ViewName = "Unauthorized" };
        }
    }
}