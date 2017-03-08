using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

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

        public bool IsInRole(string role)
        {
            return _principal.IsInRole(role);
        }

        public bool IsInClient(string client)
        {
            return _principal.Identity.IsAuthenticated
                   && GetClientsForUser(_principal.Identity.Name).Contains(client);
        }

        private IEnumerable<string> GetClientsForUser(string username)
        {
            //using (var db = new YourContext())
            //{
            //    var user = db.Users.SingleOrDefault(x => x.Name == username);
            //    return user != null
            //                ? user.Clients.Select(x => x.Name).ToArray()
            //                : new string[0];
            //}

            return new string[0];
        }
    }
}