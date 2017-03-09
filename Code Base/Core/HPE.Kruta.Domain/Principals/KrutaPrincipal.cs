﻿using System.Security.Principal;

namespace HPE.Kruta.Domain.Principals
{
    public class KrutaPrincipal : IKrutaPrincipal
    {
        private readonly IPrincipal _principal;

        public KrutaPrincipal(IPrincipal principal)
        {
            _principal = principal;
        }

        public IIdentity Identity
        {
            get
            {
                return _principal.Identity;
            }
        }

        public int UserID { get; set; }

        public bool IsInRole(string role)
        {
            return _principal.IsInRole(role);
        }
    }
}