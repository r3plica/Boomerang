using Core.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Core.Helpers;

namespace Core.Components
{
    public static class ClientManager
    {
        public static IEnumerable<Client> UpcomingContact()
        {
            return Clients.Get().Where(c => c.PrimaryContact().CallbackDate.IsInDateRange(DateTime.Now, DateTime.Now.AddMonths(6)));
        }

        public static Collection<Client> CurrentClients()
        {
            return Clients.Get();
        }
    }
}
