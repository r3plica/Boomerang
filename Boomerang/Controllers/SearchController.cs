using Core;
using System.Web.Mvc;

namespace Boomerang.Controllers
{
    public class SearchController : Controller
    {

        //
        // GET: /Search/Clients

        public ActionResult Clients(BasicSearchModel Basic)
        {
            ViewBag.Title = "Searching \"" + Basic.Query + "\"";

            return View(new ClientResults(Basic).Search());
        }

        //
        // GET: /Search/Candidates

        public ActionResult Candidates(BasicSearchModel Basic)
        {
            ViewBag.Title = "Searching \"" + Basic.Query + "\"";

            return View(new CandidateResults(Basic).Search());
        }
    }
}
