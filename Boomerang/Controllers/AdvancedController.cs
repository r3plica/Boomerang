using Boomerang.Models;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Boomerang.Controllers
{
    public class AdvancedController : Controller
    {

        //
        // GET: /Advanced/

        public ActionResult Index()
        {
            return View();
        }
        
        //
        // POST: /Advanced/

        public ActionResult Clients(AdvancedSearchModel Model)
        {
            try
            {
                return View(new ClientResults(Model.Client).Search());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
            }

            return View(Model);
        }
        
        //
        // POST: /Advanced/

        public ActionResult Candidates(AdvancedSearchModel Model)
        {
            try
            {
                return View(new CandidateResults(Model.Candidate).Search());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
            }

            return View(Model);
        }
    }
}
