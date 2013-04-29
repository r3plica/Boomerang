using Core;
using Core.Components;
using Core.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Boomerang.Controllers
{
    public class ListsController : Controller
    {

        //
        // GET: /Lists/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Edit/

        public ActionResult Edit(string Id)
        {
            Collection<GenericType> List;
            string Target = Id.Substring(0, Id.IndexOf("-"));

            ViewBag.Name = Id.Replace("-", " ");
            ViewBag.Target = Target;

            switch (Target.ToLower())
            {
                case "salary": List = GenericManager.GetSalaryRates(); break;
                case "transport": List = GenericManager.GetTransportTypes(); break;
                default: List = GenericManager.GetSalaryRates(); break;
            }

            return View(List);
        }

        #region AJAX

        public JsonResult MoveSalary(int Order, int Direction)
        {
            try
            {
                GenericType SalaryRate = new SalaryRate();
                int order = SalaryRate.Move(Order, (MoveDirection)Direction);

                return new JsonResult { Data = new { success = true, order = order } }; // Return our new order if there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult MoveTransport(int Order, int Direction)
        {
            try
            {
                GenericType TransportType = new TransportType();
                int order = TransportType.Move(Order, (MoveDirection)Direction);

                return new JsonResult { Data = new { success = true, order = order } }; // Return our new order if there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        #endregion

    }
}
