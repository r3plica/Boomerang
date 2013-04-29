using Boomerang.Models;
using Core;
using Core.Components;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace Boomerang.Controllers
{
    public class UsersController : Controller
    {

        //
        // GET: /Users/Index

        public ActionResult Index()
        {
            return View(ProfileManager.AllProfiles());
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Roles.AddUsersToRole(new string[] { model.UserName }, "Managers");
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Users/Edit

        public ActionResult Edit(string Id)
        {
            MembershipUser CurrentUser = Membership.GetUser(new Guid(Id));

            UserModel User = new UserModel()
            {
                UserId = CurrentUser.ProviderUserKey.ToString(), 
                UserName = CurrentUser.UserName,
                Email = CurrentUser.Email
            };

            return View(User);
        }

        //
        // POST: /Users/Edit

        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipUser User = Membership.GetUser(new Guid(model.UserId));
                Profile Profile = new Profile(User);

                bool saveProfileSucceeded;
                try
                {
                    saveProfileSucceeded = Profile.Save();                 
                }
                catch (Exception)
                {
                    saveProfileSucceeded = false;
                }

                bool changePasswordSucceeded = true;
                if (model.OldPassword != null && model.NewPassword != null)
                {
                    try
                    {
                        changePasswordSucceeded = Profile.ChangePassword(model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }
                }

                if (changePasswordSucceeded && saveProfileSucceeded)
                {
                    if (User.ProviderUserKey.ToString() == Profile.ProviderUserKey.ToString())
                        FormsAuthentication.SetAuthCookie(model.UserName, true);

                    return View(model);
                }
                else
                {
                    if (!changePasswordSucceeded)
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");

                    if (!saveProfileSucceeded)
                        ModelState.AddModelError("", "Failed to save the user.");
                }
            }

            return View(model);
        }

        #region AJAX

        public JsonResult Deactivate(string Id)
        {
            try
            {
                MembershipUser CurrentUser = Membership.GetUser(new Guid(Id));
                CurrentUser.IsApproved = false;
                Membership.UpdateUser(CurrentUser);

                return new JsonResult { Data = new { success = true } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult Activate(string Id)
        {
            try
            {
                MembershipUser CurrentUser = Membership.GetUser(new Guid(Id));
                CurrentUser.IsApproved = true;
                Membership.UpdateUser(CurrentUser);

                return new JsonResult { Data = new { success = true } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        public JsonResult Unlock(string Id)
        {
            try
            {
                MembershipUser CurrentUser = Membership.GetUser(new Guid(Id));
                CurrentUser.UnlockUser();

                return new JsonResult { Data = new { success = true } }; // Return nothing as there are no errors
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = new { success = false, error = ex.Message } };
            }
        }

        #endregion

        #region Status Codes

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion
    }
}
