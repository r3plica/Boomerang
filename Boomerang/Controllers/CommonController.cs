using Core;
using Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Boomerang.Controllers
{
    public class CommonController : Controller
    {

        //
        // POST: /Common/Export

        [HttpPost]
        public ActionResult Export(string FileContent, string FileName)
        {
            var cd = new ContentDisposition
            {
                FileName = FileName + ".csv",
                Inline = false
            };
            Response.AddHeader("Content-Disposition", cd.ToString());
            return Content(FileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public JsonResult SaveAddress(Address Address)
        {
            try
            {
                Address.Save();

                return new JsonResult { Data = new { success = true, id = Address.Id } };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult DeleteAddress(int id)
        {
            try
            {
                Address address = new Address(id);
                address.Delete();

                return new JsonResult { Data = new { success = true } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult AddAddress(bool newRow)
        {
            try
            {
                return new JsonResult { Data = new { success = true, address = AddressManager.AddAddressForDisplay(newRow) } };
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult DeleteNote(int id)
        {
            try
            {
                Note note = new Note(id);
                note.Delete();

                return new JsonResult { Data = new { success = true } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }
    }
}
