using HPE.Kruta.Common.Enum;
using HPE.Kruta.Domain.Principals;
using HPE.Kruta.Domain.User;
using HPE.Kruta.Model;
using System;
using System.DirectoryServices.AccountManagement;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web;

namespace HPE.Kruta.Web
{
    /// <summary>
    /// Custom attribute to handle the authorization logic for the logged in user
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class KrutaAuthorize : AuthorizeAttribute
    {
        /// <summary>
        /// Gets or sets a session variable indicates that the logged in user has been already authorized to login to the system. 
        /// </summary>
        private bool IsAuthorized
        {
            get
            {
                if (HttpContext.Current.Session["IsAuthorized"] != null &&
                    !string.IsNullOrWhiteSpace(HttpContext.Current.Session["IsAuthorized"].ToString()))
                {
                    return Convert.ToBoolean(HttpContext.Current.Session["IsAuthorized"].ToString());
                }

                return false;
            }
            set
            {
                HttpContext.Current.Session["IsAuthorized"] = value;
            }
        }


        /// <summary>
        /// Handles the authorization routine for the requested action
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // This will e executed once the logged in user access the system
            // we need to check if the user is allowed to access the system
            // if allowed, check if the user already exists in the database
            // if the user is new user and allowed, we need to create the user
            // if the user is not allowed redirect to unauthorized page

            // if the user already authorized, skip the process to save performance
            if (IsAuthorized)
            {
                return;
            }

            UserManager userManager = new UserManager();
            Employee employee = userManager.GetByUsername(filterContext.HttpContext.User.Identity.Name);

            if (employee != null)
            {
                // user already exist
                // check permission or role

                KrutaPrincipal user = filterContext.HttpContext.User as KrutaPrincipal;
                if (user != null)
                {
                    // set the logged in user id
                    user.UserID = employee.EmployeeID;

                    if (!user.IsInRole(RolesEnum.Login.ToString()))
                    {
                        // user is NOT allowed to login
                        SendToUnauthorized(filterContext);
                    }
                    else
                    {
                        // Set the authorized session    
                        IsAuthorized = true;
                    }
                }
                else
                {
                    SendToUnauthorized(filterContext);
                }
            }
            else
            {
                // user does not exist
                // send to unauthorized page
                // TODO: check if we need to create a record in the EMployee table for this user in this case?
                //string fullname = string.Empty;
                //using (UserPrincipal userPrincipal = UserPrincipal.Current)
                //{
                //    fullname = userPrincipal.DisplayName;
                //}

                SendToUnauthorized(filterContext);
            }
        }

        private static void SendToUnauthorized(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = 403;
            filterContext.Result = new ViewResult { ViewName = "Unauthorized" };
        }

        /// <summary>
        /// Handle the unauthorized request, in case the windows authentication failed
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                SendToUnauthorized(filterContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}