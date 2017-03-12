using HPE.Kruta.Common.Enum;
using System.Security.Principal;

namespace HPE.Kruta.Domain.Principals
{
    public interface IKrutaPrincipal : IPrincipal
    {
        /// <summary>
        /// Gets or sets the logged in user ID
        /// </summary>
        int UserID { get; set; }

        /// <summary>
        /// Check if the user has the specified role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        bool IsInRole(RolesEnum role);
    }
}
