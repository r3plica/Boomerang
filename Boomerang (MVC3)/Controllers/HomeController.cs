using Core.Components;
using System.Web.Mvc;
using System.Linq;
using System.Web.Security;

namespace Boomerang.Controllers
{
    [Authorize(Roles = "Managers")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MembershipUser user = Membership.GetUser(User.Identity.Name);

            return View(ClientManager.UpcomingContact().Where(c => c.UserId == user.ProviderUserKey.ToString()));
        }
    }
}
