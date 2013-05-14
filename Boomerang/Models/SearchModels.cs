using Boomerang.Web;
using Boomerang.Web.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Security;

namespace Boomerang.Models
{
    public class HomeModel
    {
        private MembershipUser _CurrentUser;

        public HomeModel()
        {
        }

        public HomeModel(MembershipUser CurrentUser)
        {
            _CurrentUser = CurrentUser;
        }

        public IEnumerable<Client> UpcomingClients 
        { 
            get 
            {
                if (_CurrentUser == null)
                    return null;

                return ClientProvider.UpcomingContact().Where(c => c.UserId == _CurrentUser.ProviderUserKey.ToString());
            } 
        }
        public BasicSearchModel BasicSearch { get; set; }
    }

    public class AdvancedSearchModel
    {
        public AdvancedClientSearchModel Client { get; set; }
        public AdvancedCandidateSearchModel Candidate { get; set; }
    }
}