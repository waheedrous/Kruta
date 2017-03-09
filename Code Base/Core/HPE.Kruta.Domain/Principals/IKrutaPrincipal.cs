using System.Security.Principal;

namespace HPE.Kruta.Domain.Principals
{
    public interface IKrutaPrincipal : IPrincipal
    {
        /// <summary>
        /// Gets or sets the logged in user ID
        /// </summary>
        int UserID { get; set; }
    }
}
