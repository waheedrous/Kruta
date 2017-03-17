using HPE.Kruta.Common.Enums;
using System.Security.Principal;

namespace HPE.Kruta.Domain.Principals
{
    public interface IKrutaPrincipal : IPrincipal
    {
        

        /// <summary>
        /// Check if the user has the specified role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        bool IsInRole(RolesEnum role);
    }
}
