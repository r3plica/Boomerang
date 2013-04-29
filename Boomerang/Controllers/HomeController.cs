using Core.Components;
using System.Web.Mvc;
using System.Linq;
using System.Web.Security;
using Boomerang.Models;
using Core;

namespace Boomerang.Controllers
{
    [Authorize(Roles = "Managers")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeModel Model = new HomeModel(Membership.GetUser(User.Identity.Name));            
            return View(Model);
        }

        [HttpPost]
        public ActionResult Index(HomeModel model, int Type)
        {
            if (Type == (int)QueryType.Candidate)
                return RedirectToAction("Candidates", "Search", model.BasicSearch);
            else
                return RedirectToAction("Clients", "Search", model.BasicSearch);
        }
    }
}
