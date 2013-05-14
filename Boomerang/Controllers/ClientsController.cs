using Boomerang.Web;
using Boomerang.Web.Providers;
using System;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;

namespace Boomerang.Controllers
{
    [Authorize(Roles = "Managers")]
    public class ClientsController : Controller
    {
        //
        // GET: /Clients/

        public ActionResult Index()
        {
            return View(ClientProvider.CurrentClients());
        }

        //
        // GET: /Clients/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // GET: /Clients/Edit

        public ActionResult Edit(int Id)
        {
            Client Client = new Client(Id);

            ViewData["Managers"] = new SelectList(ProfileProvider.AllProfiles().Where(p => p.RoleName == "Managers").ToList(), "ProviderUserKey", "UserName", Client.UserId);

            return View(Client);
        }

        #region AJAX

        public JsonResult Save(Client Client)
        {
            try
            {
                Client.Save();

                return new JsonResult { Data = new { success = true, id = Client.Id } }; // Return the client Id if there are no errors
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
                Client Client = new Client(Id);
                Client.Active = false;
                Client.Save();

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
                Client Client = new Client(Id);
                Client.Active = true;
                Client.Save();

                return new JsonResult { Data = new { success = true } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult Delete(int Id)
        {
            try
            {
                new Client(Id).Delete();

                return new JsonResult { Data = new { success = true } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult SaveSector(Sector Sector)
        {
            try
            {
                Sector.Save();

                return new JsonResult { Data = new { success = true, id = Sector.Id } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult SaveContact(Contact Contact)
        {
            try
            {
                Contact.Save();

                return new JsonResult { Data = new { success = true, id = Contact.Id } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult DeleteContact(int Id)
        {
            try
            {
                new Contact().Delete(Id);

                return new JsonResult { Data = new { success = true } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult GetContact(int Id)
        {
            try
            {
                Contact Contact = new Contact(Id);

                return new JsonResult { Data = new { success = true, contact = Contact } }; // Return our contact
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        #endregion

    }
}
