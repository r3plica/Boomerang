using Boomerang.Web;
using Boomerang.Web.Providers;
using System;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using System.Web.Security;

namespace Boomerang.Controllers
{
    public class CandidatesController : Controller
    {
        //
        // GET: /Candidates/

        [Authorize(Roles = "Managers")]
        public ActionResult Index()
        {
            return View(CandidateProvider.CurrentCandidates());
        }

        //
        // GET: /Candidates/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // GET: /Candidates/Edit

        [Authorize]
        public ActionResult Edit(int Id)
        {
            Candidate Candidate = new Candidate(Id);
            Collection<GenericType> Rates = GenericProvider.GetSalaryRates();

            SelectList TempRate = (Candidate.SalaryDetails().TempRateId > 0) ? new SelectList(Rates, "Id", "Name", Candidate.SalaryDetails().TempRateId) : new SelectList(Rates, "Id", "Name", 1);
            SelectList PermRate = (Candidate.SalaryDetails().PermRateId > 0) ? new SelectList(Rates, "Id", "Name", Candidate.SalaryDetails().PermRateId) : new SelectList(Rates, "Id", "Name", 1);

            ViewBag.TempRateId = TempRate;
            ViewBag.PermRateId = PermRate;

            return View(Candidate);
        }

        #region AJAX

        [Authorize]
        public JsonResult Save(Candidate Candidate)
        {
            try
            {
                MembershipUser user = Membership.GetUser(User.Identity.Name);
                Candidate.UserId = user.ProviderUserKey.ToString();
                Candidate.UserName = user.UserName;

                Candidate.Save();

                return new JsonResult { Data = new { success = true, id = Candidate.Id } }; // Return the candidate Id if there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult Deactivate(int Id)
        {
            try
            {
                Candidate Candidate = new Candidate(Id);
                Candidate.Active = false;
                Candidate.Save();

                return new JsonResult { Data = new { success = true } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult Activate(int Id)
        {
            try
            {
                Candidate Candidate = new Candidate(Id);
                Candidate.Active = true;
                Candidate.Save();

                return new JsonResult { Data = new { success = true } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        [Authorize(Roles = "Managers")]
        public JsonResult Delete(int Id)
        {
            try
            {
                new Candidate(Id).Delete();

                return new JsonResult { Data = new { success = true } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        [Authorize]
        public JsonResult SaveSalary(Salary Salary)
        {
            try
            {
                Salary.Save();

                return new JsonResult { Data = new { success = true, id = Salary.Id } }; // Return the salary Id if there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        [Authorize]
        public JsonResult SaveExperience(Experience Experience, int TypeId)
        {
            try
            {
                Experience.TypeId = (ExperienceType)TypeId;
                Experience.Save();

                return new JsonResult { Data = new { success = true, id = Experience.Id } }; // Return the salary Id if there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        [Authorize]
        public JsonResult SaveNote(Note Note, int TypeId)
        {
            try
            {
                Note.TypeId = (NoteType)TypeId;
                Note.UserId = Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString();
                Note.UserName = User.Identity.Name;
                Note.Save();

                Note.Message = Note.DisplayHtmlMessage(); // For display

                return new JsonResult { Data = new { success = true, note = Note } }; // Return the note Id if there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        #endregion

    }
}
