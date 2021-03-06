﻿using HPE.Kruta.Common.Enums;
using System.Security.Principal;

namespace HPE.Kruta.Domain.Principals
{
    /// <summary>
    /// Provide a custom principal for the authenticated user.
    /// </summary>
    public class KrutaPrincipal : IKrutaPrincipal
    {
        private readonly IPrincipal _principal;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="principal"></param>
        public KrutaPrincipal(IPrincipal principal)
        {
            _principal = principal;
        }

        /// <summary>
        /// Gets the current user identity.
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return _principal.Identity;
            }
        }
                
        /// <summary>
        /// Check if the user has the specified role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(RolesEnum role)
        {
            return this.IsInRole(role.ToString());
        }

        /// <summary>
        /// Check if the user has the specified role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsInRole(string role)
        {
            return _principal.IsInRole(role);
        }
    }
}