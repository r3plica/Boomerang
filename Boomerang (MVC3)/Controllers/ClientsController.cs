using Core;
using Core.Components;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Boomerang.Controllers
{
    [Authorize(Roles = "Managers")]
    public class ClientsController : Controller
    {
        //
        // GET: /Clients/

        public ActionResult Index()
        {
            return View(ClientManager.CurrentClients());
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
            return View(new Client(Id));
        }

        #region AJAX

        public JsonResult Save(Client Client)
        {
            try
            {
                MembershipUser user = Membership.GetUser(User.Identity.Name);
                Client.UserId = user.ProviderUserKey.ToString();
                Client.UserName = user.UserName;

                Client.Save();

                return new JsonResult { Data = new { success = true, id = Client.Id } }; // Return the client Id if there are no errors
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

        public JsonResult SaveAddress(Address Address, int TypeId)
        {
            try
            {
                Address.TypeId = (AddressType)TypeId;
                Address.Save();

                return new JsonResult { Data = new { success = true, id = Address.Id } }; // Return nothing as there are no errors
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
