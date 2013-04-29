using Core;
using System.Web.Mvc;

namespace Boomerang.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search

        public ActionResult Index(BasicSearchModel Basic)
        {
            if (Basic.Type == QueryType.Candidate)
                return RedirectToAction("Candidates", Basic);
            else
                return RedirectToAction("Clients", Basic);
        }

        //
        // GET: /Search/Clients

        public ActionResult Clients(BasicSearchModel Basic)
        {
            ViewBag.Title = "Searching \"" + Basic.Query + "\"";

            return View(new ClientCollection().BasicSearch(Basic.Query));
        }

        //
        // GET: /Search/Candidates

        public ActionResult Candidates(BasicSearchModel Basic)
        {
            ViewBag.Title = "Searching \"" + Basic.Query + "\"";

            return View(new ClientCollection().BasicSearch(Basic.Query));
        }
    }
}
