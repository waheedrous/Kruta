using HPE.Kruta.Domain;
using System.Security.Claims;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class BaseController : Controller
    {
        public QueueManager _queueManager = new QueueManager();

        public int LoggedInUserId
        {
            get
            {
                var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid);
                var userIdClaim = claim == null ? null : claim.Value;

                int userId = 0;
                int.TryParse(userIdClaim, out userId);

                return userId;
            }
        }
    }
}