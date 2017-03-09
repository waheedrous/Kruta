using HPE.Kruta.Domain.Principals;
using HPE.Kruta.Domain.User;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HPE.Kruta.Web
{
    /// <summary>
    /// Custom role provider based of off ASP.NET role provider
    /// </summary>
    public class KrutaRoleProvider : RoleProvider
    {
        public override string ApplicationName { get; set; }
        
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the roles for the related user name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string username)
        {
            UserManager um = new UserManager();

            var user = HttpContext.Current.User as KrutaPrincipal;

            if (user != null)
            {
                return um.GetRolesForUser(user.UserID);
            }

            return um.GetRolesForUser(username);
            // return new string[] { };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verify if the user has the role
        /// </summary>
        /// <param name="username"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            var roles = GetRolesForUser(username);
            return roles.Contains(roleName);
            //return true;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}