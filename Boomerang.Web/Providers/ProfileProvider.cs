﻿using Boomerang.Web.Data;
using System.Collections.ObjectModel;
using System.Web.Security;
using System.Linq;

namespace Boomerang.Web.Providers
{
    public class ProfileProvider
    {
        public static void Initialize()
        {
            if (!Roles.RoleExists("Managers"))
                Roles.CreateRole("Managers");

            if (Membership.GetUser("r3plica") == null)
                Membership.CreateUser("r3plica", "zer0kewl");

            if (!Roles.GetUsersInRole("Managers").Contains("r3plica"))
                Roles.AddUsersToRole(new string[] { "r3plica" }, "Managers");
        }

        public static Collection<Profile> AllProfiles()
        {
            return Profiles.Get();
        }
    }
}
